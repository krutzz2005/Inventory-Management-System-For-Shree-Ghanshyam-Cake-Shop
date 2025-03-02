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
    public partial class abouts_us : Form
    {
        public abouts_us()
        {
            InitializeComponent();
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void abouts_us_Load(object sender, EventArgs e)
        {
            label23.Text = DateTime.Now.ToString("dd:mm:yyyy");
            label24.Text = DateTime.Now.ToString("hh:mm:ss");
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click_1(object sender, EventArgs e)
        {

        }
    }
}
