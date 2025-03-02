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
    public partial class order_master : Form
    {
        OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\@\welcome\a8.accdb");
        //  OleDbCommand cmd;
        OleDbDataAdapter da = new OleDbDataAdapter();
        DataTable dt = new DataTable();
        project obj = new project();
        DataSet ds = new DataSet();
        int r = 0;
        public order_master()
        {
            InitializeComponent();
        }

        private void label15_Click(object sender, EventArgs e){}
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e){}
        private void groupBox2_Enter(object sender, EventArgs e){}

        private void order_master_Load(object sender, EventArgs e)
        {
           
            //OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=F:\kad_08\kk\New folder\welcome\a8.accdb");
            con.Open();
            ds = obj.select_qury("select c_id from customer_master");
            int a = Convert.ToInt32(ds.Tables[0].Rows.Count.ToString());
            for (int i = 0; i < a; i++)
            {
                cid.Items.Add(ds.Tables[0].Rows[i][0].ToString());
            }
            con.Close();
            data_grid();

            ds = obj.select_qury("select it_id from item_master");
            int b = Convert.ToInt32(ds.Tables[0].Rows.Count.ToString());
            for (int i = 0; i < b; i++)
            {
                i_id.Items.Add(ds.Tables[0].Rows[i][0].ToString());
            }

            con.Close();
            data_grid();
            
        }

        private void insertbtn_Click(object sender, EventArgs e)
        {
            if (oid.Text == "")
            {
                MessageBox.Show("please enter add button for id?", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else{
            if (MessageBox.Show("Do You Really Want To Add This Record?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
            {
                con.Open();
                da.InsertCommand = new OleDbCommand("insert into order_master (ord_id,ord_date,ord_lastdate,it_id,it_nm,it_type,it_size,it_price,it_qty,total,c_id,c_nm) values('" + oid.Text + "','" + odate.Text + "','" + olastdate.Text + "','" + i_id.Text + "','" + i_nm.Text + "','" + i_type.Text + "','" + i_size.Text + "','" + i_price.Text + "','" + qty.Text + "','" +total.Text+"','"+ cid.Text + "','" + cnm.Text + "')", con);
                da.InsertCommand.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Record Has Been insert Successfully Saved", "Success", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                clear();

                data_grid();
            }
            }
        }
        public void data_grid()
        {
            ds = obj.select_qury("select * from order_master");
            dataGridView2.DataSource = ds.Tables[0];

        }
        public void auto_inc()
        {

            da = new OleDbDataAdapter("select max(ord_id) from order_master", con);

            DataSet ds = new DataSet();
            da.Fill(ds);
            int ono;
            if (ds.Tables[0].Rows[0][0].ToString().Equals(""))
            {
                ono = 1 ;
            }
            else
            {
                ono = Int32.Parse(ds.Tables[0].Rows[0][0].ToString());
                ono = ono + 1;
            }
            oid.Text = ono.ToString();

        }

        public void clear()
        {
            oid.Text = "";
            odate.Text = "";
            olastdate.Text = "";
            i_id.Text = "";
            i_nm.Text = "";
            i_type.Text = "";
            
            i_size.Text = "";
           
            i_price.Text = "";
            total.Text = "";
            qty.Text = "";
            cid.Text = "";
            cnm.Text = "";
        }
        public void edtext(Boolean b)
        {
            oid.Enabled = b;
            odate.Enabled = b;
            olastdate.Enabled = b;
            i_id.Enabled = b;
          
            i_nm.Enabled = b;
            i_type.Enabled = b;
            i_size.Enabled = b;
            
            i_price.Enabled = b;
            qty.Enabled = b;
            cid.Enabled = b;
            cnm.Enabled = b;
        }


        public void display(int c)
        {
            DataGridViewRow row = this.dataGridView2.Rows[c];

            oid.Text = row.Cells["ord_id"].Value.ToString();
            odate.Text = row.Cells["ord_date"].Value.ToString();
            olastdate.Text = row.Cells["ord_lastdate"].Value.ToString();
            i_id.Text = row.Cells["it_id"].Value.ToString();
            i_nm.Text = row.Cells["it_nm"].Value.ToString();
            i_type.Text = row.Cells["it_type"].Value.ToString();
            i_size.Text = row.Cells["it_size"].Value.ToString();
           
            i_price.Text = row.Cells["it_price"].Value.ToString();
            qty.Text = row.Cells["it_qty"].Value.ToString();
            total.Text = row.Cells["total"].Value.ToString();
            cid.Text = row.Cells["c_id"].Value.ToString();
            cnm.Text = row.Cells["c_nm"].Value.ToString();
        }

        private void Updatebtn_Click(object sender, EventArgs e)
        {
            if (cid.Text == "")
            {
                MessageBox.Show("please enter a data.", "message", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }


            else
            {
                con.Open();
                string s1 = "select ord_id from order_master";
                int temp = 0;
                OleDbCommand cmd = new OleDbCommand(s1, con);
                OleDbDataReader d = cmd.ExecuteReader();

                while (d.Read())
                {
                    if (oid.Text == d.GetInt32(0).ToString())
                    {
                        temp = 1;
                        break;
                    }
                }
                con.Close();
                if (temp == 1)
                {
                    con.Open();
                    da.UpdateCommand = new OleDbCommand("update order_master set ord_date='" + odate.Text + "',ord_lastdate='" + olastdate.Text + "',it_id='" + i_type.Text + "',it_nm='" + i_nm.Text + "',it_type='" + i_id.Text + "',it_size='" + i_size.Text + "',it_qty='" + qty.Text + "',it_price='" + i_price.Text + "' ,c_id='" + cid.Text + "',c_nm='" + cnm.Text  +"' where ord_id=" + oid.Text, con);
                    da.UpdateCommand.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Record Has Been update Successfully Saved", "Success", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    data_grid();
                    clear();
                }
                else
                {
                    MessageBox.Show("this is new id so can't be updated.", "message", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                }
            }
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
                oid_combo.Visible = false;
                oid_combo.Text = "";
            }
        }

        private void oid_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ds = obj.select_qury("select * from order_master where ord_id=" + oid_combo.Text + "");
            dataGridView2.DataSource = ds.Tables[0];
        }

        private void Addbtn_Click(object sender, EventArgs e)
        {
            auto_inc();
            edtext(true);
        }

        private void Deletebtn_Click(object sender, EventArgs e)
        {
            bool ans = obj.modify("delete from order_master where ord_id=" + oid.Text);
            if (ans)
            {

                MessageBox.Show("record is deleted.", "Delete", MessageBoxButtons.OK);
                data_grid();
            }

            else
            {
                MessageBox.Show("record is not deleted.", "Delete", MessageBoxButtons.OK);
                con.Close();
            }
            clear();
        }

        private void Cancelbtn_Click(object sender, EventArgs e)
        {
            oid.Text = "";
            odate.Text = "";

            olastdate.Text = "";
            i_id.Text = "";
            i_nm.Text = "";
            i_type.Text = "";
            i_size.Text = "";
            i_price.Text = "";
            qty.Text = "";
            total.Text = "";
            cid.Text = "";
            cnm.Text = "";
        }

        private void Backbtn_Click(object sender, EventArgs e)
        {
            DialogResult r = MessageBox.Show("Are you sure?", "confirmation", MessageBoxButtons.OKCancel);
            if (r == DialogResult.OK)
            {
                mdi m = new mdi();
                m.Show();
                this.Hide();
            }
            else
            {
                this.Show();
            }

        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void firstbtn_Click(object sender, EventArgs e)
        {
            display(0);
            r = 0;
        }

        private void Nextbtn_Click(object sender, EventArgs e)
        {
            if (r != dataGridView2.RowCount - 2)
            {

                r++;
                display(r);

            }
            else
            {
                MessageBox.Show("you are on last record.");
            }
        }

        private void Previousbtn_Click(object sender, EventArgs e)
        {
            if (r != 0)
            {
                r--;
                display(r);
            }
            else
            {
                MessageBox.Show("you are on first record.");
            }
        }

        private void Lastbtn_Click(object sender, EventArgs e)
        {
            display(r);
            r = dataGridView2.RowCount - 2;
        }

        /*private void cusid_rd_CheckedChanged(object sender, EventArgs e)
        {

        }*/

        private void all_rd_CheckedChanged(object sender, EventArgs e)
        {
            data_grid();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView2.Rows[e.RowIndex];
                oid.Text = row.Cells["ord_id"].Value.ToString();
                odate.Text = row.Cells["ord_date"].Value.ToString();

                olastdate.Text = row.Cells["ord_lastdate"].Value.ToString();
                i_id.Text = row.Cells["it_id"].Value.ToString();
                i_nm.Text = row.Cells["it_nm"].Value.ToString();
                i_type.Text = row.Cells["it_type"].Value.ToString();
                i_size.Text = row.Cells["it_size"].Value.ToString();
              
                i_price.Text = row.Cells["it_price"].Value.ToString();
                qty.Text = row.Cells["it_qty"].Value.ToString();
                total.Text = row.Cells["total"].Value.ToString();


                cid.Text = row.Cells["c_id"].Value.ToString();
                cnm.Text = row.Cells["c_nm"].Value.ToString();
            }
        }

        private void cid_SelectedIndexChanged(object sender, EventArgs e)
        {
            OleDbDataReader d;
            con.Open();
            OleDbCommand cmd = new OleDbCommand("select * from customer_master where c_id=" + cid.Text + "", con);
            d = cmd.ExecuteReader();
            while (d.Read())
            {
                string a = d.GetString(1);
                cnm.Text = a;
            }
            con.Close();

        }

        private void ittype_SelectedIndexChanged(object sender, EventArgs e)
        {
            OleDbDataReader d;
            con.Open();
            OleDbCommand cmd = new OleDbCommand("select* from item_master where it_id=" + i_id.Text + "", con);
            d = cmd.ExecuteReader();
            while (d.Read())
            {
                String a = d.GetString(1);
                i_nm.Text = a;
                String aa= d.GetString(2);
                i_type.Text = aa;
                int a1 = d.GetInt32(3);
                /*i_size.Text = a1;
                int a2 = d.GetInt32(4);
                i_price.Text = a2;
                int a3 = d.GetInt32(5);
                qty.Text = a3;*/

            }
            con.Close();
        }

        private void qty_Leave(object sender, EventArgs e)
        {
            double a = Convert.ToInt32(i_price.Text);
            double b = Convert.ToInt32(qty.Text);
            double c = a * b;
            total.Text = Convert.ToString(c);
        }

    }
}
