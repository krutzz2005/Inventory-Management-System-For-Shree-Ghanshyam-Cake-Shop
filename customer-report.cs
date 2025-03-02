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
    public partial class customer_report : Form
    {
        OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=G:\@\welcome\a8.accdb");
        //  OleDbCommand cmd;
        OleDbDataAdapter da = new OleDbDataAdapter();
        DataTable dt = new DataTable();
        project obj = new project();
        DataSet ds = new DataSet();
       
        public customer_report()
        {
            InitializeComponent();
        }

        private void cusnm_rd_CheckedChanged(object sender, EventArgs e)
        {
            cusnm_combo.Items.Clear();
            if (cusnm_rd.Checked == true)
            {

                cusnm_combo.Items.Clear();
                cusnm_combo.Visible = true;
                ds = new DataSet();
                ds = obj.select_qury("select c_nm from customer_master");
                int a = Convert.ToInt32(ds.Tables[0].Rows.Count.ToString());
                for (int i = 0; i < a; i++)
                {
                    cusnm_combo.Items.Add(ds.Tables[0].Rows[i][0].ToString());
                }
            }
            else
            {
               
                cusnm_combo.Text = "";
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string st = Application.StartupPath + "\\report\\customer_report.rpt";
            axCrystalReport1.ReportFileName = st;

            if (cusid_rd.Checked == true)
            {

                string str = Application.StartupPath + "\\report\\customer_report.rpt";
                axCrystalReport1.ReportFileName = str;

                axCrystalReport1.SelectionFormula = "{ customer_master.c_id}=" + cusid_combo.Text + "";
            }
            else if (cusnm_rd.Checked == true)
            {
                string str = Application.StartupPath + "\\report\\customer_report.rpt";
                axCrystalReport1.ReportFileName = str;
                axCrystalReport1.SelectionFormula = "{ customer_master.c_nm}='" + cusnm_combo.Text + "' ";
            }
            else
            {
                axCrystalReport1.SelectionFormula = "{ customer_master.c_id}>0";
            }

            axCrystalReport1.WindowState = Crystal.WindowStateConstants.crptMaximized;
            axCrystalReport1.WindowShowRefreshBtn = true;
            axCrystalReport1.Action = 1;
        }

        private void cusid_rd_CheckedChanged(object sender, EventArgs e)
        {
            if (cusid_rd.Checked == true)
            {
                cusid_combo.Items.Clear();
                cusid_combo.Visible = true;
                
                ds = obj.select_qury("select c_id from customer_master");
                int a = Convert.ToInt32(ds.Tables[0].Rows.Count.ToString());
                for (int i = 0; i < a; i++)
                {
                    cusid_combo.Items.Add(ds.Tables[0].Rows[i][0].ToString());
                }

            }
            else
            {
               
                cusid_combo.Text = "";
            }
        }

        private void cusid_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            con.Open();
            OleDbCommand cmd = new OleDbCommand("select * from customer_master where c_id=" + cusid_combo.Text + " ", con);
            OleDbDataReader dr = cmd.ExecuteReader();
           
            dt.Load(dr);
            //dataGridView1.DataSource = dt;
            con.Close();
        }

        private void cusnm_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            con.Open();
            OleDbCommand cmd = new OleDbCommand("select * from customer_master where c_nm='" + cusnm_combo.Text + "' ", con);
            var r = cmd.ExecuteReader();
            
            dt.Load(r);
            // dataGridView1.DataSource = dt;
            con.Close();
        }

        private void axCrystalReport1_Enter(object sender, EventArgs e)
        {

        }

        private void customer_report_Load(object sender, EventArgs e)
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
