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
        DataTable dt = s.SelezionaTutto();
        PopolaListaConPaginazione(dt, rptStudenti);

    }

    protected void btnRicerca_Click(object sender, EventArgs e)
    {
        ViewState["PaginaCorrente"] = 0;
        int matricolaRicerca;
        //se è un valore intero allora entra nell'if sennò devi inserire una matricola valida
        if (String.IsNullOrEmpty(txtRicercaMatricola.Text.Trim()))
        {
            lblErrore.Text = "";
            lblErrore.Visible = false;
            PopolaList();
            return;
        }

        if (int.TryParse(txtRicercaMatricola.Text.Trim(), out matricolaRicerca))
        {

            //avendo creato la variabile matricolaRicerca la poniamo uguale alla matricola della classe
            STUDENTI s = new STUDENTI();
            s.Matricola = matricolaRicerca;
            DataTable dt = s.SelezionaPerMatricola();

            if (dt != null && dt.Rows.Count > 0) //se la matricola esiste allora dt è maggiore di 0 e non è null
            {

                ViewState["RisultatiRicerca"] = dt;
                PopolaListaConPaginazione(dt, rptStudenti);
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

        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Studente abilitato con successo')", true);
        return dt != null && dt.Rows.Count > 0;

    }

    protected bool DisattivaStudente(string matrico)
    {
        STUDENTI s = new STUDENTI();
        s.Matricola = int.Parse(matrico);
        DataTable dt = s.Disattiva();

        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Studente disabilitato con successo')", true);
        return dt != null && dt.Rows.Count > 0;
    }

    //PAGINAZIONE
    private int PaginaCorrente
    {
        get { return ViewState["PaginaCorrente"] != null ? (int)ViewState["PaginaCorrente"] : 0; }
        set { ViewState["PaginaCorrente"] = value; }
    }
    public int GetPaginaCorrente()
    {
        return ViewState["PaginaCorrente"] != null ? (int)ViewState["PaginaCorrente"] : 0;
        ////return PaginaCorrente;
    }
    protected void PopolaListaConPaginazione(DataTable dati, Repeater rptDati)
    {
        PagedDataSource paged = new PagedDataSource();
        paged.DataSource = dati.DefaultView;
        paged.AllowPaging = true;
        paged.PageSize = 10;
        paged.CurrentPageIndex = PaginaCorrente;

        rptDati.DataSource = paged;
        rptDati.DataBind();

        // Pagine numeriche
        List<int> pagine = new List<int>();
        for (int i = 0; i < paged.PageCount; i++)
        {
            pagine.Add(i + 1); // Le pagine partono da 1
        }

        rptPaginazione.DataSource = pagine;
        rptPaginazione.DataBind();
    }
    protected void rptPaginazione_ItemCommand(object source, RepeaterCommandEventArgs e)
    {

        if (e.CommandName == "CambiaPagina")
        {
            int nuovaPagina = Convert.ToInt32(e.CommandArgument) - 1;
            ViewState["PaginaCorrente"] = nuovaPagina;

            DataTable dt;

            if (ViewState["RisultatiRicerca"] != null)
            {
                dt = (DataTable)ViewState["RisultatiRicerca"];
            }

            STUDENTI s = new STUDENTI();
            dt = s.SelezionaTutto();

            PopolaListaConPaginazione(dt, rptStudenti);
        }
    }


}