using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTCSDL_Nhom9.DTO
{
    public class AccountDTO
    {
        public AccountDTO(string userName, string displayName, int type, string passWord = null)
        {
            this.UserName = userName;
            this.DisplayName = displayName;
            this.Type = type;
            this.PassWord = passWord;
        }
        public AccountDTO(DataRow row)
        {
            this.UserName = row["UserName"].ToString();
            this.DisplayName = row["DisplayName"].ToString();
            this.Type = (int)row["Type"];
            this.PassWord = row["PassWord"].ToString();
        }
        private int type;

        public int Type
        {
            get { return type; }
            set { type = value; }
        }

        private string passWord;

        public string PassWord
        {
            get { return passWord; }
            set { passWord= value; }
        }

        private string displayName;

        public string DisplayName
        {
            get { return displayName; }
            set { displayName = value; }
        }

        private string userName;

        public string UserName 
        {
            get { return userName; }
            set { userName = value; }
        }
    }
}
