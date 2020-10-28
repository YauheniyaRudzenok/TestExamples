using Jane.UI.Tests.PageObjectModels;

namespace Jane.UI.Tests.PageObjectModels
{
	public static class Main
	{
		private static PageManager _pageManager;

		public static PageManager PageManager => _pageManager ??= new PageManager();
		public static void Clean()
		{
			PageManager.CleanWebDriver();
			_pageManager = null;
		}
	}
}
