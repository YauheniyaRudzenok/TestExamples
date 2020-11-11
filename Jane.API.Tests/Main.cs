using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Jane.API.Tests
{
	public static class ApiMain
	{
		private static ClientManager _clientManager;
		public static ClientManager ClientManager => _clientManager ??= new ClientManager();
		public static void Clean()
		{
			_clientManager = null;
		}
	}
}
