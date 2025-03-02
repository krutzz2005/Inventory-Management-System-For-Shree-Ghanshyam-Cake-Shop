﻿using System;
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
    public partial class sales_report : Form
    {
        OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=E:\@\welcome\a8.accdb");
        //  OleDbCommand cmd;
        OleDbDataAdapter da = new OleDbDataAdapter();
        DataTable dt = new DataTable();
        project obj = new project();
        DataSet ds = new DataSet();
       
        public sales_report()
        {
            InitializeComponent();
        }

        private void itemid_rd_CheckedChanged(object sender, EventArgs e)
        {
            if (itemid_rd.Checked == true)
            {
                itemid_combo.Items.Clear();
                itemid_combo.Visible = true;
               
                ds = obj.select_qury("select it_id from item_master");
                int a = Convert.ToInt32(ds.Tables[0].Rows.Count.ToString());
                for (int i = 0; i < a; i++)
                {
                    itemid_combo.Items.Add(ds.Tables[0].Rows[i][0].ToString());
                }
            }
            else
            {

                itemid_combo.Text = "";
            }
        }

        private void itemnm_rd_CheckedChanged(object sender, EventArgs e)
        {
            itemnm_combo.Items.Clear();
            if (itemnm_rd.Checked == true)
            {

                itemnm_combo.Items.Clear();
                itemnm_combo.Visible = true;
               
                ds = obj.select_qury("select it_nm from item_master");
                int a = Convert.ToInt32(ds.Tables[0].Rows.Count.ToString());
                for (int i = 0; i < a; i++)
                {
                    itemnm_combo.Items.Add(ds.Tables[0].Rows[i][0].ToString());
                }
            }
            else
            {

                itemnm_combo.Text = "";
            }
        }

        private void bno_rd_CheckedChanged(object sender, EventArgs e)
        {
            bno_combo.Items.Clear();
            if (bno_rd.Checked == true)
            {

                bno_combo.Items.Clear();
                bno_combo.Visible = true;
               // ds = new DataSet();
                ds = obj.select_qury("select bill_no from bill_master");
                int a = Convert.ToInt32(ds.Tables[0].Rows.Count.ToString());
                for (int i = 0; i < a; i++)
                {
                    bno_combo.Items.Add(ds.Tables[0].Rows[i][0].ToString());
                }
            }
            else
            {

                bno_combo.Text = "";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string st = Application.StartupPath + "\\report\\sales_report.rpt";
            axCrystalReport1.ReportFileName = st;

            if (itemid_rd.Checked == true)
            {

                string str = Application.StartupPath + "\\report\\sales_report.rpt";
                axCrystalReport1.ReportFileName = str;

                axCrystalReport1.SelectionFormula = "{ customer.c_id}=" + itemid_combo.Text + "";
            }
            else if (itemnm_rd.Checked == true)
            {
                string str = Application.StartupPath + "\\report\\sales_report.rpt";
                axCrystalReport1.ReportFileName = str;
                axCrystalReport1.SelectionFormula = "{ customer.c_nm}='" + itemnm_combo.Text + "' ";
            }
            else
            {
                axCrystalReport1.SelectionFormula = "{ customer.c_id}>0";
            }

            axCrystalReport1.WindowState = Crystal.WindowStateConstants.crptMaximized;
            axCrystalReport1.WindowShowRefreshBtn = true;
            axCrystalReport1.Action = 1;
        
        }

        private void itemid_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            con.Open();
            OleDbCommand cmd = new OleDbCommand("select * from item_master where it_id=" + itemid_combo.Text + " ", con);
            OleDbDataReader dr = cmd.ExecuteReader();

            dt.Load(dr);
            //dataGridView1.DataSource = dt;
            con.Close();
        }

        private void itemnm_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            con.Open();
            OleDbCommand cmd = new OleDbCommand("select * from item_master where it_nm='" + itemnm_combo.Text + "' ", con);
            var r = cmd.ExecuteReader();

            dt.Load(r);
            // dataGridView1.DataSource = dt;
            con.Close();
        }

        private void bno_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            con.Open();
            OleDbCommand cmd = new OleDbCommand("select * from bill_master where bill_no='" + bno_combo.Text + "' ", con);
            var r = cmd.ExecuteReader();

            dt.Load(r);
            // dataGridView1.DataSource = dt;
            con.Close();
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
