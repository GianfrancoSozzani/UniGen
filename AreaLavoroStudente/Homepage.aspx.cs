﻿using System;
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
            //// 1. Prova a recuperare i dati dalla Session
            string K_Studente = Session["cod"] as string;
            string Matricola = Session["mat"] as string;
            string Abilitazione = Session["a"] as string;
            string Ruolo = Session["r"] as string;


            //// 2. Se mancano, prova dalla QueryString
            if (!string.IsNullOrEmpty(K_Studente) || !string.IsNullOrEmpty(Matricola) || !string.IsNullOrEmpty(Abilitazione) || !string.IsNullOrEmpty(Ruolo))
            {
                STUDENTI s = new STUDENTI();
                s.Matricola = int.Parse(Session["mat"].ToString());
                DataTable dt = s.SelezionaPerMatricola();
                if (dt.Rows.Count != 1 || dt.Rows[0]["K_Studente"].ToString() != Session["cod"].ToString())
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "authError", "showAuthErrorModal();", true);
                    return;
                }
            }
            else
            {
                Matricola = Request.QueryString["mat"];
                Abilitazione = Request.QueryString["a"];
            }

            if (!string.IsNullOrEmpty(Matricola))
            {
                // 5. Carica dati dello studente
                CaricaLBL(int.Parse(Matricola));

                // 6. Visibilità delle sezioni in base all'abilitazione
                bool abilitato = Abilitazione == "S";
                divLezioni.Visible = abilitato;
                divComunicazioni.Visible = abilitato;
                divAppelli.Visible = abilitato;
            }
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



    protected void btnComunicazioni_Click(object sender, EventArgs e)
    {
        string Ruolo;
        string Cod;
        string Mat;
        string A;
        Ruolo = Session["r"].ToString();
        Cod = Session["cod"].ToString();
        Mat = Session["mat"].ToString();
        A = Session["a"].ToString();

        string url = "https://localhost:7098/Comunicazioni/List?cod=" + Cod +
                 "&r=" + Ruolo + "&mat=" + Mat + "&a=" + A;
        Response.Redirect(url);

    }
}
