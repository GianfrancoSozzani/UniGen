using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibreriaClassi
{
    public class DB
    {
        SqlConnection conn = new SqlConnection();
        SqlDataAdapter DA = new SqlDataAdapter();

        public SqlCommand cmd = new SqlCommand();
        public string query;

        public DB()
        {
            conn.ConnectionString = "Data source=PC-ROBERTO\\SQLEXPRESS;Initial Catalog=GENERATION;Integrated Security=true;";
            //conn.ConnectionString = "Data source=MSSQLSERVER2019\\SQLEXPRESS;Initial Catalog=GENERATION;Integrated Security=true;";
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
        }

        public DataTable SQLselect()
        {
            DataTable DT = new DataTable();
            cmd.CommandText = query;
            DA.SelectCommand = cmd;
            DA.Fill(DT);
            return DT;
        }

        public void SQLcommand()
        {
            cmd.CommandText = query;
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

    }
}
