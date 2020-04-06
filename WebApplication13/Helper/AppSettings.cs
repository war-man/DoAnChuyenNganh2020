using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace WebApplication13
{
    public class AppSettings
    {
        public SmtpConfig SmtpConfig { get; set; }

    }



    public class SmtpConfig
    {
        public static string SendHost = "";
        public static int SendPort = 25;
        public static string ReceiveHost = "";
        public static int ReceivePort = 993;
        public static string Name = "WebBanHang";
        public static string Username = "WebBanHang";
        public static string EmailAddress = "";
        //public static string Domain = "RVV8HC";
        //public static string Password = "@RBVHfcm2019VMS";
    }
}

