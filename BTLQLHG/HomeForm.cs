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

    public partial class HomeForm : Form
    {
        private IconButton currentBtn;
        private Panel leftBorderBtn;
        private Form currentForm;
        public HomeForm()
        {
            InitializeComponent();
            leftBorderBtn = new Panel();
            leftBorderBtn.Size = new Size(7, 60);
            PanelSlideMenu.Controls.Add(leftBorderBtn);

            this.Text = string.Empty;
            this.ControlBox = false;
            this.DoubleBuffered = true;
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
        }
        const int WM_NCHITTEST = 0x84;
        const int HTCLIENT = 0x1;
        const int HTCAPTION = 0x2;
        protected override void WndProc(ref Message message)
        {
            base.WndProc(ref message);

            if (message.Msg == WM_NCHITTEST && (int)message.Result == HTCLIENT)
                message.Result = (IntPtr)HTCAPTION;
        }

        private void HomeForm_Load(object sender, EventArgs e)
        {
            this.Show();
        }
        // struct
        private struct colors
        {
            public static Color color1 = Color.FromArgb(192, 192, 255);

        }
        private void ActivateButton(object senderBtn, Color color)
        {
            if (senderBtn != null)
            {
                DisableButton();
                //Button
                currentBtn = (IconButton)senderBtn;
                currentBtn.BackColor = Color.FromArgb(192, 255, 192);
                currentBtn.ForeColor = color;
                currentBtn.TextAlign = ContentAlignment.MiddleLeft;
                currentBtn.IconColor = color;
                currentBtn.TextImageRelation = TextImageRelation.TextBeforeImage;
                currentBtn.ImageAlign = ContentAlignment.MiddleRight;
                // left Boder button
                leftBorderBtn.BackColor = color;
                leftBorderBtn.Location = new Point(0, currentBtn.Location.Y);
                leftBorderBtn.Visible = true;
                leftBorderBtn.BringToFront();
                //IconCurrrnt form
                iconCurrent.IconChar = currentBtn.IconChar;
                iconCurrent.IconColor = color;

            }
        }
        private void DisableButton()
        {
            if (currentBtn != null)
            {
                currentBtn.BackColor = Color.FromArgb(128, 255, 128);
                currentBtn.ForeColor = Color.Blue;
                currentBtn.TextAlign = ContentAlignment.MiddleLeft;
                currentBtn.IconColor = Color.Black;
                currentBtn.TextImageRelation = TextImageRelation.ImageBeforeText;
                currentBtn.ImageAlign = ContentAlignment.MiddleRight;
            }
        }
        private void BtnNhanVien_Click_1(object sender, EventArgs e)
        {
            ActivateButton(sender, colors.color1);
            OpenForm(new FormNhanVien());
        }
        private void btnKhachHang_Click_1(object sender, EventArgs e)
        {
            ActivateButton(sender, colors.color1);
            OpenForm(new FormKhachHang());
        }
        private void BtnSanPham_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, colors.color1);
            OpenForm(new FormSanPham());
        }
        private void btnHoaDon_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, colors.color1);
            OpenForm(new FormHoaDon());
        }
        private void label3_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void btnHome_Click(object sender, EventArgs e)
        {
            currentForm.Close();
            Reset();
        }
        private void Reset()
        {
            DisableButton();
            leftBorderBtn.Visible = false;
            iconCurrent.IconChar = IconChar.Home;
            iconCurrent.IconColor = Color.Black;
            LabelHome.Text = "Home";
        }
        //di chuyển cửa sổ
        [DllImport("user32.dll", EntryPoint = "ReleaseCapture")]
        public static extern void ReleaseCapture();

        [DllImport("user32.dll", EntryPoint = "SendMessage")]
        public static extern void SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        private void guna2Panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
        private void OpenForm( Form form)
        {
            if (currentForm != null)
            {
                //open only form
                currentForm.Close();
            }
            currentForm = form;
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;
            panelGreen.Controls.Add(form);
            panelGreen.Tag = form;
            form.BringToFront();
            form.Show();
            LabelHome.Text = form.Text;
        }
        private void btnhome_Click(object sender, EventArgs e)
        {
            currentForm.Close();
            Reset();
        }
        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, colors.color1);
            FormDangNhap but = new FormDangNhap();
            but.FormClosed += new FormClosedEventHandler(HomeForm_Load);
            but.Show();
            this.Hide();
        }

        private void btnChitietHD_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, colors.color1);
            OpenForm(new FormCTHD());
        }
    } 
}
