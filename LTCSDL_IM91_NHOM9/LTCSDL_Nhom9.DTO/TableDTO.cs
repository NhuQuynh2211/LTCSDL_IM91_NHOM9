using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTCSDL_Nhom9.DTO
{
    public class TableDTO
    {
        public TableDTO(int iD, string name, string status)
        {
            this.ID = iD;
            this.Name = name;
            this.Status = status;
        }
        public TableDTO(DataRow row)  // hàm dựng xử lý DataRow 
        {
            this.ID = (int)row["TableID"];
            this.Name = row["Tablename"].ToString();
            this.Status = row["status"].ToString();
        }
        private string status;
        public string Status
        {
            get { return status; }
            set { status = value; }
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
