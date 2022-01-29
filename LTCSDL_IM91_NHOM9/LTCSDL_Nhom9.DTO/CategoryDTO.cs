using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTCSDL_Nhom9.DTO
{
    public class CategoryDTO
    {
        public CategoryDTO(int iD, string name)
        {
            this.ID = iD;
            this.Name = name;
        }

        public CategoryDTO(DataRow row)
        {
            this.ID = (int)row["CategoryID"];
            this.Name = row["CategoryName"].ToString();
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
