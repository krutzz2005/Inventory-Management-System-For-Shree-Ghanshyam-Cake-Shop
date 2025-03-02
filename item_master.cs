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
    public partial class item_master : Form
    {
        OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=E:\@\welcome\a8.accdb");
        //  OleDbCommand cmd;
        OleDbDataAdapter da = new OleDbDataAdapter();
        DataTable dt = new DataTable();
        project obj = new project();
        DataSet ds = new DataSet();
        int r = 0;

 
        public item_master()
        {
            InitializeComponent();
        }

        private void item_master_Load(object sender, EventArgs e)
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
            con.Close();
            data_grid();
        }

        private void txtiprice_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void combocid_SelectedIndexChanged(object sender, EventArgs e)
        {
            OleDbDataReader d;
            con.Open();
            OleDbCommand cmd = new OleDbCommand("select * from customer_master where c_id=" + cid.Text + "", con);
            d = cmd.ExecuteReader();
            while (d.Read())
            {
                string a = d.GetString(1);
                //cnm.Text = a;
            }
            con.Close();
        }

        private void Lastbtn_Click(object sender, EventArgs e)
        {
            display(r);
            r = dataGridView1.RowCount - 2;
        }

        private void insertbtn_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do You Really Want To Add This Record?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
            {
                con.Open();
                da.InsertCommand = new OleDbCommand("insert into item_master (it_id,it_nm,it_type,it_size,it_price,it_qty,total,it_des,c_id) values('" + i_id.Text + "','" + i_nm.Text + "','" + i_type.Text + "','" + i_size.Text + "','" + i_price.Text + "','"+qty.Text+"','" + total.Text + "','"+des.Text+"','"+cid.Text+"')", con);
                da.InsertCommand.ExecuteNonQuery();
                da.InsertCommand = new OleDbCommand("insert into stock_master(it_id,it_nm,it_type,it_size,it_qty) values('" + i_id.Text + "','" + i_nm.Text + "','" + i_type.Text + "','" + i_size.Text + "','" + qty.Text + "')", con);
                da.InsertCommand.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Record Has Been insert Successfully Saved", "Success", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                clear();

                data_grid();
            }
        }
        public void data_grid()
        {
            ds = obj.select_qury("select * from item_master");
            dataGridView1.DataSource = ds.Tables[0];

        }
        public void auto_inc()
        {

            da = new OleDbDataAdapter("select max(it_id) from item_master", con);

            DataSet ds = new DataSet();
            da.Fill(ds);
            int ino;
            if (ds.Tables[0].Rows[0][0].ToString().Equals(""))
            {
                ino = 101; ;
            }
            else
            {
                ino = Int32.Parse(ds.Tables[0].Rows[0][0].ToString());
                ino = ino + 1;
            }
            i_id.Text = ino.ToString();

        }

        public void clear()
        {
            i_id.Text = "";
            i_nm.Text = "";
            i_type.Text = "";
            i_size.Text = "";
            i_price.Text = "";
            qty.Text = "";
            total.Text = "";
            des.Text = "";
            cid.Text = "";
        }
        public void edtext(Boolean b)
        {
            i_id.Enabled = b;
            i_nm.Enabled = b;

            i_type.Enabled = b;
            i_size.Enabled = b;
            i_price.Enabled = b;
            qty.Enabled = b;
            total.Enabled = b;
            des.Enabled = b;
            cid.Enabled = b;
        }


        public void display(int c)
        {
            DataGridViewRow row = this.dataGridView1.Rows[c];

            i_id.Text = row.Cells["it_id"].Value.ToString();
            i_nm.Text = row.Cells["it_nm"].Value.ToString();
            i_type.Text = row.Cells["it_type"].Value.ToString();
            i_size.Text = row.Cells["it_size"].Value.ToString();
            i_price.Text = row.Cells["it_price"].Value.ToString();
            qty.Text = row.Cells["it_qty"].Value.ToString();
            total.Text = row.Cells["total"].Value.ToString();
            des.Text = row.Cells["it_des"].Value.ToString();
            cid.Text = row.Cells["c_id"].Value.ToString();

        }

        private void Updatebtn_Click(object sender, EventArgs e)
        {
            if (i_id.Text == "")
            {
                MessageBox.Show("please enter a data.", "message", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }


            else
            {
                con.Open();
                string s1 = "select it_id from item_master";
                int temp = 0;
                OleDbCommand cmd = new OleDbCommand(s1, con);
                OleDbDataReader d = cmd.ExecuteReader();

                while (d.Read())
                {
                    if (i_id.Text == d.GetInt32(0).ToString())
                    {
                        temp = 1;
                        break;
                    }
                }
                con.Close();
                if (temp == 1)
                {
                    con.Open();
                    da.UpdateCommand = new OleDbCommand("update item_master set it_nm='" + i_nm.Text + "',it_type='" + i_type.Text + "',it_size='" + i_size.Text + "',it_price='" + i_price.Text + "',it_qty='" + qty.Text +"',total='"+total.Text+ "',it_des='" + des.Text + "',c_id='"+ cid.Text + "' where it_id=" + i_id.Text, con);
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

        private void iid_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ds = obj.select_qury("select * from item_master where it_id=" + iid_combo.Text + "");
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void iid_rd_CheckedChanged(object sender, EventArgs e)
        {
            if (iid_rd.Checked == true)
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

        private void Addbtn_Click(object sender, EventArgs e)
        {

            auto_inc();
            edtext(true);
        }

        private void Deletebtn_Click(object sender, EventArgs e)
        {
            bool ans = obj.modify("delete from item_master where it_id=" + i_id.Text);
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
            i_id.Text = "";
            i_nm.Text = "";

            i_type.Text = "";
            i_size.Text = "";
            i_price.Text = "";
            qty.Text = "";
            total.Text = "";
            des.Text = "";
            cid.Text = "";
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

        private void inm_rd_CheckedChanged(object sender, EventArgs e)
        {
            inm_combo.Items.Clear();
            if (inm_rd.Checked == true)
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

        private void inm_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ds = new DataSet();
            ds = obj.select_qury("select * from item_master where it_nm='" + inm_combo.Text + "'");
            dataGridView1.DataSource = ds.Tables[0];
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
                i_id.Text = row.Cells["it_id"].Value.ToString();
                i_nm.Text = row.Cells["it_nm"].Value.ToString();

                i_type.Text = row.Cells["it_type"].Value.ToString();
                i_size.Text = row.Cells["it_size"].Value.ToString();
                i_price.Text = row.Cells["it_price"].Value.ToString();
                qty.Text = row.Cells["it_qty"].Value.ToString();
                total.Text = row.Cells["total"].Value.ToString();
                des.Text = row.Cells["it_des"].Value.ToString();
                cid.Text = row.Cells["c_id"].Value.ToString();
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void i_type_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void qty_Leave(object sender, EventArgs e)
        {
            /*double a = Convert.ToInt32(i_price.Text);
            double b = Convert.ToInt32(qty.Text);
            double c = a * b;
            total.Text = Convert.ToString(c);*/
        }

        private void total_MouseClick(object sender, MouseEventArgs e)
        {
            double a = Convert.ToInt32(i_price.Text);
            double b = Convert.ToInt32(qty.Text);
            double c = a * b;
            total.Text = Convert.ToString(c);
        }

    }
}
