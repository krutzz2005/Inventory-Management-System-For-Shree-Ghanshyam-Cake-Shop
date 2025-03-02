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
    public partial class change_pass : Form
    {
        OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=F:\ty-45\welcome\a8.accdb");
        OleDbDataAdapter da;
        public change_pass()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                da = new OleDbDataAdapter();
                con.Open();

                if (textBox1 == null || textBox2 == null || textBox3 == null)
                {
                    MessageBox.Show("please fill up information");
                    return;
                }
                if (textBox2.Text == textBox3.Text && !string.IsNullOrWhiteSpace(textBox2.Text) && !string.IsNullOrWhiteSpace(textBox3.Text))
                {
                    da.UpdateCommand = new OleDbCommand("Update login set[password]=? where [password]=?", con);
                    da.UpdateCommand.Parameters.AddWithValue("?", textBox2.Text);
                    da.UpdateCommand.Parameters.AddWithValue("?", textBox1.Text);


                    int rowaffect = da.UpdateCommand.ExecuteNonQuery();

                    con.Close();

                    if (rowaffect > 0)
                    {
                        MessageBox.Show("congratulation! password change successfully");
                        login l1 = new login();
                        l1.Show();
                    }
                    else
                    {
                        MessageBox.Show("password is not match!");
                    }


                }
                else {
                    MessageBox.Show("Passwords do not match or fields are empty.");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("error" +ex.Message);
            }

            finally
            {

                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
            }

        }

        private void change_pass_Load(object sender, EventArgs e)
        {

        }
    }
}
