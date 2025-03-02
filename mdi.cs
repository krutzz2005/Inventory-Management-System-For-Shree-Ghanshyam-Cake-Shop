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
    public partial class mdi : Form
    {
        public mdi()
        {
            InitializeComponent();
        }

        private void notpadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("notepad.exe");
        }

        private void calculatorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("calc.exe");
        }

        private void browserToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void googleChromeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("Chrome.exe");
        }

        private void moozilaFirefoxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("Firefox.exe");
        }

        private void microsoftEdgeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void mdi_Load(object sender, EventArgs e)
        {

        }

        private void contectUsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void contectUsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
           
         
        }

        private void contactUsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            contect_us c1 = new contect_us();
            c1.Show();
        }

        private void aboutUsToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void aboutUsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            abouts_us a1 = new abouts_us();
            a1.Show();
        }

        private void masterToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("Are You Want Sure To Exit ?", "Warning Windor", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            login l1 = new login();
            l1.Show();
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            change_pass cp = new change_pass();
            cp.Show();
        }

        private void salesDetailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sales_master sm = new sales_master();
            sm.Show();
        }

        private void billMasterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bill1 b1 = new bill1();
            b1.Show();
        }

        private void customerMasterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            customer_master c10 = new customer_master();
            c10.Show();
        }

        private void orderMasterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            order_master o10 = new order_master();
            o10.Show();
        }

        private void stockMasterToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            stock_master s10 = new stock_master();
            s10.Show();
        }

        private void reportToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void customerReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            customer_report cr = new customer_report();
            cr.Show();
        }

        private void itemReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            item_report ir = new item_report();
            ir.Show();
        }

        private void salesReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sales_report sr = new sales_report();
            sr.Show();
        }

        private void orderReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            order_report or = new order_report();
            or.Show();
        }

        private void itemMasterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            item_master im = new item_master();
            im.Show();
        }

        private void stockReportToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            help h = new help();
            h.Show();
        }
    }
}
