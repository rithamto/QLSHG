using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using FontAwesome.Sharp;
using System.Runtime.InteropServices;

namespace BTLQLHG
{
    public partial class FormNhanVien : Form
    {
        SqlConnection connection;
        SqlCommand command;
        string str = @"Data Source=DESKTOP-4M4HACO;Initial Catalog=QLHG;User ID=sa; Password=demo123";
        SqlDataAdapter adapter = new SqlDataAdapter();
        DataTable table = new DataTable();
        public FormNhanVien()
        {
            InitializeComponent();
        }
        private void LoadData()
        {
            command = connection.CreateCommand();
            command.CommandText = "Select *from NHANVIEN";
            adapter.SelectCommand = command;
            table.Clear();
            adapter.Fill(table);
            dgvThongTinNV.DataSource = table;
        }
        private void FormNhanVien_Load(object sender, EventArgs e)
        {
            connection = new SqlConnection(str);
            connection.Open();
            LoadData();
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                command = connection.CreateCommand();
                command.CommandText = "Insert Into NHANVIEN " +
                    "values('" + TxtMNV.Text + "','" + TxtTenNV.Text + "','" + DateNS.Text + "','" + TxtDiachi.Text + "','" + TxtCMT.Text + "','" + TxtEmail.Text + "','" + TxtDT.Text + "','" + DateNVL.Text + "','" + CbChucVu.Text + "')";
                command.ExecuteNonQuery();
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi khi load dữ liệu!\n", ex.ToString());
            }
     
        }

        private void dgvThongTinNV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            TxtMNV.ReadOnly = true;
            int rowindex = dgvThongTinNV.CurrentCell.RowIndex;
            TxtMNV.Text = dgvThongTinNV.Rows[rowindex].Cells[0].Value.ToString();
            TxtTenNV.Text = dgvThongTinNV.Rows[rowindex].Cells[1].Value.ToString();
            DateNS.Text = dgvThongTinNV.Rows[rowindex].Cells[2].Value.ToString();
            TxtDiachi.Text = dgvThongTinNV.Rows[rowindex].Cells[3].Value.ToString();
            TxtCMT.Text = dgvThongTinNV.Rows[rowindex].Cells[4].Value.ToString();
            TxtEmail.Text = dgvThongTinNV.Rows[rowindex].Cells[5].Value.ToString();
            TxtDT.Text = dgvThongTinNV.Rows[rowindex].Cells[6].Value.ToString();
            DateNS.Text = dgvThongTinNV.Rows[rowindex].Cells[7].Value.ToString();
            CbChucVu.SelectedItem = dgvThongTinNV.Rows[rowindex].Cells[8].Value.ToString();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                command = connection.CreateCommand();
                command.CommandText = "delete from NHANVIEN Where MaNV= '" + TxtMNV.Text + "'";
                command.ExecuteNonQuery();
                LoadData();
                MessageBox.Show("Bạn đã xoá dữ liệu thành công!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi khi load dữ liệu!\n", ex.ToString());
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                command = connection.CreateCommand();
                command.CommandText = "Update NHANVIEN Set TenNV=N'" + TxtTenNV.Text + "', NgaySinh='" + DateNS.Text + "',DiaChi='" + TxtDiachi.Text + "',IDCMND='" + TxtCMT.Text + "',Email = '" + TxtEmail.Text + "',SDT = '" + TxtDT.Text + "',NgayVaoLam='" + DateNVL.Text + "',MaCV = '" + CbChucVu.Text + "'where MaNV = '" + TxtMNV.Text + "'";
                command.ExecuteNonQuery();
                LoadData();
                MessageBox.Show("Bạn đã sửa dữ liệu thành công!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi khi load dữ liệu!\n", ex.ToString());
            }
        }
        private void btnKhoiTao_Click(object sender, EventArgs e)
        {
            TxtMNV.ReadOnly = false;
            TxtMNV.Text = "";
            TxtTenNV.Text = "";
            DateNS.Text = "2021/1/1";
            TxtDiachi.Text= "";
            TxtCMT.Text = "";
            TxtEmail.Text = "";
            TxtDT.Text = "";
            DateNVL.Text = "2021/1/1";
            CbChucVu.Text = "";
            LoadData();
            MessageBox.Show("Bạn đã khởi tạo dữ liệu thành công!");
        }
        private void TimKiemLoad()
        {
            command = connection.CreateCommand();
            command.CommandText = "Select * from NHANVIEN Where TenNV like N'%"+Timkiem.Text.Trim()+"%'";
            adapter.SelectCommand = command;
            table.Clear();
            adapter.Fill(table);
            dgvThongTinNV.DataSource = table;
        }
        private void btnThoat_Click(object sender, EventArgs e)
        {
            HomeForm but = new HomeForm();
            but.FormClosed += new FormClosedEventHandler(FormNhanVien_Load);
            but.Show();
            this.Hide();
        }

        private void btnTimkiem_Click(object sender, EventArgs e)
        {
            TimKiemLoad();
        }

        private void Timkiem_TextChanged(object sender, EventArgs e)
        {
            TimKiemLoad();
        }
    }
}
    