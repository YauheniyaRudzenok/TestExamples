using NLog;
using RestSharp;

namespace Jane.API.Tests
{
    public static class Log
    {
        public static void TestTitle(Logger log, string title) => log.Info(title.ToUpper);
        public static void StatusCode(Logger log, IRestResponse response, string expected) =>
            log.Info("Response code is: {0}, Expected: {1}", response.StatusCode, expected);

        public static void ClientCreation(Logger log) => log.Info("Client is created");
    }
}
