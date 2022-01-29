using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTCSDL_Nhom9.DTO
{
    public class MenuDTO
    {
        public MenuDTO(string foodName, int cout, float price, float totaPrice = 0)
        {
            this.FoodName = foodName;
            this.Count = cout;
            this.Price = price;
            this.TotalPrice = totaPrice;
        }
        public MenuDTO(DataRow row)
        {
            this.FoodName = row["ProductName"].ToString();
            this.Count = (int)row["count"];
            this.Price = (float)Convert.ToDouble(row["Price"].ToString());
            this.TotalPrice = (float)Convert.ToDouble(row["totalPrice"].ToString());
        }


        private float totalPrice;

        public float TotalPrice
        {
            get { return totalPrice; }
            set { totalPrice = value; }
        }

        private float price;

        public float Price
        {
            get { return price; }
            set { price = value; }
        }

        private int count;

        public int Count
        {
            get { return count; }
            set { count = value; }
        }

        private string foodName;

        public string FoodName
        {
            get { return foodName; }
            set { foodName = value; }
        }
    }
}
