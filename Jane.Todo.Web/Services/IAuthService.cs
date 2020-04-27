using System.Threading.Tasks;
using Jane.Todo.Dto;

namespace Jane.Todo.Web.Services
{
	public interface IAuthService
	{
		Task<AuthenticationResultDto> SignIn(string userName, string password);
	}
}