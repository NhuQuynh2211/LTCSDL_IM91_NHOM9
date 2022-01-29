using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LTCSDL_IM91_NHOM9
{
    public partial class Choose : Form
    {
        string UserName , DisplayName, PassWord ;
        int Type;
        public Choose()
        {
            InitializeComponent();
        }


        public Choose(string UserName, string DisplayName, string PassWord, int Type)
        {
            InitializeComponent();
            this.UserName = UserName;
            this.DisplayName = DisplayName;
            this.PassWord = PassWord;
            this.Type = Type;
        }

        private void btQTHT_MouseHover(object sender, EventArgs e)
        {
            btQTHT.BackColor = Color.Sienna;
            pictureBox1.BackColor = Color.Sienna;
        }

        private void btBanHang_MouseHover(object sender, EventArgs e)
        {
            btBanHang.BackColor = Color.Sienna;
            pictureBox2.BackColor = Color.Sienna;
        }

        private void btBanHang_MouseLeave(object sender, EventArgs e)
        {
            btBanHang.BackColor = Color.Tan;
            pictureBox2.BackColor = Color.Tan;
        }

        private void btQTHT_MouseLeave(object sender, EventArgs e)
        {
            btQTHT.BackColor = Color.Tan;
            pictureBox1.BackColor = Color.Tan;
        }

        private void btQTHT_Click(object sender, EventArgs e)
        {
            if (Type == 1) //1: quản lý
            {
                fQuanLy f = new fQuanLy();
                this.Hide();
                f.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("Bạn không có quyền truy cập !");
            }
        }
        private void btBanHang_Click(object sender, EventArgs e)
        {
            fBanHang f = new fBanHang();
            this.Hide();
            f.ShowDialog();
            this.Close();
        }
    }
}
