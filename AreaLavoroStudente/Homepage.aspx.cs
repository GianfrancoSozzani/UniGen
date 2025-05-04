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


    protected void Page_Load(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty((string)Session["mat"]) || string.IsNullOrEmpty((string)Session["a"]))
        {
            // RECUPERO PARAMETRI DA QUERYSTRING
            string K_Studente = Request.QueryString["cod"];
            string Matricola = Request.QueryString["mat"];
            string Abilitazione = Request.QueryString["a"];

            // CONTROLLO SE I PARAMETRI NON SONO CORRETTI (SESSIONS O PARAMETRI)
            if (string.IsNullOrEmpty(K_Studente) || string.IsNullOrEmpty(Matricola) || string.IsNullOrEmpty(Abilitazione))
            {
                // Se i parametri non sono validi, mostra modal
                ClientScript.RegisterStartupScript(this.GetType(), "authError", "showAuthErrorModal();", true);
                return;
            }

            // CONTROLLO CHE K_STUDENTE E MATRICOLA CORRISPONDANO
            STUDENTI s = new STUDENTI();
            DataTable dt = s.SelezionaPerMatricola(int.Parse(Matricola));
            if (dt.Rows.Count != 1 || dt.Rows[0]["K_Studente"].ToString() != K_Studente)
            {
                // Se non corrispondono, mostra il modal di errore
                ClientScript.RegisterStartupScript(this.GetType(), "authError", "showAuthErrorModal();", true);
                return;
            }

            if ((Session["mat"] == null) || (Session["a"] == null) || (Session["cod"] == null))
            {
            Session["mat"] = Matricola;
            Session["a"] = Abilitazione;
            Session["cod"] = K_Studente;

            }
            // SALVA DATI IN SESSION
        }

        /*VISBIILITA*/
        if (Session["a"].ToString() == "S")
        {
            divLezioni.Visible = true;
            divComunicazioni.Visible = true;
            divAppelli.Visible = true;
        }
        else
        {
            divLezioni.Visible = false;
            divComunicazioni.Visible = false;
            divAppelli.Visible = false;
        }
        //CARICO SPECIFICHE STUDENTE
        CaricaAA();
    }

    public void CaricaAA()
    {
        STUDENTI studente = new STUDENTI();
        //studente.Matricola = Matricola;
        DataTable dt = studente.SelezionaAnnoAccademico(int.Parse(Session["mat"].ToString()));

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
