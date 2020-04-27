using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;

namespace Jane.Todo.Web.Extensions
{
	public static class AuthenticationStateExtensions
	{
		public static async Task<bool> IsAuthenticated(this Task<AuthenticationState> authenticationStateTask)
		{
			var authState = await authenticationStateTask;

			return authState.User.Identity.IsAuthenticated;
		}
	}
}