using IdentityServer3.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IdentityServer.RazorViewEngine.Sample.App_Code
{
	public static class Clients
	{
		public static IEnumerable<Client> Get()
		{
			return new[]
			{
				new Client
				{
					Enabled = true,
					ClientName = "MVC Client",
					ClientId = "mvc",
					Flow = Flows.Implicit,
					EnableLocalLogin = true,

					RedirectUris = new List<string>
					{
						"https://localhost:44300/"
					},

					AllowedScopes = StandardScopes.All.Select(x => x.Name).ToList()
				}
			};
		}
	}
}