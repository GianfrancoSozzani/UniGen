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
    }

    //Popolo la dropdownlist dei tipicorsi
    protected void CaricaTipoCorsi()
    {
        TIPICORSI t = new TIPICORSI();
        ddlTipoCorso.DataSource = t.SelezionaTutto();
        ddlTipoCorso.DataTextField = "Tipo";
        ddlTipoCorso.DataValueField = "K_TipoCorso";
        ddlTipoCorso.DataBind();
    }

    protected void btnSalva_Click(object sender, EventArgs e)
    {
        string titoloCorso = txtTitoloCorso.Text.Trim();

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
        string inputCosto = txtCostoAnnuale.Text.Trim().Replace(".", ",");

        decimal costoAnnuale;
        if (!decimal.TryParse(inputCosto, out costoAnnuale))
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
}