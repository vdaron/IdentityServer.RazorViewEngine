using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityServer.RazorViewEngine.ViewLoaders;
using IdentityServer3.Core.Configuration;
using IdentityServer3.Core.Services.Default;
using RazorEngine;
using RazorEngine.Compilation;
using RazorEngine.Configuration;
using RazorEngine.Templating;
using RazorEngine.Text;

namespace IdentityServer.RazorViewEngine
{
	public class RazorViewServiceConfiguration
	{
		public RazorViewServiceConfiguration(Registration<IRazorViewLoader> razorViewLoaderRegistration)
		{
			if (razorViewLoaderRegistration == null)
				throw new ArgumentNullException(nameof(razorViewLoaderRegistration));

			RazorViewLoader = razorViewLoaderRegistration;
			CompilerServiceFactory = new DefaultCompilerServiceFactory();
			EncodedStringFactory = new HtmlEncodedStringFactory();
			CachingProvider = new DefaultCachingProvider();
			Namespaces = new HashSet<string>()
			{
				"System",
				"System.Collections.Generic",
				"System.Linq"
			};
			RazorEngineConfigurationSection configuration = RazorEngineConfigurationSection.GetConfiguration();
			Language = configuration?.DefaultLanguage ?? Language.CSharp;
		}

		public Registration<IRazorViewLoader> RazorViewLoader { get; set; }

		public Type BaseTemplateType { get; set; }

		/// <summary>
		/// Gets or sets the caching provider.
		/// 
		/// </summary>
		public ICachingProvider CachingProvider { get; set; }

		/// <summary>
		/// Gets or sets the compiler service factory.
		/// 
		/// </summary>
		public ICompilerServiceFactory CompilerServiceFactory { get; set; }

		/// <summary>
		/// Gets whether the template service is operating in debug mode.
		/// 
		/// </summary>
		public bool Debug { get; set; }

		/// <summary>
		/// Gets or sets the encoded string factory.
		/// 
		/// </summary>
		public IEncodedStringFactory EncodedStringFactory { get; set; }

		/// <summary>
		/// Gets or sets the language.
		/// 
		/// </summary>
		public Language Language { get; set; }

		/// <summary>
		/// Gets or sets the collection of namespaces.
		/// 
		/// </summary>
		public ISet<string> Namespaces { get; set; }
	}
}
