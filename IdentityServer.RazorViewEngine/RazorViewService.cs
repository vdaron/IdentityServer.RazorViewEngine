using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityServer.RazorViewEngine.ViewLoaders;
using IdentityServer3.Core.Models;
using IdentityServer3.Core.Services;
using IdentityServer3.Core.Services.Default;
using IdentityServer3.Core.Validation;
using IdentityServer3.Core.ViewModels;
using RazorEngine.Compilation.ImpromptuInterface.InvokeExt;
using RazorEngine.Configuration;
using RazorEngine.Templating;

namespace IdentityServer.RazorViewEngine
{
	public class RazorViewService : DefaultViewService
	{
		private readonly IRazorEngineService _service;

		public RazorViewService(TemplateServiceConfiguration config) : base(new DefaultViewServiceOptions(), new EmbeddedAssetsViewLoader())
		{            
			config.Debug = true;
			_service = RazorEngineService.Create(config);
		}

		public override Task<Stream> Login(LoginViewModel model, SignInMessage message)
		{
			return Task.FromResult(RunTemplate("login", model, message.ClientId, message.Tenant));
		}

        public override Task<Stream> Logout(LogoutViewModel model, SignOutMessage message)
		{
			return Task.FromResult(RunTemplate("logout", model, message?.ClientId));
		}

        public override Task<Stream> LoggedOut(LoggedOutViewModel model, SignOutMessage message)
		{
			return Task.FromResult(RunTemplate("loggedout", model, message?.ClientId));
		}

        public override Task<Stream> Consent(ConsentViewModel model, ValidatedAuthorizeRequest authorizeRequest)
		{
			return Task.FromResult(RunTemplate("consent", model, authorizeRequest.ClientId));
		}

        public virtual Task<Stream> ClientPermissions(ClientPermissionsViewModel model)
		{
			return Task.FromResult(RunTemplate("permission", model));
		}

        public override Task<Stream> Error(ErrorViewModel model)
		{
			return Task.FromResult(RunTemplate("error", model));
		}

        public override Task<Stream> AuthorizeResponse(AuthorizeResponseViewModel model)
        {
            return Task.FromResult(RunTemplate("formpostresponse", model));
        }

        protected Stream RunTemplate(string key, object model, string clientId = null, string tenant = null)
	        {
	            var viewBag = new DynamicViewBag(new Dictionary<string, object>
	            {
	                { "Page", key }
	            });
	            return StringToStream(_service.RunCompile(new IdentityTemplateKey(key, clientId, tenant), model.GetType(), model, viewBag));
	        }

		private static Stream StringToStream(string s)
		{
			MemoryStream stream = new MemoryStream();
			StreamWriter writer = new StreamWriter(stream);
			writer.Write(s);
			writer.Flush();
			stream.Position = 0;
			return stream;
		}
	}
}
