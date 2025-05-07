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
        if (!IsPostBack)
        {
            // 1. Prova a recuperare i dati dalla Session
            string K_Studente = Session["cod"] as string;
            string Matricola = Session["mat"] as string;
            string Abilitazione = Session["a"] as string;

            // 2. Se mancano, prova dalla QueryString
            if (string.IsNullOrEmpty(K_Studente) || string.IsNullOrEmpty(Matricola) || string.IsNullOrEmpty(Abilitazione))
            {
                K_Studente = Request.QueryString["cod"];
                Matricola = Request.QueryString["mat"];
                Abilitazione = Request.QueryString["a"];


                // Se ora li trovi, salvali nella sessione
                if (!string.IsNullOrEmpty(K_Studente) && !string.IsNullOrEmpty(Matricola) && !string.IsNullOrEmpty(Abilitazione))
                {
                    Session["cod"] = K_Studente;
                    Session["mat"] = Matricola;
                    Session["a"] = Abilitazione;

                }
            }

            // 3. Se ancora mancanti, mostra errore
            if (string.IsNullOrEmpty(K_Studente) || string.IsNullOrEmpty(Matricola) || string.IsNullOrEmpty(Abilitazione))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "authError", "showAuthErrorModal();", true);
                return;
            }

            // 4. Verifica che matricola e K_Studente corrispondano
            STUDENTI s = new STUDENTI();
            s.Matricola = int.Parse(Matricola);
            DataTable dt = s.SelezionaPerMatricola();
            if (dt.Rows.Count != 1 || dt.Rows[0]["K_Studente"].ToString() != K_Studente)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "authError", "showAuthErrorModal();", true);
                return;
            }

            // 5. Carica dati dello studente
            CaricaLBL(int.Parse(Matricola));

            // 6. Visibilità delle sezioni in base all'abilitazione
            bool abilitato = Abilitazione == "S";
            divLezioni.Visible = abilitato;
            divComunicazioni.Visible = abilitato;
            divAppelli.Visible = abilitato;
        }
    }

    public void CaricaLBL(int Matricola)
    {
        STUDENTI studente = new STUDENTI();
        studente.Matricola = Matricola;
        DataTable dt2 = studente.SelezionaAnnoAccademico(Matricola);

        if (dt2.Rows.Count >= 1)
        {
            string annoAccademico = dt2.Rows[0]["AnnoAccademico"].ToString();
            string corso = dt2.Rows[0]["TitoloCorso"].ToString();
            string facolta = dt2.Rows[0]["TitoloFacolta"].ToString();

            lblAnno.Text = "Anno Accademico " + annoAccademico;
            lblFacolta.Text = "Facoltà: " + facolta;
            lblCorso.Text = "Corso: " + corso;
        }
    }




}
