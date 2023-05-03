using StockManagamentSystem.Modules.MySQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManagamentSystem.Modules.Browser
{
    public class StockMenuFunctions
    {
        public static string AddStockForm()
        {
            return GlobalProc.ReplaceHTML(@"<div class='card card-primary'>
			  <div class='card-header'>
				<h3 class='card-title'>Stoğa Ürün Ekle</h3>
			  </div>
			  <!-- /.card-header -->
			  <!-- form start -->
			  <div>
				<div class='card-body'>
				  <div class='form-group' id='warningformlabel'>
				  </div>
				  <div class='form-group'>
					<label for='category'>Ürün Adı</label>
					<input id='stockName' type='input' class='form-control'  placeholder='Ürün Adı'>
				  </div>
				<div class='form-group'>
					<label for='category'>Ürün Markası</label>
					<input id='stockBrand' type='input' class='form-control'  placeholder='Ürün Markası'>
				  </div>
				<div class='form-group'>
					<label for='category'>Alış Fiyatı</label>
					<input id='buyPrice' type='input' class='form-control'  placeholder='Alış Fiyatı'>
				  </div>
				<div class='form-group'>
					<label for='category'>Satış Fiyatı</label>
					<input id='sellPrice' type='input' class='form-control'  placeholder='Satış Fiyatı'>
				  </div>
				<div class='form-group'>
					<label for='category'>Ürün Kategorisi</label>
					" + CategoryMenuFunctions.GetCategoryOptions() + @"
				  </div>
				<div class='form-group'>
					<label for='category'>Ürün Adedi</label>
					<input id='stockCount' type='input' class='form-control'  placeholder='Ürün Adedi'>
				  </div>
				</div>
				<!-- /.card-body -->

				<div class='card-footer'>
				  <button class='btn btn-primary' id='" + Defines.BUTTON_ADD_STOCK + @"'>Ürün Ekle</button>
				</div>
			  </div>
			</div>");
        }

        public static string GetStockListForTable()
        {
            List<Stocks> stocksData = DBContext.Instance.Stocks.ToList();
            string tableData = "";
            foreach (Stocks stock in stocksData)
            {
                tableData += "<tr>" +
                     "<td>" + stock.ID + "</td>" +
                     "<td>" + stock.Name + "</td>" +
                     "<td>" + stock.Brand + "</td>" +
                     "<td>" + stock.Count + "</td>" +
                     "<td>" + stock.CreatedDate + "</td>" +
                     "<td><button type='button' id='" + Defines.BUTTON_EDIT_STOCK + "' value='" + stock.ID + "' class='btn btn-primary btn-sm'>Düzenle</button></td>" +
                    "</tr>";
            }
            return GlobalProc.ReplaceHTML(tableData);
        }

        public static string UpdateStock(Stocks stock)
        {
            Data.PageName = "Stok Düzenleme / " + stock.Name;
            return GlobalProc.ReplaceHTML(@"<div class='card card-primary'>
			  <div class='card-header'>
				<h3 class='card-title'>Stoktaki Ürünü Düzenle</h3>
			  </div>
			  <!-- /.card-header -->
			  <!-- form start -->
			  <div>
				<div class='card-body'>
				  <div class='form-group' id='warningformlabel'>
				  </div>
				  <div class='form-group'>
					<label for='category'>Ürün Adı</label>
					<input id='stockName' type='input' class='form-control' value='" + stock.Name + @"'  placeholder='Ürün Adı'>
				  </div>
				  <div class='form-group'>
					<label for='category'>Ürün Markası</label>
					<input id='stockBrand' type='input' class='form-control' value='" + stock.Brand + @"'  placeholder='Ürün Markası'>
				  </div>
				  <div class='form-group'>
					<label for='category'>Alış Fiyatı</label>
					<input id='buyPrice' type='input' class='form-control' value='" + stock.BuyPrice + @"'  placeholder='Alış Fiyatı'>
				  </div>
				  <div class='form-group'>
					<label for='category'>Satış Fiyatı</label>
					<input id='sellPrice' type='input' class='form-control' value='" + stock.SellPrice + @"'  placeholder='Satış Fiyatı'>
				  </div>
				  <div class='form-group'>
					<label for='category'>Kategori Adı</label>
					" + CategoryMenuFunctions.GetCategoryOptions(stock.CategoryID) + @"
				  </div>
				  <div class='form-group'>
					<label for='category'>Ürün Adedi</label>
					<input id='stockCount' type='input' class='form-control' value='" + stock.Count + @"'  placeholder='Ürün Adedi'>
				  </div>
				</div>
				<!-- /.card-body -->

				<div class='card-footer'>
				  <button class='btn btn-primary' id='" + Defines.BUTTON_EDIT_SAVE_STOCK + @"'>Ürünü Düzenle</button>
				  <button class='btn btn-danger' id='" + Defines.BUTTON_EDIT_DELETE_STOCK + @"'>Ürünü Sil</button>
				</div>
			  </div>
			</div>");
        }

        public static void DeleteStock(Stocks stock)
        {
            DBContext.Instance.Remove(stock);
            DBContext.Instance.SaveChanges();
        }

        public static string EditStockForm(int id)
        {
            Stocks stock = DBContext.Instance.Stocks.Where(x => x.ID == id).FirstOrDefault();
            if (stock == null) return "Bu ürün silinmiş.";
            return UpdateStock(stock);
        }

        public static string DeleteStockForm(int id)
        {
            Stocks stock = DBContext.Instance.Stocks.Where(x => x.ID == id).FirstOrDefault();
            if (stock == null) return "Bu ürün silinmiş.";
            DeleteStock(stock);
            OFDBrowser.JSReaction("addStockList");
            OFDBrowserFunction.WariningFormLabel("success", "Ürün silindi!", stock.Name + " adlı ürün başarılı bir şekilde silindi!");
			TelegramAPI.SendMessage(Data.UserName + "(" + Data.Name + " " + Data.Surname + " - #" + Data.ID + ") adlı kullanıcı bir ürün sildi." + "\n\nÜrün ID:" + stock.ID + "\nÜrün Adı:" + stock.Name + "\nÜrün Markası:" + stock.Brand + "\nAlış Fiyatı:" + stock.BuyPrice + "\nSatış Fiyatı:" + stock.SellPrice + "\nÜrün Kategorisi: " + CategoryMenuFunctions.GetCategoryName(stock.CategoryID) + "\nÜrün Adedi:" + stock.Count);
            return "Stok Silindi";
        }

    }
}