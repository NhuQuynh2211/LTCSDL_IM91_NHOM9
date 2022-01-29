using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LTCSDL_Nhom9.DTO;

namespace LTCSDL_Nhom9.DAL
{
    public class BillInfoDAL
    {
        private static BillInfoDAL instance;

        public static BillInfoDAL Instance
        {
            get { if (instance == null) instance = new BillInfoDAL(); return BillInfoDAL.instance; }
            private set { BillInfoDAL.instance = value; }

        }

        private BillInfoDAL() { }

        public List<BillInfoDTO> GetListBillInfo(int id)
        {
            List<BillInfoDTO> listBillInfo = new List<BillInfoDTO>();
            DataTable data = KetNoiDatabase.Instance.ExcuteQuery("select * from dbo.BillInfo where IDBill = " + id);

            foreach (DataRow item in data.Rows)
            {
                BillInfoDTO info = new BillInfoDTO(item);
                listBillInfo.Add(info);
            }

            return listBillInfo;
        }

        public void InsertBillInfo(int IDBill, int IDProduct, int count)
        {
            KetNoiDatabase.Instance.ExcuteNonQuery("InsertBillInfo @IDBill , @IDProduct , @count", new object[] { IDBill, IDProduct, count });
        }

        public void DeleteBillInfo(int id)
        {
            KetNoiDatabase.Instance.ExcuteQuery("DELETE from dbo.BillInfo where IDProduct = " + id);
        }
    }
}
