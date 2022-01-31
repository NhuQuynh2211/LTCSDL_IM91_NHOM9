using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LTCSDL_Nhom9.DAL;
using LTCSDL_Nhom9.DTO; 


namespace LTCSDL_IM91_NHOM9
{
    public partial class fQuanLy : Form
    {
        BindingSource ProductList = new BindingSource();
        BindingSource accountList = new BindingSource();
        //BindingSource TableList = new BindingSource();
        public fQuanLy()
        {
            InitializeComponent();
            dtGvMenu.DataSource = ProductList;
            dtgvAccount.DataSource = accountList;
            LoadDateTimePickerBill();
            LoadListViewByDate(dtPkTuNgay.Value, dtPkDenNgay.Value);
            LoadListProduct();
            LoadAccount();
            LoadCategoryComboBox(cbbLoaiSP);
            AddProductBinding();
            AddAccountBinding();

        }
        #region Quản Lý Bill - Món
        void LoadListViewByDate(DateTime checkIn, DateTime checkOut)
        {
            dtgvBill.DataSource = BillDAL.Instance.GetBillListByDate(checkIn, checkOut);
        }
        void AddProductBinding()
        {
            txtTenMon.DataBindings.Add(new Binding("Text", dtGvMenu.DataSource, "Name", true, DataSourceUpdateMode.Never));
            txtMaMon.DataBindings.Add(new Binding("Text", dtGvMenu.DataSource, "ID", true, DataSourceUpdateMode.Never));
            nmProductPrice.DataBindings.Add(new Binding("Value", dtGvMenu.DataSource, "Price", true, DataSourceUpdateMode.Never));
        }
        void LoadCategoryComboBox(ComboBox cbb)
        {
            cbb.DataSource = CategoryDAL.Instance.GetListCategory();
            cbb.DisplayMember = "Name";
        }
        void LoadListProduct()
        {
            
            ProductList.DataSource = ProductDAL.Instance.GetListProduct();
        }

        List<ProductDTO> SearchByName(string name)
        {
            List<ProductDTO> listProduct = ProductDAL.Instance.SearchByName(name);

            return listProduct;
        }
        #endregion

        #region Quản lý Tài khoản
        void AddAccountBinding()
        {
            txtUserName.DataBindings.Add(new Binding("Text", dtgvAccount.DataSource, "UserName", true, DataSourceUpdateMode.Never)); //Không chuyển đổi textbox về Dtgv
            txtDisplayName.DataBindings.Add(new Binding("Text", dtgvAccount.DataSource, "DisplayName", true, DataSourceUpdateMode.Never));
            nmType.DataBindings.Add(new Binding("Value", dtgvAccount.DataSource, "Type", true, DataSourceUpdateMode.Never));
        }
        void LoadAccount()
        {
            accountList.DataSource = AccoutDAL.Instance.GetListAccount();
        }
        void AddAccount(string userName, string displayName, int type)
        {
            if (AccoutDAL.Instance.InsertAccount(userName, displayName, type))
            {
                MessageBox.Show("Thêm tài khoản thành công!");
            }
            else
            {
                MessageBox.Show("Thêm tài khoản không thành công!");
            }
            LoadAccount();
        }
        void EditAccount(string userName, string displayName, int type)
        {
            if (AccoutDAL.Instance.UpdateAccpunt(userName, displayName, type))
            {
                MessageBox.Show("Cập nhật tài khoản thành công!");
            }
            else
            {
                MessageBox.Show("Cập nhật tài khoản thất bại!");
            }
            LoadAccount();
        }
        void DeleteAccount(string userName)
        {
            if (AccoutDAL.Instance.DeleteAccount(userName))
            {
                MessageBox.Show("Xoá tài khoản thành công!");
            }
            else
            {
                MessageBox.Show("Xoá tài khoản thất bại!");
            }
            LoadAccount();

        }
        #endregion

        #region Sự kiện
        private void fQuanLy_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Bạn muốn thoát chương trình?", "Thông báo", MessageBoxButtons.OKCancel) != System.Windows.Forms.DialogResult.OK)
            {
                e.Cancel = true;
            }
        }

        private void btThoatQLM_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btThoatTK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //THỐNG KÊ DOANH THU
        private void btThongkeDT_Click(object sender, EventArgs e)
        {
            LoadListViewByDate(dtPkTuNgay.Value, dtPkDenNgay.Value);
        }
        void LoadDateTimePickerBill()
        {
            DateTime today = DateTime.Now;
            dtPkTuNgay.Value = new DateTime(today.Year, today.Month, 1);
            dtPkDenNgay.Value = dtPkTuNgay.Value.AddMonths(1).AddDays(-1);
        }

        // QUẢN LÝ MÓN
        private void btXemQLSP_Click(object sender, EventArgs e)
        {
            LoadListProduct();
        }

        private void txtMaMon_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (dtGvMenu.SelectedCells.Count > 0)
                {
                        int id = (int)dtGvMenu.SelectedCells[0].OwningRow.Cells["CategoryID"].Value;

                        CategoryDTO category = CategoryDAL.Instance.GetCategoryByID(id);
                        cbbLoaiSP.SelectedItem = category;

                        int index = -1;
                        int i = 0;
                        foreach (CategoryDTO item in cbbLoaiSP.Items)
                        {
                            if (item.ID == category.ID)
                            {
                                index = i;
                                break;
                            }
                            i++;
                        }
                        cbbLoaiSP.SelectedIndex = index;
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi!", ex.Message);
            }
        }

        private void btThemQLSP_Click(object sender, EventArgs e)
        {
            string name = txtTenMon.Text;
            int categoryID = (cbbLoaiSP.SelectedItem as CategoryDTO).ID;
            float price = (float)nmProductPrice.Value;

            if (ProductDAL.Instance.ThemMon(name, price, categoryID))
            {
                MessageBox.Show("Thêm món thành công");
                LoadListProduct();
            }
            else
            {
                MessageBox.Show("Không thêm được món");
            }
        }

        private void btSuaQLSP_Click(object sender, EventArgs e)
        {
            string name = txtTenMon.Text;
            int categoryID = (cbbLoaiSP.SelectedItem as CategoryDTO).ID;
            float price = (float)nmProductPrice.Value;
            int id = Convert.ToInt32(txtMaMon.Text);

            if (ProductDAL.Instance.SuaMon(id, name, price, categoryID))
            {
                MessageBox.Show("Sửa món thành công");
                LoadListProduct();
            }
            else
            {
                MessageBox.Show("Không thể sửa món");
            }
        }

        private void btXoaQLSP_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtMaMon.Text);

            if (ProductDAL.Instance.XoaMon(id))
            {
                MessageBox.Show("Xoá món thành công");
                LoadListProduct();
            }
            else
            {
                MessageBox.Show("Không thể xoá món");
            }
        }
        private void btTimKiem_Click(object sender, EventArgs e)
        {
            txtMaMon.Text = "";
            txtTenMon.Text = "";
            nmProductPrice.Value = 0;
            ProductList.DataSource = SearchByName(txtSearch.Text);
        }

        // QUẢN LÝ TÀI KHOẢN
        private void btXemAcc_Click(object sender, EventArgs e)
        {
            LoadAccount();
        }

        private void btThemAcc_Click(object sender, EventArgs e)
        {
            try 
            {
                string userName = txtUserName.Text;
                string displayName = txtDisplayName.Text;
                int type = (int)nmType.Value;
                AddAccount(userName, displayName, type);
            } 
            catch 
            {
                MessageBox.Show("Username đã tồn tại !!");
            }
        }

        private void btSuaAcc_Click(object sender, EventArgs e)
        {
            string userName = txtUserName.Text;
            string displayName = txtDisplayName.Text;
            int type = (int)nmType.Value;
            EditAccount(userName, displayName, type);
        }

        private void btXoaAcc_Click(object sender, EventArgs e)
        {
            string userName = txtUserName.Text;
            DeleteAccount(userName);
        }

        #endregion

        string path = Application.StartupPath + @"\AnhTab\";
        private void tcAdmin_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            if(tcAdmin.SelectedIndex == 0)
            {
                picIcon.Image = Image.FromFile(path + "1.png");

            }
            if (tcAdmin.SelectedIndex == 1)
            {
                picIcon.Image = Image.FromFile(path + "2.png");

            }
            if (tcAdmin.SelectedIndex == 2)
            {
                picIcon.Image = Image.FromFile(path + "3.png");

            }
        }

        private void fQuanLy_Load(object sender, EventArgs e)
        {
            picIcon.Image = Image.FromFile(path + "1.png");
        }
    }
}
