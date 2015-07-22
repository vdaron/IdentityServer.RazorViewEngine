using System;
using System.Collections.Generic;
using System.IO;
using IdentityServer.RazorViewEngine;
using IdentityServer.RazorViewEngine.ViewLoaders;
using IdentityServer3.Core.Models;
using IdentityServer3.Core.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Identity.razorViewEngine.Tests
{
	[TestClass]
	public class Tests
	{
		[TestMethod]
		public void RenderLoginPage()
		{
			var viewLoader = new InMemoryViewLoader();
			viewLoader.AddView(@"<b>@Model.ClientName</b>", "login");

			RazorViewService e = new RazorViewService(new RazorViewServiceConfiguration(viewLoader));

			var str = e.Login(new LoginViewModel{ClientName = "My Client Name"}, new SignInMessage()).Result;

			Assert.AreEqual("<b>My Client Name</b>", StreamToString(str));
		}

		[TestMethod]
		public void RenderLoginPageWithLayout()
		{
			var viewLoader = new InMemoryViewLoader();
			viewLoader.AddView("<layout>@RenderBody()</layout>", "myLayout");
			viewLoader.AddView(@"@{this.Layout = @""mylayout"";}<b>@Model.ClientName</b>", "login");

			RazorViewService e = new RazorViewService(new RazorViewServiceConfiguration(viewLoader));

			var str = e.Login(new LoginViewModel { ClientName = "My Client Name" }, new SignInMessage()).Result;

			Assert.AreEqual("<layout><b>My Client Name</b></layout>", StreamToString(str));
		}

		[TestMethod]
		public void RenderLoginPageWithClientOverride()
		{
			var viewLoader = new InMemoryViewLoader();
			viewLoader.AddView(@"<b>@Model.ClientName</b>", "login");
			viewLoader.AddView(@"<override>@Model.ClientName</override>", "login", "clientid");

			RazorViewService e = new RazorViewService(new RazorViewServiceConfiguration(viewLoader));

			var str = e.Login(new LoginViewModel { ClientName = "My Client Name" }, new SignInMessage {ClientId = "clientid"}).Result;

			Assert.AreEqual("<override>My Client Name</override>", StreamToString(str));
		}

		[TestMethod]
		public void RenderLoginPageWithClientAndTenantOverride()
		{
			var viewLoader = new InMemoryViewLoader();
			viewLoader.AddView(@"<b>@Model.ClientName</b>", "login");
			viewLoader.AddView(@"<override>@Model.ClientName</override>", "login", "clientid");
			viewLoader.AddView(@"<T1>@Model.ClientName</T1>", "login", "clientid", "T1");

			RazorViewService e = new RazorViewService(new RazorViewServiceConfiguration(viewLoader));

			var str = e.Login(new LoginViewModel { ClientName = "My Client Name" }, new SignInMessage { ClientId = "clientid" }).Result;

			Assert.AreEqual("<override>My Client Name</override>", StreamToString(str));

			str = e.Login(new LoginViewModel { ClientName = "My Client Name" }, new SignInMessage { ClientId = "clientid", Tenant = "T1"}).Result;

			Assert.AreEqual("<T1>My Client Name</T1>", StreamToString(str));
		}

		private static string StreamToString(Stream stream)
		{
			using (var reader = new StreamReader(stream))
			{
				return reader.ReadToEnd();
			}
		}
	}
}
