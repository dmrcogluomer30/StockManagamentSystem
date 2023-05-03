using Newtonsoft.Json;
using StockManagamentSystem.Modules.MySQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StockManagamentSystem.Modules.Browser
{
    public class OFDCallbacks
    {
        public static void OnUpdate(string name, string json)
        {
            try
            {
                dynamic jsonApi = JsonConvert.DeserializeObject("{" + json + "}");

                switch (name)
                {
                    case Defines.BUTTON_ADD_USER:
                        string mail = (string)jsonApi.mail,
                            firstname = (string)jsonApi.firstname,
                            surname = (string)jsonApi.surname,
                            username = (string)jsonApi.username,
                            password = (string)jsonApi.password;
                        byte adminlevel = (byte)jsonApi.adminlevel;
                        if (string.IsNullOrEmpty(mail))
                        {
                            OFDBrowserFunction.WariningFormLabel("danger", "Alanları kontrol edin!", "E-Posta adresi alanını boş bıraktınız, lütfen kontrol edin.");
                            return;
                        }
                        else if (string.IsNullOrEmpty(firstname))
                        {
                            OFDBrowserFunction.WariningFormLabel("danger", "Alanları kontrol edin!", "Ad alanını boş bıraktınız lütfen kontrol edin.");
                            return;
                        }
                        else if (string.IsNullOrEmpty(surname))
                        {
                            OFDBrowserFunction.WariningFormLabel("danger", "Alanları kontrol edin!", "Soyad alanını boş bıraktınız lütfen kontrol edin.");
                            return;
                        }
                        else if (string.IsNullOrEmpty(username))
                        {
                            OFDBrowserFunction.WariningFormLabel("danger", "Alanları kontrol edin!", "Kullanıcı adı alanını boş bıraktınız lütfen kontrol edin.");
                            return;
                        }
                        else if (string.IsNullOrEmpty(password))
                        {
                            OFDBrowserFunction.WariningFormLabel("danger", "Alanları kontrol edin!", "Şifre alanını boş bıraktınız lütfen kontrol edin.");
                            return;
                        }
                        else if (!GlobalProc.IsValidEmail(mail))
                        {
                            OFDBrowserFunction.WariningFormLabel("danger", "Hatalı E-Posta!", "Lütfen geçerli bir mail adresi giriniz.");
                            return;
                        }
                        else if (DBContext.Instance.Users.Where(u => u.UserName == username).Count() > 0)
                        {
                            OFDBrowserFunction.WariningFormLabel("danger", "Kullanıcı mevcut!", username + " ismiyle zaten bir kullanıcı oluşturulmuş, lütfen başka kullanıcı adı deneyin veya " + username + " kullanıcısının bilgilerinde değişiklik yapın.");
                            return;
                        }
                        Users newUser = new Users();
                        newUser.Created = DateTime.Now;
                        newUser.Name = firstname;
                        newUser.Surname = surname;
                        newUser.UserName = username;
                        newUser.Password = GlobalProc.ConvertToSHA256(password);
                        newUser.Mail = mail;
                        newUser.Admin = adminlevel;
                        DBContext.Instance.Users.Add(newUser);
                        DBContext.Instance.SaveChanges();
                        OFDBrowserFunction.WariningFormLabel("success", "Kullanıcı eklendi!", username + " adlı kullanıcı başarılı bir şekilde eklendi!");
                        break;
                    case Defines.BUTTON_EDIT_SAVE_USER:
                        string updatedmail = (string)jsonApi.mail,
                            updatedfirstname = (string)jsonApi.firstname,
                            updatedsurname = (string)jsonApi.surname,
                            updatedusername = (string)jsonApi.username,
                            updatedpassword = (string)jsonApi.password;
                        byte updatedadminlevel = (byte)jsonApi.adminlevel;
                        if (string.IsNullOrEmpty(updatedmail))
                        {
                            OFDBrowserFunction.WariningFormLabel("danger", "Alanları kontrol edin!", "E-Posta adresi alanını boş bıraktınız, lütfen kontrol edin.");
                            return;
                        }
                        else if (string.IsNullOrEmpty(updatedfirstname))
                        {
                            OFDBrowserFunction.WariningFormLabel("danger", "Alanları kontrol edin!", "Ad alanını boş bıraktınız lütfen kontrol edin.");
                            return;
                        }
                        else if (string.IsNullOrEmpty(updatedsurname))
                        {
                            OFDBrowserFunction.WariningFormLabel("danger", "Alanları kontrol edin!", "Soyad alanını boş bıraktınız lütfen kontrol edin.");
                            return;
                        }
                        else if (string.IsNullOrEmpty(updatedusername))
                        {
                            OFDBrowserFunction.WariningFormLabel("danger", "Alanları kontrol edin!", "Kullanıcı adı alanını boş bıraktınız lütfen kontrol edin.");
                            return;
                        }
                        else if (!GlobalProc.IsValidEmail(updatedmail))
                        {
                            OFDBrowserFunction.WariningFormLabel("danger", "Hatalı E-Posta!", "Lütfen geçerli bir mail adresi giriniz.");
                            return;
                        }
                        Users updatedUser = DBContext.Instance.Users.Where(u => u.ID == Data.DynamicUpdateID).FirstOrDefault();
                        if (updatedUser != null)
                        {
                            if (DBContext.Instance.Users.Where(u => u.UserName == updatedusername && u.ID != Data.DynamicUpdateID).Count() > 0)
                            {
                                OFDBrowserFunction.WariningFormLabel("danger", "Kullanıcı mevcut!", updatedusername + " ismiyle zaten bir kullanıcı oluşturulmuş, lütfen başka kullanıcı adı deneyin veya " + updatedusername + " kullanıcısının bilgilerinde değişiklik yapın.");
                                return;
                            }
                            updatedUser.Mail = updatedmail;
                            updatedUser.Name = updatedfirstname;
                            updatedUser.Surname = updatedsurname;
                            updatedUser.UserName = updatedusername;
                            if (!string.IsNullOrEmpty(updatedpassword)) updatedUser.Password = GlobalProc.ConvertToSHA256(updatedpassword);
                            updatedUser.Admin = updatedadminlevel;
                            DBContext.Instance.SaveChanges();
                            OFDBrowserFunction.InnerHTML("container", UserMenuFunctions.EditUserForm(updatedusername));
                            OFDBrowserFunction.WariningFormLabel("success", "Kullanıcı düzenlendi!", "Kullanıcı başarılı bir şekilde düzenlendi!");
                            OFDBrowserFunction.AddClickEventFunction(Defines.BUTTON_EDIT_SAVE_USER);
                            OFDBrowserFunction.AddClickEventFunction(Defines.BUTTON_EDIT_DELETE_USER);
                        }
                        else
                        {
                            OFDBrowserFunction.WariningFormLabel("danger", "Kullanıcı düzenlenemedi!", updatedusername + " kullanıcısının bilgileri güncellenemedi, lütfen tekrar deneyin.");
                            return;
                        }
                        break;
                    case Defines.BUTTON_ADD_CATEGORY:
                        string categoryName = (string)jsonApi.categoryName;
                        if (string.IsNullOrEmpty(categoryName)) 
                        {
                            OFDBrowserFunction.WariningFormLabel("danger", "Kategori Adını kontrol edin!", "Kategori adını boş bıraktınız lütfen boş alanı doldurun.");
                            return;
                        }
                        Category newCategory = new Category();
                        newCategory.Name = categoryName;
                        DBContext.Instance.Category.Add(newCategory);
                        DBContext.Instance.SaveChanges();
                        OFDBrowserFunction.WariningFormLabel("success", "Kategori eklendi!", categoryName + " adlı kategori başarılı bir şekilde eklendi!");
                        break;
                    case Defines.BUTTON_EDIT_SAVE_CATEGORY:
                        string categoryNameEdit = (string)jsonApi.categoryName;
                        if (string.IsNullOrEmpty(categoryNameEdit))
                        {
                            OFDBrowserFunction.WariningFormLabel("danger", "Kategori Adını kontrol edin!", "Kategori adını boş bıraktınız lütfen boş alanı doldurun.");
                            return;
                        }
                        Category updateCategory = DBContext.Instance.Category.Where(u => u.ID == Data.DynamicUpdateID).FirstOrDefault();
                        if (updateCategory != null)
                        {
                            if (DBContext.Instance.Category.Where(u => u.Name == categoryNameEdit && u.ID != Data.DynamicUpdateID).Count() > 0)
                            {
                                OFDBrowserFunction.WariningFormLabel("danger", "Kategori mevcut!", categoryNameEdit + " ismiyle zaten bir kategori oluşturulmuş, lütfen başka kategori adı deneyin veya " + categoryNameEdit + " kategorisinmin bilgilerinde değişiklik yapın.");
                                return;
                            }
                            updateCategory.Name = categoryNameEdit;
                            DBContext.Instance.SaveChanges();
                            OFDBrowserFunction.InnerHTML("container", CategoryMenuFunctions.EditCategoryForm(Data.DynamicUpdateID));
                            OFDBrowserFunction.WariningFormLabel("success", "Kategori düzenlendi!", "Kategori başarılı bir şekilde düzenlendi!");
                            OFDBrowserFunction.AddClickEventFunction(Defines.BUTTON_EDIT_SAVE_CATEGORY);
                            OFDBrowserFunction.AddClickEventFunction(Defines.BUTTON_EDIT_DELETE_CATEGORY);
                        }
                        else
                        {
                            OFDBrowserFunction.WariningFormLabel("danger", "Kategori düzenlenemedi!", categoryNameEdit + " isimli kategorinin bilgileri güncellenemedi, lütfen tekrar deneyin.");
                            return;
                        }
                        break;
                    case Defines.BUTTON_ADD_STOCK:
                        string stockName = (string)jsonApi.stockName,
                            stockBrand = (string)jsonApi.stockBrand,
                            buyPrice = (string)jsonApi.buyPrice,
                            sellPrice = (string)jsonApi.sellPrice,
                            categoryOptions = (string)jsonApi.categoryOptions,
                            stockCount = (string)jsonApi.stockCount;
                        if(string.IsNullOrEmpty(stockName))
                        {
                            OFDBrowserFunction.WariningFormLabel("danger", "Alanları kontrol edin!", "Ürün adı alanını boş bıraktınız, lütfen kontrol edin.");
                            return;
                        }
                        else if (string.IsNullOrEmpty(stockBrand))
                        {
                            OFDBrowserFunction.WariningFormLabel("danger", "Alanları kontrol edin!", "Ürün marka alanını boş bıraktınız, lütfen kontrol edin.");
                            return;
                        }
                        else if (string.IsNullOrEmpty(buyPrice))
                        {
                            OFDBrowserFunction.WariningFormLabel("danger", "Alanları kontrol edin!", "Alış fiyatı alanını boş bıraktınız, lütfen kontrol edin.");
                            return;
                        }
                        else if(!GlobalProc.isNumeric(buyPrice))
                        {
                            OFDBrowserFunction.WariningFormLabel("danger", "Alanları kontrol edin!", "Alış fiyatı alanı sayı içermelidir, lütfen kontrol edin.");
                            return;
                        }
                        else if (string.IsNullOrEmpty(sellPrice))
                        {
                            OFDBrowserFunction.WariningFormLabel("danger", "Alanları kontrol edin!", "Satış fiyatı alanını boş bıraktınız, lütfen kontrol edin.");
                            return;
                        }
                        else if (!GlobalProc.isNumeric(sellPrice))
                        {
                            OFDBrowserFunction.WariningFormLabel("danger", "Alanları kontrol edin!", "Satış fiyatı alanı sayı içermelidir, lütfen kontrol edin.");
                            return;
                        }
                        else if (string.IsNullOrEmpty(categoryOptions))
                        {
                            OFDBrowserFunction.WariningFormLabel("danger", "Alanları kontrol edin!", "Ürün kategori alanını boş bıraktınız, lütfen kontrol edin.");
                            return;
                        }
                        else if (string.IsNullOrEmpty(stockCount))
                        {
                            OFDBrowserFunction.WariningFormLabel("danger", "Alanları kontrol edin!", "Ürün adedi alanını boş bıraktınız, lütfen kontrol edin.");
                            return;
                        }
                        else if (!GlobalProc.isNumeric(stockCount))
                        {
                            OFDBrowserFunction.WariningFormLabel("danger", "Alanları kontrol edin!", "Ürün adedi alanı sayı içermelidir, lütfen kontrol edin.");
                            return;
                        }
                        else if(decimal.Parse(sellPrice) < decimal.Parse(buyPrice))
                        {
                            OFDBrowserFunction.WariningFormLabel("danger", "Alanları kontrol edin!", "Ürünün satış fiyatı alış fiyatından küçük olamaz, lütfen kontrol edin.");
                            return;
                        }
                        else if(decimal.Parse(sellPrice) <= 0)
                        {
                            OFDBrowserFunction.WariningFormLabel("danger", "Alanları kontrol edin!", "Ürünün satış fiyatı sıfırdan küçük ve eşit olamaz, lütfen kontrol edin.");
                            return;
                        }
                        else if (decimal.Parse(buyPrice) < 0)
                        {
                            OFDBrowserFunction.WariningFormLabel("danger", "Alanları kontrol edin!", "Ürünün alış fiyatı sıfırdan küçük olamaz, lütfen kontrol edin.");
                            return;
                        }
                        Stocks newProduct = new Stocks();
                        newProduct.Name = stockName;
                        newProduct.Brand = stockBrand;
                        newProduct.BuyPrice = decimal.Parse(buyPrice);
                        newProduct.SellPrice = decimal.Parse(sellPrice);
                        newProduct.CategoryID = int.Parse(categoryOptions);
                        newProduct.Count = int.Parse(stockCount);
                        newProduct.CreatedDate = DateTime.Now;
                        DBContext.Instance.Stocks.Add(newProduct);
                        DBContext.Instance.SaveChanges();
                        OFDBrowserFunction.WariningFormLabel("success", "Ürün eklendi!", stockName + " adlı ürün başarılı bir şekilde eklendi!");
                        TelegramAPI.SendMessage(Data.UserName + "(" + Data.Name + " " + Data.Surname + " - #" + Data.ID + ") adlı kullanıcı bir ürün ekledi." + "\n\nÜrün ID: " + newProduct.ID + "\nÜrün Markası: " + newProduct.Brand + "\nAlış Fiyatı: " + newProduct.BuyPrice + "\nSatış Fiyatı: " + newProduct.SellPrice + "\nÜrün Kategorisi: " + CategoryMenuFunctions.GetCategoryName(newProduct.CategoryID) + "\nÜrün Adedi: " + newProduct.Count);
                        break;
                    case Defines.BUTTON_EDIT_SAVE_STOCK:
                        string updatedstockName = (string)jsonApi.stockName,
                            updatedstockBrand = (string)jsonApi.stockBrand,
                            updatedbuyPrice = (string)jsonApi.buyPrice,
                            updatedsellPrice = (string)jsonApi.sellPrice,
                            updatedcategoryOptions = (string)jsonApi.categoryOptions,
                            updatedstockCount = (string)jsonApi.stockCount;
                        if (string.IsNullOrEmpty(updatedstockName))
                        {
                            OFDBrowserFunction.WariningFormLabel("danger", "Alanları kontrol edin!", "Ürün adı alanını boş bıraktınız, lütfen kontrol edin.");
                            return;
                        }
                        else if (string.IsNullOrEmpty(updatedstockBrand))
                        {
                            OFDBrowserFunction.WariningFormLabel("danger", "Alanları kontrol edin!", "Ürün marka alanını boş bıraktınız, lütfen kontrol edin.");
                            return;
                        }
                        else if (string.IsNullOrEmpty(updatedbuyPrice))
                        {
                            OFDBrowserFunction.WariningFormLabel("danger", "Alanları kontrol edin!", "Alış fiyatı alanını boş bıraktınız, lütfen kontrol edin.");
                            return;
                        }
                        else if (!GlobalProc.isNumeric(updatedbuyPrice))
                        {
                            OFDBrowserFunction.WariningFormLabel("danger", "Alanları kontrol edin!", "Alış fiyatı alanı sayı içermelidir, lütfen kontrol edin.");
                            return;
                        }
                        else if (string.IsNullOrEmpty(updatedsellPrice))
                        {
                            OFDBrowserFunction.WariningFormLabel("danger", "Alanları kontrol edin!", "Satış fiyatı alanını boş bıraktınız, lütfen kontrol edin.");
                            return;
                        }
                        else if (!GlobalProc.isNumeric(updatedsellPrice))
                        {
                            OFDBrowserFunction.WariningFormLabel("danger", "Alanları kontrol edin!", "Satış fiyatı alanı sayı içermelidir, lütfen kontrol edin.");
                            return;
                        }
                        else if (string.IsNullOrEmpty(updatedcategoryOptions))
                        {
                            OFDBrowserFunction.WariningFormLabel("danger", "Alanları kontrol edin!", "Ürün kategori alanını boş bıraktınız, lütfen kontrol edin.");
                            return;
                        }
                        else if (string.IsNullOrEmpty(updatedstockCount))
                        {
                            OFDBrowserFunction.WariningFormLabel("danger", "Alanları kontrol edin!", "Ürün adedi alanını boş bıraktınız, lütfen kontrol edin.");
                            return;
                        }
                        else if (!GlobalProc.isNumeric(updatedstockCount))
                        {
                            OFDBrowserFunction.WariningFormLabel("danger", "Alanları kontrol edin!", "Ürün adedi alanı sayı içermelidir, lütfen kontrol edin.");
                            return;
                        }
                        else if (decimal.Parse(updatedsellPrice) < decimal.Parse(updatedbuyPrice))
                        {
                            OFDBrowserFunction.WariningFormLabel("danger", "Alanları kontrol edin!", "Ürünün satış fiyatı alış fiyatından küçük olamaz, lütfen kontrol edin.");
                            return;
                        }
                        else if (decimal.Parse(updatedsellPrice) <= 0)
                        {
                            OFDBrowserFunction.WariningFormLabel("danger", "Alanları kontrol edin!", "Ürünün satış fiyatı sıfırdan küçük ve eşit olamaz, lütfen kontrol edin.");
                            return;
                        }
                        else if (decimal.Parse(updatedbuyPrice) < 0)
                        {
                            OFDBrowserFunction.WariningFormLabel("danger", "Alanları kontrol edin!", "Ürünün alış fiyatı sıfırdan küçük olamaz, lütfen kontrol edin.");
                            return;
                        }
                        Stocks stocks = DBContext.Instance.Stocks.Where(x => x.ID == Data.DynamicUpdateID).FirstOrDefault();
                        if (stocks != null)
                        {
                            string telegramMSG = Data.UserName + "(" + Data.Name + " " + Data.Surname + " - #" + Data.ID + ") adlı kullanıcı bir ürününün bilgilerini güncelledi.";
                            telegramMSG += "\n\nÜrün ID: " + stocks.ID;
                            telegramMSG += "\nÜrün Adı: " + stocks.Name + " -> " + updatedstockName;
                            telegramMSG += "\nÜrün Markası: " + stocks.Brand + " -> " + updatedstockBrand;
                            telegramMSG += "\nAlış Fiyatı: " + stocks.BuyPrice + " -> " + updatedbuyPrice;
                            telegramMSG += "\nSatış Fiyatı: " + stocks.SellPrice + " -> " + updatedsellPrice;
                            telegramMSG += "\nÜrün Kategorisi: " + CategoryMenuFunctions.GetCategoryName(stocks.CategoryID) + " -> " + CategoryMenuFunctions.GetCategoryName(int.Parse(updatedcategoryOptions));
                            telegramMSG += "\nÜrün Adedi: " + stocks.Count + " -> " + updatedstockCount;
                            stocks.Name = updatedstockName;
                            stocks.Brand = updatedstockBrand;
                            stocks.BuyPrice = decimal.Parse(updatedbuyPrice);
                            stocks.SellPrice = decimal.Parse(updatedsellPrice);
                            stocks.CategoryID = int.Parse(updatedcategoryOptions);
                            stocks.Count = int.Parse(updatedstockCount);
                            DBContext.Instance.SaveChanges();
                            OFDBrowserFunction.InnerHTML("container", StockMenuFunctions.EditStockForm(Data.DynamicUpdateID));
                            OFDBrowserFunction.WariningFormLabel("success", "Ürün düzenlendi!", "Ürün başarılı bir şekilde düzenlendi!");
                            OFDBrowserFunction.AddClickEventFunction(Defines.BUTTON_EDIT_SAVE_STOCK);
                            OFDBrowserFunction.AddClickEventFunction(Defines.BUTTON_EDIT_DELETE_STOCK);
                            TelegramAPI.SendMessage(telegramMSG);
                        }
                        else
                        {
                            OFDBrowserFunction.WariningFormLabel("danger", "Ürün düzenlenemedi!", updatedstockName + " isimli ürünün bilgileri güncellenemedi, lütfen tekrar deneyin.");
                            return;
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bilinmeyen bir hata meydana geldi, yaptığınız işlemi tekrar deneyin. Bu hata tekrarlanmaya devam ederse geliştiriciniz ile iletişime geçmelisiniz. Hata Detayları: " + ex.Message, "Hata #93841", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
    }
}
