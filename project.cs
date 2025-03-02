using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Data;


namespace welcome
{
    class project
    {
        OleDbConnection con = new OleDbConnection();
        OleDbCommand cmd = new OleDbCommand();
        OleDbDataAdapter da = new OleDbDataAdapter();
        DataSet ds = new DataSet();
        String s = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=E:\@\welcome\a8.accdb";

        public bool modify(string qry)
        {
            //cn.ConnectionString = Properties.Resources.Conn.ToString();
            con.ConnectionString = s;
            con.Open();
            cmd = new OleDbCommand();
            cmd.Connection = con;
            cmd.CommandText = qry;
            int a = cmd.ExecuteNonQuery();
            con.Close();
            if (a >= 1)
                return true;
            else
                return false;
        }

        public DataSet select_qury(string qry)
        {
            // cn.ConnectionString = Properties.Resources.Conn.ToString();
            con.ConnectionString = s;
            con.Open();
            cmd = new OleDbCommand();
            cmd.Connection = con;
            cmd.CommandText = qry;
            da = new OleDbDataAdapter(cmd);
            ds = new DataSet();
            da.Fill(ds);
            con.Close();
            return ds;

        }
    }
}
