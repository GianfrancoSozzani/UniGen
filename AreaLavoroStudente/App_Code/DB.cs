using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;


public class DB
{
    SqlConnection conn = new SqlConnection();
    public SqlCommand cmd = new SqlCommand();
    SqlDataAdapter DA = new SqlDataAdapter();
    public string query;

    public DB() //COSTRUTTORE
    {
        conn.ConnectionString = "Data Source=LAPTOP-7G28UR81\\SQLEXPRESS;Initial Catalog=GENERATION; Integrated Security=true";
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

    public void Sqlcommand()
    {
        cmd.CommandText = query;
        //parametri
        conn.Open();
        cmd.ExecuteNonQuery();
        conn.Close();
    }

}
