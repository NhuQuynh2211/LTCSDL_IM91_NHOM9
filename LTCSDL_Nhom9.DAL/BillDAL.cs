using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LTCSDL_Nhom9.DTO;

namespace LTCSDL_Nhom9.DAL
{
    public class BillDAL
    {
        private static BillDAL instance;

        public static BillDAL Instance
        {
            get { if (instance == null) instance = new BillDAL(); return BillDAL.instance; }
            private set { BillDAL.instance = value; }
        }

        private BillDAL() { }

        public int GetUncheckBillIDByTableID(int id)
        {
            DataTable data = KetNoiDatabase.Instance.ExcuteQuery("select * from dbo.Bill where IDTable = " + id + "AND status = 0");

            if (data.Rows.Count > 0) // số trường trả về lớn hơn không
            {
                BillDTO bill = new BillDTO(data.Rows[0]);
                return bill.ID;
            }
            return -1; // không có
        }

        public void InsertBill(int id)
        {
            KetNoiDatabase.Instance.ExcuteNonQuery("exec InsertBill @IDTable", new object[] { id });
        }

        public int GetMaxIDBill()
        {
            try
            {
                return (int)KetNoiDatabase.Instance.ExcuteScalar("select max(BillID) from dbo.Bill");
            }
            catch
            {
                return 1;
            }

        }

        public void CheckOut(int id, float totalPrice)
        {
            string query = "UPDATE dbo.Bill SET DateCheckOut= GETDATE(), status=1," + "totalPrice = " + totalPrice +" WHERE BILLID = " + id;
            KetNoiDatabase.Instance.ExcuteNonQuery(query);
        }

        public DataTable GetBillListByDate(DateTime checkIn, DateTime checkOut)
        {
            return KetNoiDatabase.Instance.ExcuteQuery(" EXEC GetListBillByDate @checkIn , @checkOut", new object[]{ checkIn, checkOut});
        }

    }
}
