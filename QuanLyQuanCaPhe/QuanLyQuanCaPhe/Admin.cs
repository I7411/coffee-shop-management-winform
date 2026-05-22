using QuanLyQuanCaPhe.DAL;
using QuanLyQuanCaPhe.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

using System.IO;
using OfficeOpenXml;
using OfficeOpenXml.Style;


using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using DevExpress.Xpo.DB;

namespace QuanLyQuanCaPhe
{
    public partial class Admin : Form
    {
        //Khởi tạo 1 listfood thuộc BDSource để duy trì việc hiển thị tt khi binding từ datagridview sang các element khác
        BindingSource listFood = new BindingSource(); 
        BindingSource listCategory = new BindingSource();
        BindingSource listTable = new BindingSource();
        BindingSource listAccount = new BindingSource();

        public AccountDTO loginAccount; //Khai báo 1 tkhoan mới để lưu thông tin tk hiện đang đăng nhập

        public Admin()
        {
            InitializeComponent();

            LoadListBillRevenueByDate(dtpkFromDate.Value, dtpkToDate.Value);
            LoadDateTimePickerbill();

            LoadListFood();
            AddFoodBinding();
            LoadCategoryIntoComboBoxFoodCategory(cbFoodCategory);

            LoadListCategory();
            AddCategoryBinding();

            LoadListTable();
            addTableBinding();
           
            LoadListAccount();
            addAccountBinding();
            

        }

        #region methods
        //Phần Load Doanh Thu

        void LoadListBillRevenueByDate(DateTime checkIn, DateTime checkOut)
        {
            dtgvBill.DataSource = BillDAL.Instance.GetListBillRevenueByDate(checkIn, checkOut);
            
        }
        void LoadDateTimePickerbill()
        {
            //Thread.CurrentThread.CurrentCulture = new CultureInfo("vi-VN");
            //Thread.CurrentThread.CurrentUICulture = new CultureInfo("vi-VN");

            CultureInfo culture = new CultureInfo("vi-VN");
            Thread.CurrentThread.CurrentCulture = culture;

            DateTime date = DateTime.Now;
            dtpkFromDate.Value = new DateTime(date.Year, date.Month, 1); //lấy năm, tháng, ngày đầu tiên
            dtpkToDate.Value = dtpkFromDate.Value.AddMonths(1).AddDays(-1); //lấy tháng hiện tại + 1 tháng nữa trừ ngày đầu tiên là ra cuối tháng của tháng hiện tại
        }

        private void ExportToExcel(Excel.Worksheet worksheet)
        {
            int colCount = dtgvBill.Columns.Count;
            int rowCount = dtgvBill.Rows.Count;
            if (dtgvBill.Rows[rowCount - 1].IsNewRow)
                rowCount--;

            // ===== TIÊU ĐỀ =====
            Excel.Range titleRange = worksheet.Range[worksheet.Cells[1, 1], worksheet.Cells[1, colCount]];
            titleRange.Merge();
            titleRange.Value = "BÁO CÁO DOANH THU";
            titleRange.Font.Size = 20;
            titleRange.Font.Bold = true;
            titleRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            titleRange.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
            titleRange.RowHeight = 30;

            // ===== THỜI GIAN =====
            Excel.Range dateRange = worksheet.Range[worksheet.Cells[2, 1], worksheet.Cells[2, colCount]];
            dateRange.Merge();
            dateRange.Value = $"Từ ngày: {dtpkFromDate.Value:dd/MM/yyyy} - Đến ngày: {dtpkToDate.Value:dd/MM/yyyy}";
            dateRange.Font.Size = 12;
            dateRange.Font.Italic = true;
            dateRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            dateRange.RowHeight = 20;

            // Dòng trống
            worksheet.Cells[3, 1].RowHeight = 10;

            // ===== HEADER CỘT =====
            int startRow = 4;
            for (int i = 0; i < colCount; i++)
            {
                Excel.Range headerCell = worksheet.Cells[startRow, i + 1];
                headerCell.Value = dtgvBill.Columns[i].HeaderText;
                headerCell.Font.Bold = true;
                headerCell.Font.Size = 12;
                headerCell.Interior.Color = Excel.XlRgbColor.rgbLightBlue;
                headerCell.Font.Color = Excel.XlRgbColor.rgbBlack;
                headerCell.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                headerCell.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                headerCell.Borders.Weight = Excel.XlBorderWeight.xlThin;
            }

            // ===== DỮ LIỆU =====
            decimal tongDoanhThu = 0;
            decimal tongGiamGia = 0;
            decimal tongThanhTien = 0;

            for (int i = 0; i < rowCount; i++)
            {
                if (dtgvBill.Rows[i].IsNewRow) continue;

                for (int j = 0; j < colCount; j++)
                {
                    Excel.Range dataCell = worksheet.Cells[startRow + 1 + i, j + 1];
                    var cellValue = dtgvBill.Rows[i].Cells[j].Value;

                    if (cellValue != null)
                    {
                        dataCell.Value = cellValue;
                    }

                    dataCell.Borders.Weight = Excel.XlBorderWeight.xlThin;
                    dataCell.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;

                    // Format cho các cột
                    string headerText = dtgvBill.Columns[j].HeaderText;

                    if (headerText == "Tổng tiền" || headerText == "Giảm giá" || headerText == "Thành tiền")
                    {
                        dataCell.NumberFormat = "#,##0\" ₫\"";
                        dataCell.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;

                        // Tính tổng
                        if (cellValue != null && decimal.TryParse(cellValue.ToString(), out decimal value))
                        {
                            if (headerText == "Tổng tiền")
                                tongDoanhThu += value;
                            else if (headerText == "Giảm giá")
                                tongGiamGia += value;
                            else if (headerText == "Thành tiền")
                                tongThanhTien += value;
                        }
                    }
                    else if (headerText == "Ngày vào" || headerText == "Ngày ra")
                    {
                        dataCell.NumberFormat = "dd/MM/yyyy HH:mm";
                        dataCell.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    }
                    else
                    {
                        dataCell.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                    }
                }
            }

            // ===== TỔNG KẾT =====
            int totalRow = startRow + rowCount + 1;

            // Tìm vị trí cột "Tổng tiền", "Giảm giá", "Thành tiền"
            int colTongTien = -1, colGiamGia = -1, colThanhTien = -1;
            for (int i = 0; i < colCount; i++)
            {
                string header = dtgvBill.Columns[i].HeaderText;
                if (header == "Tổng tiền") colTongTien = i + 1;
                else if (header == "Giảm giá") colGiamGia = i + 1;
                else if (header == "Thành tiền") colThanhTien = i + 1;
            }

            // Merge cell cho label "TỔNG CỘNG"
            if (colTongTien > 1)
            {
                Excel.Range totalLabelRange = worksheet.Range[worksheet.Cells[totalRow, 1], worksheet.Cells[totalRow, colTongTien - 1]];
                totalLabelRange.Merge();
                totalLabelRange.Value = "TỔNG CỘNG:";
                totalLabelRange.Font.Bold = true;
                totalLabelRange.Font.Size = 12;
                totalLabelRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                totalLabelRange.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
            }

            // Tổng tiền
            if (colTongTien > 0)
            {
                Excel.Range totalCell = worksheet.Cells[totalRow, colTongTien];
                totalCell.Value = tongDoanhThu;
                totalCell.Font.Bold = true;
                totalCell.NumberFormat = "#,##0\" ₫\"";
                totalCell.Interior.Color = Excel.XlRgbColor.rgbLightYellow;
                totalCell.Borders.Weight = Excel.XlBorderWeight.xlThin;
            }

            // Giảm giá
            if (colGiamGia > 0)
            {
                Excel.Range discountCell = worksheet.Cells[totalRow, colGiamGia];
                discountCell.Value = tongGiamGia;
                discountCell.Font.Bold = true;
                discountCell.NumberFormat = "#,##0\" ₫\"";
                discountCell.Interior.Color = Excel.XlRgbColor.rgbLightYellow;
                discountCell.Borders.Weight = Excel.XlBorderWeight.xlThin;
            }

            // Thành tiền
            if (colThanhTien > 0)
            {
                Excel.Range finalCell = worksheet.Cells[totalRow, colThanhTien];
                finalCell.Value = tongThanhTien;
                finalCell.Font.Bold = true;
                finalCell.Font.Size = 12;
                finalCell.NumberFormat = "#,##0\" ₫\"";
                finalCell.Interior.Color = Excel.XlRgbColor.rgbYellow;
                finalCell.Borders.Weight = Excel.XlBorderWeight.xlThin;
            }

            // ===== FOOTER =====
            int footerRow = totalRow + 2;
            Excel.Range footerRange = worksheet.Range[worksheet.Cells[footerRow, colCount - 1], worksheet.Cells[footerRow, colCount]];
            footerRange.Merge();
            footerRange.Value = $"Ngày xuất: {DateTime.Now:dd/MM/yyyy HH:mm}";
            footerRange.Font.Italic = true;
            footerRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;

            // ===== AUTO FIT COLUMNS =====
            worksheet.Columns.AutoFit();
        }//----------------------------------------------------------------------------------------------------------------------------

        //Phần Load Food
        public void LoadListFood() //Load danh sách món ăn vào phần món ăn của admin
        {
            listFood.DataSource = FoodDAL.Instance.GetListFood(); //listFood từ BindingSource đc gán ds thức ăn để reset thông tin món
            dtgvFood.DataSource = listFood; //dữ liệu dtgridViewfood gán với listFood tìm ra món tương ứng  
            dtgvFood.Columns["Id"].HeaderText = "STT";
            dtgvFood.Columns["Name"].HeaderText = "Tên món";
            dtgvFood.Columns["IdCategory"].HeaderText = "Mã số loại món";
            dtgvFood.Columns["Price"].HeaderText = "Đơn giá";
           
        }
        void AddFoodBinding() //Hiển thị món qua phần txb tương ứng khi click vào món bất kì
        {
            txbFoodName.DataBindings.Add(new Binding("Text", dtgvFood.DataSource, "name", true, DataSourceUpdateMode.Never));
            txbFoodID.DataBindings.Add(new Binding("Text", dtgvFood.DataSource, "ID", true, DataSourceUpdateMode.Never));
            nmrFoodPrice.DataBindings.Add(new Binding("Value", dtgvFood.DataSource, "price", true, DataSourceUpdateMode.Never));
        }

        void LoadCategoryIntoComboBoxFoodCategory(ComboBox cb) //Load thể loại món vào combobox thể loại của phần món ăn
        {
            cb.DataSource = CategoryDAL.Instance.GetListCategory();
            cbFoodCategory.DisplayMember = "Name";
            
        }
        
        List<FoodDTO> FindingFoodByName(string name) //Tìm kiếm thông tin Food bằng tên món ăn
        {
            List<FoodDTO> lstFood = new List<FoodDTO> ();
            lstFood = FoodDAL.Instance.SearchFoodByName(name);
            return lstFood;
        }
        //----------------------------------------------------------------------------------------------------------------------------

        //Phần Load Category
        public void LoadListCategory() 
        {
            listCategory.DataSource = CategoryDAL.Instance.GetListCategory();
            dtgvCategory.DataSource = listCategory;
            dtgvCategory.Columns["id"].HeaderText = "STT";
            dtgvCategory.Columns["name"].HeaderText = "Thể loại món";
        }
        
        void AddCategoryBinding()
        {
            txbCategoryID.DataBindings.Add(new Binding("Text", dtgvCategory.DataSource, "id", true, DataSourceUpdateMode.Never));
            txbCategoryName.DataBindings.Add(new Binding("Text", dtgvCategory.DataSource, "name", true, DataSourceUpdateMode.Never));
            
        }
        public void LoadCategoryAdminForTabAdmin() 
        {
            List<CategoryDTO> lstCategory = CategoryDAL.Instance.GetListCategory();
            cbFoodCategory.DataSource = lstCategory;
        }
        public void RefreshCategory()
        {
            if (cbFoodCategory.DataSource == null)
            {
                LoadCategoryAdminForTabAdmin();
            }
        }
        //----------------------------------------------------------------------------------------------------------------------------

        //Phần Load Bàn Ăn
        private void RefreshAllTableManagerForms()
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form is TableManager)
                {
                    TableManager tableManager = form as TableManager;
                    tableManager.LoadTable(); // Gọi hàm LoadTable để refresh
                }
            }
        }
        public void LoadListTable()
        {
            listTable.DataSource = TableDAL.Instance.GetTableList();
            dtgvTable.DataSource = listTable;
            dtgvTable.Columns["id"].HeaderText = "STT";
            dtgvTable.Columns["name"].HeaderText = "Tên bàn";
            dtgvTable.Columns["status"].HeaderText = "Tình trạng bàn";
        
        }
        void addTableBinding()
        {
            txbTableID.DataBindings.Add(new Binding("Text", dtgvTable.DataSource, "id", true, DataSourceUpdateMode.Never));
            txbTableName.DataBindings.Add(new Binding("Text", dtgvTable.DataSource, "name", true, DataSourceUpdateMode.Never));
            txbTableStatus.DataBindings.Add(new Binding("Text", dtgvTable.DataSource, "status", true, DataSourceUpdateMode.Never));
        }
        
        
        //----------------------------------------------------------------------------------------------------------------------------

        //Phần Load Tài Khoản
        public void LoadListAccount()
        {
            listAccount.DataSource = AccountDAL.Instance.GetListAccount();
            dtgvAccount.DataSource = listAccount;
            dtgvAccount.Columns["UserName"].HeaderText = "Tên tài khoản";
            dtgvAccount.Columns["DisplayName"].HeaderText = "Tên hiển thị";
            dtgvAccount.Columns["Type"].HeaderText = "Loại tài khoản";
        }
        void addAccountBinding()
        {
            txbUserName.DataBindings.Add(new Binding("Text", dtgvAccount.DataSource, "UserName", true, DataSourceUpdateMode.Never));
            txbDisplayName.DataBindings.Add(new Binding("Text", dtgvAccount.DataSource, "DisplayName", true, DataSourceUpdateMode.Never));
            nmrType.DataBindings.Add(new Binding("Value", dtgvAccount.DataSource, "Type", true, DataSourceUpdateMode.Never));
        }
        public void AddAccount(string userName, string displayName, string password,int type)
        {
            if (AccountDAL.Instance.InsertAccountForUser(userName, displayName, password,type))
                MessageBox.Show("Thêm tài khoản thành công!");
            else
                MessageBox.Show("Có lỗi khi thực hiện thêm tài khoản!");

            LoadListAccount();
        }
        void EditAccount(string userName, string displayName, int type)
        {
            if (AccountDAL.Instance.UpdateAccountForUser(userName, displayName, type))
                MessageBox.Show("Sửa tài khoản thành công!");
            else
                MessageBox.Show("Có lỗi khi thực hiện sửa tài khoản!");
            
            LoadListAccount();
        }
        
        void DeleteAccount(string userName)
        {
            if (loginAccount.UserName.Equals(userName))
            {
                MessageBox.Show("Không thể xóa tài khoản đang sử dụng phần mềm!");
                return;
            }
            if (AccountDAL.Instance.DeleteAccountForUser(userName))
                MessageBox.Show("Xóa tài khoản thành công!");
            else
                MessageBox.Show("Có lỗi khi thực hiện xóa tài khoản!");

            LoadListAccount();
        }

      
        #endregion

        //----------------------------------------------------------------------------------------------------------------------------


        #region events

        //Phần Admin Doanh Thu

        //Nút thống kê doanh thu 
        private void btnViewBill_Click(object sender, EventArgs e)
        {
            LoadListBillRevenueByDate(dtpkFromDate.Value, dtpkToDate.Value);
        }

        //In file ra excel
        private void btnIn_Click(object sender, EventArgs e)
        {
            Excel.Application excelApp = null;
            Excel.Workbook workbook = null;
            Excel.Worksheet worksheet = null;

            try
            {
                // Kiểm tra có dữ liệu không
                if (dtgvBill.Rows.Count == 0 || (dtgvBill.Rows.Count == 1 && dtgvBill.Rows[0].IsNewRow))
                {
                    MessageBox.Show("Không có dữ liệu để xuất báo cáo!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Tạo SaveFileDialog
                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.Filter = "Excel files (*.xlsx)|*.xlsx|Excel 97-2003 (*.xls)|*.xls";
                saveDialog.FilterIndex = 1;
                saveDialog.FileName = $"BaoCaoDoanhThu_{dtpkFromDate.Value:ddMMyyyy}_{dtpkToDate.Value:ddMMyyyy}.xlsx";
                saveDialog.Title = "Chọn nơi lưu báo cáo doanh thu";
                saveDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    // Khởi tạo Excel
                    excelApp = new Excel.Application();
                    excelApp.DisplayAlerts = false;
                    workbook = excelApp.Workbooks.Add(Type.Missing);
                    worksheet = (Excel.Worksheet)workbook.Worksheets[1];

                    // Xuất dữ liệu
                    ExportToExcel(worksheet);

                    // Lưu file
                    workbook.SaveAs(saveDialog.FileName);
                    workbook.Close();
                    excelApp.Quit();

                    // Thông báo thành công
                    DialogResult result = MessageBox.Show(
                        $"Xuất báo cáo thành công!\n\nFile đã được lưu tại:\n{saveDialog.FileName}\n\nBạn có muốn mở file Excel ngay bây giờ không?",
                        "Thành công",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Information);

                    // Nếu chọn Yes thì mở file
                    if (result == DialogResult.Yes)
                    {
                        System.Diagnostics.Process.Start(saveDialog.FileName);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xuất báo cáo: {ex.Message}\n\nVui lòng đảm bảo đã cài đặt Microsoft Excel trên máy.",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Giải phóng COM objects
                if (worksheet != null) Marshal.ReleaseComObject(worksheet);
                if (workbook != null) Marshal.ReleaseComObject(workbook);
                if (excelApp != null) Marshal.ReleaseComObject(excelApp);

                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }
     
       

        //---------------------------------------------------------------------------------------------------------//

        //Phần Admin Thức Ăn
        private void btnSearchFood_Click(object sender, EventArgs e)
        {
            listFood.DataSource = FindingFoodByName(txbSearchFoodName.Text);

        }
        private void btnShowFood_Click(object sender, EventArgs e) //Button Xem
        {
            LoadListFood();
            LoadCategoryAdminForTabAdmin();
        }

        //Sự kiện lấy id của food so sánh với id của Bảng category để lấy id cate xuất ra thể loại món
        private void txbFoodID_TextChanged(object sender, EventArgs e)
        {
            if (dtgvFood.SelectedCells.Count > 0) //đếm bảng nếu số lượng dòng > 0
            {
                int id = (int)dtgvFood.SelectedCells[0].OwningRow.Cells["IdCategory"].Value; //gán biến id dc lấy từ idCategory bất kì của 1 dòng trên bảng
                CategoryDTO cate = CategoryDAL.Instance.GetCategoryById(id); //truyền id vào vào hàm tìm thể loại món băng ID rồi gán vào 1 category

                int index = -1; //gán biến cục bộ bằng số -1
                int i = 0; //set index = 0
                foreach(CategoryDTO item in cbFoodCategory.Items) //mỗi item category trong combobox
                {
                    if(item.Id == cate.Id) //nếu id trong item trùng với id đã được lấy từ category ở trên 
                    {
                        index = i;  //set index = i
                        break; //break để trả giá trị tại thời điểm đó 
                    }
                    i++; //tiếp tục tăng để gán giá trị i trùng với category khác
                }
                cbFoodCategory.SelectedIndex = i; //set index trả về của combobox bằng i để trả về tên loại 
            }
        }

        private void btnAddFood_Click(object sender, EventArgs e)
        {
            //string name = txbFoodName.Text;
            //int idCategory = (cbFoodCategory.SelectedItem as CategoryDTO).Id;
            //float price = (float)nmrFoodPrice.Value;
            // if(FoodDAL.Instance.InsertFood(name, idCategory, price) == true) 
            //{
            //    MessageBox.Show("Thêm món thành công!");
            //    LoadListFood();
            //}
            //else
            //{
            //    MessageBox.Show("Có lỗi khi thực hiện thêm món!");
            //}

            ButtonAddFood f = new ButtonAddFood();
            f.ShowDialog();
        }

        private void btnEditFood_Click(object sender, EventArgs e)
        {
            string name = txbFoodName.Text;
            int idCategory = (cbFoodCategory.SelectedItem as CategoryDTO).Id;
            float price = (float)nmrFoodPrice.Value;
            int idFood = Convert.ToInt32(txbFoodID.Text);

            if (FoodDAL.Instance.UpdateFood(idFood,name, idCategory, price) == true)
            {
                MessageBox.Show("Sửa món thành công!");
                LoadListFood();

                LoadCategoryAdminForTabAdmin();
                if (updateFood != null)
                    updateFood(this, new EventArgs());
            }
            
            else
            {
                MessageBox.Show("Có lỗi khi thực hiện sửa món!");
            }
        }

        private void btnDeleteFood_Click(object sender, EventArgs e)
        {
            if (dtgvTable.CurrentRow == null || dtgvTable.CurrentRow.Index < 0)
            {
                MessageBox.Show("Vui lòng chọn bàn cần xóa!", "Thông báo",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Lấy ID từ dòng hiện tại
            int id = Convert.ToInt32(dtgvTable.CurrentRow.Cells["id"].Value);

            DialogResult result = MessageBox.Show($"Bạn có chắc chắn muốn xóa bàn số {id}?",
                                                  "Xác nhận",
                                                  MessageBoxButtons.YesNo,
                                                  MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                if (TableDAL.Instance.DeleteTable(id) == true)
                {
                    MessageBox.Show("Xóa bàn thành công!");
                    LoadListTable();
                    if (deleteTableFood != null)
                        deleteTableFood(this, new EventArgs());
                    RefreshAllTableManagerForms();
                }
                else
                {
                    MessageBox.Show("Xóa bàn thất bại!", "Lỗi",
                                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private event EventHandler deleteFood;
        public event EventHandler DeleteFood 
        {
            add { deleteFood += value; }
            remove { deleteFood -= value; }
        }

        private event EventHandler updateFood;
        public event EventHandler UpdateFood
        {
            add { updateFood += value; }
            remove { updateFood -= value; }
        }

        //----------------------------------------------------------------------------------------------------------------//

        //Phần Admin Danh mục
        private void btnShowCategory_Click(object sender, EventArgs e)
        {
            LoadListCategory();
        }

        private event EventHandler updateFoodCategory;
        public event EventHandler UpdateFoodCategory
        {
            add { updateFoodCategory += value; }
            remove { updateFoodCategory -= value; }
        }

        private event EventHandler deleteFoodCategory;
        public event EventHandler DeleteFoodCategory
        {
            add { deleteFoodCategory += value; }
            remove { deleteFoodCategory -= value; }
        }
        private void btnAddCategory_Click(object sender, EventArgs e)
        {
            ButtonAddCategoryFood f = new ButtonAddCategoryFood();
            f.ShowDialog();
        }
        private void btnEditCategory_Click(object sender, EventArgs e)
        {
            string name = txbCategoryName.Text;
            int idCategory = Convert.ToInt32(txbCategoryID.Text);

            if (CategoryDAL.Instance.UpdateCategory( idCategory, name) == true)
            {
                MessageBox.Show("Sửa loại món thành công!");
                LoadListCategory();
                if (updateFoodCategory != null)
                    updateFoodCategory(this, new EventArgs());
            }
            else
            {
                MessageBox.Show("Có lỗi khi thực hiện sửa loại món!");
            }
        }
        private void btnDeleteCategory_Click(object sender, EventArgs e)
        {
          
            int idCategory = Convert.ToInt32(txbCategoryID.Text);

            if (CategoryDAL.Instance.DeleteCategory(idCategory) == true)
            {
                MessageBox.Show("Xóa loại món thành công!");
                LoadListCategory();
                if (deleteFoodCategory != null)
                    deleteFoodCategory(this, new EventArgs());
            }
            else
            {
                MessageBox.Show("Có lỗi khi thực hiện xóa loại món!");
            }
        }

        //----------------------------------------------------------------------------------------------------------------//
        private void tabAdmin_SelectedIndexChanged(object sender, EventArgs e) //Load các tabAdmin khi click vào
        {
            TabControl tab = sender as TabControl;
            if(tab.SelectedTab.Text == "Thức ăn")
            {
                LoadCategoryAdminForTabAdmin();
            }
        }


        //----------------------------------------------------------------------------------------------------------------//

        //Phần Admin Bàn ăn

        private void btnShowTable_Click(object sender, EventArgs e)
        {
            LoadListTable();
        }

        private event EventHandler deleteTableFood;
        public event EventHandler DeleteTableFood
        {
            add { deleteTableFood += value; }
            remove { deleteTableFood -= value; }
        }

        private event EventHandler updateTableFood;
        public event EventHandler UpdateTableFood
        {
            add { updateTableFood += value; }
            remove { updateTableFood -= value; }
        }
        private void btnAddTable_Click(object sender, EventArgs e)
        {
            ButtonAddTableFood f = new ButtonAddTableFood();
            
            f.ShowDialog();
            RefreshAllTableManagerForms();
        }
        private void btnDeleteTable_Click(object sender, EventArgs e)
        {
            if (dtgvTable.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn bàn cần xóa!", "Thông báo",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Lấy ID từ dòng được chọn
            int id = Convert.ToInt32(dtgvTable.SelectedRows[0].Cells["ID"].Value);

            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa bàn này?",
                                                  "Xác nhận",
                                                  MessageBoxButtons.YesNo,
                                                  MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                if (TableDAL.Instance.DeleteTable(id) == true)
                {
                    MessageBox.Show("Xóa bàn thành công!");
                    LoadListTable();
                    if (deleteTableFood != null)
                        deleteTableFood(this, new EventArgs());
                    RefreshAllTableManagerForms();
                }
            }
        }
        private void btnEditTable_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txbTableID.Text);
            string name = txbTableName.Text;
            string status = txbTableStatus.Text;

            if (TableDAL.Instance.UpdateTable(id, name, status) == true)
            {
                MessageBox.Show("Sửa bàn ăn thành công!");
                LoadListTable(); 
                if (updateFoodCategory != null)
                    updateFoodCategory(this, new EventArgs());
                RefreshAllTableManagerForms();
            }
            else
            {
                MessageBox.Show("Có lỗi khi thực hiện sửa bàn ăn!");
            }
        }


        //----------------------------------------------------------------------------------------------------------------//

        //Phần Admin Tài khoản
        private void btnShowAccount_Click(object sender, EventArgs e)
        {
            LoadListAccount();
        }

        private void btnAddAccount_Click(object sender, EventArgs e)
        {
            ButtonAddAccount f = new ButtonAddAccount();

            f.ShowDialog();
            LoadListAccount();
        }

        private void btnDeleteAccount_Click(object sender, EventArgs e)
        {

            string userName = txbUserName.Text;
            
            DeleteAccount(userName);
        }

        private void btnEditAccount_Click(object sender, EventArgs e)
        {
            string userName = txbUserName.Text;
            string displayName = txbDisplayName.Text;
            int Type= (int)nmrType.Value;

            EditAccount(userName, displayName, Type);
        }


        //Phần Sao lưu phục hồi 
        private string connectionString = "Data Source=DESKTOP-F81P7JH\\MINHPHUC;Initial Catalog=master;Integrated Security=True;Encrypt=False";
        private string databaseName = "QL_QuanCaFe";
        private void LogMessage(string message)
        {
            try
            {
                string timestamp = DateTime.Now.ToString("HH:mm:ss");
                string logEntry = $"[{timestamp}] {message}\r\n";

                // Thêm vào RichTextBox - giả sử tên là richTextBox1
                // Nếu RichTextBox có tên khác, bạn đổi tên cho đúng
                if (richTextBox1.InvokeRequired)
                {
                    richTextBox1.Invoke(new Action(() =>
                    {
                        richTextBox1.AppendText(logEntry);
                        richTextBox1.SelectionStart = richTextBox1.Text.Length;
                        richTextBox1.ScrollToCaret();
                    }));
                }
                else
                {
                    richTextBox1.AppendText(logEntry);
                    richTextBox1.SelectionStart = richTextBox1.Text.Length;
                    richTextBox1.ScrollToCaret();
                }
            }
            catch (Exception ex)
            {
                // Nếu không ghi được log thì bỏ qua
                Console.WriteLine($"Log error: {ex.Message}");
            }
        }  
        private void btnChonDuongDan_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Backup Files (*.bak)|*.bak|All Files (*.*)|*.*";
                saveFileDialog.Title = "Chọn nơi lưu file backup";
                saveFileDialog.DefaultExt = "bak";

                // Tự động đặt tên file theo format: TenDatabase_NgayGio.bak
                string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                saveFileDialog.FileName = $"{databaseName}_Backup_{timestamp}.bak";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    txbDuongDanLuu.Text = saveFileDialog.FileName;
                    LogMessage($"Đã chọn đường dẫn lưu: {saveFileDialog.FileName}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi chọn đường dẫn: {ex.Message}",
                               "Lỗi",
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Error);
                LogMessage($"LỖI: {ex.Message}");
            }
        }

        private void btnBackup_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txbDuongDanLuu.Text))
            {
                MessageBox.Show("Vui lòng chọn đường dẫn lưu file backup!",
                               "Thông báo",
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Disable button để tránh click nhiều lần
                btnBackup.Enabled = false;
                Cursor = Cursors.WaitCursor;

                LogMessage("===========================================");
                LogMessage($"BẮT ĐẦU BACKUP DATABASE: {databaseName}");
                LogMessage($"Đường dẫn lưu: {txbDuongDanLuu.Text}");

                // Tạo câu lệnh SQL Backup
                string backupQuery = $@"
                    BACKUP DATABASE [{databaseName}] 
                    TO DISK = '{txbDuongDanLuu.Text}'
                    WITH FORMAT, 
                    INIT,
                    NAME = '{databaseName}-Full Backup',
                    SKIP,
                    NOREWIND,
                    NOUNLOAD,
                    STATS = 10";

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    LogMessage("Đang kết nối đến SQL Server...");
                    conn.Open();
                    LogMessage("✓ Kết nối thành công!");

                    using (SqlCommand cmd = new SqlCommand(backupQuery, conn))
                    {
                        // Set timeout lớn hơn cho database lớn
                        cmd.CommandTimeout = 600; // 10 phút

                        LogMessage("Đang thực hiện backup...");

                        // Thực thi backup
                        cmd.ExecuteNonQuery();

                        LogMessage("✓ BACKUP HOÀN TẤT THÀNH CÔNG!");
                        LogMessage($"File backup: {txbDuongDanLuu.Text}");

                        // Lấy kích thước file
                        FileInfo fileInfo = new FileInfo(txbDuongDanLuu.Text);
                        double fileSizeMB = fileInfo.Length / (1024.0 * 1024.0);
                        LogMessage($"Kích thước file: {fileSizeMB:F2} MB");
                        LogMessage("===========================================");

                        MessageBox.Show("Backup database thành công!",
                                       "Thành công",
                                       MessageBoxButtons.OK,
                                       MessageBoxIcon.Information);
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                string errorMsg = $"Lỗi SQL: {sqlEx.Message}\n\nChi tiết:\n";
                foreach (SqlError error in sqlEx.Errors)
                {
                    errorMsg += $"- {error.Message}\n";
                }

                LogMessage($"✗ LỖI BACKUP: {sqlEx.Message}");
                MessageBox.Show(errorMsg, "Lỗi Backup", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                LogMessage($"✗ LỖI: {ex.Message}");
                MessageBox.Show($"Lỗi khi backup: {ex.Message}",
                               "Lỗi",
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Error);
            }
            finally
            {
                btnBackup.Enabled = true;
                Cursor = Cursors.Default;
            }
        }

        private void btnChonFile_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Backup Files (*.bak)|*.bak|All Files (*.*)|*.*";
                openFileDialog.Title = "Chọn file backup để restore";
                openFileDialog.DefaultExt = "bak";
                openFileDialog.CheckFileExists = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    txbChonFileBackup.Text = openFileDialog.FileName;

                    // Hiển thị thông tin file
                    FileInfo fileInfo = new FileInfo(openFileDialog.FileName);
                    double fileSizeMB = fileInfo.Length / (1024.0 * 1024.0);

                    LogMessage($"Đã chọn file backup: {openFileDialog.FileName}");
                    LogMessage($"Kích thước: {fileSizeMB:F2} MB | Ngày tạo: {fileInfo.CreationTime:dd/MM/yyyy HH:mm:ss}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi chọn file: {ex.Message}",
                               "Lỗi",
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Error);
                LogMessage($"LỖI: {ex.Message}");
            }
        }

        private void btnRestore_Click(object sender, EventArgs e)
        {
            // Kiểm tra file backup
            if (string.IsNullOrWhiteSpace(txbChonFileBackup.Text))
            {
                MessageBox.Show("Vui lòng chọn file backup để restore!",
                               "Thông báo",
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Warning);
                return;
            }

            if (!File.Exists(txbChonFileBackup.Text))
            {
                MessageBox.Show("File backup không tồn tại!",
                               "Thông báo",
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Warning);
                return;
            }

            // Xác nhận restore
            DialogResult result = MessageBox.Show(
                $"CẢNH BÁO: Restore sẽ thay thế toàn bộ dữ liệu hiện tại của database '{databaseName}'!\n\n" +
                "Tất cả dữ liệu hiện tại sẽ bị XÓA và thay thế bằng dữ liệu từ file backup.\n\n" +
                "Bạn có chắc chắn muốn tiếp tục?",
                "Xác nhận Restore",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result != DialogResult.Yes)
            {
                LogMessage("Người dùng đã hủy restore.");
                return;
            }

            try
            {
                // Disable button
                btnRestore.Enabled = false;
                Cursor = Cursors.WaitCursor;

                LogMessage("===========================================");
                LogMessage($"BẮT ĐẦU RESTORE DATABASE: {databaseName}");
                LogMessage($"Từ file: {txbChonFileBackup.Text}");

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    LogMessage("Đang kết nối đến SQL Server...");
                    conn.Open();
                    LogMessage("✓ Kết nối thành công!");

                    // Bước 1: Chuyển database sang chế độ SINGLE_USER
                    LogMessage("Đang ngắt kết nối người dùng khác...");
                    string setSingleUser = $@"
                ALTER DATABASE [{databaseName}] 
                SET SINGLE_USER 
                WITH ROLLBACK IMMEDIATE";

                    using (SqlCommand cmd = new SqlCommand(setSingleUser, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                    LogMessage("✓ Đã chuyển sang chế độ SINGLE_USER");

                    try
                    {
                        // Bước 2: Thực hiện Restore
                        LogMessage("Đang restore dữ liệu...");
                        string restoreQuery = $@"
                    RESTORE DATABASE [{databaseName}] 
                    FROM DISK = '{txbChonFileBackup.Text}'
                    WITH REPLACE,
                         STATS = 10";

                        using (SqlCommand cmd = new SqlCommand(restoreQuery, conn))
                        {
                            cmd.CommandTimeout = 600; // 10 phút
                            cmd.ExecuteNonQuery();
                        }
                        LogMessage("✓ Restore dữ liệu hoàn tất!");

                        // Bước 3: Chuyển database về chế độ MULTI_USER
                        LogMessage("Đang khôi phục chế độ MULTI_USER...");
                        string setMultiUser = $@"
                    ALTER DATABASE [{databaseName}] 
                    SET MULTI_USER";

                        using (SqlCommand cmd = new SqlCommand(setMultiUser, conn))
                        {
                            cmd.ExecuteNonQuery();
                        }
                        LogMessage("✓ Đã chuyển về chế độ MULTI_USER");

                        LogMessage("✓ RESTORE HOÀN TẤT THÀNH CÔNG!");
                        LogMessage("===========================================");

                        MessageBox.Show("Restore database thành công!",
                                       "Thành công",
                                       MessageBoxButtons.OK,
                                       MessageBoxIcon.Information);
                    }
                    catch
                    {
                        // Nếu restore lỗi, cố gắng chuyển lại về MULTI_USER
                        try
                        {
                            string setMultiUser = $"ALTER DATABASE [{databaseName}] SET MULTI_USER";
                            using (SqlCommand cmd = new SqlCommand(setMultiUser, conn))
                            {
                                cmd.ExecuteNonQuery();
                            }
                        }
                        catch { }

                        throw; // Ném lại exception để xử lý ở catch bên ngoài
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                string errorMsg = $"Lỗi SQL: {sqlEx.Message}\n\nChi tiết:\n";
                foreach (SqlError error in sqlEx.Errors)
                {
                    errorMsg += $"- {error.Message}\n";
                }

                LogMessage($"✗ LỖI RESTORE: {sqlEx.Message}");
                MessageBox.Show(errorMsg, "Lỗi Restore", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                LogMessage($"✗ LỖI: {ex.Message}");
                MessageBox.Show($"Lỗi khi restore: {ex.Message}",
                               "Lỗi",
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Error);
            }
            finally
            {
                btnRestore.Enabled = true;
                Cursor = Cursors.Default;
            }
        }

        //----------------------------------------------------------------------------------------------------------------//

        #endregion

    }
}
