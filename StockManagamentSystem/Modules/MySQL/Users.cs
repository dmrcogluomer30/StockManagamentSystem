using System;

namespace StockManagamentSystem.Modules.MySQL
{
    public class Users
    {
        private int iD;
        private string userName;
        private string name;
        private string surname;
        private string mail;
        private string password;
        private int admin;
        private DateTime created;

        public int ID { get => iD; set => iD = value; }
        public string UserName { get => userName; set => userName = value; }
        public string Name { get => name; set => name = value; }
        public string Surname { get => surname; set => surname = value; }
        public string Mail { get => mail; set => mail = value; }
        public string Password { get => password; set => password = value; }
        public int Admin { get => admin; set => admin = value; }
        public DateTime Created { get => created; set => created = value; }
    }
}
