using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LibreriaClassi;

public partial class _Default : System.Web.UI.Page
{
    public int matricola = 123562;
    protected void Page_Load(object sender, EventArgs e)
    {
        
       

    }

    protected void btnConfermaRinuncia_Click(object sender, EventArgs e)
    {
        
        DB db = new DB();
        db.cmd.Parameters.Clear();
        db.cmd.Parameters.AddWithValue("@Matricola", matricola);
        db.query = "Studenti_Disabilita";
        Response.Redirect("Homepage.aspx");

    }
    
    protected void CaricaDati() { 
        DB db = new DB();   
        db.query = "Studenti_SelectForRinuncia";
        db.cmd.Parameters.AddWithValue("@Matricola", matricola) ;
        DataTable dt = new DataTable();
       
        lblMatricola = (Label)dt.Rows[0]["Matricola"]; 
        lblNome = (Label)dt.Rows[0]["Nome"]; 
        lblCognome = (Label)dt.Rows[0]["Cognome"]; 
        lblCorso = (Label)dt.Rows[0]["Corso"];
        lblDataRichiesta.Text = DateTime.Now.ToString("dd/MM/yyyy");








    }
}