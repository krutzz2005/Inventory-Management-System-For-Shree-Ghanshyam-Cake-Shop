using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace welcome
{
    public partial class success : Form
    {
        public success()
        {
            InitializeComponent();
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        int startpos = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            startpos += 2;
            progressBar1.Value = startpos;
            label2.Text = startpos + "%";
            if (progressBar1.Value == 100)
            {
                progressBar1.Value = 0;
                timer1.Stop();
                login f1 = new login();
                f1.Show();
                mdi f2 = new mdi();
                f2.Show();
            }
        }

        private void success_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }
    }
}
