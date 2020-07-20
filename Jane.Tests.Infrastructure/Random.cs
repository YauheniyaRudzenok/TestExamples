using System;
using System.Text;
using System.Text.RegularExpressions;

namespace Jane.UI.Tests.TestServices
{
	public static class Randoms
	{
		public static string GenerateStringValueInRange(int min, int max)
		{
			const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ abcdefghijklmnopqrstuvwxyz 0123456789!@#$%^&*()_+?P{}:/~";
			StringBuilder stringBuilder = new StringBuilder();
			var random = new Random();

			var length = random.Next(min, max);

			for (int i = 0; i < length; i++)
			{
				stringBuilder.Append(chars[random.Next(chars.Length)]);
			}

			var stringWithMultipleSpaces = stringBuilder.ToString().Trim();
			var finalString = Regex.Replace(stringWithMultipleSpaces, " {2,}", " ");
			return finalString;
		}
		public static int GenerateRandomNumberInRange(int min, int max)
		{
			var random = new Random();
			int randomValue = random.Next(min, max);
			return randomValue;
		}

		public static string GenerateRandomDateToString()
		{
			var year = GenerateRandomNumberInRange(2000, 2025);
			var month = GenerateRandomNumberInRange(1, 12);
			var maxDays = DateTime.DaysInMonth(year, month);
			var day = GenerateRandomNumberInRange(1, maxDays);
			var date = new DateTime(year, month, day);
			return date.ToString("yyyy-MM-dd");
		}

		public static DateTime GenerateRandomDate()
		{
			var year = GenerateRandomNumberInRange(2000, 2025);
			var month = GenerateRandomNumberInRange(1, 12);
			var maxDays = DateTime.DaysInMonth(year, month);
			var day = GenerateRandomNumberInRange(1, maxDays);
			var date = new DateTime(year, month, day);
			return date;
		}
	}
}
