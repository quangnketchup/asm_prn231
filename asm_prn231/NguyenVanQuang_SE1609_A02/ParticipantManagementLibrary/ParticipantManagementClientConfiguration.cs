using Microsoft.Extensions.Configuration;

namespace ParticipantManagementLibrary
{
    public static class ParticipantManagementClientConfiguration
    {
        #region Private Members to get Configuration
        private static IConfigurationRoot GetConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.client.json", true, true);
            return builder.Build();
        }
        #endregion


        public static string DefaultAppName => GetConfiguration()["AppName:Default"];
        public static string DefaultBaseApiUrl => GetConfiguration()["ApiUrl:Default"];
        public static string DefaultOdataUrl => DefaultBaseApiUrl + "odata";
        public static string DefaultApiUrl => DefaultBaseApiUrl + "api";
    }
}
