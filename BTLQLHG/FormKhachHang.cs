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
    public partial class FormKhachHang : Form
    {
        SqlConnection connection;
        SqlCommand command;
        string str = @"Data Source=DESKTOP-4M4HACO;Initial Catalog=QLHG;User ID=sa; Password=demo123";
        SqlDataAdapter adapter = new SqlDataAdapter();
        DataTable table = new DataTable();
        public FormKhachHang()
        {
            InitializeComponent();
        }
        private void LoadData()
        {
            command = connection.CreateCommand();
            command.CommandText = "Select *from KHACHHANG";
            adapter.SelectCommand = command;
            table.Clear();
            adapter.Fill(table);
            dgvThongTinKH.DataSource = table;
        }
        private void FormKhachHang_Load(object sender, EventArgs e)
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
                command.CommandText = "Insert Into KHACHHANG values('" + txtMaKH.Text + "',N'" + txtTenKh.Text + "','" + DateNS.Text + "',N'" + txtDiachi.Text + "','" + txtSDT.Text + "')";
                command.ExecuteNonQuery();
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi khi load dữ liệu!\n", ex.ToString());
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                command = connection.CreateCommand();
                command.CommandText = "delete from KHACHHANG Where MaKH= '" + txtMaKH.Text + "'";
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
                command.CommandText = "Update KHACHHANG Set" +
                    " TenKH=N'" + txtTenKh.Text + "',NgaySinh = '" + DateNS.Text + "',DiaChi=N'" + txtDiachi.Text + "',SDT = '" + txtSDT.Text + "' Where MaKH= '" + txtMaKH.Text + "'";
                command.ExecuteNonQuery();
                LoadData();
                MessageBox.Show("Bạn đã Sửa dữ liệu thành công!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi khi load dữ liệu!\n", ex.ToString());
            }
        }

        private void btnKhoiTao_Click(object sender, EventArgs e)
        {
            txtMaKH.ReadOnly = false; 
            txtMaKH.Text = "";
            txtTenKh.Text = "";
            DateNS.Text = "2021/12/12";
            txtSDT.Text = "";
            txtDiachi.Text = "";
            LoadData();
            MessageBox.Show("Bạn đã khởi tạo dữ liệu thành công!");
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            HomeForm but = new HomeForm();
            but.FormClosed += new FormClosedEventHandler(FormKhachHang_Load);
            but.Show();
            this.Hide();
        }
        private void dgvThongTinKH_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMaKH.ReadOnly = true;
            int rowindex = dgvThongTinKH.CurrentCell.RowIndex;
            txtMaKH.Text = dgvThongTinKH.Rows[rowindex].Cells[0].Value.ToString();
            txtTenKh.Text = dgvThongTinKH.Rows[rowindex].Cells[1].Value.ToString();
            DateNS.Text = dgvThongTinKH.Rows[rowindex].Cells[2].Value.ToString();
            txtDiachi.Text = dgvThongTinKH.Rows[rowindex].Cells[3].Value.ToString();
            txtSDT.Text = dgvThongTinKH.Rows[rowindex].Cells[4].Value.ToString();
        }
        private void TimKiemLoad()
        {
            command = connection.CreateCommand();
            command.CommandText = "Select * from KHACHHANG Where TenKH like N'%"+Timkiem.Text.Trim()+"%'";
            adapter.SelectCommand = command;
            table.Clear();
            adapter.Fill(table);
            dgvThongTinKH.DataSource = table;
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
