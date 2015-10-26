using System.Collections.Generic;

namespace IdentityServer.RazorViewEngine.ViewLoaders
{
	public class InMemoryViewLoader : IRazorViewLoader
	{
		private readonly Dictionary<string, string> _templates = new Dictionary<string, string>();

		public void AddView(string view, string name, string clientId = null, string tenant = null)
		{
			_templates.Add($"{name}:{clientId}:{tenant}".ToLower(), view);
		}

		public string Load(string name, string clientId = null, string tenant = null)
		{
			string tenantKey = $"{name}:{clientId}:{tenant}".ToLower();
			string clientKey = $"{name}:{clientId}:".ToLower();
			string nameKey = $"{name}::".ToLower();

			return _templates.ContainsKey(tenantKey)
				? _templates[tenantKey]
				: _templates.ContainsKey(clientKey)
					? _templates[clientKey]
					: _templates.ContainsKey(nameKey) 
						? _templates[nameKey] 
						: null;
		}
	}
}
