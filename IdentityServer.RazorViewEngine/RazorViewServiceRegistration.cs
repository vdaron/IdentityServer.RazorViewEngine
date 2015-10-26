using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using IdentityServer.RazorViewEngine.ViewLoaders;
using IdentityServer3.Core.Configuration;
using IdentityServer3.Core.Services;
using IdentityServer3.Core.Services.Default;
using RazorEngine.Configuration;
using RazorEngine.Templating;

namespace IdentityServer.RazorViewEngine
{
	public class RazorViewServiceRegistration<T> : Registration<IViewService, T> where T : RazorViewService
	{
		public RazorViewServiceRegistration(RazorViewServiceConfiguration options) : base(null)
		{
			if (options == null)
				throw new ArgumentNullException(nameof(options));

			AdditionalRegistrations.Add(options.RazorViewLoader);
			AdditionalRegistrations.Add(new Registration<RazorViewServiceConfiguration>(options));
			AdditionalRegistrations.Add(new Registration<TemplateServiceConfiguration>(x => new TemplateServiceConfiguration
			{
				Namespaces = options.Namespaces,
				Debug = options.Debug,
				Language = options.Language,
				CachingProvider = options.CachingProvider,
				CompilerServiceFactory = options.CompilerServiceFactory,
				EncodedStringFactory = options.EncodedStringFactory,
				BaseTemplateType = options.BaseTemplateType,
				TemplateManager = new IdentityTemplateManager(x.Resolve<IRazorViewLoader>())
			}));
		}
	}

	public class RazorViewServiceRegistration : RazorViewServiceRegistration<RazorViewService>
	{
		public RazorViewServiceRegistration(RazorViewServiceConfiguration options)
		  : base(options)
		{
		}
	}
}
