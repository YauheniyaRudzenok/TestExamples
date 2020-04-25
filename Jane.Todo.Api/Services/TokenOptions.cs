using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Jane.Todo.Api.Services
{
	public static class TokenOptions
	{
		public const int TokenExpiryInDays = 10;
		public const string Issuer = "http://localhost";
		public const string Audience = "Todo";
		public const string SigningKey = "jsakldaslkdahsbcjhasdjhas7786qeajsdaksjdakjsdbhasdgajsdasjdaushdkjas76348wq7e6r8q7weshadjs";

		public static SecurityKey SecurityKey =>
			new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SigningKey));
	}
}