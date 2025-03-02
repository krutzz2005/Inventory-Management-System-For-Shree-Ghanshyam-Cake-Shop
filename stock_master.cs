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
    public partial class stock_master : Form
    {
        OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=F:\@\welcome\a8.accdb");
        //  OleDbCommand cmd;
        OleDbDataAdapter da = new OleDbDataAdapter();
        DataTable dt = new DataTable();
        project obj = new project();
        DataSet ds = new DataSet();
        int r = 0;
        public stock_master()
        {
            InitializeComponent();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void stock_master_Load(object sender, EventArgs e)
        {
           // label23.Text = DateTime.Now.ToString("dd:mm:yyyy");
            //label24.Text = DateTime.Now.ToString("hh:mm:ss");


          
           
            //data_grid();
        }
        public void data_grid()
        {
            ds = obj.select_qury("select * from stock_master");
            dataGridView1.DataSource = ds.Tables[0];

        }

        private void iid_rd_CheckedChanged(object sender, EventArgs e)
        {
            if (iid_rd.Checked == true)
            {
                iid_combo.Items.Clear();
                iid_combo.Visible = true;
                ds = new DataSet();
                ds = obj.select_qury("select it_id from stock_master");
                int a = Convert.ToInt32(ds.Tables[0].Rows.Count.ToString());
                for (int i = 0; i < a; i++)
                {
                    iid_combo.Items.Add(ds.Tables[0].Rows[i][0].ToString());
                }
            }
            else
            {
               // iid_combo.Visible = false;
                iid_combo.Text = "";
            }
        }

        private void inm_rd_CheckedChanged(object sender, EventArgs e)
        {
            inm_combo.Items.Clear();
            if (inm_rd.Checked == true)
            {

                inm_combo.Items.Clear();
                inm_combo.Visible = true;
                ds = new DataSet();
                ds = obj.select_qury("select it_nm from stock_master");
                int a = Convert.ToInt32(ds.Tables[0].Rows.Count.ToString());
                for (int i = 0; i < a; i++)
                {
                    inm_combo.Items.Add(ds.Tables[0].Rows[i][0].ToString());
                }
            }
            else
            {
               // inm_combo.Visible = false;
                inm_combo.Text = "";
            }
        }

        private void iid_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ds = obj.select_qury("select * from stock_master where it_id=" + iid_combo.Text + "");
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void inm_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ds = new DataSet();
            ds = obj.select_qury("select * from stock_master where it_nm='" + inm_combo.Text + "'");
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            data_grid();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void showbtn_Click(object sender, EventArgs e)
        {
            data_grid();
        }
    }
}
