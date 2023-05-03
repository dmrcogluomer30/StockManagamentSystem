using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManagamentSystem.Modules.Browser
{
    public class SidebarFunctions
    {
        public static void UpdateSidebarVariables()
        {
            string scriptCode = "$('#userNameSurname').html('" + Data.Name + " " + Data.Surname.ToUpper() + "');";
            OFDBrowser.CallJS(scriptCode);
        }

        public static string AddUserOptions()
        {
            return GlobalProc.ReplaceHTML($@"
				<li class='nav-item'>
				<a class='nav-link'>
				  <i class='nav-icon fas fa-user'></i>
				  <p>
					Kullanıcı İşlemleri
					<i class='right fas fa-angle-left'></i>
				  </p>
				</a>
				<ul class='nav nav-treeview'>
				  <li class='nav-item'>
					<a id='addUserForm' class='nav-link'>
					  <i class='fas fa-user-plus nav-icon'></i>
					  <p>Kullanıcı Ekle</p>
					</a>
				  </li>
				  <li class='nav-item'>
					<a id='addUserList' class='nav-link'>
					  <i class='fas fa-user-cog nav-icon'></i>
					  <p>Kullanıcı Listesi</p>
					</a>
				  </li>
				</ul>
				</li>");
        }

		public static string AddCategoryOptions()
		{
            return GlobalProc.ReplaceHTML($@"
				<li class='nav-item'>
				<a class='nav-link'>
				  <i class='nav-icon fas fa-list'></i>
				  <p>
					Kategori İşlemleri
					<i class='right fas fa-angle-left'></i>
				  </p>
				</a>
				<ul class='nav nav-treeview'>
				  <li class='nav-item'>
					<a id='addCategoryForm' class='nav-link'>
					  <i class='fas fa-list-ol nav-icon'></i>
					  <p>Kategori Ekle</p>
					</a>
				  </li>
				  <li class='nav-item'>
					<a id='addCategoryList' class='nav-link'>
					  <i class='fas fa-list-ul nav-icon'></i>
					  <p>Kategori Listesi</p>
					</a>
				  </li>
				</ul>
				</li>");
        }

        public static string AddStockOptions()
        {
            return GlobalProc.ReplaceHTML($@"
				<li class='nav-item'>
				<a class='nav-link'>
				  <i class='nav-icon fas fa-tag'></i>
				  <p>
					Stok İşlemleri
					<i class='right fas fa-angle-left'></i>
				  </p>
				</a>
				<ul class='nav nav-treeview'>
				  <li class='nav-item'>
					<a id='addStockForm' class='nav-link'>
					  <i class='fas fa-plus nav-icon'></i>
					  <p>Ürün Ekle</p>
					</a>
				  </li>
				  <li class='nav-item'>
					<a id='addStockList' class='nav-link'>
					  <i class='fas fa-clipboard-list nav-icon'></i>
					  <p>Ürün Listesi</p>
					</a>
				  </li>
				</ul>
				</li>");
        }

    }
}
