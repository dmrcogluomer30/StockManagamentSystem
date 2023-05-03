using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StockManagamentSystem.Modules
{
    public class GlobalProc
    {
        public static string AppPath { get => Application.StartupPath; }
        public static string AppPathHTML { get => Directory.GetParent(Application.StartupPath).FullName + "\\HTML"; }
        public static string AssemblyName { get => System.Reflection.Assembly.GetExecutingAssembly().GetName().Name; }


        public static bool DoesPropertyExist(dynamic json, string name)
        {
            if (json is ExpandoObject)
                return ((IDictionary<string, object>)json).ContainsKey(name);

            return json.GetType().GetProperty(name) != null;
        }

        public static bool IsValidEmail(string email)
        {
            string pattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";
            return Regex.IsMatch(email, pattern);
        }

        public static string ConvertToSHA256(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);

                byte[] hashBytes = sha256.ComputeHash(inputBytes);

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    builder.Append(hashBytes[i].ToString("x2"));
                }

                return builder.ToString();
            }
        }

        public static string ReplaceHTML(string str)
        {
            return str.Replace("\t", "").Replace("\r", "").Replace("\n", "");
        }

        public static string AddTable(string tableName, params string[] colons)
        {
            string cols = "";
            foreach (string col in colons) cols += "<th rowspan='1' colspan='1'>" + col + "</th>";
            return ReplaceHTML(@"<div class='form-group' id='warningformlabel'></div><table class='table'>
                                  <thead>
                                    <tr>
                                      " + cols + @"
                                    </tr>
                                  </thead>
                                  <tbody id='tableContentBody'>
                                    
                                  </tbody>
                                </table>");

        }

        public static string GetAdminLevelName(int adminLevel)
        {
            switch(adminLevel)
            {
                case 1: return "YETKİLİ";
                case 5: return "YÖNETİCİ";
                default: return "KULLANICI";
            }
        }

        public static bool isNumeric(string str)
        {
            decimal num;
            bool isNumeric = decimal.TryParse(str, out num);
            if (isNumeric)
            {
                return true;
            }
            return false;
        }

    }
}
