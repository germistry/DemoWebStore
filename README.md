# DemoWebStore - An ASP.NET Core Web Store Template
This is a demo ASP.NET Core Web Store that uses Bulma, Vue and a common payment provider. This demo has it's logic separated into domain, database, application and UI layers following
the single responsibility principle so that it can be scaled as necessary. I can use this demo as a starting point or as a boilerplate solution to significantly cut down on development
time of a web store project for a client.

### The Domain
Contains the models, enums and service interfaces. Built on NET Standard 2.1 framework and has no other dependencies. 
### The Database 
Contains the application database context, and implementions of service interfaces. Is directly dependent upon the domain layer. The service implementations use entity framework 
core to interact with the actual database. Built on NET Standard 2.1 framework, and also uses AspNetCore.Identity.EntityFramework for the default identity. This can easily be switched 
out for Identity Server if required. 
### The Application
Contains the services which are CRUD operations and any other business logic (such as the logic for shopping cart operations). Dependency injection is used to register these services, 
therefore keeping all CRUD and business logic out of the UI. Is directly dependent upon the domain layer and is also built on the NET Standard 2.1 framework.
### The UI
This is a NET Core 3.1 App that is directly dependent upon the application layer. It implements the Session Manager Interface for a shopping cart cookie, which could be easily scaled
to a database solution if the site was to grow in terms of usage or as a requirement of the business. The UI uses Default Identity and User Claims for administration roles. The Admin 
Interface makes use of Vue.js apps for managing products, stock and orders. The Admin user can also create and delete Manager users. The Public Interface allows any user to make an 
order, and uses the Stripe API for handling credit cards. This could be switched out to Paypal API if required. The app can also be easily extended to enable customers to create 
logins with the site using Default Identity if required. 

Bulma is used as a boilerplate styling solution, but this can obviously be switched to either a custom HTML template or restyled to meet client design requirements.   

More details on how I went about designing and creating this project can be found in [my portfolio](https://germistry.com/Portfolio/4/aspnet-core-web-store-template).
