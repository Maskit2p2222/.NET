using System.Configuration;

namespace VinsUncoderLibrary
{
    public static class StaticConfigValues
    {
        public static string CONNECTION_STRING = @"
        Data Source=DESKTOP-FLV2ICD\SQLEXPRESS;Initial Catalog=AllVins;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False
        ";
    }
}