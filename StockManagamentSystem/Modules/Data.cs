using StockManagamentSystem.Modules.Browser;
using StockManagamentSystem.Modules.MySQL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace StockManagamentSystem.Modules
{
    public class Data
    {
        private static int iD;
        private static string userName;
        private static string name;
        private static string surname;
        private static string mail;
        private static string password;
        private static int admin;
        private static DateTime created;

        private static string currentURL;
        private static ArrayList createdIDs = new ArrayList();
        private static ArrayList createdEvents = new ArrayList();
        private static int dynamicUpdateID = Defines.INVALID_ID;
        private static string pageName;
        private static string pageAnimate;
        private static bool pageClose = false;

        public static int ID { get => iD; set => iD = value; }
        public static string UserName { get => userName; set => userName = value; }
        public static string Name { get => name; set => name = value; }
        public static string Surname { get => surname; set => surname = value; }
        public static string Mail { get => mail; set => mail = value; }
        public static string Password { get => password; set => password = value; }
        public static int Admin { get => admin; set => admin = value; }
        public static DateTime Created { get => created; set => created = value; }
        public static string CurrentURL { get => currentURL; set => currentURL = value; }
        public static ArrayList CreatedIDs { get => createdIDs; set => createdIDs = value; }
        public static ArrayList CreatedEvents { get => createdEvents; set => createdEvents = value; }
        public static int DynamicUpdateID { get => dynamicUpdateID; set => dynamicUpdateID = value; }
        public static string PageName { get => pageName; set { pageName = value; OFDBrowserFunction.InnerHTML("pageName", GlobalProc.ReplaceHTML(pageName)); } }

        public static string PageAnimate { get => pageAnimate; set => pageAnimate = value; }
        public static bool PageClose { get => pageClose; set => pageClose = value; }

        public static string[] AnimList = new string[] { "zoomIn", "fadeIn" };
    }
}
