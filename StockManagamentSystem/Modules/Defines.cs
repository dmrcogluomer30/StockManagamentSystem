using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManagamentSystem.Modules
{
    public class Defines
    {
        public const string HOMEPAGE = "homepage";


        public const int MANAGEMENT_LEVEL = 5;
        public const int PERSONEL_LEVEL = 3;

        public const int USER_MENU_ADMIN_LEVEL = MANAGEMENT_LEVEL;
        public const int CATEGORY_MENU_ADMIN_LEVEL = MANAGEMENT_LEVEL;
        public const int STOCK_MENU_ADMIN_LEVEL = PERSONEL_LEVEL;
        public const int INVALID_ID = -1;

        public const string TABLE_NAME = "DTDRMC";
        public const string BUTTON_ADD_USER = "buttonAddUser";
        public const string BUTTON_LIST_USER = "buttonListUser";
        public const string BUTTON_EDIT_USER = "buttonEditUser";
        public const string BUTTON_EDIT_SAVE_USER = "buttonEditSaveUser";
        public const string BUTTON_EDIT_DELETE_USER = "buttonEditDeleteUser";

        public const string BUTTON_ADD_CATEGORY = "buttonAddCategory";
        public const string BUTTON_EDIT_CATEGORY = "buttonEditCategory";
        public const string BUTTON_EDIT_SAVE_CATEGORY = "buttonEditSaveCategory";
        public const string BUTTON_EDIT_DELETE_CATEGORY = "buttonEditDeleteCategory";

        public const string BUTTON_ADD_STOCK = "buttonAddStock";
        public const string BUTTON_EDIT_STOCK = "buttonEditStock";
        public const string BUTTON_EDIT_SAVE_STOCK = "buttonEditSaveStock";
        public const string BUTTON_EDIT_DELETE_STOCK = "buttonEditDeleteStock";
    }
}
