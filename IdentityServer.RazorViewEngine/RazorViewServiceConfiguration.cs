using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityServer.RazorViewEngine.ViewLoaders;
using IdentityServer3.Core.Services.Default;
using RazorEngine.Configuration;

namespace IdentityServer.RazorViewEngine
{
	public class RazorViewServiceConfiguration
	{
		public RazorViewServiceConfiguration(IRazorViewLoader loader)
		{
			ViewLoader = loader;
			TemplateServiceConfiguration = new TemplateServiceConfiguration
			{
				TemplateManager = new IdentityTemplateManager(ViewLoader)
			};
		}

		public IRazorViewLoader ViewLoader { get; set; }
		public TemplateServiceConfiguration TemplateServiceConfiguration { get; set; }
   }
}
