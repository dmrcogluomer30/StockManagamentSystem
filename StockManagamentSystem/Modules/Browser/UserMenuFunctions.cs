using StockManagamentSystem.Modules.MySQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManagamentSystem.Modules.Browser
{
	public class UserMenuFunctions
	{
		public static string AddUserForm()
		{
			return GlobalProc.ReplaceHTML(@"<div class='card card-primary'>
			  <div class='card-header'>
				<h3 class='card-title'>Kullanıcı Ekle</h3>
			  </div>
			  <!-- /.card-header -->
			  <!-- form start -->
			  <div>
				<div class='card-body'>
				  <div class='form-group' id='warningformlabel'>
				  </div>
				  <div class='form-group'>
					<label for='mail'>E Posta Adresi</label>
					<input id='mail' type='email' class='form-control'  placeholder='E Posta adresi'>
				  </div>
				  <div class='form-group'>
					<label for='firstname'>Ad</label>
					<input id='firstname' type='text' class='form-control'  placeholder='Ömer Faruk'>
				  </div>
				  <div class='form-group'>
					<label for='surname'>Soyad</label>
					<input id='surname' type='text' class='form-control'  placeholder='DEMİRCİOĞLU'>
				  </div>
				  <div class='form-group'>
					<label for='username'>Kullanıcı Adı</label>
					<input id='username' type='text' class='form-control'  placeholder='dmrcogluomer'>
				  </div>
				  <div class='form-group'>
					<label for='password'>Şifre</label>
					<input id='password' type='password' class='form-control' placeholder='Şifre'>
				  </div>
					<div class='form-group'>
						<label for='adminlevel'>Yetki seviyesi</label>
						<select class='custom-select form-control-border border-width-2' id='adminlevel' name='adminlevel'>
							<option value='0' selected>KULLANICI</option>
							<option value='1'>YETKİLİ</option>
							<option value='5'>YÖNETİCİ</option>
						</select>
					</div>
				</div>
				<!-- /.card-body -->

				<div class='card-footer'>
				  <button class='btn btn-primary' id='" + Defines.BUTTON_ADD_USER + @"'>Kullanıcı Oluştur</button>
				</div>
			  </div>
			</div>");
		}

		public static string GetUserListForTable() {
			List<Users> userData = DBContext.Instance.Users.ToList();
			string tableData = "";
			foreach (Users user in userData)
			{
				tableData += "<tr>" +
					 "<td>" + user.UserName + "</td>" +
                     "<td>" + user.Name + "</td>" +
                     "<td>" + user.Surname + "</td>" +
                     "<td>" + user.Mail + "</td>" +
					 "<td><button type='button' id='" + Defines.BUTTON_EDIT_USER + "' value='" + user.ID + "' class='btn btn-primary btn-sm'>Düzenle</button></td>" + 
                    "</tr>";
			}
			return GlobalProc.ReplaceHTML(tableData);
		}

		public static string UpdateUser(Users user)
		{
            Data.PageName = "Kullanıcı Düzenleme / " + user.UserName;
            return GlobalProc.ReplaceHTML(@"<div class='card card-primary'>
			  <div class='card-header'>
				<h3 class='card-title'>Kullanıcı Düzenle</h3>
			  </div>
			  <!-- /.card-header -->
			  <!-- form start -->
			  <div>
				<div class='card-body'>
				  <div class='form-group' id='warningformlabel'>
				  </div>
				  <div class='form-group'>
					<label for='mail'>E Posta Adresi</label>
					<input id='mail' type='email' class='form-control' value='" + user.Mail + @"'  placeholder='E Posta adresi'>
				  </div>
				  <div class='form-group'>
					<label for='firstname'>Ad</label>
					<input id='firstname' type='text' class='form-control' value='" + user.Name + @"'  placeholder='Ömer Faruk'>
				  </div>
				  <div class='form-group'>
					<label for='surname'>Soyad</label>
					<input id='surname' type='text' class='form-control' value='" + user.Surname + @"' placeholder='DEMİRCİOĞLU'>
				  </div>
				  <div class='form-group'>
					<label for='username'>Kullanıcı Adı</label>
					<input id='username' type='text' class='form-control' value='" + user.UserName + @"' placeholder='dmrcogluomer'>
				  </div>
				  <div class='form-group'>
					<label for='password'>Şifre</label>
					<input id='password' type='password' class='form-control' placeholder='Değiştirmek istemiyorsanız boş bırakın.'>
				  </div>
					<div class='form-group'>
						<label for='adminlevel'>Yetki seviyesi</label>
						<select class='custom-select form-control-border border-width-2' id='adminlevel' name='adminlevel'>
							<option value='0' " + (user.Admin == 0 ? "selected" : "") + @">" + GlobalProc.GetAdminLevelName(0) + @"</option>
							<option value='1' " + (user.Admin == 1 ? "selected" : "") + @">" + GlobalProc.GetAdminLevelName(1) + @"</option>
							<option value='5' " + (user.Admin == 5 ? "selected" : "") + @">" + GlobalProc.GetAdminLevelName(5) + @"</option>
						</select>
					</div>
				</div>
				<!-- /.card-body -->

				<div class='card-footer'>
				  <button class='btn btn-primary' id='" + Defines.BUTTON_EDIT_SAVE_USER + @"'>Kullanıcıyı Düzenle</button>
				  <button class='btn btn-danger' id='" + Defines.BUTTON_EDIT_DELETE_USER + @"'>Kullanıcıyı Sil</button>
				</div>
			  </div>
			</div>");
        }

		public static void DeleteUser(Users user)
		{
			DBContext.Instance.Remove(user);
			DBContext.Instance.SaveChanges();
		}

        public static string EditUserForm(int id)
        {
			Users user = DBContext.Instance.Users.Where(x => x.ID == id).FirstOrDefault();
			if (user == null) return "Bu kullanıcı silinmiş.";
			return UpdateUser(user);
        }

        public static string EditUserForm(string username)
        {
            Users user = DBContext.Instance.Users.Where(x => x.UserName == username).FirstOrDefault();
            if (user == null) return "Bu kullanıcı silinmiş.";
            return UpdateUser(user);
        }

        public static string DeleteUserForm(int id)
        {
            Users user = DBContext.Instance.Users.Where(x => x.ID == id).FirstOrDefault();
            if (user == null) return "Bu kullanıcı silinmiş.";
            DeleteUser(user);
			OFDBrowser.JSReaction("addUserList");
            OFDBrowserFunction.WariningFormLabel("success", "Kullanıcı silindi!", user.UserName +  " adlı kullanıcı başarılı bir şekilde silindi!");
            return "Kullanıcı Silindi";
        }

    }
}