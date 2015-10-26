namespace IdentityServer.RazorViewEngine.ViewLoaders
{
	public interface IRazorViewLoader
	{
		string Load(string name, string clientId = null, string tenant = null);
	}
}