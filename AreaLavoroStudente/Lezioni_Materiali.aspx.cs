using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LibreriaClassi;

public partial class Default2 : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //Simulazione della session
            Session["Matricola"] = 1;

            CaricaFacoltaECorso();
        }
    }

    private void CaricaFacoltaECorso()
    {
        //Recupero matricola da session
        if (Session["Matricola"] == null)
        {
            // Se non c'è la session reindirizza al login
            Response.Redirect("Login.aspx");
            return;
        }

        int matricola = Convert.ToInt32(Session["Matricola"]);


        DB db = new DB();
        db.query = "GetFacoltaCorsoByMatricola";
        db.cmd.Parameters.Clear();
        db.cmd.Parameters.AddWithValue("@Matricola", matricola);
        DataTable dt = db.SQLselect();

        if (dt.Rows.Count > 0)
        {
            //Prendo i valori e li carico nelle label
            lblFacolta.Text = dt.Rows[0]["Facolta"].ToString();
            lblCorso.Text = dt.Rows[0]["Corso"].ToString();
        }
        else
        {
            lblFacolta.Text = "Facoltà non trovata";
            lblCorso.Text = "Corso non trovato";
        }
    }
  
}
