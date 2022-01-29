using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LTCSDL_Nhom9.DTO;

namespace LTCSDL_Nhom9.DAL
{
    public class CategoryDAL
    {
        private static CategoryDAL instance;
        public static CategoryDAL Instance
        {
            get { if (instance == null) instance = new CategoryDAL(); return CategoryDAL.instance; }
            private set { CategoryDAL.instance = value; }

        }

        private CategoryDAL() { }
        public List<CategoryDTO> GetListCategory()
        {
            List<CategoryDTO> list = new List<CategoryDTO>();
            string query = "select * FROM Categories";
            DataTable dt = KetNoiDatabase.Instance.ExcuteQuery(query);

            foreach (DataRow item in dt.Rows)
            {
                CategoryDTO Category = new CategoryDTO(item);
                list.Add(Category);
            }
            return list;
        }

        public CategoryDTO GetCategoryByID(int id)
        {
            CategoryDTO category = null;
            string query = "select * FROM Categories WHERE CategoryID = " + id;
            DataTable dt = KetNoiDatabase.Instance.ExcuteQuery(query);

            foreach (DataRow item in dt.Rows)
            {
                category = new CategoryDTO(item);
                return category;
            }

            return category;
        }
    }
}
