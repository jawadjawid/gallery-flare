# [Gallery Flare]

An easy to use web application to upload and search for images. Built as a POC for the [Shopify Fall 2021 Backend Developer Internship]. You can visit the app from here: [Gallery Flare]
## Tech
- ASP.NET Core for the server
- React.JS with MaterialUI for the frontend
- Bing Image API to tag images
- Azure Storage for image files stoarge
- MongoDB for User and Image information stoarge

## Features

- Upload unlimited images securely to Microsoft Azure servers all around the world!
- Upload multiple images at once with the drag and drop functionality for more efficiency!
- Decide if other users can see your images by making them public or private
- Each image is processed with smart APIs and given tags according to its characteristics for faster search functionality!
- Search by tags and get all corelated images with high accurecy!
- Search by image and get all the images that are similar to it with no time!
- You can search and view all Public images without creating an account!

## Installation

Unfortunately, you cannot run this app locally because I wanted to protect my connection strings, but ideally all you have to do is run the following in the path of the soloution.
```sh
dotnet run
```
You can visit the app from here:
[Gallery Flare]

## Tests

```sh
npm install --production
NODE_ENV=production node app
```

## Architecture

This project follows the Modal View Controller architectural design pattern.
The Models are C# files that are a concrete prototypes of the data in the databases.
They include ImageModel and UserModel.
All the views are React componentes inside:
```sh
\ClientApp\src\components
```
### Controllers
The Controller folder is divided into two parts, API files in the home directory and an Operations folder with some helper functions. 
- **AuthenticationController.cs**: api endpoints realted to user auth
- **GalleryController.cs**: an api enpoint to get all gallery images from database
- **SearchController.cs**: two api endpoints fro searching by tags or searching by image
- **UploadController.cs**: an api endpoint to upload images to azure and store their info in MongoDB

#### Operations
- **AzureStoarge.cs**: A helper function to post images to Azure blob stoarge
- **Database.cs**: Contains all direct interactions with the databases
- **JWTService.cs**: functions to generate and verify jwt tokens
- **TagImages.cs**: Uses the Bing Image Api to do research about images then cleans the data
- **UserService.cs**: A helper to get logged in user and verify they exist in DB


   [Shopify Fall 2021 Backend Developer Internship]: <https://docs.google.com/document/d/1ZKRywXQLZWOqVOHC4JkF3LqdpO3Llpfk_CkZPR8bjak/edit#heading=h.n7bww7g70ipk>
[Gallery Flare]: https://galleryflare.azurewebsites.net

