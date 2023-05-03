using StockManagamentSystem.Modules.MySQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StockManagamentSystem.Modules.Browser
{
    public class OFDBrowserFunction
    {
        public static void Login(string username, string password)
        {
            string scriptCode = string.Empty;
            if (username == string.Empty || password == string.Empty)
            {
                scriptCode = "$('#alertmsg').html('" + "Kullanıcı adı veya şifrenizi boş bıraktınız, lütfen alanları kontrol ederek tekrar deneyin." + "');" +
                    "$('#alertmsg').attr('class', 'alert alert-danger');";
                OFDBrowser.CallJS(scriptCode);
                return;
            }

            if (password.Length < 6)
            {
                scriptCode = "$('#alertmsg').html('" + "Şifreniz en az altı karakter içermelidir." + "');" +
                    "$('#alertmsg').attr('class', 'alert alert-danger');";
                OFDBrowser.CallJS(scriptCode);
                return;
            }

            password = GlobalProc.ConvertToSHA256(password);

            var query = DBContext.Instance.Users.Where(q => q.UserName == username && q.Password == password);
            if (GlobalProc.IsValidEmail(username))
            {
                query = DBContext.Instance.Users.Where(q => q.Mail == username && q.Password == password);
            }

            if (query.Count() <= 0)
            {
                scriptCode = "$('#alertmsg').html('" + "Geçersiz kullanıcı adı/e-posta veya şifre, kontrol edip tekrar deneyin." + "');" +
                   "$('#alertmsg').attr('class', 'alert alert-danger');";
                OFDBrowser.CallJS(scriptCode);
                return;
            }
            else
            {
                scriptCode = "$('#alertmsg').html('" + "Başarılı bir şekilde giriş yaptınız, yönlendiriliyorsunuz." + "');" +
                   "$('#alertmsg').attr('class', 'alert alert-info');" +
                   "$('#loginButton').attr('disabled', 'true');";
                OFDBrowser.CallJS(scriptCode);

                var data = query.FirstOrDefault();

                if (data != null)
                {
                    Data.ID = data.ID;
                    Data.UserName = data.UserName;
                    Data.Name = data.Name;
                    Data.Surname = data.Surname;
                    Data.Mail = data.Mail;
                    Data.Password = data.Password;
                    Data.Admin = data.Admin;
                    Data.Created = data.Created;
                }

                OFDBrowser.ChangePage("index.html");

                return;
            }

        }

        public static void InsertAdjacentHTMLBefore(string id, string html)
        {
            if (Data.CreatedIDs.Count > 0 && Data.CreatedIDs.Contains(id))
            {
                InnerHTML(id, html);
                return;
            }
            Data.CreatedIDs.Add(id);
            OFDBrowser.CallJS("document.getElementById(\"" + id + "\").insertAdjacentHTML(\"beforeend\", \"" + html + "\");");
        }

        public static void InnerHTML(string id, string html, bool plus = false)
        {
            OFDBrowser.CallJS("document.getElementById(\"" + id + "\").innerHTML " + (plus ? "+" : "") + "= \"" + html + "\";");
            Random rnd = new Random();
            string oldAnimate = Data.PageAnimate;
            Data.PageAnimate = Data.AnimList[rnd.Next(Data.AnimList.Length)];
            AddAnimate(id, Data.PageAnimate, oldAnimate);
        }

        public static void AddClickEventFunction(string targetFunctionName)
        {
            if (Data.CreatedEvents.Count > 0 && Data.CreatedEvents.Contains(targetFunctionName)) Data.CreatedEvents.Remove(Data.CreatedEvents.IndexOf(targetFunctionName));
            Data.CreatedEvents.Add(targetFunctionName);
            OFDBrowser.CallJS("triggerAddEvent('"+ targetFunctionName +"');");
        }

        public static void WariningFormLabel(string type, string head, string message)
        {
            string html = @"
            <div class='alert alert-" + type + @" alert-dismissible'>
	            <button type='button' class='close' data-dismiss='alert' aria-hidden='true'>×</button>
	            <h5><i class='icon fas fa-ban'></i> " + head + @"</h5>
	            " + message + @"
            </div>";
            InnerHTML("warningformlabel", GlobalProc.ReplaceHTML(html));
        }

        public static void AddAnimate(string id, string animate, string oldAnimate)
        {
            OFDBrowser.CallJS("document.getElementById(\"" + id + "\").classList.remove(\"animate__animated\", \"animate__" + oldAnimate + "\");");
            OFDBrowser.CallJS("void document.getElementById(\"" + id + "\").offsetWidth;");
            OFDBrowser.CallJS("document.getElementById(\"" + id + "\").classList.add(\"animate__animated\", \"animate__"+ animate + "\");");
        }

    }
}
