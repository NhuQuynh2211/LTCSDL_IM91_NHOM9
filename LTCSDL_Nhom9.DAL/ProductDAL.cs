using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LTCSDL_Nhom9.DTO;

namespace LTCSDL_Nhom9.DAL
{
    public class ProductDAL
    {
        private static ProductDAL instance;
        public static ProductDAL Instance
        {
            get { if (instance == null) instance = new ProductDAL(); return ProductDAL.instance; }
            private set { ProductDAL.instance = value; }

        }

        private ProductDAL() { }

        public List<ProductDTO> GetProductByCategory(int id)
        {
            List<ProductDTO> list = new List<ProductDTO>();
            string query = "select * FROM Products where IDCategory = " + id;
            DataTable dt = KetNoiDatabase.Instance.ExcuteQuery(query);

            foreach (DataRow item in dt.Rows)
            {
                ProductDTO product = new ProductDTO(item);
                list.Add(product);
            }
            return list;
        }

        public List<ProductDTO> GetListProduct()
        {
            List<ProductDTO> list = new List<ProductDTO>();
            string query = "select ProductID, ProductName, Price, IDCategory From Products";
            DataTable data = KetNoiDatabase.Instance.ExcuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                ProductDTO product = new ProductDTO(item);
                list.Add(product);
            }
            return list;
        }

        public List<ProductDTO> SearchByName(string name)
        {
            List<ProductDTO> list = new List<ProductDTO>();
            string query =string.Format("SELECT * FROM dbo.Products WHERE dbo.fuConvertToUnsign1 (ProductName) LIKE N'%' + dbo.fuConvertToUnsign1(N'{0}') + '%'", name);
            DataTable data = KetNoiDatabase.Instance.ExcuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                ProductDTO product = new ProductDTO(item);
                list.Add(product);
            }
            return list;
        }
        public bool ThemMon(string name, float price, int id)
        {
            string query =string.Format( "insert into Products ( ProductName, price, IDCategory) values (N'{0}', {1}, {2})", name, price, id) ;
            int result = KetNoiDatabase.Instance.ExcuteNonQuery(query);
            return result > 0;
        }
        public bool SuaMon(int idProduct, string name, float price, int id)
        {
            string query = string.Format("UPDATE dbo.Products SET ProductName = N'{0}', Price = {1}, IDCategory = {2} WHERE ProductID = {3}", name, price, id, idProduct);
            int result = KetNoiDatabase.Instance.ExcuteNonQuery(query);
            return result > 0;
        }
        public bool XoaMon(int idProduct)
        {
            BillInfoDAL.Instance.DeleteBillInfo(idProduct);
            string query = string.Format("DELETE dbo.Products WHERE ProductID = {0}", idProduct);
            int result = KetNoiDatabase.Instance.ExcuteNonQuery(query);
            return result > 0;
        }

    }
}
