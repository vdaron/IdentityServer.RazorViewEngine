using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace IdentityServer.RazorViewEngine.ViewLoaders
{
	public class DiskViewLoader : IRazorViewLoader
	{
		private readonly string _basePath;

		public DiskViewLoader(string basePath)
		{
			_basePath = basePath;
		}

		public string Load(string name, string clientId = null, string tenant = null)
		{
			var paths = new List<string>();

			if (!string.IsNullOrWhiteSpace(clientId) && !string.IsNullOrWhiteSpace(tenant))
			{
				paths.Add(Path.Combine(_basePath, clientId, tenant, name + ".cshtml"));
			}

			if (!string.IsNullOrWhiteSpace(clientId))
			{
				paths.Add(Path.Combine(_basePath, clientId, name + ".cshtml"));
			}
			
			paths.Add(Path.Combine(_basePath, name + ".cshtml"));

			string file = paths.FirstOrDefault(File.Exists);

			if (file != null)
			{
				using (var reader = new StreamReader(file, Encoding.UTF8))
				{
					return reader.ReadToEnd();
				}
			}
			return null;
		}
	}
}
