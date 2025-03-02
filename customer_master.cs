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
    public partial class customer_master : Form
    {
        OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=G:\@\welcome\a8.accdb");
        //  OleDbCommand cmd;
        OleDbDataAdapter da = new OleDbDataAdapter();
        DataTable dt = new DataTable();
        project obj = new project();
        DataSet ds = new DataSet();
        int r = 0;

        public customer_master()
        {
            InitializeComponent();
        }

        /* private void insertbtn_Click(object sender, EventArgs e)
         {
           
         }*/


        private void insertbtn_Click(object sender, EventArgs e)
        {
            if (cid.Text == "")
            {
                MessageBox.Show("please Enter add button for id", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            else
            {
                if (string.IsNullOrEmpty(city.Text.Trim()) || !Regex.Match(city.Text, "^[a-zA-Z]*$").Success)
                {
                    errorProvider3.SetError(city, "Enter valid city.");
                    return;
                }
                else
                {
                    errorProvider3.SetError(city, string.Empty);
                }
                if (string.IsNullOrEmpty(cno.Text.Trim()) || !Regex.Match(cno.Text, @"^\d{10}$").Success)
                {
                    errorProvider4.SetError(cno, "Enter only 10 digit phone number.");
                    return;
                }
                else
                {
                    errorProvider4.SetError(cno, string.Empty);
                }
                if (string.IsNullOrEmpty(cadd.Text.Trim()) || !Regex.Match(cadd.Text, "^[a-z A-Z 0-9(,)]*$").Success)
                {
                    errorProvider2.SetError(cadd, "Enter valid address.");
                    return;
                }
                else
                {
                    errorProvider2.SetError(cadd, string.Empty);
                }
                if (string.IsNullOrEmpty(pincode.Text.Trim()) || !Regex.Match(pincode.Text, @"^\d{6}$").Success)
                {
                    errorProvider5.SetError(pincode, "Enter only 6digit pincode number.");
                    return;
                }
                else
                {
                    errorProvider5.SetError(pincode, string.Empty);
                }
                if (string.IsNullOrEmpty(email.Text.Trim()) || !Regex.Match(email.Text, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$").Success)
                {
                    errorProvider6.SetError(email, "Enter valid email address.");
                    return;
                }
                else
                {
                    errorProvider6.SetError(email, string.Empty);
                }


                if (MessageBox.Show("Do You Really Want To Add This Record?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
                {
                    con.Open();
                    da.InsertCommand = new OleDbCommand("insert into customer_master (c_id,c_nm,c_city,c_mono,c_add,c_pincode,c_email) values('" + cid.Text + "','" + cnm.Text + "','" + city.Text + "','" + cno.Text + "','" + cadd.Text + "','" + pincode.Text + "','" + email.Text + "')", con);
                    da.InsertCommand.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Record Has Been insert Successfully Saved", "Success", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                    clear();

                    data_grid();
                }
            }
        }



        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void customer_master_Load(object sender, EventArgs e)
        {
            label23.Text = DateTime.Now.ToString("dd:mm:yyyy");
            label24.Text = DateTime.Now.ToString("hh:mm:ss");
        }

        public void data_grid()
        {
            ds = obj.select_qury("select * from customer_master");
            dataGridView1.DataSource = ds.Tables[0];

        }
        public void auto_inc()
        {

            da = new OleDbDataAdapter("select max(c_id) from customer_master", con);

            DataSet ds = new DataSet();
            da.Fill(ds);
            int cno;
            if (ds.Tables[0].Rows[0][0].ToString().Equals(""))
            {
                cno = 101; ;
            }
            else
            {
                cno = Int32.Parse(ds.Tables[0].Rows[0][0].ToString());
                cno = cno + 1;
            }
            cid.Text = cno.ToString();

        }

        public void clear()
        {
            cid.Text = "";
            cnm.Text = "";
            city.Text = "";
            cno.Text = "";
            cadd.Text = "";
            pincode.Text = "";
            email.Text = "";
        }
        public void edtext(Boolean b)
        {
            cid.Enabled = b;
            cnm.Enabled = b;
           
            city.Enabled = b;
            cno.Enabled = b;
            cadd.Enabled = b;
            pincode.Enabled = b;
            email.Enabled = b;
        }


        public void display(int c)
        {
            DataGridViewRow row = this.dataGridView1.Rows[c];

            cid.Text = row.Cells["c_id"].Value.ToString();
            cnm.Text = row.Cells["c_nm"].Value.ToString();
            city.Text = row.Cells["c_city"].Value.ToString();
            cno.Text = row.Cells["c_mono"].Value.ToString();
            cadd.Text = row.Cells["c_add"].Value.ToString();
            pincode.Text = row.Cells["c_pincode"].Value.ToString();
            email.Text = row.Cells["c_email"].Value.ToString();
        }
        private void pincode_TextChanged(object sender, EventArgs e)
        {

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
                string s1 = "select c_id from customer_master";
                int temp = 0;
                OleDbCommand cmd = new OleDbCommand(s1, con);
                OleDbDataReader d = cmd.ExecuteReader();

                while (d.Read())
                {
                    if (cid.Text == d.GetInt32(0).ToString())
                    {
                        temp = 1;
                        break;
                    }
                }
                con.Close();
                if (temp == 1)
                {
                    con.Open();
                    da.UpdateCommand = new OleDbCommand("update customer_master set c_nm='" + cnm.Text + "',c_city='" + city.Text + "',c_mono='" + cno.Text + "',c_add='" + cadd.Text + "',c_pincode='" + pincode.Text + "',c_email='" + email.Text + "' where c_id=" + cid.Text, con);
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

        private void cusid_rd_CheckedChanged(object sender, EventArgs e)
        {
            if (cusid_rd.Checked == true)
            {
                cusid_combo.Items.Clear();
                cusid_combo.Visible = true;
                ds = new DataSet();
                ds = obj.select_qury("select c_id from customer_master");
                int a = Convert.ToInt32(ds.Tables[0].Rows.Count.ToString());
                for (int i = 0; i < a; i++)
                {
                    cusid_combo.Items.Add(ds.Tables[0].Rows[i][0].ToString());
                }

            }
            else
            {
                cusid_combo.Visible = false;
                cusid_combo.Text = "";
            }
        }

        private void cusid_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ds = obj.select_qury("select * from customer_master where c_id=" + cusid_combo.Text + "");
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void Addbtn_Click(object sender, EventArgs e)
        {
            auto_inc();
            edtext(true);
        }

        private void Deletebtn_Click(object sender, EventArgs e)
        {
            bool ans = obj.modify("delete from customer_master where c_id=" + cid.Text);
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
            cid.Text = "";
            cnm.Text = "";
           
            city.Text = "";
            cno.Text = "";
            cadd.Text = "";
            pincode.Text = "";
            email.Text = "";
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
                cusnm_combo.Visible = false;
                cusnm_combo.Text = "";
            }
        }

        private void cusnm_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ds = new DataSet();
            ds = obj.select_qury("select * from customer_master where c_nm='" + cusnm_combo.Text + "'");
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
                cid.Text = row.Cells["c_id"].Value.ToString();
                cnm.Text = row.Cells["c_nm"].Value.ToString();
               
                city.Text = row.Cells["c_city"].Value.ToString();
                cno.Text = row.Cells["c_mono"].Value.ToString();
                cadd.Text = row.Cells["c_add"].Value.ToString();
                pincode.Text = row.Cells["c_pincode"].Value.ToString();
                email.Text = row.Cells["c_email"].Value.ToString();
            }
        }
    }
        }


