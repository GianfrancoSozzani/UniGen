using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;


public class DB
{
    SqlConnection conn = new SqlConnection();
    SqlDataAdapter DA = new SqlDataAdapter();
    public SqlCommand cmd = new SqlCommand();
    public string query;
    public DB()
    {
        conn.ConnectionString = @"Data Source=LAPTOP-83HKI4IS\SQLEXPRESS; Initial Catalog=GENERATION;Integrated Security=true;";
        cmd.Connection = conn;
        cmd.CommandType = CommandType.Text;
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
        // parametri
        conn.Open();
        cmd.ExecuteNonQuery();
        conn.Close();

    }
}