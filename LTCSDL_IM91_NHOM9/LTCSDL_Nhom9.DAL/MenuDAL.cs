using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LTCSDL_Nhom9.DTO;

namespace LTCSDL_Nhom9.DAL
{
    public class MenuDAL
    {
        private static MenuDAL instance;

        public static MenuDAL Instance
        {
            get { if (instance == null) instance = new MenuDAL(); return MenuDAL.instance; }
            private set { instance = value; }
        }
        private MenuDAL() { }

        public List<MenuDTO> GetListMenuByTable(int id)
        {
            List<MenuDTO> listMenu = new List<MenuDTO>();
            string query = "select p.ProductName, bi.count, p.Price, p.Price*bi.count as totalPrice FROM dbo.BillInfo as bi, dbo.Bill as b, dbo.Products as p WHERE bi.IDBill = b.BillID AND bi.IDProduct = p.ProductID and b.status = 0 and b.IDTable = " + id;
            DataTable dt = KetNoiDatabase.Instance.ExcuteQuery(query);

            foreach (DataRow item in dt.Rows)
            {
                MenuDTO menu = new MenuDTO(item);
                listMenu.Add(menu);
            }

            return listMenu;
        }
    }
}
