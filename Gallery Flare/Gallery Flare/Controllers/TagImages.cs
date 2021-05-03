using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Threading;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Globalization;

namespace Gallery_Flare.Controllers
{
    public class TagImages
    {
        private static string _subscriptionKey = "89b7066e87e34632a64bfd5deca10af9";
        private static string _baseUri = "https://api.bing.microsoft.com/v7.0/images/visualsearch";

        private static string _imageUrl = "";

        private const string MKT_PARAMETER = "?mkt=";

        private static string allNames = "";

        public TagImages(string url)
        {
            _imageUrl = url;
            allNames = "";
        }

        public async Task<string> MostCommonTags(int top)
        {
            RunAsync().Wait();
            IList<String> mostCommonResults = new List<String>();

            String[] arr = allNames.Split(" ");
            Dictionary<String, int> hs =
           new Dictionary<String, int>();

            for (int i = 0; i < arr.Length; i++)
            {
                if (hs.ContainsKey(arr[i]))
                {
                    hs[arr[i]] = hs[arr[i]] + 1;
                }
                else
                {
                    hs.Add(arr[i], 1);
                }
            }

            int numOfDesiredTags = 0;
            int iterations = 0;

            while (numOfDesiredTags < top && iterations < arr.Length)
            {
                String key = "";
                int value = 0;
                iterations++;
                foreach (KeyValuePair<String, int> me in hs)
                {
                    if (me.Value > value)
                    {
                        value = me.Value;
                        key = me.Key;
                    }
                }
                hs[key] = 0;
                Database database = new Database("words");
                string irrelevantTags = await database.GetIrrelevantTags();
                if (!mostCommonResults.Any(s => s.Equals(key, StringComparison.OrdinalIgnoreCase)) && !irrelevantTags.ToLower().Contains(key.ToLower()))
                {
                    mostCommonResults.Add(key.ToLower());
                    numOfDesiredTags++;
                }
            }
            allNames = "";
            return string.Join(",", mostCommonResults);
        }

        static async Task RunAsync()
        {
            try
            {
                var queryString = MKT_PARAMETER + "en-ca";
                using var postContent = new MultipartFormDataContent("boundary_" + DateTime.Now.ToString(CultureInfo.InvariantCulture));
                var visualSearchParams = new Dictionary<string, object>()
                    {
                        {"ImageInfo", new Dictionary<string, object>()
                            {
                                {"url", _imageUrl}
                            }
                        },
                        {"KnowledgeRequest", new Dictionary<string, object>()
                            {
                                {"InvokedSkillsRequestData",  new Dictionary<string, object>()
                                    {
                                        {"EnableEntityData", true}
                                    }
                                }
                            }
                        }
                    };

                using var jsonContent = new StringContent(JsonConvert.SerializeObject(visualSearchParams,
                    new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }));
                var dispositionHeader = new ContentDispositionHeaderValue("form-data")
                {
                    Name = "knowledgeRequest"
                };
                jsonContent.Headers.ContentDisposition = dispositionHeader;
                jsonContent.Headers.ContentType = null;

                postContent.Add(jsonContent);

                HttpResponseMessage response = await MakeRequestAsync(queryString, postContent);
        
                var contentString = await response.Content.ReadAsStringAsync();
                Dictionary<string, object> searchResponse = JsonConvert.DeserializeObject<Dictionary<string, object>>(contentString);

                if (response.IsSuccessStatusCode)
                {
                    StoreInsights(searchResponse);
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        static async Task<HttpResponseMessage> MakeRequestAsync(string queryString, MultipartFormDataContent postContent)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", _subscriptionKey);
            return (await client.PostAsync(_baseUri + queryString, postContent));
        }

        static void StoreInsights(Dictionary<string, object> response)
        {
            var tags = response["tags"] as Newtonsoft.Json.Linq.JToken;

            foreach (Newtonsoft.Json.Linq.JToken tag in tags)
            {
                var displayName = (string)tag["displayName"];

                if (string.IsNullOrEmpty(displayName))
                {
                    FillNamesForNullOrEmpty(tag);
                }
                else
                {
                    allNames += $"{displayName} ";
                }
            }
        }

        static void FillNamesForNullOrEmpty(Newtonsoft.Json.Linq.JToken tag)
        {
            var actions = tag["actions"];
            string[] actionTypes = { "PagesIncluding", "VisualSearch", "ProductVisualSearch" };

            foreach (Newtonsoft.Json.Linq.JToken action in actions)
            {
                if (actionTypes.Contains((string)action["actionType"]))
                {
                    foreach (Newtonsoft.Json.Linq.JToken data in action["data"]["value"])
                    {
                        allNames += $"{(string)data["name"]} ";
                    }
                }           
            }
        }
    }
}
