# Bertoni Technical Assessment - [![germmand/bertoni-project](https://circleci.com/gh/germmand/bertoni-project.svg?style=shield)](https://app.circleci.com/pipelines/github/germmand/bertoni-project)

This is a **Proof of Concept** project developed for [Bertoni Solutions](https://www.bertonisolutions.com/). 

### Prerequisites 📋

In order to install this application locally you would need to have installed the following software:

* [ASP.NET Core SDK (Version 3.0)](https://dotnet.microsoft.com/download/dotnet-core/3.0)

### Installing 🔧

1. Clone the repository:
```
$ git clone git@github.com:germmand/bertoni-project.git && cd bertoni-project
```

Or if you'd rather use **https**:
```
$ git clone https://github.com/germmand/bertoni-project.git && cd bertoni-project
```

2. Install NuGet dependencies:
```
$ dotnet restore
```

3. Cd into the Bertoni.Web project:
```
$ cd src/Bertoni.Web
```

4. Launch the development server:
```
$ dotnet run
```

And at this point, you should be able to access the application through: `https://localhost:5001`.

## Running tests ⚙️

To run the test suite, all you need to do is running in the project's root directory:

```
$ dotnet test
```

## Deployment 📦

If you wish to deploy your own version of this, this supports [Heroku Container Deployment](https://devcenter.heroku.com/articles/build-docker-images-heroku-yml). 

> Note: This assumes you are already familar with Heroku.

1. You need to install the [Heroku CLI](https://devcenter.heroku.com/articles/heroku-cli)
2. If you're not logged in, log into the heroku cli. 
```
$ heroku login
```
3. Create an application. Either through the Heroku Dashboard, or through the CLI.
Keep in mind you need to do this in the project's root directory:
```
$ heroku create <app-name>
```
4. Set the stack of the application to Container:
```
$ heroku stack:set container --app=<app-name>
```
5. Deploy using Git:
```
$ git push heroku master
```

At this point you should be able to see the application under: `https://<app-name>.herokuapp.com/`

## Live Demo 💻

There's a live demo of this application already deployed, you can [check it out here.](http://bertoniproject.herokuapp.com/)

## Built with 🛠️

* [ASP.NET Core 3.0 MVC](https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-mvc-app/start-mvc?view=aspnetcore-3.0&tabs=visual-studio) - The web framework used
* [NuGet](https://www.nuget.org/) - Dependency Management
* [Docker](https://docs.docker.com/) - Containers tool for application packaging
* [Heroku](https://heroku.com/) - Deployment platform
* [CircleCI](https://circleci.com/) - Continuous Integration and Delivery

## Contributing 🖇️

Although this is a **Proof of Concept** project, contributions are welcomed. :)
If you don't know where to start, you can check out [this amazing guide](https://github.com/firstcontributions/first-contributions) that will give you a walkthrough.

## License 📄

This project is under the license (MIT) - take a look at the [LICENSE.md](LICENSE.md) file for more details.

## Acknowledgments 🎁

* Thanks to [PurpleBooth](https://github.com/PurpleBooth) for the Gist of the `README` template. 📢
* Thanks to [Villanuevand](https://github.com/Villanuevand)  for making the Gist of the `README` template more elegant. 📢


---
Built with ❤️ by [germmand](https://github.com/germmand) 😊
