using StockManagamentSystem.Modules.MySQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace StockManagamentSystem.Modules.Browser
{
	public class CategoryMenuFunctions
	{
		public static string AddCategoryForm()
		{
			return GlobalProc.ReplaceHTML(@"<div class='card card-primary'>
			  <div class='card-header'>
				<h3 class='card-title'>Kategori Ekle</h3>
			  </div>
			  <!-- /.card-header -->
			  <!-- form start -->
			  <div>
				<div class='card-body'>
				  <div class='form-group' id='warningformlabel'>
				  </div>
				  <div class='form-group'>
					<label for='category'>Kategori Adı</label>
					<input id='categoryName' type='input' class='form-control'  placeholder='Kategori Adı'>
				  </div>
				</div>
				<!-- /.card-body -->

				<div class='card-footer'>
				  <button class='btn btn-primary' id='" + Defines.BUTTON_ADD_CATEGORY + @"'>Kategori Oluştur</button>
				</div>
			  </div>
			</div>");
		}

		public static string GetCategoryListForTable() {
			List<Category> categoryData = DBContext.Instance.Category.ToList();
			string tableData = "";
			foreach (Category category in categoryData)
			{
				tableData += "<tr>" +
					 "<td>" + category.ID + "</td>" +
                     "<td>" + category.Name + "</td>" +
					 "<td><button type='button' id='" + Defines.BUTTON_EDIT_CATEGORY + "' value='" + category.ID + "' class='btn btn-primary btn-sm'>Düzenle</button></td>" + 
                    "</tr>";
			}
			return GlobalProc.ReplaceHTML(tableData);
		}

		public static string UpdateCategory(Category category)
		{
            Data.PageName = "Kategori Düzenleme / " + category.Name;
            return GlobalProc.ReplaceHTML(@"<div class='card card-primary'>
			  <div class='card-header'>
				<h3 class='card-title'>Kategori Düzenle</h3>
			  </div>
			  <!-- /.card-header -->
			  <!-- form start -->
			  <div>
				<div class='card-body'>
				  <div class='form-group' id='warningformlabel'>
				  </div>
				  <div class='form-group'>
					<label for='category'>Kategori Adı</label>
					<input id='categoryName' type='input' class='form-control' value='" + category.Name + @"'  placeholder='Kategori Adı'>
				  </div>
				</div>
				<!-- /.card-body -->

				<div class='card-footer'>
				  <button class='btn btn-primary' id='" + Defines.BUTTON_EDIT_SAVE_CATEGORY + @"'>Kategoriyi Düzenle</button>
				  <button class='btn btn-danger' id='" + Defines.BUTTON_EDIT_DELETE_CATEGORY + @"'>Kategoriyi Sil</button>
				</div>
			  </div>
			</div>");
        }

		public static void DeleteCategory(Category category)
		{
			DBContext.Instance.Remove(category);
			DBContext.Instance.SaveChanges();
		}

        public static string EditCategoryForm(int id)
        {
			Category category = DBContext.Instance.Category.Where(x => x.ID == id).FirstOrDefault();
			if (category == null) return "Bu kategori silinmiş.";
			return UpdateCategory(category);
        }

        public static string DeleteCategoryForm(int id)
        {
            Category category = DBContext.Instance.Category.Where(x => x.ID == id).FirstOrDefault();
            if (category == null) return "Bu kategori silinmiş.";
            DeleteCategory(category);
			OFDBrowser.JSReaction("addCategoryList");
            OFDBrowserFunction.WariningFormLabel("success", "Kategori silindi!", category.Name +  " adlı kategori başarılı bir şekilde silindi!");
            return "Kategori Silindi";
        }

		public static string GetCategoryOptions(int selectedID = Defines.INVALID_ID)
		{
			string catName = string.Empty;
			if(selectedID != Defines.INVALID_ID)
			{
				Category cat = DBContext.Instance.Category.Where(c => c.ID == selectedID).FirstOrDefault();
				if(cat != null) catName = @"<option value='" + selectedID + @"'>" + cat.Name + @"</option>";
            }
			List<Category> categories = DBContext.Instance.Category.ToList();
			string selectHTML = @"<select class='custom-select form-control-border border-width-2' id='categoryOptions' name='categoryOptions'>";
			selectHTML += catName;
            foreach (Category item in categories)
            {
				if (selectedID != Defines.INVALID_ID && selectedID == item.ID) continue;
				selectHTML += @" <option value='" + item.ID + @"'>" + item.Name + @"</option>";
            }
			selectHTML += "</select>";
            return GlobalProc.ReplaceHTML(selectHTML);
		}

		public static string GetCategoryName(int selectedID)
		{
            string catName = "NULL";
            if (selectedID != Defines.INVALID_ID)
            {
                Category cat = DBContext.Instance.Category.Where(c => c.ID == selectedID).FirstOrDefault();
				if (cat != null) catName = cat.Name;
            }
			return catName;
        }

    }
}