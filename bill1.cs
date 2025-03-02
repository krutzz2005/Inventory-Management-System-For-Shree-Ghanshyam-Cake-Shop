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
namespace welcome
{
    public partial class bill1 : Form
    {

        OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=E:\@\welcome\a8.accdb");
        //  OleDbCommand cmd;
        OleDbDataAdapter da = new OleDbDataAdapter();
        DataTable dt = new DataTable();
        project obj = new project();
        DataSet ds = new DataSet();
        public bill1()
        {
            InitializeComponent();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {


        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do You Really Want To Add This Record?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
            {
                con.Open();
                da.InsertCommand = new OleDbCommand("insert into bill_master (bill_no,c_nm,it_id,it_nm,qty,price,total) values('" + sbill.Text + "','" + cnm.Text + "','" + i_id.Text + "','" + i_nm.Text + "','" + qty.Text + "','" + i_price.Text + "','" + total.Text + "')", con);
                da.InsertCommand.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Record Has Been insert Successfully Saved", "Success", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

              //  clear();

                //data_grid();
            }
        }
        /*public void data_grid()
        {
            ds = obj.select_qury("select * from bill_master");
            //dataGridView1.DataSource = ds.Tables[0];

        }*/

        private void sbill_SelectedIndexChanged(object sender, EventArgs e)
        {
            OleDbDataReader d;
            con.Open();
            OleDbCommand cmd = new OleDbCommand("select* from sales_master where bill_no=" + sbill.Text + "", con);
            d = cmd.ExecuteReader();
            while (d.Read())
            {
                String a = d.GetString(6);
                cnm.Text = a;
                
                i_id.Text = d.GetValue(2).ToString();
                String a1 = d.GetString(3);
                i_nm.Text = a1;
                qty.Text = d.GetValue(10).ToString();
                i_price.Text = d.GetValue(9).ToString();
                total.Text = d.GetValue(12).ToString();
              

            }
            con.Close();
        }

        private void bill1_Load(object sender, EventArgs e)
        {

            ds = obj.select_qury("select bill_no from sales_master");
            int b = Convert.ToInt32(ds.Tables[0].Rows.Count.ToString());
            for (int i = 0; i < b; i++)
            {
                sbill.Items.Add(ds.Tables[0].Rows[i][0].ToString());
            }

            con.Close();
            //data_grid();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string st = Application.StartupPath + "\\report\\bill_report.rpt";
            axCrystalReport1.ReportFileName = st;

            axCrystalReport1.WindowState = Crystal.WindowStateConstants.crptMaximized;
            axCrystalReport1.WindowShowRefreshBtn = true;
            axCrystalReport1.Action = 1;
        }
    }
}
