using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace StockManagamentSystem.Modules.Browser
{
    public class OFDBrowser
    {
        private static ChromiumWebBrowser chromium;

        public static ChromiumWebBrowser Chromium { get => chromium; }


        public static void Load()
        {
            chromium = new ChromiumWebBrowser(@GlobalProc.AppPathHTML + @"\" + "login.html");
            chromium.ConsoleMessage += OnBrowserConsoleMessage;
            chromium.JavascriptMessageReceived += OnBrowserJavascriptMessageReceived;
            chromium.FrameLoadEnd += OnBrowserFrameLoadEnd;
        }

        public static void ChangePage(string pageName)
        {
            chromium.LoadUrl(@GlobalProc.AppPathHTML + @"\" + pageName);
        }

        private static void OnBrowserConsoleMessage(object sender, ConsoleMessageEventArgs args)
        {
            MessageBox.Show(args.Message);
        }

        public static void CallJSFunction(string functionName, string targetName, params string[] functionValues)
        {
            string values = string.Empty;
            foreach (string functionValue in functionValues)
            {
                values += functionValue + ", ";
            }
            if(values != string.Empty)
            {
                values = values.Substring(0, values.Length - 2);
            }
            chromium.ExecuteScriptAsync(functionName + "('"+ targetName + "', '" + values + "');");
        }

        public static void CallJS(string script)
        {
            chromium.ExecuteScriptAsync(script);
        }

        private static void OnBrowserJavascriptMessageReceived(object sender, JavascriptMessageReceivedEventArgs e)
        {
            if(e.Message.ToString() == "APP_CLOSE")
            {
                Data.PageClose = true;
                Application.Exit();
                return;
            }
            dynamic jsonApi = JsonConvert.DeserializeObject(e.Message.ToString());
            JSReaction((string)jsonApi.name, jsonApi);
        }

        public static void JSReaction(string name, dynamic jsonApi = null)
        {
            switch (name)
            {
                case "triggerOnUpdate":
                    if (jsonApi == null) return;
                    OFDCallbacks.OnUpdate((string)jsonApi.targetName, (string)jsonApi.json);
                    return;
                case "login":
                    if (jsonApi == null) return;
                    OFDBrowserFunction.Login((string)jsonApi.username, (string)jsonApi.password);
                    return;
                case Defines.HOMEPAGE when Data.CurrentURL == "index":
                    Data.PageName = string.Empty;
                    OFDBrowserFunction.InnerHTML("container", HomePageFunctions.GetHomePage());
                    return;
                case "addUserForm" when Data.CurrentURL == "index":
                    OFDBrowserFunction.InnerHTML("container", UserMenuFunctions.AddUserForm());
                    OFDBrowserFunction.AddClickEventFunction(Defines.BUTTON_ADD_USER);
                    Data.PageName = "Kullanıcı Ekle";
                    return;
                case "addUserList" when Data.CurrentURL == "index":
                    OFDBrowserFunction.InnerHTML("container", GlobalProc.AddTable(Defines.TABLE_NAME, "Kullanıcı Adı", "Ad", "Soyad", "E-Posta", "#"));
                    OFDBrowserFunction.InnerHTML("tableContentBody", UserMenuFunctions.GetUserListForTable());
                    OFDBrowserFunction.AddClickEventFunction(Defines.BUTTON_EDIT_USER);
                    Data.PageName = "Kullanıcı Listesi";
                    return;
                case Defines.BUTTON_ADD_USER:
                    CallJSFunction("triggerOnUpdate", Defines.BUTTON_ADD_USER, "mail", "firstname", "surname", "username", "password", "adminlevel");
                    return;
                case Defines.BUTTON_EDIT_USER:
                    Data.DynamicUpdateID = (int)jsonApi.value;
                    OFDBrowserFunction.InnerHTML("container", UserMenuFunctions.EditUserForm(Data.DynamicUpdateID));
                    OFDBrowserFunction.AddClickEventFunction(Defines.BUTTON_EDIT_SAVE_USER);
                    OFDBrowserFunction.AddClickEventFunction(Defines.BUTTON_EDIT_DELETE_USER);
                    return;
                case Defines.BUTTON_EDIT_SAVE_USER:
                    CallJSFunction("triggerOnUpdate", Defines.BUTTON_EDIT_SAVE_USER, "mail", "firstname", "surname", "username", "password", "adminlevel");
                    return;
                case Defines.BUTTON_EDIT_DELETE_USER:
                    if (Data.DynamicUpdateID <= 0) return;
                    if (Data.DynamicUpdateID == 1)
                    {
                        OFDBrowserFunction.WariningFormLabel("danger", "Bu kullanıcı silinemez!", "Bu kullanıcı yönetici olduğundan silinemiyor!!!");
                        return;
                    }
                    UserMenuFunctions.DeleteUserForm(Data.DynamicUpdateID);
                    return;
                case "addCategoryForm" when Data.CurrentURL == "index":
                    OFDBrowserFunction.InnerHTML("container", CategoryMenuFunctions.AddCategoryForm());
                    OFDBrowserFunction.AddClickEventFunction(Defines.BUTTON_ADD_CATEGORY);
                    Data.PageName = "Kategori Ekle";
                    return;
                case "addCategoryList" when Data.CurrentURL == "index":
                    OFDBrowserFunction.InnerHTML("container", GlobalProc.AddTable(Defines.TABLE_NAME, "#", "Kategori Adı", "#"));
                    OFDBrowserFunction.InnerHTML("tableContentBody", CategoryMenuFunctions.GetCategoryListForTable());
                    OFDBrowserFunction.AddClickEventFunction(Defines.BUTTON_EDIT_CATEGORY);
                    Data.PageName = "Kategori Listesi";
                    return;
                case Defines.BUTTON_ADD_CATEGORY:
                    CallJSFunction("triggerOnUpdate", Defines.BUTTON_ADD_CATEGORY, "categoryName");
                    return;
                case Defines.BUTTON_EDIT_CATEGORY:
                    Data.DynamicUpdateID = (int)jsonApi.value;
                    OFDBrowserFunction.InnerHTML("container", CategoryMenuFunctions.EditCategoryForm(Data.DynamicUpdateID));
                    OFDBrowserFunction.AddClickEventFunction(Defines.BUTTON_EDIT_SAVE_CATEGORY);
                    OFDBrowserFunction.AddClickEventFunction(Defines.BUTTON_EDIT_DELETE_CATEGORY);
                    return;
                case Defines.BUTTON_EDIT_SAVE_CATEGORY:
                    CallJSFunction("triggerOnUpdate", Defines.BUTTON_EDIT_SAVE_CATEGORY, "categoryName");
                    return;
                case Defines.BUTTON_EDIT_DELETE_CATEGORY:
                    if (Data.DynamicUpdateID <= 0) return;
                    CategoryMenuFunctions.DeleteCategoryForm(Data.DynamicUpdateID);
                    return;
                case "addStockForm" when Data.CurrentURL == "index":
                    OFDBrowserFunction.InnerHTML("container", StockMenuFunctions.AddStockForm());
                    OFDBrowserFunction.AddClickEventFunction(Defines.BUTTON_ADD_STOCK);
                    Data.PageName = "Stok > Ürün Ekle";
                    return;
                case "addStockList" when Data.CurrentURL == "index":
                    OFDBrowserFunction.InnerHTML("container", GlobalProc.AddTable(Defines.TABLE_NAME, "#", "Ürün Adı", "Ürün Markası", "Ürün Adedi", "Eklenme Tarihi", "#"));
                    OFDBrowserFunction.InnerHTML("tableContentBody", StockMenuFunctions.GetStockListForTable());
                    OFDBrowserFunction.AddClickEventFunction(Defines.BUTTON_EDIT_STOCK);
                    Data.PageName = "Stok > Ürün Listesi";
                    return;
                case Defines.BUTTON_ADD_STOCK:
                    CallJSFunction("triggerOnUpdate", Defines.BUTTON_ADD_STOCK, "stockName", "stockBrand", "buyPrice", "sellPrice", "categoryOptions", "stockCount");
                    return;
                case Defines.BUTTON_EDIT_STOCK:
                    Data.DynamicUpdateID = (int)jsonApi.value;
                    OFDBrowserFunction.InnerHTML("container", StockMenuFunctions.EditStockForm(Data.DynamicUpdateID));
                    OFDBrowserFunction.AddClickEventFunction(Defines.BUTTON_EDIT_SAVE_STOCK);
                    OFDBrowserFunction.AddClickEventFunction(Defines.BUTTON_EDIT_DELETE_STOCK);
                    return;
                case Defines.BUTTON_EDIT_SAVE_STOCK:
                    CallJSFunction("triggerOnUpdate", Defines.BUTTON_EDIT_SAVE_STOCK, "stockName", "stockBrand", "buyPrice", "sellPrice", "categoryOptions", "stockCount");
                    return;
                case Defines.BUTTON_EDIT_DELETE_STOCK:
                    if (Data.DynamicUpdateID <= 0) return;
                    StockMenuFunctions.DeleteStockForm(Data.DynamicUpdateID);
                    return;
            }
        }


        private static void OnBrowserFrameLoadEnd(object sender, FrameLoadEndEventArgs e)
        {
            if (e.Frame.IsMain)
            {
                Data.CurrentURL = e.Url.Substring(e.Url.IndexOf("HTML/") + 5).Replace(".html", "");
                switch(Data.CurrentURL)
                {
                    case "index":
                        string sidebarMenuSTR = "";
                        ArrayList events = new ArrayList();
                        OFDBrowserFunction.AddClickEventFunction(Defines.HOMEPAGE);
                        JSReaction(Defines.HOMEPAGE);
                        SidebarFunctions.UpdateSidebarVariables();
                        if(Data.Admin >= Defines.USER_MENU_ADMIN_LEVEL)
                        {
                            sidebarMenuSTR += SidebarFunctions.AddUserOptions();
                            events.Add("addUserForm");
                            events.Add("addUserList");
                        }
                        if(Data.Admin >= Defines.CATEGORY_MENU_ADMIN_LEVEL)
                        {
                            sidebarMenuSTR += SidebarFunctions.AddCategoryOptions();
                            events.Add("addCategoryForm");
                            events.Add("addCategoryList");
                        }
                        if(Data.Admin >= Defines.STOCK_MENU_ADMIN_LEVEL)
                        {
                            sidebarMenuSTR += SidebarFunctions.AddStockOptions();
                            events.Add("addStockForm");
                            events.Add("addStockList");
                        }
                        OFDBrowserFunction.InsertAdjacentHTMLBefore("sidebarMenu", sidebarMenuSTR);
                        foreach (string item in events)
                        {
                            OFDBrowserFunction.AddClickEventFunction(item);
                        }
                        break;
                }
            }
        }

    }
}
