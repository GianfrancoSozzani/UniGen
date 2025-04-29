using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.EnterpriseServices;
using LibreriaClassi;
using System.ComponentModel;
using System.Activities.Expressions;
using System.Security.Cryptography.X509Certificates;


public partial class MasterPage : System.Web.UI.MasterPage
{
    public int matricolaTest;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            string abilitazione = "N";
            /*Session["Abilitazione"]?.ToString();*/
            if (abilitazione == "N")
            {
                liAppelli.Visible = (abilitazione == "admin");
                liMateriali.Visible = (abilitazione == "admin");
                liComunicazioni.Visible = (abilitazione == "admin");
            }

            // Nasconde la card se non sei admin

        }


        // Verifica che la sessione contenga una matricola valida
        //if (Session["matricola"] == null || !int.TryParse(Session["matricola"].ToString(), out matricolaTest) || matricolaTest == 0)
        //{
        //    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Utente non loggato');", true);
        //    Response.Redirect("~/Login.aspx");
        //  
        //}

        //STUDENTI studente = new STUDENTI();
        //studente.Matricola = matricolaTest;
        //DataTable dt = studente.SelezionaPerMatricola();

        //if (dt.Rows.Count == 1)
        //{
        //    string nome = dt.Rows[0]["Nome"].ToString();
        //    string cognome = dt.Rows[0]["Cognome"].ToString();
        //    lblStudente.Text = nome + " " + cognome;
        //}

        matricolaTest = 2;

        if (matricolaTest == 0)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Utente non loggato');", true);
            Response.Redirect("~/Login.aspx");
            
        }

        STUDENTI studente = new STUDENTI();
        studente.Matricola = matricolaTest;
        DataTable dt = studente.SelezionaPerMatricola();

        if (dt.Rows.Count == 1)
        {
            string nome = dt.Rows[0]["Nome"].ToString();
            string cognome = dt.Rows[0]["Cognome"].ToString();
            lblStudente.Text = nome + " " + cognome;
        }

        








    }
}




