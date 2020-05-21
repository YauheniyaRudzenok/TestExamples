using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Jane.UI.Tests.TestServices
{
	public static class TestService
	{
		public static string GenerateStringValueInRange(int min, int max)
		{
			const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ abcdefghijklmnopqrstuvwxyz 0123456789";
			StringBuilder stringBuilder = new StringBuilder();
			var random = new Random();

			var length = random.Next(min, max);

			for (int i = 0; i < length; i++)
			{
				stringBuilder.Append(chars[random.Next(chars.Length)]);
			}

			var stringWithMultipleSpaces = stringBuilder.ToString();
			var finalString = Regex.Replace(stringWithMultipleSpaces, " {2,}", " ");
			return finalString.ToString();
		}
	}
}
