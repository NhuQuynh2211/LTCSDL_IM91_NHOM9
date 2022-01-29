using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTCSDL_Nhom9.DTO
{
    public class BillInfoDTO
    {
        public BillInfoDTO(int iD, int billID, int productID, int count)
        {
            this.ID = iD;
            this.BillID = billID;
            this.ProductID = productID;
            this.Count = count;
        }

        public BillInfoDTO(DataRow row)
        {
            this.ID = (int)row["ID"];
            this.BillID = (int)row["IDBill"];
            this.ProductID = (int)row["IDProduct"];
            this.Count = (int)row["count"];
        }


        private int count;
        public int Count
        {

            get { return count; }
            set { count = value; }
        }

        private int productID;
        public int ProductID
        {

            get { return productID; }
            set { productID = value; }
        }

        private int billID;
        public int BillID
        {

            get { return billID; }
            set { billID = value; }
        }

        private int iD;
        public int ID
        {

            get { return iD; }
            set { iD = value; }
        }
    }
}
