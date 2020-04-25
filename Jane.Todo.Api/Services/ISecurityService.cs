namespace Jane.Todo.Api.Services
{
	public interface ISecurityService
	{
		bool SignIn(string userName, string password);
	}
}