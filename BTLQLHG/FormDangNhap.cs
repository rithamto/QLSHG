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
    public partial class FormDangNhap : Form
    {
        const int WM_NCHITTEST = 0x84;
        const int HTCLIENT = 0x1;
        const int HTCAPTION = 0x2;
        public FormDangNhap()
        {
            InitializeComponent();
        }
        protected override void WndProc(ref Message message)
        {
            base.WndProc(ref message);

            if (message.Msg == WM_NCHITTEST && (int)message.Result == HTCLIENT)
                message.Result = (IntPtr)HTCAPTION;
        }
        private void BtnDN_Click_1(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-4M4HACO;Initial Catalog=QLHG;User ID=sa; Password=demo123");
            con.Open();
            string tk = TxtTK.Text;
            string mk = TxtMK.Text;
            string sql = "SELECT * FROM QUANLY Where UserName = '" + tk + "' and PasswordHint ='" + mk + "'";
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader data = cmd.ExecuteReader();
            if (data.Read() == true)
            {

                HomeForm but = new HomeForm();
                but.FormClosed += new FormClosedEventHandler(FormDangNhap_Load);
                but.Show();
                this.Hide();

            }
            else
            {
                MessageBox.Show("Sai tài khoản/mật khẩu" + "\nMời bạn nhập lại!!");
            }
        }
        private void FormDangNhap_Load(object sender, EventArgs e)
        {
            this.Show();
        }

        private void BtnThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
