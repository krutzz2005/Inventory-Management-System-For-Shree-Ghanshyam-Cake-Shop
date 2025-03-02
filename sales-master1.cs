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
    public partial class sales_master : Form
    {
        OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=E:\@\welcome\a8.accdb");
         OleDbCommand cmd;
        OleDbDataAdapter da = new OleDbDataAdapter();
        DataTable dt = new DataTable();
        project obj = new project();
        DataSet ds = new DataSet();
        int r = 0, new_qty=0,old_qty=0;

        public sales_master()
        {
            InitializeComponent();
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void Addbtn_Click(object sender, EventArgs e)
        {
            auto_inc();
            edtext(true);
            auto_inc1();
        }

        private void insertbtn_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do You Really Want To Add This Record?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
            {
                con.Open();
                OleDbDataReader dr;
                int q1 = 0, q2;
               
                da.InsertCommand = new OleDbCommand("insert into sales_master(s_id,bill_no,it_id,it_nm,it_type,c_id,c_nm,ord_date,s_date,sprice,sqty,sgst,samount) values('" + sid.Text + "','" + sbill.Text + "','" + i_id.Text + "','" + i_nm.Text + "','" + i_type.Text + "','" + cid.Text + "','" + cnm.Text + "','" + odate.Value.Date.ToString() + "','" + sdate.Value.Date.ToString() + "','" + price.Text + "','" + qty.Text + "','" + gst.Text + "','" + total.Text + "')", con);
                da.InsertCommand.ExecuteNonQuery();
                
                cmd = new OleDbCommand("select it_qty from stock_master where it_id=" + i_id.Text, con);
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        q1 = Convert.ToInt32(dr[0]);
                    }
                }


                q2 = q1 - (Convert.ToInt32(qty.Text));




                cmd = new OleDbCommand("update stock_master set it_qty='" + q2 + "' where it_id=" + i_id.Text , con);
                cmd.ExecuteNonQuery();


               
                MessageBox.Show("Record Has Been insert Successfully Saved", "Success", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);


                data_grid();
               
                con.Close();
            }
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void sales_master_Load(object sender, EventArgs e)
        {
            label23.Text = DateTime.Now.ToString("dd:mm:yyyy");
            label24.Text = DateTime.Now.ToString("hh:mm:ss");

            con.Open();
            ds = obj.select_qury("select c_id from customer_master");
            int a = Convert.ToInt32(ds.Tables[0].Rows.Count.ToString());
            for (int i = 0; i < a; i++)
            {
                cid.Items.Add(ds.Tables[0].Rows[i][0].ToString());
            }

            ds = obj.select_qury("select it_id from item_master");
            int a1 = Convert.ToInt32(ds.Tables[0].Rows.Count.ToString());
            for (int i = 0; i < a1; i++)
            {
                i_id.Items.Add(ds.Tables[0].Rows[i][0].ToString());
            }
            con.Close();
            data_grid();
        }
        public void data_grid()
        {
            ds = obj.select_qury("select * from sales_master");
            dataGridView1.DataSource = ds.Tables[0];

        }
        public void auto_inc()
        {

            da = new OleDbDataAdapter("select max(s_id) from sales_master", con);

            DataSet ds = new DataSet();
            da.Fill(ds);
            int cno;
            if (ds.Tables[0].Rows[0][0].ToString().Equals(""))
            {
                cno = 101 ;
            }
            else
            {
                cno = Int32.Parse(ds.Tables[0].Rows[0][0].ToString());
                cno = cno + 1;
            }
            sid.Text = cno.ToString();

        }

        public void auto_inc1()
        {

            da = new OleDbDataAdapter("select max(bill_no) from bill_master", con);

            DataSet ds = new DataSet();
            da.Fill(ds);
            int bno;
            if (ds.Tables[0].Rows[0][0].ToString().Equals(""))
            {
                bno = 101;
            }
            else
            {
                bno = Int32.Parse(ds.Tables[0].Rows[0][0].ToString());
                bno = bno + 1;
            }
            sbill.Text = bno.ToString();

        }

        public void clear()
        {
            sid.Text = "";
            sbill.Text = "";
            i_id.Text = "";
            i_nm.Text = "";
            i_type.Text = "";
            cid.Text = "";
            cnm.Text = "";
            odate.Text = "";
            sdate.Text = "";
            price.Text = "";
            qty.Text = "";
            gst.Text = "";
            total.Text = "";



        }
        public void edtext(Boolean b)
        {
            sid.Enabled = b;
            sbill.Enabled = b;

            i_id.Enabled = b;
            i_nm.Enabled = b;
            i_type.Enabled = b;
            cid.Enabled = b;
            cnm.Enabled = b;
            odate.Enabled = b;
            sdate.Enabled = b;
            price.Enabled = b;
            qty.Enabled = b;
            gst.Enabled = b;
            total.Enabled = b;


        }


        public void display(int c)
        {
            DataGridViewRow row = this.dataGridView1.Rows[c];

            sid.Text = row.Cells["s_id"].Value.ToString();
            sbill.Text = row.Cells["bill_no"].Value.ToString();
            i_id.Text = row.Cells["it_id"].Value.ToString();
            i_nm.Text = row.Cells["it_nm"].Value.ToString();
            i_type.Text = row.Cells["it_type"].Value.ToString();
            cid.Text = row.Cells["c_id"].Value.ToString();
            cnm.Text = row.Cells["c_nm"].Value.ToString();
            odate.Text = row.Cells["ord_date"].Value.ToString();
            sdate.Text = row.Cells["s_date"].Value.ToString();
            price.Text = row.Cells["sprice"].Value.ToString();
            qty.Text = row.Cells["sqty"].Value.ToString();
            gst.Text = row.Cells["sgst"].Value.ToString();
            total.Text = row.Cells["samount"].Value.ToString();
        }

        private void updatebtn_Click(object sender, EventArgs e)
        {
            if (sid.Text == "")
            {
                MessageBox.Show("please enter a data.", "message", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }


            else
            {
                con.Open();
                string s1 = "select s_id from sales_master";
                int temp = 0;
                OleDbCommand cmd = new OleDbCommand(s1, con);
                OleDbDataReader d = cmd.ExecuteReader();

                while (d.Read())
                {
                    if (sid.Text == d.GetInt32(0).ToString())
                    {
                        temp = 1;
                        break;
                    }
                }
                con.Close();
                if (temp == 1)
                {
                    con.Open();
                    da.UpdateCommand = new OleDbCommand("update sales_master set bill_no='" + sbill.Text + "',it_id='" + i_id.Text + "',it_nm='" + i_nm.Text + "',it_type='" + i_type.Text + "',c_id='" + cid.Text + "',c_nm='" + cnm.Text + "',ord_date='" + odate.Text + "',s_date='" + sdate.Text + "',sprice='" + price.Text + "',sqty='" + qty.Text + "',sgst='" + gst.Text + "',samount='" + total.Text + "' where s_id=" + sid.Text, con);
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

        private void id_rd_CeckedChanged(object sender, EventArgs e)
        {
            if (byid.Checked == true)
            {
                iid_combo.Items.Clear();
                iid_combo.Visible = true;
                ds = new DataSet();
                ds = obj.select_qury("select it_id from item_master");
                int a = Convert.ToInt32(ds.Tables[0].Rows.Count.ToString());
                for (int i = 0; i < a; i++)
                {
                    iid_combo.Items.Add(ds.Tables[0].Rows[i][0].ToString());
                }

            }
            else
            {
                iid_combo.Visible = false;
                iid_combo.Text = "";
            }
        }

        private void iid_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ds = obj.select_qury("select * from item_master where it_id=" + iid_combo.Text + "");
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void deletebtn_Click(object sender, EventArgs e)
        {
            bool ans = obj.modify("delete from sales_master where s_id=" + sid.Text);
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
            sid.Text = "";
            sbill.Text = "";
            i_id.Text = "";
            i_nm.Text = "";
            i_type.Text = "";
            cid.Text = "";
            cnm.Text = "";
            odate.Text = "";
            sdate.Text = "";
            price.Text = "";
            qty.Text = "";
            gst.Text = "";
            total.Text = "";

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

        private void firstbtn_Click(object sender, EventArgs e)
        {
            display(0);
            r = 0;
        }

        private void Nextbtn_Click(object sender, EventArgs e)
        {
            if (r != dataGridView1.RowCount - 2)
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
            r = dataGridView1.RowCount - 2;
        }

        private void bynm_CheckedChanged(object sender, EventArgs e)
        {
            inm_combo.Items.Clear();
            if (bynm.Checked == true)
            {

                inm_combo.Items.Clear();
                inm_combo.Visible = true;
                ds = new DataSet();
                ds = obj.select_qury("select it_nm from item_master");
                int a = Convert.ToInt32(ds.Tables[0].Rows.Count.ToString());
                for (int i = 0; i < a; i++)
                {
                    inm_combo.Items.Add(ds.Tables[0].Rows[i][0].ToString());
                }
            }
            else
            {
                inm_combo.Visible = false;
                inm_combo.Text = "";
            }
        }

        private void all_rd_CheckedChanged(object sender, EventArgs e)
        {
            data_grid();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                sid.Text = row.Cells["s_id"].Value.ToString();
                sbill.Text = row.Cells["bill_no"].Value.ToString();

                i_id.Text = row.Cells["it_id"].Value.ToString();
                i_nm.Text = row.Cells["it_nm"].Value.ToString();
                i_type.Text = row.Cells["it_type"].Value.ToString();
                cid.Text = row.Cells["c_id"].Value.ToString();
                cnm.Text = row.Cells["c_nm"].Value.ToString();
                odate.Text = row.Cells["ord_date"].Value.ToString();
                sdate.Text = row.Cells["s_date"].Value.ToString();
                price.Text = row.Cells["sprice"].Value.ToString();
                qty.Text = row.Cells["sqty"].Value.ToString();
                gst.Text = row.Cells["sgst"].Value.ToString();
                total.Text = row.Cells["samount"].Value.ToString();
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

        private void sid_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ds = obj.select_qury("select * from sales_master where s_id=" + sid_combo.Text + "");
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void bysid_CheckedChanged(object sender, EventArgs e)
        {
            if (bysid.Checked == true)
            {
                sid_combo.Items.Clear();
                sid_combo.Visible = true;
                ds = new DataSet();
                ds = obj.select_qury("select s_id from sales_master");
                int a = Convert.ToInt32(ds.Tables[0].Rows.Count.ToString());
                for (int i = 0; i < a; i++)
                {
                    sid_combo.Items.Add(ds.Tables[0].Rows[i][0].ToString());
                }

            }
            else
            {
                sid_combo.Visible = false;
                sid_combo.Text = "";
            }
        }

        private void inm_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ds = obj.select_qury("select * from item_master where it_nm=" + inm_combo.Text + "");
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void qty_Leave(object sender, EventArgs e)
        {
            int a = Convert.ToInt32(price.Text);
            int b = Convert.ToInt32(qty.Text);
            int c = a * b;
            amt.Text = Convert.ToString(c);

            int p = Convert.ToInt32(amt.Text);

            int g = (p * 5) / 100;
            gst.Text = Convert.ToString(g);

            int x = Convert.ToInt32(gst.Text);
            int y = Convert.ToInt32(amt.Text);
            int z = x + y;
            total.Text = Convert.ToString(z);
        }

        private void amt_Leave(object sender, EventArgs e)
        {
            
        }

        private void total_Leave(object sender, EventArgs e)
        {
           
        }

        private void itid_SelectedIndexChanged(object sender, EventArgs e)
        {
            OleDbDataReader d;
            con.Open();
            OleDbCommand cmd = new OleDbCommand("select * from item_master where it_id=" + i_id.Text + "", con);
            d = cmd.ExecuteReader();
            while (d.Read())
            {
                string a = d.GetString(1);
                i_nm.Text = a;
                string aa = d.GetString(2);
                i_type.Text = aa;


            }
            con.Close();
        }

        private void i_type_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox5_Enter(object sender, EventArgs e)
        {

        }

        private void sbill_TextChanged(object sender, EventArgs e)
        {

        }

        private void sid_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
