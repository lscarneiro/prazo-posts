# prazo-posts
This project was developed as a coding exercise for .NET Core, MongoDB, Unit Tests and mobile development.

**Technologies involved in the API...**
*  .NET Core v2.1
*  WebAPI
*  MongoDB
*  Automapper
*  FluentValidation
*  JWT (for Authentication token)
*  xUnit tests
*  Moq (for mocking)

**... and on the mobile app**
*  ionic v3
*  Typescript
*  Angular 5



## Running the API

**To run the WebAPI you first need a working MongoDB server that you should configure on the `appsettings.Development.json` (for debug) or `appsettings.json` (for release)**

Once you have your MongoDB instance ready and configured, access `PrazoPosts.Api` folder and run the following commands:

`dotnet restore` to restore NuGet packages

`dotnet run` to run up the project and you should get your API up! Cool!


To Unit test the service level just go to  the `PrazoPosts.Service.Tests` folder and run: `dotnet test`


## Running the mobile app
First you need `ionic` and `cordova` installed

To do that just run on your terminal:

`npm i -g ionic cordova`

`npm install`

After that you're ready to Rock'n'Roll baby!

To change the settings like `API_URL` and things like that, modify the file `PrazoPosts.Mobile/src/app/modules/core/config.service.ts`

Now, simply head to `PrazoPosts.Mobile` root and run `ionic serve`

## How-to use it 
Steps to navigate through the app:
1. Create your account
2. Create an Author
3. Create your first Post
4. Enjoy

## Some images
**Login page**

![Login page](https://github.com/lscarneiro/prazo-posts/blob/master/readme-img/ogin-page.png)

**Home page**

![Home page](https://github.com/lscarneiro/prazo-posts/blob/master/readme-img/home-page.png)

**Authors page**

![Authors page](https://github.com/lscarneiro/prazo-posts/blob/master/readme-img/author-page.png)

**Registration page**

![Registrtion page](https://github.com/lscarneiro/prazo-posts/blob/master/readme-img/user-registration.png)
