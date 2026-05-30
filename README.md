# TSTOP

## About this solution

This is a layered startup solution based on [Domain Driven Design (DDD)](https://abp.io/docs/latest/framework/architecture/domain-driven-design) practises. All the fundamental ABP modules are already installed. Check the [Application Startup Template](https://abp.io/docs/latest/solution-templates/layered-web-application) documentation for more info.

### Pre-requirements

* [.NET10.0+ SDK](https://dotnet.microsoft.com/download/dotnet)
* [Node v18 or 20](https://nodejs.org/en)

### Configurations

The solution comes with a default configuration that works out of the box. However, you may consider to change the following configuration before running your solution:

* Check the `ConnectionStrings` in `appsettings.json` files under the `TSTOP.HttpApi.Host` and `TSTOP.DbMigrator` projects and change it if you need.

### Before running the application

* Run `abp install-libs` command on your solution folder to install client-side package dependencies. This step is automatically done when you create a new solution, if you didn't especially disabled it. However, you should run it yourself if you have first cloned this solution from your source control, or added a new client-side package dependency to your solution.
* Run `TSTOP.DbMigrator` to create the initial database. This step is also automatically done when you create a new solution, if you didn't especially disabled it. This should be done in the first run. It is also needed if a new database migration is added to the solution later.

#### Generating a Signing Certificate

In the production environment, you need to use a production signing certificate. ABP Framework sets up signing and encryption certificates in your application and expects an `openiddict.pfx` file in your application.

To generate a signing certificate, you can use the following command:

```bash
dotnet dev-certs https -v -ep openiddict.pfx -p cb879b06-0887-4986-9f72-4a70d2c4c834
```

> `cb879b06-0887-4986-9f72-4a70d2c4c834` is the password of the certificate, you can change it to any password you want.

It is recommended to use **two** RSA certificates, distinct from the certificate(s) used for HTTPS: one for encryption, one for signing.

For more information, please refer to: [OpenIddict Certificate Configuration](https://documentation.openiddict.com/configuration/encryption-and-signing-credentials.html#registering-a-certificate-recommended-for-production-ready-scenarios)

> Also, see the [Configuring OpenIddict](https://abp.io/docs/latest/Deployment/Configuring-OpenIddict#production-environment) documentation for more information.

### Solution structure

This is a layered monolith application that consists of the following applications:

* `angular`: Angular application.
* `TSTOP.DbMigrator`: A console application which applies the migrations and also seeds the initial data. It is useful on development as well as on production environment.
* `TSTOP.HttpApi.Host`: ASP.NET Core API application that is used to expose the APIs to the clients.

#### Test Projects

The `test` folder contains the following test projects:

* `TSTOP.Application.Tests`: Application layer tests.
* `TSTOP.Domain.Tests`: Domain layer tests.
* `TSTOP.EntityFrameworkCore.Tests`: Entity Framework Core integration tests.




## Deploying the application

Deploying an ABP application follows the same process as deploying any .NET or ASP.NET Core application. However, there are important considerations to keep in mind. For detailed guidance, refer to ABP's [deployment documentation](https://abp.io/docs/latest/Deployment/Index).

### GitHub Actions CI/CD

This repository includes `.github/workflows/ci-cd.yml`:

* **CI** runs on pull requests and `main` pushes, then builds/tests both the .NET solution and Angular app.
* **CD** runs after CI on `main` pushes and deploys with Docker containers on a self-hosted runner.

For local-machine CD, configure these repository variables as needed:

* `CD_API_IMAGE`: API image name/tag (default: `tstop-api:latest`).
* `CD_WEB_IMAGE`: Angular image name/tag (default: `tstop-web:latest`).
* `CD_API_CONTAINER`: API container name (default: `tstop-api`).
* `CD_WEB_CONTAINER`: Angular container name (default: `tstop-web`).
* `CD_API_PORT`: host port mapped to API container `8080` (default: `8080`).
* `CD_WEB_PORT`: host port mapped to web container `80` (default: `80`).

### Additional resources


#### Internal Resources

You can find detailed setup and configuration guide(s) for your solution below:

* [Angular](./angular/README.md)

#### External Resources
You can see the following resources to learn more about your solution and the ABP Framework:

* [Web Application Development Tutorial](https://abp.io/docs/latest/tutorials/book-store/part-1)
* [Application Startup Template](https://abp.io/docs/latest/startup-templates/application/index)
