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
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
           
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            label11.Text = DateTime.Now.ToString("dd:mm:yyyy");
            label12.Text = DateTime.Now.ToString("hh:mm:ss");
        
        
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=G:\@\welcome\a8.accdb");
            con.Open();
            OleDbCommand cmd = new OleDbCommand("select* from login where username='" + textBox2.Text + "' and password='" + textBox1.Text+ "'", con);
            OleDbDataReader dr = cmd.ExecuteReader();
           
            if (dr.Read())
            {
                DialogResult res = MessageBox.Show("Successfully Login......", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (res == DialogResult.Yes)
                {
                    mdi s1 = new mdi();
                    s1.Show();

                }
                else
                {
                    login l2 = new login();
                    l2.Show();
                }
            }
            else
            {
                DialogResult res = MessageBox.Show("Username And Password Incorrect", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                textBox2.Text = "";
                textBox1.Text = "";
            }
            con.Close();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox2.Text= "";
            textBox1.Text = "";


        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
                textBox1.UseSystemPasswordChar = true;
            else
                textBox1.UseSystemPasswordChar = false;
        }
    }
}
