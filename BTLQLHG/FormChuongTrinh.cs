using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BTLQLHG
{
    public partial class FormChuongTrinh : Form
    {
        const int WM_NCHITTEST = 0x84;
        const int HTCLIENT = 0x1;
        const int HTCAPTION = 0x2;
        public FormChuongTrinh()
        {
            InitializeComponent();
        }
        int startPoint = 0;
        private void FormChuongTrinh_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }
        protected override void WndProc(ref Message message)
        {
            base.WndProc(ref message);

            if (message.Msg == WM_NCHITTEST && (int)message.Result == HTCLIENT)
                message.Result = (IntPtr)HTCAPTION;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            startPoint += 5;
            Prosbar.Value = startPoint;
            if (Prosbar.Value == 100)
            {
                Prosbar.Value = 0;
                timer1.Stop();
                FormDangNhap log = new FormDangNhap();
                this.Hide();
                log.Show();
            }
        }
    }
}
