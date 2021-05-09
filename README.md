# [Gallery Flare]

An easy-to-use web application to upload and search for images. Built as a POC for the [Shopify Fall 2021 Backend Developer Internship]. You can visit the app from here: [Gallery Flare]
## Tech
- ASP.NET Core for the server
- React.JS with MaterialUI for the frontend
- Bing Image API to tag images
- Azure Storage for image files storage
- MongoDB for User and Image information storage
- BCrypt for hashing user password
- JWT for managing user sessions
- xUnit for endpoints tests

## Features

- Upload unlimited images securely to Microsoft Azure servers all around the world!
- Quickly upload multiple images at once with the drag and drop functionality!
- Decide if other users can see your images by making them public or private
- Each image is processed with smart APIs and tags are generated based on its content for a practical and advanced search functionality!
- Search by tags and get all correlated images with high accuracy!
- Search by image and get all similar images with no time!
- Search and view all public images without creating an account!

You can view a quick demo on youtube on how it works by clicking on the image

[![Video](https://webpagetracker.blob.core.windows.net/pics/flare.png)](https://youtu.be/12mRON3D640 "Demo")

## Installation

Unfortunately, you cannot run this app locally because I wanted to protect my connection strings, but ideally, all you have to do is run the following in the path of the solution after filling in your connection strings (includes Bing API, MongoDB, and azure storage) and installing [.NET Core].
```sh
dotnet run
```
You can visit the app from here:
[Gallery Flare]

## Tests
Unit tests are written in xUnit and C#. To run them, all you have to do is navigate to
```sh
\Gallery Flare\Tests\GalleryFlareTests
```
then run, after installing [.NET Core]
```sh
dotnet run
```
They contain a wide range of tests for the API endpoints which are divided into multiple files for organization purposes. Each API endpoint has multiple test cases.
## Architecture

This project follows the Modal View Controller architectural design pattern. And implements dependency injection and separation of concerns.
The Models are C# files that are concrete prototypes of the data in the databases.
They include ImageModel and UserModel.
All the views are React components inside:
```sh
\ClientApp\src\components
```
### Controllers
The Controller folder is divided into two parts, API files in the home directory and an Operations folder with some helper functions. All of the dependencies in each controller are injected to prevent tight coupling. 
- **AuthenticationController.cs**: api endpoints related to user auth
- **GalleryController.cs**: an api endpoint to get all gallery images from the database
- **SearchController.cs**: two api endpoints for searching by tags or searching by image
- **UploadController.cs**: an api endpoint to upload images to azure and store their info in MongoDB

#### Operations
- **AzureStoarge.cs**: A helper function to post images to Azure blob storage
- **Database**: Contains all direct interactions with the databases, divided into different classes such as User and Gallery connectors
- **JWTService.cs**: functions to generate and verify jwt tokens
- **TagImages.cs**: Uses the Bing Image Api to do research about images then cleans the data
- **UserService.cs**: A helper to get logged in user and verify they exist in DB

## How does it work:
#### Upload
While uploading an image the server will call the Bing image API and analyze that image inside the TagImages class. After that it will upload that image to Azure using the method inside AzureStoarge.cs, then it will store all the image info in MongoDB such as its azure public URL, tags, username, and access.
#### View
Every time the user views the Gallery, the GalleryController.cs API endpoint is called, which then calls Database.GetImagesFromDbAsync() with the appropriate filters, such as username, access, and tags. The endpoint returns a stringified array with image URLs and other needed image data.
#### Search
Users can search for an image by its tags, or by a similar image, both of them can be accessed from SearchController.cs. When a user searches by a tag, the search API calls the Database helper and filters all previous images with that tag and returns them. When a user searches by image, the server analyzes that image just as if it's being uploaded and then it compares its tags with other images in the database and returns all the images with 4 or more matching tags (can be changed if needed).


   [Shopify Fall 2021 Backend Developer Internship]: <https://docs.google.com/document/d/1ZKRywXQLZWOqVOHC4JkF3LqdpO3Llpfk_CkZPR8bjak/edit#heading=h.n7bww7g70ipk>
[Gallery Flare]: https://galleryflare.azurewebsites.net
[.NET Core]: https://dotnet.microsoft.com/download

