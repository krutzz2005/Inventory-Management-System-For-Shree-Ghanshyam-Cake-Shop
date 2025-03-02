using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Text.RegularExpressions;

namespace welcome
{
    public partial class order_report : Form
    {
        OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\@\welcome\a8.accdb");
        //  OleDbCommand cmd;
        OleDbDataAdapter da = new OleDbDataAdapter();
        DataTable dt = new DataTable();
        project obj = new project();
        DataSet ds = new DataSet();
       
        public order_report()
        {
            InitializeComponent();
        }

        private void oid_rd_CheckedChanged(object sender, EventArgs e)
        {
            if (oid_rd.Checked == true)
            {
                oid_combo.Items.Clear();
                oid_combo.Visible = true;
                ds = new DataSet();
                ds = obj.select_qury("select ord_id from order_master");
                int a = Convert.ToInt32(ds.Tables[0].Rows.Count.ToString());
                for (int i = 0; i < a; i++)
                {
                    oid_combo.Items.Add(ds.Tables[0].Rows[i][0].ToString());
                }

            }
            else
            {
                
                oid_combo.Text = "";
            }
        }

        private void oid_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            con.Open();
            OleDbCommand cmd = new OleDbCommand("select * from order_master where ord_id=" + oid_combo.Text + " ", con);
            OleDbDataReader dr = cmd.ExecuteReader();

            dt.Load(dr);
            //dataGridView1.DataSource = dt;
            con.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string st = Application.StartupPath + "\\report\\order_report.rpt";
            axCrystalReport1.ReportFileName = st;

            if (oid_rd.Checked == true)
            {

                string str = Application.StartupPath + "\\report\\order_report.rpt";
                axCrystalReport1.ReportFileName = str;

                axCrystalReport1.SelectionFormula = "{ order_master.ord_id}=" + oid_combo.Text + "";
            }
            
            else
            {
                axCrystalReport1.SelectionFormula = "{ order_master.ord_id}>0";
            }

            axCrystalReport1.WindowState = Crystal.WindowStateConstants.crptMaximized;
            axCrystalReport1.WindowShowRefreshBtn = true;
            axCrystalReport1.Action = 1;
        }

        private void order_report_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("Are You Want Sure To Exit ?", "Warning Windor", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {
                this.Hide();

            }
        }
    }
}
