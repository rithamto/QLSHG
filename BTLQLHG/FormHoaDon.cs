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
    public partial class FormHoaDon : Form
    {
        SqlConnection connection;
        SqlCommand command;
        string str = @"Data Source=DESKTOP-4M4HACO;Initial Catalog=QLHG;User ID=sa; Password=demo123";
        SqlDataAdapter adapter = new SqlDataAdapter();
        DataTable table = new DataTable();
        public FormHoaDon()
        {
            InitializeComponent();
        }
        private void LoadData()
        {
            command = connection.CreateCommand();
            command.CommandText = "Select MaHD, MaKH, NgayMua from HOADON";
            adapter.SelectCommand = command;
            table.Clear();
            adapter.Fill(table);
            dgvThongTinHD.DataSource = table;
        }
        private void FormHoaDon_Load(object sender, EventArgs e)
        {
            connection = new SqlConnection(str);
            connection.Open();
            LoadComboBoxMaKH();
            LoadData();

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
                command = connection.CreateCommand();
                command.CommandText = "Insert Into HOADON(MaHD, MaKH, NgayMua) values(@mahd, @makh, @ngaymua)";
                command.Parameters.Add("@mahd", SqlDbType.NVarChar).Value = txtMaHD.Text;
                command.Parameters.Add("@makh", SqlDbType.NVarChar).Value = CbbMaKH.SelectedValue;
                command.Parameters.Add("@ngaymua", SqlDbType.DateTime).Value = Datetime.Text;
                command.ExecuteNonQuery();
                LoadData();
                MessageBox.Show("Bạn đã thêm dữ liệu thành công!");
        }
        private void btnXoa_Click(object sender, EventArgs e)
        {
            command = connection.CreateCommand();
            command.CommandText = "delete from HOADON Where MaHD= '" +txtMaHD.Text + "'";
            command.ExecuteNonQuery();
            LoadData();
            MessageBox.Show("Bạn đã xoá dữ liệu thành công!");
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            
            command = connection.CreateCommand();
            command.CommandText = "Update HOADON Set MaKH='" + CbbMaKH.SelectedValue + "',NgayMua='" + Datetime.Text + "' where MaHD ='" + txtMaHD.Text + "'";
            command.ExecuteNonQuery();
            LoadData();
            MessageBox.Show("Bạn đã sửa dữ liệu thành công!");
        }

        private void btnKhoiTao_Click(object sender, EventArgs e)
        {
            txtMaHD.ReadOnly = false;
            txtMaHD.Text = "";
            CbbMaKH.Text = "";
            Datetime.Text = "";
            LoadData();
            MessageBox.Show("Bạn đã khởi tạo dữ liệu thành công!");
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            HomeForm but = new HomeForm();
            but.FormClosed += new FormClosedEventHandler(FormHoaDon_Load);
            but.Show();
            this.Hide();
        }

        private void dgvThongTinHD_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMaHD.ReadOnly = true;
            int rowindex = dgvThongTinHD.CurrentCell.RowIndex;
            txtMaHD.Text = dgvThongTinHD.Rows[rowindex].Cells[0].Value.ToString();
            CbbMaKH.SelectedValue = dgvThongTinHD.Rows[rowindex].Cells[1].Value.ToString();
            Datetime.Text = dgvThongTinHD.Rows[rowindex].Cells[2].Value.ToString();
        }

        private void cbbxem_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbxem.Text == "Thông Tin Sản Phẩm")
            {
                FormSanPham form = new FormSanPham();
                form.Show();
            }
            if (cbbxem.Text == "Thông Tin Khách Hàng")
            {
                FormKhachHang form = new FormKhachHang();
                form.Show();
            }
            
        }
        public void LoadComboBoxMaKH()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("Select MaKH From KHACHHANG", connection);
                da.Fill(dt);
                CbbMaKH.DataSource = dt;
                CbbMaKH.DisplayMember = "MaKH";
                CbbMaKH.ValueMember = "MaKH";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi khi load dữ liệu!\n", ex.ToString());
            }
        }
  
    }
}
