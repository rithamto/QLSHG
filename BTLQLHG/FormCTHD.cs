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
    public partial class FormCTHD : Form
    {
        SqlConnection connection;
        SqlCommand command;
        string str = @"Data Source=DESKTOP-4M4HACO;Initial Catalog=QLHG;User ID=sa; Password=demo123";
        SqlDataAdapter adapter = new SqlDataAdapter();
        DataTable table = new DataTable();
        public FormCTHD()
        {
            InitializeComponent();
        }
        private void LoadData()
        {
            command = connection.CreateCommand();
            command.CommandText = "Select CTHD.STT,CTHD.MaHD, CTHD.MaSP,CTHD.TenSP,CTHD.MaKH," +
                "CTHD.TenKH, CTHD.NgayMua, CTHD.SoLuong,CTHD.GiaSP, CTHD.GiaSP*CTHD.SoLuong As TongTien From CTHD";
            adapter.SelectCommand = command;
            table.Clear();
            adapter.Fill(table);
            dgvThongTinHD.DataSource = table;
        }
        private void FormCTHD_Load(object sender, EventArgs e)
        {
            connection = new SqlConnection(str);
            connection.Open();
            LoadComboBoxMaHD();
            LoadComboBoxMasp();
            LoadComboBoxMaKH();
            TongTien();
            LoadData();
           
        }

        private void cbbxem_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cbbxem.Text =="Thông Tin Sản Phẩm")
            {
                FormSanPham form = new FormSanPham();
                form.Show();
            }
            if(cbbxem.Text == "Thông Tin Khách Hàng")
            {
                FormKhachHang form = new FormKhachHang();
                form.Show();
            }
            if(cbbxem.Text == "Thông Tin Hoá Đơn")
            {
                FormHoaDon form = new FormHoaDon();
                form.Show();
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                command = connection.CreateCommand();
                command.CommandText = "Insert Into CTHD(STT, MaHD, MaKH, TenKH, MaSP, TenSP, NgayMua, SoLuong, GiaSP) values(@stt, @mahd, @makh, @TenKH, @masp ,@tensp, @ngaymua, @soluong, @gia)";
                command.Parameters.Add("@stt", SqlDbType.Int).Value = txtSTT.Text;
                command.Parameters.Add("@mahd", SqlDbType.NVarChar).Value = CbbMaHD.SelectedValue;
                command.Parameters.Add("@makh", SqlDbType.NVarChar).Value = CbbMaKH.SelectedValue;
                command.Parameters.Add("@TenKH", SqlDbType.NVarChar).Value = CbbTenKh.SelectedValue;
                command.Parameters.Add("@masp", SqlDbType.NVarChar).Value = cbbMasp.SelectedValue;
                command.Parameters.Add("@tensp", SqlDbType.NVarChar).Value = cbbTenSP.SelectedValue;
                command.Parameters.Add("@ngaymua", SqlDbType.DateTime).Value = Datetime.Text;
                command.Parameters.Add("@soluong", SqlDbType.Int).Value = txtSoLuong.Text;
                command.Parameters.Add("@gia", SqlDbType.Int).Value = cbbgia.SelectedValue;
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
                command.CommandText = "delete from CTHD Where STT= '" + txtSTT.Text + "'";
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
                command.CommandText = "Update CTHD Set MaHD='" + CbbMaHD.SelectedValue + "', MaSP='" + cbbMasp.SelectedValue + "',TenSP =N'" + cbbTenSP.SelectedValue + "',MaKH='" + CbbMaKH.SelectedValue + "',TenKH=N'" + CbbTenKh.SelectedValue + "',NgayMua='" + Datetime.Text + "',SoLuong ='" + txtSoLuong.Text + "', GiaSP='" + cbbgia.SelectedValue + "',TongTien='" + txtTongTien.Text + "' where STT ='" + txtSTT.Text + "'";
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
            txtSTT.ReadOnly = false;
            txtSTT.Text = "";
            CbbMaHD.Text = "";
            CbbMaKH.Text = "";
            CbbTenKh.Text = "";
            cbbTenSP.Text = "";
            CbbTenKh.Text = "";
            txtSoLuong.Text = "";
            Datetime.Text = "";
            cbbgia.Text = "";
            txtTongTien.Text = "";
            LoadData();
            MessageBox.Show("Bạn đã khởi tạo dữ liệu thành công!");
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            HomeForm but = new HomeForm();
            but.FormClosed += new FormClosedEventHandler(FormCTHD_Load);
            but.Show();
            this.Hide();
        }

        private void dgvThongTinHD_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
     
            int sl = 0; decimal giaban, tongcong = 0;
            txtTongTien.ReadOnly = true;
            txtSTT.ReadOnly = true;
            int rowindex = dgvThongTinHD.CurrentCell.RowIndex;
            txtSTT.Text = dgvThongTinHD.Rows[rowindex].Cells[0].Value.ToString();
            CbbMaHD.SelectedValue = dgvThongTinHD.Rows[rowindex].Cells[1].Value.ToString();
            cbbMasp.SelectedValue = dgvThongTinHD.Rows[rowindex].Cells[2].Value.ToString();
            cbbTenSP.SelectedValue = dgvThongTinHD.Rows[rowindex].Cells[3].Value.ToString();
            CbbMaKH.SelectedValue = dgvThongTinHD.Rows[rowindex].Cells[4].Value.ToString();
            CbbTenKh.SelectedValue = dgvThongTinHD.Rows[rowindex].Cells[5].Value.ToString();
            Datetime.Text = dgvThongTinHD.Rows[rowindex].Cells[6].Value.ToString();
            txtSoLuong.Text = dgvThongTinHD.Rows[rowindex].Cells[7].Value.ToString();
            cbbgia.SelectedValue = dgvThongTinHD.Rows[rowindex].Cells[8].Value.ToString();
            txtTongTien.Text = dgvThongTinHD.Rows[rowindex].Cells[9].Value.ToString();

            giaban = Convert.ToDecimal(dgvThongTinHD.Rows[rowindex].Cells[8].Value);
            sl = Convert.ToInt32(dgvThongTinHD.Rows[rowindex].Cells[7].Value);
            dgvThongTinHD.Rows[rowindex].Cells[9].Value = giaban * sl;
            tongcong += giaban * sl;
            txtTongTien.Text = tongcong.ToString();
        }
        public void LoadComboBoxMasp()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("Select MaSP,TenSP, GiaSP From SANPHAM", connection);
                da.Fill(dt);
                cbbMasp.DataSource = dt;
                cbbMasp.DisplayMember = "MaSP";
                cbbTenSP.DisplayMember = "TenSP";
                cbbgia.DisplayMember = "GiaSP";
                cbbMasp.ValueMember = "MaSP";
                cbbTenSP.ValueMember = "TenSP";
                cbbgia.ValueMember = "GiaSP";
                cbbMasp.DataSource = dt;
                cbbTenSP.DataSource = dt;
                cbbgia.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi khi load dữ liệu!\n", ex.ToString());
            }
        }
        public void LoadComboBoxMaHD()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("Select MaHD From HOADON", connection);
                da.Fill(dt);
                CbbMaHD.DataSource = dt;
                CbbMaHD.DisplayMember = "MaHD";
                CbbMaHD.ValueMember = "MaHD";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi khi load dữ liệu!\n", ex.ToString());
            }
        }
        public void LoadComboBoxMaKH()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("Select MaKH, TenKH From KHACHHANG", connection);
                da.Fill(dt);
                CbbMaKH.DataSource = dt;
                CbbMaKH.DisplayMember = "MaKH";
                CbbMaKH.ValueMember = "MaKh";
                CbbTenKh.DisplayMember = "TenKH";
                CbbTenKh.ValueMember = "TenKh";
                CbbTenKh.DataSource = dt;
                CbbMaKH.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi khi load dữ liệu!\n", ex.ToString());
            }
        }
        private void TimKiemLoad()
        {
            command = connection.CreateCommand();
            command.CommandText = "Select * from CTHD Where MaHD like N'%" + TimKiem.Text.Trim() + "%'";
            adapter.SelectCommand = command;
            table.Clear();
            adapter.Fill(table);
            dgvThongTinHD.DataSource = table;
        }
        private void TimKiem_TextChanged_1(object sender, EventArgs e)
        {
            TimKiemLoad();
        }
        private void TongTien()
        {
            string sql = "select sum(TongTien) as Tonghd From CTHD ";
            SqlCommand cmd = new SqlCommand(sql, connection);
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                txtTHD.Text = dr["Tonghd"].ToString();
            }
            dr.Close();
        }
           
    }
}