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
    public int Matricola;
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (Session["Matricola"] != null)
        //{
        //    int Matricola = Convert.ToInt32(Session["Matricola"]);
        //    CaricaAA(matricolaTest);
        //}
        //else
        //{
        //    // SE LA MATRICOLA NON E' PRESENTE, RIMANDA A LOGIN
        //    Response.Write("Utente non trovato.");
        //    Response.Redirect("~/Login.aspx");
        //}

        if (!IsPostBack)
        {

            string abilitazione = "S";
            /*Session["Abilitazione"]?.ToString();*/
            if (abilitazione == "N")
            {
                divLezioni.Visible = (abilitazione == "abilitato");
                divComunicazioni.Visible = (abilitazione == "abilitato");
                divAppelli.Visible = (abilitazione == "abilitato");

            }

            CaricaAA(Matricola);


        }



    }

    public void CaricaAA(int Matricola)
    {

        //TEST
        Matricola = 123562;

        STUDENTI studente = new STUDENTI();
        studente.Matricola = Matricola;
        DataTable dt = studente.SelezionaAnnoAccademico(Matricola);

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
