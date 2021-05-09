using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Threading;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Globalization;
using Gallery_Flare.Controllers.Operations.Database;

namespace Gallery_Flare.Controllers.Operations
{
    public class TagImages
    {
        private static string _subscriptionKey = "89b7066e87e34632a64bfd5deca10af9";
        private static string _baseUri = "https://api.bing.microsoft.com/v7.0/images/visualsearch";

        private static string _imageUrl = "";

        private const string MKT_PARAMETER = "?mkt=";

        private static string allResults = "";

        public TagImages(string url)
        {
            _imageUrl = url;
            allResults = "";
        }

        public async Task<string> MostCommonTags(int numOfDesiredTags)
        {
            //A function that returns the top numOfDesiredTags as a comma seperated string
            ResearchImage().Wait();
            IList<string> mostCommonResults = new List<string>();

            IrrelvantTagsDatabaseConnector database = new IrrelvantTagsDatabaseConnector();
            string irrelevantTags = await database.GetIrrelevantTags();

            string[] allTagsArray = allResults.Split(" ");
            Dictionary<string, int> tagsFrequencies =
           new Dictionary<string, int>();

            for (int tag = 0; tag < allTagsArray.Length; tag++)
            {
                if (tagsFrequencies.ContainsKey(allTagsArray[tag]))
                {
                    tagsFrequencies[allTagsArray[tag]] = tagsFrequencies[allTagsArray[tag]] + 1;
                }
                else
                {
                    tagsFrequencies.Add(allTagsArray[tag], 1);
                }
            }

            int numOfFoundTopTags = 0;
            int iterations = 0;

            while (numOfFoundTopTags < numOfDesiredTags && iterations < allTagsArray.Length)
            {
                string tagName = "";
                int tagFrequency = 0;
                iterations++;
                foreach (KeyValuePair<string, int> tag in tagsFrequencies)
                {
                    if (tag.Value > tagFrequency)
                    {
                        tagFrequency = tag.Value;
                        tagName = tag.Key;
                    }
                }

                //Reset frequencey of acknowledged key (to be ignored in next iterations)
                tagsFrequencies[tagName] = 0;

                //Ignore duplicates and allow only meaningful tags
                if (!mostCommonResults.Any(s => s.Equals(tagName, StringComparison.OrdinalIgnoreCase)) && !irrelevantTags.ToLower().Contains(tagName.ToLower()))
                {
                    mostCommonResults.Add(tagName.ToLower());
                    numOfFoundTopTags++;
                }
            }
            allResults = "";
            return string.Join(",", mostCommonResults);
        }

        static async Task ResearchImage()
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
                throw e;
            }
        }

        static async Task<HttpResponseMessage> MakeRequestAsync(string queryString, MultipartFormDataContent postContent)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", _subscriptionKey);
            return await client.PostAsync(_baseUri + queryString, postContent);
        }

        static void StoreInsights(Dictionary<string, object> response)
        {
            var tags = response["tags"] as Newtonsoft.Json.Linq.JToken;

            foreach (Newtonsoft.Json.Linq.JToken tag in tags)
            {
                var displayName = (string)tag["displayName"];

                if (string.IsNullOrEmpty(displayName))
                {
                    StoreInsightsForNullOrEmpty(tag);
                }
                else
                {
                    allResults += $"{displayName} ";
                }
            }
        }

        static void StoreInsightsForNullOrEmpty(Newtonsoft.Json.Linq.JToken tag)
        {
            var actions = tag["actions"];
            string[] actionTypes = { "PagesIncluding", "VisualSearch", "ProductVisualSearch" };

            foreach (Newtonsoft.Json.Linq.JToken action in actions)
            {
                if (actionTypes.Contains((string)action["actionType"]))
                {
                    foreach (Newtonsoft.Json.Linq.JToken data in action["data"]["value"])
                    {
                        allResults += $"{(string)data["name"]} ";
                    }
                }
            }
        }
    }
}
