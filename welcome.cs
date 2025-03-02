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
    public partial class welcome : Form
    {
        public welcome()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label21_Click(object sender, EventArgs e)
        {

        }
        int startpos = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            startpos += 2;
            progressBar1.Value=startpos;
            label10.Text = startpos + "%";
            if (progressBar1.Value == 100)
            {
                progressBar1.Value = 0;
                timer1.Stop();
                login f1 = new login();
                f1.Show();
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Start();
            timer2.Enabled = true;
            label23.Text = DateTime.Now.ToString("dd:mm:yyyy");
            label24.Text = DateTime.Now.ToString("hh:mm:ss tt");
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            

        }

        private void label23_Click(object sender, EventArgs e)
        {
           

        }

        private void label24_Click(object sender, EventArgs e)
        {
           
        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
