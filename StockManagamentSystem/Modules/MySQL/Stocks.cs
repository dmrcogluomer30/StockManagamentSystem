using System;

namespace StockManagamentSystem.Modules.MySQL
{
    public class Stocks
    {
        private int iD;
        private string name;
        private string brand;
        private decimal buyPrice;
        private decimal sellPrice;
        private int categoryID;
        private int count;
        private DateTime createdDate;

        public int ID { get => iD; set => iD = value; }
        public string Name { get => name; set => name = value; }
        public string Brand { get => brand; set => brand = value; }
        public decimal BuyPrice { get => buyPrice; set => buyPrice = value; }
        public decimal SellPrice { get => sellPrice; set => sellPrice = value; }
        public int CategoryID { get => categoryID; set => categoryID = value; }
        public int Count { get => count; set => count = value; }
        public DateTime CreatedDate { get => createdDate; set => createdDate = value; }
    }
}
