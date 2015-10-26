using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Text;

namespace IdentityServer.RazorViewEngine.ViewLoaders
{
	public class DiskViewLoader : IRazorViewLoader
	{
		private readonly IEnumerable<string> _viewPaths;
		private readonly string _viewExtension;

		public string ViewExtension { get; set; }

		public DiskViewLoader(IEnumerable<string> viewPaths, string viewExtension = ".cshtml")
		{
			_viewPaths = viewPaths;
			_viewExtension = viewExtension;
		}

		public string Load(string name, string clientId = null, string tenant = null)
		{
			var paths = new List<string>();

			if (!name.EndsWith(_viewExtension))
			{
				name += _viewExtension;
			}

			if (!string.IsNullOrWhiteSpace(clientId) && !string.IsNullOrWhiteSpace(tenant))
			{
				paths.AddRange(_viewPaths.Select(path => Path.Combine(path, clientId, tenant, name)));
			}

			if (!string.IsNullOrWhiteSpace(clientId))
			{
				paths.AddRange(_viewPaths.Select(path => Path.Combine(path, clientId, name)));
			}

			paths.AddRange(_viewPaths.Select(path => Path.Combine(path, name)));

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
