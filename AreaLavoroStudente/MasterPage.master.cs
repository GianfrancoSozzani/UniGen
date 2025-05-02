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
    public int Matricola;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //CONTROLLO ABILITAZIONE

            //TEST CON VARIABILI 
            Matricola = 123562;
            string Abilitazione = "S";

            //VERSIONE CON SESSION
            /*Abilitazione = Session["Abilitazione"].ToString();*/

            if (Abilitazione == "N")
            {
                liAppelli.Visible = (Abilitazione == "studimm");
                liMateriali.Visible = (Abilitazione == "studimm");
                liComunicazioni.Visible = (Abilitazione == "studimm");
            }
            CaricaUtente(Matricola);
        }

    }


    private void CaricaUtente(int Matricola)
    {
        STUDENTI studente = new STUDENTI();
        studente.Matricola = Matricola;
        DataTable dt = studente.SelezionaPerMatricola();


        if (dt.Rows.Count == 1)
        {
            string nome = dt.Rows[0]["Nome"].ToString();
            string cognome = dt.Rows[0]["Cognome"].ToString();
            lblStudente.Text = nome + " " + cognome;
        }


    }
}
