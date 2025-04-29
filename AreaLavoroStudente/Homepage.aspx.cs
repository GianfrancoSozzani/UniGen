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
    public int matricolaTest;
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (Session["Matricola"] != null)
        //{
        //    int matricolaTest = Convert.ToInt32(Session["Matricola"]);
        //    CaricaAA(matricolaTest);
        //}
        //else
        //{
        //    // Gestisci il caso in cui la matricola non è presente nella sessione
        //    Response.Write("Utente non trovato.");
        //    Response.Redirect("~/Login.aspx");

        //}
        if (!IsPostBack)
        {

            string abilitazione = "N";
                /*Session["Abilitazione"]?.ToString();*/ 
            if (abilitazione == "N")
            { divLezioni.Visible = (abilitazione == "admin");
            divComunicazioni.Visible = (abilitazione == "admin");
            divAppelli.Visible = (abilitazione == "admin");

            }

            // Nasconde la card se non sei admin
           
        }

        CaricaAA(matricolaTest);

    }

public void CaricaAA(int matricolaTest)
{
    matricolaTest = 1;
    STUDENTI studente = new STUDENTI();
    studente.Matricola = matricolaTest;
    DataTable dt = studente.SelezionaAnnoAccademico();

    if (dt.Rows.Count == 1)
    {
        string annoAccademico = dt.Rows[0]["AnnoAccademico"].ToString();
        string corso = dt.Rows[0]["TitoloCorso"].ToString();
        string facolta = dt.Rows[0]["TitoloFacolta"].ToString();

        lblAnno.Text = "Anno Accademico " + annoAccademico;
        lblCorso.Text = corso;
        lblFacolta.Text = facolta;
        }
}
}
