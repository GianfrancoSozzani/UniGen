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
            CaricaCorsi();
            CaricaFacolta();
            CaricaTipoCorsi();
            CaricaFacoltaModal();
            CaricaTipoCorsiModal();
        }
    }

    protected void CaricaCorsi()
    {
        CORSI c = new CORSI();
        rpCorso.DataSource = c.SelezionaTutto(); // Funzione che carica tutti i corsi con inner join di facolta e tipicorsi
        rpCorso.DataBind();
    }

    // Popolo la dropdownlist delle facoltà
    protected void CaricaFacolta()
    {
        FACOLTA f = new FACOLTA();
        ddlFacolta.DataSource = f.SelezionaTutto();
        ddlFacolta.DataTextField = "TitoloFacolta";
        ddlFacolta.DataValueField = "K_Facolta";
        ddlFacolta.DataBind();
        ddlFacolta.Items.Insert(0, new ListItem("Seleziona una facoltà", ""));
    }

    // Popolo la dropdownlist delle facoltà del MODAL
    protected void CaricaFacoltaModal()
    {
        FACOLTA f = new FACOLTA();
        ddlTitoloFacolta.DataSource = f.SelezionaTutto();
        ddlTitoloFacolta.DataTextField = "TitoloFacolta";
        ddlTitoloFacolta.DataValueField = "K_Facolta";
        ddlTitoloFacolta.DataBind();
    }

    //Popolo la dropdownlist dei tipicorsi
    protected void CaricaTipoCorsi()
    {
        TIPICORSI t = new TIPICORSI();
        ddlTipoCorso.DataSource = t.SelezionaTutto();
        ddlTipoCorso.DataTextField = "Tipo";
        ddlTipoCorso.DataValueField = "K_TipoCorso";
        ddlTipoCorso.DataBind();
        ddlTipoCorso.Items.Insert(0, new ListItem("Seleziona un corso", ""));
    }

    //Popolo la dropdownlist dei tipicorsi del MODAL
    protected void CaricaTipoCorsiModal()
    {
        TIPICORSI t = new TIPICORSI();
        ddlTitoloTipo.DataSource = t.SelezionaTutto();
        ddlTitoloTipo.DataTextField = "Tipo";
        ddlTitoloTipo.DataValueField = "K_TipoCorso";
        ddlTitoloTipo.DataBind();
    }

    protected void btnSalva_Click(object sender, EventArgs e)
    {
        string titoloCorso = txtCorso.Text.Trim();

        // Controllo che i campi non siano vuoti
        if (String.IsNullOrEmpty(titoloCorso) ||
            String.IsNullOrEmpty(txtCostoAnnuale.Text) ||
            String.IsNullOrEmpty(txtMinimoCFU.Text) ||
            String.IsNullOrEmpty(ddlFacolta.SelectedValue) ||
            String.IsNullOrEmpty(ddlTipoCorso.SelectedValue))
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Non sono ammessi campi vuoti')", true);
            return;
        }

        // Controllo che non permette l'uso di numeri o caratteri speciali
        if (!System.Text.RegularExpressions.Regex.IsMatch(titoloCorso, @"^[a-zA-Z\s]+$"))
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Inserire solo lettere per il corso')", true);
            return;
        }

        // Controllo CFU numerico intero e maggiore di 0
        short minimoCFU;
        if (!short.TryParse(txtMinimoCFU.Text.Trim(), out minimoCFU) || minimoCFU <= 0)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Inserisci un numero intero valido per i CFU')", true);
            return;
        }

        // Controllo costoannuale numerico decimale dove accetta anche il punto e lo converte in virgola
        string inserimentoCosto = txtCostoAnnuale.Text.Trim().Replace(".", ",");

        decimal costoAnnuale;
        if (!decimal.TryParse(inserimentoCosto, out costoAnnuale))
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Inserisci un valore numerico valido per il costo annuale')", true);
            return;
        }

        CORSI c = new CORSI();

        // Rendo la prima lettera scritta sempre maiuscola
        c.TitoloCorso = char.ToUpper(titoloCorso[0]) + titoloCorso.Substring(1);

        c.MinimoCFU = minimoCFU;
        c.CostoAnnuale = costoAnnuale;
        c.K_Facolta = Guid.Parse(ddlFacolta.SelectedValue);
        c.K_TipoCorso = Guid.Parse(ddlTipoCorso.SelectedValue);

        DataTable dt = c.VerificaDuplicato();

        // Controllo duplicato
        if (dt.Rows.Count == 1)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Corso già presente')", true);
            return;
        }

        c.Inserimento();
        CaricaCorsi();
    }

    protected void btnSalvaModifica_Click(object sender, EventArgs e)
    {
        Guid id = Guid.Parse(hiddenIdCorso.Value);
        string nuovoTitoloCorso = txtTitoloCorso.Text.Trim();
        string nuovoTitoloMinimoCFU = txtTitoloMinimoCFU.Text.Trim();
        string nuovoTitoloCostoAnnuale = txtTitoloCostoAnnuale.Text.Trim();

        // Controllo che il campo non sia vuoto
        if (string.IsNullOrEmpty(txtTitoloCorso.Text) ||
           string.IsNullOrEmpty(txtTitoloMinimoCFU.Text) ||
           string.IsNullOrEmpty(txtTitoloCostoAnnuale.Text))
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Non sono ammessi campi vuoti')", true);
            return;
        }

        // Controllo che non permette l'uso di numeri o caratteri speciali
        if (!System.Text.RegularExpressions.Regex.IsMatch(nuovoTitoloCorso, @"^[a-zA-Z\s]+$"))
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Inserire solo lettere per il corso')", true);
            return;
        }

        // Controllo CFU numerico intero e maggiore di 0
        short minimoCFU;
        if (!short.TryParse(txtTitoloMinimoCFU.Text.Trim(), out minimoCFU) || minimoCFU <= 0)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Inserisci un numero intero valido per i CFU')", true);
            return;
        }

        // Controllo costoannuale numerico decimale dove accetta anche il punto e lo converte in virgola
        string inserimentoCosto = nuovoTitoloCostoAnnuale.Replace(".", ",");

        decimal costoAnnuale;
        if (!decimal.TryParse(inserimentoCosto, out costoAnnuale))
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Inserisci un valore numerico valido per il costo annuale')", true);
            return;
        }

        CORSI c = new CORSI();
        c.K_Corso = id;
        c.TitoloCorso = char.ToUpper(nuovoTitoloCorso[0]) + nuovoTitoloCorso.Substring(1);
        c.MinimoCFU = minimoCFU;
        c.CostoAnnuale = costoAnnuale;
        c.K_Facolta = Guid.Parse(ddlTitoloFacolta.SelectedValue);
        c.K_TipoCorso = Guid.Parse(ddlTitoloTipo.SelectedValue);

        // Controllo duplicato
        DataTable dt = c.VerificaDuplicatoModifica();

        // Controllo duplicato
        if (dt.Rows.Count == 1)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Corso già presente')", true);
            return;
        }

        c.Modifica();

        CaricaCorsi();
    }

    protected void btnRicerca_Click(object sender, EventArgs e)
    {
        //avendo creato la variabile matricolaRicerca la poniamo uguale alla matricola della classe
        CORSI corsi = new CORSI();
        corsi.TitoloCorso = txtRicercaCorso.Text.Trim();
        DataTable dt = corsi.SelezionaPerNome();
        if (dt != null && dt.Rows.Count > 0) //se la matricola esiste allora dt è maggiore di 0 e non è null
        {

            rpCorso.DataSource = dt.DefaultView; //la datasource del repeater diventa dt 
            rpCorso.DataBind();

            lblErrore.Visible = false;
        }
        else
        {
            lblErrore.Text = "Nessun corso trovato con il titolo inserito.";
            lblErrore.Visible = true;
            CaricaCorsi();
            //rptStudenti.DataSource = null;
            rpCorso.DataBind();
        }
    }
}