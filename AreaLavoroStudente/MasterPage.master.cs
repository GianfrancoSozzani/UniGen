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
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            // Controlliamo se la sessione contiene i dati necessari (matricola e abilitazione)
            if (Session["mat"] == null || Session["a"] == null)
            {
                // Se non ci sono i dati nella sessione, mostriamo l'errore di autenticazione
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "authError", "showAuthErrorModal();", true);
                return;
            }

            // Recuperiamo i dati dalla sessione
            int Matricola = int.Parse(Session["mat"].ToString());
            string Abilitazione = Session["a"].ToString();

            // Carichiamo il nome utente (ad esempio nome, cognome)
            CaricaUtente(Matricola);

            // Impostiamo la visibilità dei menu in base all'abilitazione
            if (Abilitazione == "S")
            {
                liAppelli.Visible = true;
                liMateriali.Visible = true;
                liComunicazioni.Visible = true;
            }
            else
            {
                liAppelli.Visible = false;
                liMateriali.Visible = false;
                liComunicazioni.Visible = false;
            }
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

    protected void btnLogout_Click(object sender, EventArgs e)
    {
        string Cod = Session["cod"] as string;
        string Usr = Session["usr"] as string;
        string Matricola = Session["mat"] as string;
        string Abilitato = Session["a"] as string;

        string url = "https://localhost:7050/Studenti/Show?cod=" + Cod +
                     "&usr=" + Usr +
                     "&mat=" + Matricola +
                     "&a=" + Abilitato;

        Response.Redirect(url);

    }

    protected void btnComunicazioni_Click(object sender, EventArgs e)
    {
        string Ruolo = "";
        string Cod = "";

        if (Session["r"] != null && Session["cod"] != null)
        {
            string chiave = Session["r"].ToString(); // Es. "ruolo"
            Ruolo = Request.QueryString[chiave];     // Es. valore da ?ruolo=admin
            Cod = Session["cod"].ToString();         // Es. "12345"
        }

        string url = "https://localhost:7098/Comunicazioni/List?cod=" + Cod +
                     "&r=" + Ruolo;

        Response.Redirect(url);
    }
}




