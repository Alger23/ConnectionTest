using System.Net;

namespace ConnectionTest.Models
{
    public static class IpHelper
    {
        public static string IPv4 { get; private set; }
        static IpHelper()
        {
            // 取得外部 IP
            IPv4 = new WebClient().DownloadString("http://icanhazip.com");
        }
    }
}