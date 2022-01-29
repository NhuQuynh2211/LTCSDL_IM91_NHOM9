using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LTCSDL_Nhom9.DTO;

namespace LTCSDL_Nhom9.DAL
{
    public class TableDAL
    {
        private static TableDAL instance;

        public static TableDAL Instance
        {
            get { if (instance == null) instance = new TableDAL(); return TableDAL.instance; }
            private set { TableDAL.instance = value; }

        }

        public static int TableWidth = 105;
        public static int TableHeight = 105;

        private TableDAL() { }

        public void ChangeTableFood(int id1, int id2)
        {
            KetNoiDatabase.Instance.ExcuteQuery("ChangeTableFood @IDTable1 , @IDTable2", new object[] {id1, id2 });
        }

        public List<TableDTO> HTTableList()
        {
            List<TableDTO> tableList = new List<TableDTO>();
            DataTable data = KetNoiDatabase.Instance.ExcuteQuery("select * from dbo.TableFood");

            foreach (DataRow item in data.Rows)
            {
                TableDTO table = new TableDTO(item);
                tableList.Add(table);
            }

            return tableList;
        }
    }
}
