using System;
using System.Collections.Generic;
using System.Text;

namespace Jane.UI.Tests.TestServices
{
	public class TestService
	{
		public string GenerateStringValueInRange(int min, int max)
		{
			const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ abcdefghijklmnopqrstuvwxyz 0123456789";
			StringBuilder stringBuilder = new StringBuilder();
			var random = new Random();

			var length = random.Next(min, max);

			for (int i = 0; i < length; i++)
			{
				stringBuilder.Append(chars[random.Next(chars.Length)]);
			}
			return stringBuilder.ToString();
		}
	}
}
