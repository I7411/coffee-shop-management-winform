using QuanLyQuanCaPhe.DAL;
using QuanLyQuanCaPhe.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Printing;

namespace QuanLyQuanCaPhe
{
    public partial class TableManager : Form
    {
        private AccountDTO loginAccount;

        public AccountDTO LoginAccount 
        {
            get { return loginAccount; }
            set
            {
                loginAccount = value;
                ControlAccount(loginAccount.Type);
            }
        }

        public TableManager(AccountDTO acc)
        {
            InitializeComponent();

            this.LoginAccount = acc;

            LoadTable();
            LoadCategory();
            LoadComboBoxTable(cbSwitchTable);
        }

        #region Method

        void ControlAccount(int type) //Nếu là admin thì mới đc truy cập
        {
     
            if (type == 1)
            {
                adminToolStripMenuItem.Enabled = true; //nếu là 1 thì đc phép truy cập vào phần admin
            }
            else
                adminToolStripMenuItem.Enabled = false;
            thôngTinTàiKhỏaToolStripMenuItem.Text += " (" + loginAccount.DisplayName + ")";
            thôngTinCáNhânToolStripMenuItem.Text += " (" + loginAccount.DisplayName + ")"; 
        }
        public void LoadCategory()
        {
            List<CategoryDTO> lstCategory = CategoryDAL.Instance.GetListCategory();
            cbCategory.DataSource = lstCategory;
            cbCategory.DisplayMember = "Name";
        }
        void LoadFoodListByCategoryID(int id)
        {
            List<FoodDTO> lstFood = FoodDAL.Instance.GetListFoodByCategoryID(id);
            cbFood.DataSource = lstFood;
            cbFood.DisplayMember = "Name";
        }
        void LoadComboBoxTable(ComboBox cb) //Load số bàn trong phần chuyển bàn
        {
            cb.DataSource = TableDAL.Instance.GetTableList();
            cb.DisplayMember = "Name";
        }
        public void LoadTable()
        {
            flpTable.Controls.Clear();
            List<TableDTO> tableList = TableDAL.Instance.GetTableList();

            foreach (TableDTO item in tableList)
            {
                Button btn = new Button() { Width = TableDTO.TableWidth, Height = TableDTO.TableHeight };
                btn.Text = item.Name + "\n" + item.Status;
                btn.Click += Btn_Click;
                btn.Tag = item;

                if (string.Compare(item.Status, "Trống") == 0)
                {
                    btn.BackColor = Color.Green;

                }
                else
                    btn.BackColor = Color.Red;
                flpTable.Controls.Add(btn);
            }         
        }
        //List<Button> loadListButtonInTable(int id)
        //{
        //    List<Button> buttons = new List<Button>();
        //    flpTable.Controls.Clear();
        //    List<TableDTO> tableList = TableDAL.Instance.GetListTableById(id);

        //    foreach (TableDTO item in tableList)
        //    {
        //        Button btn = new Button() { Width = TableDTO.TableWidth, Height = TableDTO.TableHeight };
        //        btn.Text = item.Name + "\n" + item.Status;
        //        btn.Click += Btn_Click;
        //        btn.Tag = item;

        //        if (string.Compare(item.Status, "Trống") == 0)
        //        {
        //            btn.BackColor = Color.Green;
        //        }
        //        else
        //        {
        //            btn.BackColor = Color.Red;
        //        }

        //        flpTable.Controls.Add(btn);
        //        buttons.Add(btn);
        //    }

        //    return buttons;
        //}

        void ShowBill(int id)
        {
            listBill.Items.Clear();
            List<MenuDTO> listMenu = MenuDAL.Instance.GetListMenuByTable(id);

            float totalPrice = 0;
            foreach (MenuDTO item in listMenu)
            {
                ListViewItem lstVitem = new ListViewItem(item.FoodName.ToString());
                lstVitem.SubItems.Add(item.Count.ToString());
                lstVitem.SubItems.Add(item.Price.ToString());
                lstVitem.SubItems.Add(item.TotalPrice.ToString());
                totalPrice += item.TotalPrice;

                listBill.Items.Add(lstVitem);
            }
            CultureInfo culture = new CultureInfo("vi-VN"); //định dạng format tiền của VN
            Thread.CurrentThread.CurrentCulture = culture; //setting lại cái luồng đang chạy, cụ thể chuyển đơn vị tiền thành VNĐ

            txbTotalPrice.Text = totalPrice.ToString("c");

        }
        void showTable(int id) //Load lại tình trạng bàn ăn lúc cập nhập sửa trong phần Admin
        {
            flpTable.Controls.Clear();
            List<TableDTO> tableList = TableDAL.Instance.GetListTableById(id);

            foreach (TableDTO item in tableList)
            {
                Button btn = new Button() { Width = TableDTO.TableWidth, Height = TableDTO.TableHeight };
                btn.Text = item.Name + "\n" + item.Status;
                btn.Click += Btn_Click;
                btn.Tag = item;

                if (string.Compare(item.Status, "Trống") == 0)
                {
                    btn.BackColor = Color.Green;
                }
                else
                {
                    btn.BackColor = Color.Red;
                }

                flpTable.Controls.Add(btn);
            }
        }

        #endregion

        #region Events
     
       


        void Btn_Click(object sender, EventArgs e)
        {
            int tableId = ((sender as Button).Tag as TableDTO).ID;   //sender là 1 đối tượng button   
            listBill.Tag = (sender as Button).Tag;
            ShowBill(tableId);
        }

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();   
        }

        private void thôngTinCáNhânToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AccountProfile f = new AccountProfile(LoginAccount);
            f.UpdateAccount += f_UpdateAccount;
            f.ShowDialog();
        }

        private void f_UpdateAccount(object sender, AccountEvent e) //Cho tên đc thay đổi hiện thị sát bên chỗ thông tin tài khoản)
        {
            thôngTinTàiKhỏaToolStripMenuItem.Text = "Thông tin tài khoản (" + e.Acc.DisplayName + ")";
            thôngTinCáNhânToolStripMenuItem.Text = "Thông tin cá nhân (" + e.Acc.DisplayName + ")";
        }

        private void adminToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Admin f = new Admin();
            f.loginAccount = LoginAccount;

            ButtonAddFood btn = new ButtonAddFood();
            btn.InsertFood += btn_InsertFood;
            f.UpdateFood += f_UpdateFood;
            f.DeleteFood += f_DeleteFood;

            ButtonAddCategoryFood btn2 = new ButtonAddCategoryFood();
            btn2.InsertFoodCategory += btn_InsertFoodCategory;
            f.UpdateFoodCategory += f_UpdateFoodCategory;
            f.DeleteFoodCategory += f_DeleteFoodCategory;

            ButtonAddTableFood btn3 = new ButtonAddTableFood();
            btn3.InsertTable += btn3_InsertTable;
            f.UpdateTableFood += f_UpdateTableFood;
            f.DeleteTableFood += f_DeleteTableFood;

            f.ShowDialog();
        }



        //---------------------------------------------------------------------------------
        //Thực hiện chức năng cho Bàn ăn bên TableManager
        private void btn3_InsertTable(object sender, EventArgs e)
        {
            LoadTable();
           if(flpTable.Tag != null)
            {
                showTable((flpTable.Tag as TableDTO).ID);
            }
        }

        private void f_UpdateTableFood(object sender, EventArgs e)
        {
            LoadTable();
            if(flpTable.Tag != null)
            {
                showTable((flpTable.Tag as TableDTO).ID);
            }    
                
           
        }
        private void f_DeleteTableFood(object sender, EventArgs e)
        {
            LoadTable();
            if (flpTable.Tag != null)
            {
                showTable((flpTable.Tag as TableDTO).ID);
            }
        }
        //---------------------------------------------------------------------------------
        //Thực hiện chức năng cho Thể loại món bên TableManager
        private void f_DeleteFoodCategory(object sender, EventArgs e)
        {
            LoadCategory();
            if (listBill.Tag != null)
                ShowBill((listBill.Tag as TableDTO).ID);
        }

        private void f_UpdateFoodCategory(object sender, EventArgs e)
        {
            LoadCategory();
            if (listBill.Tag != null)
                ShowBill((listBill.Tag as TableDTO).ID);
        }

        private void btn_InsertFoodCategory(object sender, EventArgs e)
        {
            LoadCategory();
            if (listBill.Tag != null)
                ShowBill((listBill.Tag as TableDTO).ID);
        }
        //---------------------------------------------------------------------------------
        //Thực hiện chức năng cho món ăn bên TableManager
        private void btn_InsertFood(object sender, EventArgs e)// Chấp nhận load món mới trong phần table manager với listBill
        {
            LoadFoodListByCategoryID((cbCategory.SelectedItem as CategoryDTO).Id);
            if (listBill.Tag != null)
                ShowBill((listBill.Tag as TableDTO).ID);
        }

        private void f_DeleteFood(object sender, EventArgs e)// Xóa món mới trong phần table manager với ListBill
        {
            LoadFoodListByCategoryID((cbCategory.SelectedItem as CategoryDTO).Id);
            if (listBill.Tag != null)
                ShowBill((listBill.Tag as TableDTO).ID);
        }

        private void f_UpdateFood(object sender, EventArgs e) //Update lại món mới trong phần table manager với ListBill
        {
            LoadFoodListByCategoryID((cbCategory.SelectedItem as CategoryDTO).Id);
            if(listBill.Tag != null)
                ShowBill((listBill.Tag as TableDTO).ID);
        }
        //----------------------------------------------------------------------------------
        private void cbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            if (cb.SelectedItem == null)
                return;
            CategoryDTO selected = cb.SelectedItem as CategoryDTO;
            int id = selected.Id;

            LoadFoodListByCategoryID(id);
        }

        private void btnAddFood_Click(object sender, EventArgs e)
        {
            TableDTO table = listBill.Tag as TableDTO;
            if(table == null)
            {
                MessageBox.Show("Vui lòng chọn bàn trước khi thêm món!");
                return;
            }    
            int idBill = BillDAL.Instance.GetUncheckBillIdByTableId(table.ID);
            int idFood = (cbFood.SelectedItem as FoodDTO).Id;
            int count = (int)nmFoodCount.Value;

            if(idBill == -1)
            {
                BillDAL.Instance.InsertBill(table.ID);
                BillInfoDAL.Instance.InsertBillInfo(BillDAL.Instance.GetMaxBillID(), idFood, count);
            }
            else
            {
                BillInfoDAL.Instance.InsertBillInfo(idBill, idFood, count);
            }

            ShowBill(table.ID);
            LoadTable();
        }
        private int billIDToPrint = -1;
        private void btnCheck_Click(object sender, EventArgs e)
        {

            //FIX MỚI
            //TableDTO table = listBill.Tag as TableDTO;
            //int idBill = BillDAL.Instance.GetUncheckBillIdByTableId(table.ID);
            //int discount = (int)nmDiscount.Value;

            //string input = txbTotalPrice.Text.Replace(".", "").Replace("₫", "").Trim();
            //double totalPrice = Convert.ToDouble(input);
            //double finalTotalPrice = totalPrice - ((totalPrice / 100) * discount);

            //if (idBill != -1)
            //{
            //    // Bước 1: Xác nhận thanh toán
            //    if (MessageBox.Show(
            //        string.Format("Bạn có muốn thanh toán bàn {0}\nTổng tiền: {1:N0} ₫\nGiảm giá: {2}%\nThanh toán: {3:N0} ₫",
            //            table.Name, totalPrice, discount, finalTotalPrice),
            //        "Xác nhận thanh toán",
            //        MessageBoxButtons.OKCancel,
            //        MessageBoxIcon.Question) == DialogResult.OK)
            //    {
            //        // Thực hiện thanh toán
            //        BillDAL.Instance.CheckOut(idBill, discount, (float)totalPrice, (float)finalTotalPrice);

            //        // Lưu ID bill để in
            //        billIDToPrint = idBill;

            //        // Bước 2: Hỏi có muốn in hóa đơn không
            //        DialogResult printResult = MessageBox.Show(
            //            "Thanh toán thành công!\n\nBạn có muốn in hóa đơn không?",
            //            "In hóa đơn",
            //            MessageBoxButtons.YesNo,
            //            MessageBoxIcon.Question);

            //        if (printResult == DialogResult.Yes)
            //        {
            //            // In hóa đơn
            //            PrintBill(billIDToPrint);
            //        }

            //        // Refresh lại màn hình
            //        ShowBill(table.ID);
            //        LoadTable();
            //    }
            //}
            //else
            //{
            //    MessageBox.Show("Không tìm thấy hóa đơn!", "Thông báo",
            //        MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //}


            //FIX MỚI
            TableDTO table = listBill.Tag as TableDTO;
            int idBill = BillDAL.Instance.GetUncheckBillIdByTableId(table.ID);
            int discount = (int)nmDiscount.Value;

            // Giới hạn discount tối đa 100%
            if (discount > 100)
                discount = 100;

            // Chuyển totalPrice từ textbox sang double
            string input = txbTotalPrice.Text.Replace(".", "").Replace("₫", "").Trim();
            double totalPrice = Convert.ToDouble(input);

            // Tính finalTotalPrice, đảm bảo không âm
            double finalTotalPrice = Math.Max(0, totalPrice - ((totalPrice / 100) * discount));

            if (idBill != -1)
            {
                // Bước 1: Xác nhận thanh toán
                DialogResult confirm = MessageBox.Show(
                    string.Format("Bạn có muốn thanh toán bàn {0}?\nTổng tiền: {1:N0} ₫\nGiảm giá: {2}%\nThanh toán: {3:N0} ₫",
                        table.Name, totalPrice, discount, finalTotalPrice),
                    "Xác nhận thanh toán",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Question
                );

                if (confirm == DialogResult.OK)
                {
                    // Thực hiện thanh toán
                    BillDAL.Instance.CheckOut(idBill, discount, (float)totalPrice, (float)finalTotalPrice);

                    // Lưu ID bill để in
                    billIDToPrint = idBill;

                    // Bước 2: Hỏi có muốn in hóa đơn không
                    DialogResult printResult = MessageBox.Show(
                        "Thanh toán thành công!\n\nBạn có muốn in hóa đơn không?",
                        "In hóa đơn",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question
                    );

                    if (printResult == DialogResult.Yes)
                        PrintBill(billIDToPrint);

                    // Refresh lại giao diện
                    ShowBill(table.ID);
                    LoadTable();
                }
            }
            else
            {
                MessageBox.Show("Không tìm thấy hóa đơn!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void PrintBill(int billID)
        {
            try
            {
                billIDToPrint = billID;

                PrintDocument pd = new PrintDocument();
                pd.PrintPage += Pd_PrintPage_Bill;

                PrintPreviewDialog preview = new PrintPreviewDialog();
                preview.Document = pd;
                preview.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi in hóa đơn: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pd_PrintPage_Bill(object sender, PrintPageEventArgs e)
        {
            // Font chữ
            Font fTitle = new Font("Times New Roman", 24, FontStyle.Bold);
            Font fSubTitle = new Font("Times New Roman", 12, FontStyle.Italic);
            Font fNormal = new Font("Times New Roman", 14);
            Font fBold = new Font("Times New Roman", 14, FontStyle.Bold);
            Font fLarge = new Font("Times New Roman", 16, FontStyle.Bold);

            int left = 80;
            int top = 50;
            int y = top;

            // ====== TIÊU ĐỀ ======
            e.Graphics.DrawString("QUÁN CAFE ABC", fTitle, Brushes.Black, 230, y);
            y += 35;
            e.Graphics.DrawString("123 Đường XYZ, Quận 1, TP.HCM - ĐT: 0123 456 789", fSubTitle, Brushes.Black, 180, y);
            y += 50;

            e.Graphics.DrawString("HÓA ĐƠN THANH TOÁN", fTitle, Brushes.Black, 200, y);
            y += 50;

            // ====== LẤY THÔNG TIN HÓA ĐƠN ======
            string sql = @"SELECT b.id, b.DateCheckIn, b.DateCheckOut, b.discount, b.totalPrice, b.finaltotalPrice,
                          t.name as TableName
                   FROM dbo.Bill b
                   INNER JOIN dbo.TableFood t ON b.idTable = t.id
                   WHERE b.id = @billID";

            DataTable dtBill = DataProvider.Instance.ExcuteQuery(sql, new object[] { billIDToPrint });

            if (dtBill.Rows.Count == 0)
            {
                e.Graphics.DrawString("Không tìm thấy hóa đơn!", fBold, Brushes.Red, left, y);
                return;
            }

            DataRow billRow = dtBill.Rows[0];
            DateTime ngayCheckIn = Convert.ToDateTime(billRow["DateCheckIn"]);
            DateTime? ngayCheckOut = billRow["DateCheckOut"] != DBNull.Value
                ? (DateTime?)Convert.ToDateTime(billRow["DateCheckOut"])
                : null;

            // ====== THÔNG TIN HÓA ĐƠN ======
            string ngayText = "Ngày " + ngayCheckIn.Day.ToString("00") +
                              " Tháng " + ngayCheckIn.Month.ToString("00") +
                              " Năm " + ngayCheckIn.Year.ToString();
            e.Graphics.DrawString(ngayText, fNormal, Brushes.Black, left, y);
            y += 30;

            e.Graphics.DrawString("Số hóa đơn:  " + billIDToPrint.ToString(), fNormal, Brushes.Black, left, y);
            y += 30;

            e.Graphics.DrawString("Bàn:  " + billRow["TableName"].ToString(), fNormal, Brushes.Black, left, y);
            y += 30;

            e.Graphics.DrawString("Giờ vào:  " + ngayCheckIn.ToString("HH:mm:ss dd/MM/yyyy"), fNormal, Brushes.Black, left, y);
            y += 30;

            if (ngayCheckOut != null)
            {
                e.Graphics.DrawString("Giờ ra:  " + ngayCheckOut.Value.ToString("HH:mm:ss dd/MM/yyyy"), fNormal, Brushes.Black, left, y);
                y += 30;
            }

            y += 20;

            // ====== LẤY CHI TIẾT HÓA ĐƠN ======
            string sqlDetail = @"SELECT f.name as FoodName, bi.count
                         FROM dbo.BillInfo bi
                         INNER JOIN dbo.Food f ON bi.idFood = f.id
                         WHERE bi.idBill = @billID";

            DataTable dtDetail = DataProvider.Instance.ExcuteQuery(sqlDetail, new object[] { billIDToPrint });

            // ====== KẺ BẢNG ======
            int col1 = left;                
            int col2 = left + 60;          
            int col3 = left + 450;          
            int rowHeight = 35;
            int tableWidth = 530;
            int tableTop = y;

            // Vẽ viền bảng
            e.Graphics.DrawRectangle(Pens.Black, left, y, tableWidth, (dtDetail.Rows.Count + 1) * rowHeight);

            // Header bảng
            e.Graphics.DrawString("STT", fBold, Brushes.Black, col1 + 5, y + 5);
            e.Graphics.DrawString("Tên món", fBold, Brushes.Black, col2 + 5, y + 5);
            e.Graphics.DrawString("Số lượng", fBold, Brushes.Black, col3 + 5, y + 5);

            y += rowHeight;

            // Vẽ các đường kẻ dọc
            e.Graphics.DrawLine(Pens.Black, col2, tableTop, col2, tableTop + (dtDetail.Rows.Count + 1) * rowHeight);
            e.Graphics.DrawLine(Pens.Black, col3, tableTop, col3, tableTop + (dtDetail.Rows.Count + 1) * rowHeight);

            // Vẽ đường kẻ ngang sau header
            e.Graphics.DrawLine(Pens.Black, left, y, left + tableWidth, y);

            // ====== DỮ LIỆU TỪNG DÒNG ======
            int stt = 1;
            foreach (DataRow r in dtDetail.Rows)
            {
                string tenMon = r["FoodName"].ToString();
                int soLuong = Convert.ToInt32(r["count"]);

                e.Graphics.DrawString(stt.ToString(), fNormal, Brushes.Black, col1 + 15, y + 5);
                e.Graphics.DrawString(tenMon, fNormal, Brushes.Black, col2 + 5, y + 5);
                e.Graphics.DrawString(soLuong.ToString(), fNormal, Brushes.Black, col3 + 30, y + 5);

                e.Graphics.DrawLine(Pens.Black, left, y + rowHeight, left + tableWidth, y + rowHeight);
                y += rowHeight;
                stt++;
            }

            y += 30;

            // ====== TỔNG TIỀN (LẤY TỪ DATABASE) ======
            float totalPrice = billRow["totalPrice"] != DBNull.Value
                ? Convert.ToSingle(billRow["totalPrice"])
                : 0;

            int discount = billRow["discount"] != DBNull.Value
                ? Convert.ToInt32(billRow["discount"])
                : 0;

            float finalTotalPrice = billRow["finaltotalPrice"] != DBNull.Value
                ? Convert.ToSingle(billRow["finaltotalPrice"])
                : 0;

            // Hiển thị tổng tiền
            int colLabel = left + 250;
            int colValue = left + 420;

            e.Graphics.DrawString("Tổng cộng:", fBold, Brushes.Black, colLabel, y);
            e.Graphics.DrawString(totalPrice.ToString("N0") + " ₫", fBold, Brushes.Black, colValue, y);
            y += 35;

            // ====== GIẢM GIÁ (NẾU CÓ) ======
            if (discount > 0)
            {
                float tienGiam = totalPrice * discount / 100;
                e.Graphics.DrawString("Giảm giá (" + discount + "%):", fBold, Brushes.Black, colLabel, y);
                e.Graphics.DrawString("- " + tienGiam.ToString("N0") + " ₫", fBold, Brushes.Black, colValue, y);
                y += 35;
            }

            // Vẽ đường kẻ ngang
            e.Graphics.DrawLine(Pens.Black, colLabel, y - 5, left + tableWidth, y - 5);

            // ====== THANH TOÁN ======
            e.Graphics.DrawString("Thanh toán:", fLarge, Brushes.Black, colLabel, y);
            e.Graphics.DrawString(finalTotalPrice.ToString("N0") + " ₫", fLarge, Brushes.Black, colValue, y);

            y += 60;

            // ====== LỜI CẢM ƠN ======
            e.Graphics.DrawString("Cảm ơn quý khách! Hẹn gặp lại!", fBold, Brushes.Black, 230, y);
            y += 30;
            e.Graphics.DrawString("Chúc quý khách một ngày tốt lành!", fNormal, Brushes.Black, 220, y);
        }
        private void btnSwitchTable_Click(object sender, EventArgs e) //Chuyển bàn
        {
            
            int id1 = (listBill.Tag as TableDTO).ID;
            int id2 = (cbSwitchTable.SelectedItem as TableDTO).ID;
            string nameTable1 = (listBill.Tag as TableDTO).Name;
            string nameTable2 = (cbSwitchTable.SelectedItem as TableDTO).Name;
            if (MessageBox.Show(string.Format("Bạn có muốn chuyển bàn {0} qua bàn {1} không", nameTable1, nameTable2),
                "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                TableDAL.Instance.SwitchTable(id1, id2);
                LoadTable();
            }
       
        }


        #endregion

        
    }
}
