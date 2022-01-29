using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTCSDL_Nhom9.DTO
{
    public class  ProductDTO
    {
        public ProductDTO(int iD, string name, int categoryID, float price)
        {
            this.ID = iD;
            this.Name = name;
            this.CategoryID = categoryID;
            this.Price = price;
        }

        public ProductDTO(DataRow row)
        {
            this.ID = (int)row["ProductID"];
            this.Name = row["ProductName"].ToString();
            this.CategoryID = (int)row["IDCategory"];
            this.Price = (float)Convert.ToDouble(row["Price"].ToString());
        }

        private float price;

        public float Price
        {
            get { return price; }
            set { price = value; }
        }

        private int categoryID;

        public int CategoryID
        {
            get { return categoryID; }
            set { categoryID = value; }
        }

        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private int iD;

        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }
    }
}
