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
namespace BTLQLHG
{
    public partial class FormSanPham : Form
    {
        SqlConnection connection;
        SqlCommand command;
        string str = @"Data Source=DESKTOP-4M4HACO;Initial Catalog=QLHG;User ID=sa; Password=demo123";
        SqlDataAdapter adapter = new SqlDataAdapter();
        DataTable table = new DataTable();
        public FormSanPham()
        {
            InitializeComponent();
        }
        private void LoadData()
        {
            command = connection.CreateCommand();
            command.CommandText = "Select *from SANPHAM";
            adapter.SelectCommand = command;
            table.Clear();
            adapter.Fill(table);
            dgvThongTinSP.DataSource = table;
        }
        private void FormSanPham_Load(object sender, EventArgs e)
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
                command.CommandText = "Insert Into SANPHAM values(@masp, @tensp, @gia, @soluong, @trongluong, @nhacc)";
                command.Parameters.Add("@masp", SqlDbType.NVarChar).Value = TxtMSP.Text;
                command.Parameters.Add("@tensp", SqlDbType.NVarChar).Value = txtTenSP.Text;
                command.Parameters.Add("@gia", SqlDbType.Int).Value = txtGia.Text;
                command.Parameters.Add("@soluong", SqlDbType.Int).Value = txtSoLuong.Text;
                command.Parameters.Add("@trongluong", SqlDbType.NVarChar).Value = txtTrongluong.Text;
                command.Parameters.Add("@nhacc", SqlDbType.NVarChar).Value = cbbNCC.Text;
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
                command.CommandText = "delete from SANPHAM Where MaSP= '" + TxtMSP.Text + "'";
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
                command.CommandText = "Update SANPHAM Set " +
                "TenSP=N'" + txtTenSP.Text + "', GiaSP = '" + txtGia.Text + "', SoLuong ='" + txtSoLuong.Text + "' ,TrongLuong ='" + txtTrongluong.Text + "' , MaNCC = '" + cbbNCC.Text + "' where MaSP ='" + TxtMSP.Text + "'";
                command.ExecuteNonQuery();
                LoadData();
                MessageBox.Show("Bạn đã Sửa dữ liệu thành công!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi khi load dữ liệu!\n", ex.ToString());
            }
        }

        private void dgvThongTinSP_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            TxtMSP.ReadOnly = true;
            int rowindex = dgvThongTinSP.CurrentCell.RowIndex;
            TxtMSP.Text = dgvThongTinSP.Rows[rowindex].Cells[0].Value.ToString();
            txtTenSP.Text = dgvThongTinSP.Rows[rowindex].Cells[1].Value.ToString();
            txtGia.Text = dgvThongTinSP.Rows[rowindex].Cells[2].Value.ToString();
            txtSoLuong.Text = dgvThongTinSP.Rows[rowindex].Cells[3].Value.ToString();
            txtTrongluong.Text = dgvThongTinSP.Rows[rowindex].Cells[4].Value.ToString();
            cbbNCC.Text = dgvThongTinSP.Rows[rowindex].Cells[5].Value.ToString();
        }

        private void btnKhoiTao_Click(object sender, EventArgs e)
        {
            TxtMSP.ReadOnly = false;
            TxtMSP.Text = "";
            txtTenSP.Text = "";
            txtGia.Text = "";
            txtSoLuong.Text = "";
            txtTrongluong.Text = "";
            cbbNCC.Text = "";
            LoadData();
            MessageBox.Show("Bạn đã khởi tạo dữ liệu thành công!");
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            HomeForm but = new HomeForm();
            but.FormClosed += new FormClosedEventHandler(FormSanPham_Load);
            but.Show();
            this.Hide();
        }
        private void TimKiemLoad()
        {
            command = connection.CreateCommand();
            command.CommandText = "Select * from SANPHAM Where TenSP like N'%" + Timkiem.Text.Trim() + "%'";
            adapter.SelectCommand = command;
            table.Clear();
            adapter.Fill(table);
            dgvThongTinSP.DataSource = table;
        }
        private void Timkiem_TextChanged(object sender, EventArgs e)
        {
            TimKiemLoad();
        }

        private void btnTimkiem_Click(object sender, EventArgs e)
        {
            TimKiemLoad();
        }
    }
}
