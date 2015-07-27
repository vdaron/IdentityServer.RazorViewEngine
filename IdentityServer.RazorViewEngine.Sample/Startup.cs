using IdentityServer3.Core.Configuration;
using Owin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using IdentityServer.RazorViewEngine.Sample.App_Code;
using IdentityServer.RazorViewEngine.ViewLoaders;
using IdentityServer3.Core;
using IdentityServer3.Core.Logging;
using IdentityServer3.Core.Models;
using IdentityServer3.Core.Resources;
using IdentityServer3.Core.Services;
using IdentityServer3.Core.Services.Default;
using IdentityServer3.Core.Services.InMemory;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using RazorEngine;
using Serilog;

namespace IdentityServer.RazorViewEngine.Sample
{
	public class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			Log.Logger = new LoggerConfiguration()
				.WriteTo
				.LiterateConsole(outputTemplate: "{Timestamp:HH:MM} [{Level}] ({Name:l}){NewLine} {Message}{NewLine}{Exception}")
				.CreateLogger();

			var vl = new InMemoryViewLoader();
			vl.AddView("<H1>test</H1>", "login");

			string currentDirectory = Path.GetDirectoryName(new Uri(Assembly.GetCallingAssembly().CodeBase).LocalPath);

			app.Map("/identity", idsrvApp =>
			{
				idsrvApp.UseIdentityServer(new IdentityServerOptions
				{
					SiteName = "Embedded IdentityServer",
					SigningCertificate = LoadCertificate(currentDirectory),

					Factory = new IdentityServerServiceFactory
					{
						CorsPolicyService = new Registration<ICorsPolicyService>(x => new DefaultCorsPolicyService() { AllowAll = true }),
						ViewService =
							new RazorViewServiceRegistration(
								new RazorViewServiceConfiguration(
									new Registration<IRazorViewLoader>(x => new DiskViewLoader(Path.Combine(currentDirectory, @"..\UserViews")))))
					}.UseInMemoryClients(Clients.Get())
						.UseInMemoryScopes(StandardScopes.All)
						.UseInMemoryUsers(Users.Get())
				});
			});

			app.UseCookieAuthentication(new CookieAuthenticationOptions
			{
				AuthenticationType = "Cookies"
			});

			app.UseOpenIdConnectAuthentication(new OpenIdConnectAuthenticationOptions
			{
				Authority = "https://localhost:44300/identity",
				ClientId = "mvc",
				RedirectUri = "https://localhost:44300/",
				ResponseType = "id_token",
				SignInAsAuthenticationType = "Cookies"
			});
		}

		X509Certificate2 LoadCertificate(string directoryName)
		{
			return new X509Certificate2(Path.Combine(directoryName, @"..\..\idsrv3test.pfx"), "idsrv3test");
		}
	}

}