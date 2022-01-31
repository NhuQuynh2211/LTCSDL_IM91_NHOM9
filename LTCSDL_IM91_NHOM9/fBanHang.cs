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
    public partial class fBanHang : Form
    {
        public fBanHang()
        {
            InitializeComponent();
            LoadTable(); // hiển thị danh sách bàn
            LoadCategory();
            LoadCbbTable(cbbCBan);

        }

        #region LoadCategory_Product
        void LoadCategory()
        {
            List<CategoryDTO> listCategory = CategoryDAL.Instance.GetListCategory();
            cbbCategory.DataSource = listCategory;
            cbbCategory.DisplayMember = "Name";
        }

        void ProductListCategoryID(int id)
        {
            List<ProductDTO> listProduct = ProductDAL.Instance.GetProductByCategory(id);
            cbbProduct.DataSource = listProduct;
            cbbProduct.DisplayMember = "Name";
        }
        #endregion

        string path = Application.StartupPath + @"\AnhTab\";

        #region LoadTable_ShowBill
        void LoadTable()
        {
            flpTable.Controls.Clear();
            List<TableDTO> tableList = TableDAL.Instance.HTTableList();

            foreach (TableDTO item in tableList)
            {
                Button btn = new Button() { Width = TableDAL.TableWidth, Height = TableDAL.TableHeight };
                btn.Text = item.Name + Environment.NewLine + item.Status;  //hiển thị tên bàn và status
                btn.Click += btn_Click;
                btn.Tag = item;

                switch (item.Status)
                {
                    case "Bàn trống":
                        btn.BackColor = Color.MistyRose;
                        btn.FlatStyle = FlatStyle.Popup;
                        break;
                    default:
                        btn.BackColor = Color.RosyBrown;
                        btn.FlatStyle = FlatStyle.Popup;
                        break;
                }
                flpTable.Controls.Add(btn);

            }
        }

        void ShowBill(int id)
        {
            lvBill.Items.Clear();
            List<MenuDTO> listBillInfo = MenuDAL.Instance.GetListMenuByTable(id);
            float totalPrice = 0;
            foreach (MenuDTO item in listBillInfo)
            {
                ListViewItem lsvItem = new ListViewItem(item.FoodName.ToString());
                lsvItem.SubItems.Add(item.Count.ToString());
                lsvItem.SubItems.Add(item.Price.ToString());
                lsvItem.SubItems.Add(item.TotalPrice.ToString());
                totalPrice += item.TotalPrice;
                lvBill.Items.Add(lsvItem);
            }
            CultureInfo culture = new CultureInfo("Vi-VN");
            txtTotalPrice.Text = totalPrice.ToString("c", culture);

            LoadTable();
        }

        void LoadCbbTable(ComboBox cbb)
        {
            cbb.DataSource = TableDAL.Instance.HTTableList();
            cbb.DisplayMember = "Name";
        }
        #endregion

        #region Sự Kiện
        void btn_Click(object sender, EventArgs e)
        {   
            int TableID = ((sender as Button).Tag as TableDTO).ID;
            lvBill.Tag = (sender as Button).Tag;
            ShowBill(TableID);
        }
        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void fBanHang_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Bạn muốn thoát chương trình?", "Thông báo", MessageBoxButtons.OKCancel) != System.Windows.Forms.DialogResult.OK)
            {
                e.Cancel = true;
            }
        }

        private void cbbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = 0;

            ComboBox cbb = sender as ComboBox;

            if (cbb.SelectedItem == null)
                return;

            CategoryDTO selected = cbb.SelectedItem as CategoryDTO;
            id = selected.ID;

            ProductListCategoryID(id);
        }

        private void btThemMon_Click(object sender, EventArgs e)
        {
            TableDTO table = lvBill.Tag as TableDTO;
            if (table == null)
            {
                MessageBox.Show("Hãy chọn bàn!");
                return;
            }
            int IDBill = BillDAL.Instance.GetUncheckBillIDByTableID(table.ID);
            int IDProduct = (cbbProduct.SelectedItem as ProductDTO).ID;
            int count = (int)nmFoodCount.Value;

            if (IDBill == -1) // chưa có bill nào
            {
                BillDAL.Instance.InsertBill(table.ID);
                BillInfoDAL.Instance.InsertBillInfo(BillDAL.Instance.GetMaxIDBill(), IDProduct, count);
            }
            else
            {
                BillInfoDAL.Instance.InsertBillInfo(IDBill, IDProduct, count);
            }
            ShowBill(table.ID);
        }

        private void btThanhToan_Click(object sender, EventArgs e)
        {
            TableDTO table = lvBill.Tag as TableDTO;
            int IDBill = BillDAL.Instance.GetUncheckBillIDByTableID(table.ID);
            double totalPrice = Convert.ToDouble(txtTotalPrice.Text.Split(',')[0]);
            if (IDBill != -1)
            {
                if (MessageBox.Show("Bạn có chắc thanh toán hoá đơn cho " + table.Name, "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                {
                    BillDAL.Instance.CheckOut(IDBill, (float)totalPrice);
                    ShowBill(table.ID);

                    LoadTable();
                }
            }
        }

        private void btChuyenBan_Click(object sender, EventArgs e)
        {
            int id1 = (lvBill.Tag as TableDTO).ID;
            int id2 = (cbbCBan.SelectedItem as TableDTO).ID;
            if (MessageBox.Show(string.Format("Bạn có muốn chuyển bàn {0} qua bàn {1}", (lvBill.Tag as TableDTO).Name, (cbbCBan.SelectedItem as TableDTO).Name), "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                TableDAL.Instance.ChangeTableFood(id1, id2);

                LoadTable();
            }

        }
        #endregion


    }
}