# IdentityServer.RazorViewEngine

This project allows use of RazorEngine to generate views of IdentityServer.

The main objective was to use the same Layout for pages 

Usage:

```csharp 
var factory = new IdentityServerServiceFactory();

/*Initialize your factory as described in IdentityServer documentation*/

// Specify custom viewService adn corresponding ViewLoader
factory.ViewService = new RazorViewServiceRegistration(
				new RazorViewServiceConfiguration(
					new Registration<IRazorViewLoader>(
						x => new DiskViewLoader(new [] {"Views","Shared"}))));
```

#Razor ViewLoader
Two view loaders are available, they both support overloading of view based on clientname and/or tenant

* InMemoryViewLoader
* DiskViewLoader

## DiskViewLoader

This loader load view from hard disk. You can specify multiple path in the constructor (It is often usefull if you want to share the Layout between IdentityServer pages and MVC pages) 

It is also possible to customize view for a specific client or a specific tenant of a client.

**You have to specify at least all the view of IdentityServer**, a Quick Razor port of IdentityServer's default views can be found [here](https://github.com/vdaron/IdentityServer.RazorViewEngine/tree/develop/IdentityServer.RazorViewEngine.Sample/UserViews)

### Example

With a DiskViewLoader created with the folowing parameter

```csharp
new DiskViewLoader(new [] {"Views","Shared"})
```

To retrieve the login view for client "SampleClient" and tenant "Tenant1", the DiskViewLoader will look for a view using in the folowing order

    Views\SampleClient\Tenant1\login.cshtml
    Views\SampleClient\login.cshtml
    Views\login.cshtml
    Shared\SampleClient\Tenant1\login.cshtml
    Shared\SampleClient\login.cshtml
    Shared\login.cshtml

## InMemoryViewLoader

This ViewLoader is mainly used for testing purpose. Usage samples can be found int the [Unit Tests project](https://github.com/vdaron/IdentityServer.RazorViewEngine/blob/develop/IdentityServer.RazorViewEngine.Tests/Tests.cs)
