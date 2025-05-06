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
            PopolaList();
        }
    }
    protected void PopolaList()
    {
        STUDENTI s = new STUDENTI();
        rptStudenti.DataSource = s.SelezionaTutto();
        rptStudenti.DataBind();
    }
    protected void btnRicerca_Click(object sender, EventArgs e)
    {
        int matricolaRicerca;
        //se è un valore intero allora entra nell'if sennò devi inserire una matricola valida
        if (int.TryParse(txtRicercaMatricola.Text.Trim(), out matricolaRicerca))
        {

            //avendo creato la variabile matricolaRicerca la poniamo uguale alla matricola della classe
            STUDENTI s = new STUDENTI();
            s.Matricola = matricolaRicerca;
            DataTable dt = s.SelezionaPerMatricola();

            if (dt != null && dt.Rows.Count > 0) //se la matricola esiste allora dt è maggiore di 0 e non è null
            {

                rptStudenti.DataSource = dt.DefaultView; //la datasource del repeater diventa dt 
                rptStudenti.DataBind();

                lblErrore.Visible = false;
            }
            else
            {
                lblErrore.Text = "Nessuno studente trovato con la matricola inserita.";
                lblErrore.Visible = true;
                PopolaList();
                //rptStudenti.DataSource = null;
                rptStudenti.DataBind();

            }
        }
        else
        {
            lblErrore.Text = "Inserisci una matricola valida.";
            lblErrore.Visible = true;
            // Potresti voler ricaricare l'intera lista o pulire il Repeater qui
            PopolaList();
            //rptStudenti.DataSource = null;
            rptStudenti.DataBind();

        }
    }

    protected void rptStudenti_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        string matricola = e.CommandArgument.ToString();

        if (!string.IsNullOrEmpty(matricola))
        {
            if (e.CommandName == "Abilita")
            {
                if (AttivaStudente(matricola))
                {
                    lblErrore.Visible = true;
                    lblErrore.Text = "Errore durante l'abilitazione dello studente.";
                }
                else
                {
                    lblErrore.Visible = true;
                    lblErrore.Text = "Studente abilitato.";
                }
            }
            else if (e.CommandName == "Disabilita")
            {
                if (DisattivaStudente(matricola))
                {
                    lblErrore.Visible = true;
                    lblErrore.Text = "Errore durante la disabilitazione dello studente.";
                }
                else
                {
                    lblErrore.Visible = true;
                    lblErrore.Text = "Studente disabilitato.";
                }
            }

            lblErrore.Visible = true;
            PopolaList();
        }
    }

    protected bool AttivaStudente(string matricola)
    {
        STUDENTI s = new STUDENTI();
        s.Matricola = int.Parse(matricola);
        DataTable dt = s.Attiva();
        return dt != null && dt.Rows.Count > 0;
    }

    protected bool DisattivaStudente(string matrico)
    {
        STUDENTI s = new STUDENTI();
        s.Matricola = int.Parse(matrico);
        DataTable dt = s.Disattiva();
        return dt != null && dt.Rows.Count > 0;
    }

}