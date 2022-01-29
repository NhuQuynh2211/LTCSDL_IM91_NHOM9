using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LTCSDL_Nhom9.DAL;


namespace LTCSDL_IM91_NHOM9
{
    public partial class fLogin : Form
    {
        public fLogin()
        {
            InitializeComponent();
        }

        //ĐĂNG NHẬP 
        private void btLogin_Click(object sender, EventArgs e)
        {
            try
            {
                string userName = txtUsername.Text;
                string passWord = txtPassword.Text;

                if (Login(userName, passWord))
                {
                    string query = "SELECT * FROM dbo.Account WHERE UserName= N'" + userName + "'AND PassWord = N'" + passWord + " ' ";
                    DataTable result = KetNoiDatabase.Instance.ExcuteQuery(query);
                    if (result.Rows.Count > 0)
                    {
                        MessageBox.Show("Đăng nhập thành công!");
                        Choose f = new Choose(result.Rows[0][0].ToString(), result.Rows[0][1].ToString(), result.Rows[0][2].ToString(), (int)result.Rows[0][3]);
                        this.Hide();
                        f.ShowDialog();
                        this.Show();
                    }

                }
                else
                {
                    MessageBox.Show("Vui lòng kiểm tra lại tên đăng nhập hoặc mật khẩu!");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Lỗi hệ thống!!");
            } 
        }
        bool Login(string userName, string passWord)
        {
            return AccoutDAL.Instance.Login(userName, passWord);

        }
        private void btExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void fLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Bạn muốn thoát chương trình?", "Thông báo", MessageBoxButtons.OKCancel) != System.Windows.Forms.DialogResult.OK)
            {
                e.Cancel = true;
            }
        }

        
    }
}
