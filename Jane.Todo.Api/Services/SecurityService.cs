using System;

namespace Jane.Todo.Api.Services
{
	public class SecurityService : ISecurityService
	{
		public bool SignIn(string userName, string password)
		{
			return userName.Equals("jane", StringComparison.OrdinalIgnoreCase) && 
				   password.Equals("password", StringComparison.OrdinalIgnoreCase);
		}
	}
}