using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using IdentityServer3.Core;
using IdentityServer3.Core.Services.InMemory;

namespace IdentityServer.RazorViewEngine.Sample.App_Code
{
	public static class Users
	{
		public static List<InMemoryUser> Get()
		{
			return new List<InMemoryUser>
		  {
				new InMemoryUser
				{
					 Username = "bob",
					 Password = "secret",
					 Subject = "1",

					 Claims = new[]
					 {
						  new Claim(Constants.ClaimTypes.GivenName, "Bob"),
						  new Claim(Constants.ClaimTypes.FamilyName, "Smith")
					 }
				}
		  };
		}
	}

}