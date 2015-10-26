using System.IO;
using RazorEngine.Templating;

namespace IdentityServer.RazorViewEngine
{
	class IdentityTemplateKey : ITemplateKey
	{
		public IdentityTemplateKey(string name, 
		                           string clientId,
		                           string tenant,
		                           ResolveType resolveType = ResolveType.Global)
		{
			Name = name;
			ClientId = clientId;
			Tenant = tenant;
			TemplateType = resolveType;
		}

		public string GetUniqueKeyString()
		{
			return $"{Name}:{ClientId}:{Tenant}".ToLower();
		}

		public string Name { get; set; }
		public string ClientId { get; set; }
		public string Tenant { get; set; }
		public ResolveType TemplateType { get; set; }
		public ITemplateKey Context { get; set; }
	}
}
