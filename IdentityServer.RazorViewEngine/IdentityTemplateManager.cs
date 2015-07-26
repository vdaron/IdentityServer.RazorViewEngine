using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using IdentityServer.RazorViewEngine.ViewLoaders;
using IdentityServer3.Core.Services.Default;
using RazorEngine.Templating;

namespace IdentityServer.RazorViewEngine
{
	public class IdentityTemplateManager: ITemplateManager
	{
		private readonly IRazorViewLoader _razorViewLoader;

		public IdentityTemplateManager(IRazorViewLoader razorViewLoader)
		{
			_razorViewLoader = razorViewLoader;
		}

		public ITemplateSource Resolve(ITemplateKey key)
		{
         IdentityTemplateKey itk = (IdentityTemplateKey)key;

			var templateContent = _razorViewLoader.Load(itk.Name, itk.ClientId, itk.Tenant);

			if (templateContent != null)
			{
				return new LoadedTemplateSource(templateContent, null);
			}
			throw new FileNotFoundException();
      }

		public ITemplateKey GetKey(string name, ResolveType resolveType, ITemplateKey context)
		{
			var itk = context as IdentityTemplateKey;

			if (itk != null)
			{
				return new IdentityTemplateKey(name, itk.ClientId, itk.Tenant, resolveType);
			}

			return null;
		}

		public void AddDynamic(ITemplateKey key, ITemplateSource source)
		{
			throw new NotImplementedException("dynamic templates are not supported!");
		}
	}
}