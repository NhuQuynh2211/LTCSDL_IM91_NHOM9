using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LTCSDL_Nhom9.DTO;

namespace LTCSDL_Nhom9.DAL
{
    public class AccoutDAL
    {
        private static AccoutDAL instance;

        public static AccoutDAL Instance
        {
            get { if (instance == null) instance = new AccoutDAL(); return instance; }
            private set { instance = value; }
        }

        private AccoutDAL() { }

        public bool Login(string userName , string passWord)
        {
            string query = "SELECT * FROM dbo.Account WHERE UserName= N'" + userName + "'AND PassWord = N'" + passWord + " ' ";
            DataTable result = KetNoiDatabase.Instance.ExcuteQuery(query);  
            return result.Rows.Count > 0;  // ĐK: số dòng trả ra phải lớn hơn 0
        }
        public DataTable GetListAccount()
        {
            return KetNoiDatabase.Instance.ExcuteQuery("SELECT UserName, DisplayName, Type FROM dbo.Account");
        }
        //THÊM TK
        public bool InsertAccount(string name,string displayName, int type)
        {
            string query = string.Format("insert into Account ( UserName, DisplayName, Type) values (N'{0}', N'{1}', {2})", name, displayName, type);
            int result = KetNoiDatabase.Instance.ExcuteNonQuery(query);
            return result > 0;
        }
        //SỬA TK
        public bool UpdateAccpunt( string name, string displayName, int type)
        {
            string query = string.Format("UPDATE dbo.Account SET DisplayName = N'{1}', Type = {2} WHERE UserName = N'{0}'", name, displayName, type);
            int result = KetNoiDatabase.Instance.ExcuteNonQuery(query);
            return result > 0;
        }
        //XOÁ TK
        public bool DeleteAccount(string name)
        {
            string query = string.Format("DELETE dbo.Account WHERE UserName = N'{0}'", name);
            int result = KetNoiDatabase.Instance.ExcuteNonQuery(query);
            return result > 0;
        }
    }
}
