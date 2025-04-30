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
            CaricaEsami();
            CaricaDocenti();
            CaricaDocentiModal();
        }
    }

    protected void CaricaEsami()
    {
        ESAMI e = new ESAMI();
        rpEsame.DataSource = e.SelezionaTutto(); // Funzione che carica tutti gli esami
        rpEsame.DataBind();
    }

    // Popolo la dropdownlist dei docenti
    protected void CaricaDocenti()
    {
        DOCENTI d = new DOCENTI();
        ddlDocente.DataSource = d.SelezionaNomeCompleto();
        ddlDocente.DataTextField = "NomeCompleto";
        ddlDocente.DataValueField = "K_Docente";
        ddlDocente.DataBind();
    }

    // Popolo la dropdownlist dei docenti MODAL
    protected void CaricaDocentiModal()
    {
        DOCENTI d = new DOCENTI();
        ddlTitoloDocente.DataSource = d.SelezionaNomeCompleto();
        ddlTitoloDocente.DataTextField = "NomeCompleto";
        ddlTitoloDocente.DataValueField = "K_Docente";
        ddlTitoloDocente.DataBind();
    }

    protected void btnSalva_Click(object sender, EventArgs e)
    {
        string titoloEsame = txtEsami.Text.Trim();

        // Controllo che i campi non siano vuoti
        if (String.IsNullOrEmpty(titoloEsame) ||
            String.IsNullOrEmpty(txtCFU.Text))
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Non sono ammessi campi vuoti')", true);
            return;
        }

        // Controllo che non permette l'uso caratteri speciali
        if (!System.Text.RegularExpressions.Regex.IsMatch(titoloEsame, @"^[a-zA-Z0-9\s]+$"))
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Inserisci solo lettere per l\\'esame')", true);
            return;
        }

        // Controllo CFU numerico intero e maggiore di 0
        byte CFU;
        if (!byte.TryParse(txtCFU.Text.Trim(), out CFU) || CFU <= 0)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Inserisci un numero intero valido per i CFU')", true);
            return;
        }

        ESAMI es = new ESAMI();

        // Rendo la prima lettera scritta sempre maiuscola
        es.TitoloEsame = char.ToUpper(titoloEsame[0]) + titoloEsame.Substring(1);

        es.CFU = CFU;
        es.K_Docente = Guid.Parse(ddlDocente.SelectedValue);

        DataTable dt = es.VerificaDuplicato();

        // Controllo duplicato
        if (dt.Rows.Count == 1)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Esame già presente')", true);
            return;
        }

        es.Inserimento();
        CaricaEsami();
    }

    protected void btnSalvaModifica_Click(object sender, EventArgs e)
    {
        Guid id = Guid.Parse(hiddenIdEsame.Value);
        string nuovoTitoloEsame = txtTitoloEsame.Text.Trim();
        string nuovoTitoloCFU = txtTitoloCFU.Text.Trim();

        // Controllo che il campo non sia vuoto
        if (string.IsNullOrEmpty(nuovoTitoloEsame) ||
           string.IsNullOrEmpty(nuovoTitoloCFU))
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Non sono ammessi campi vuoti')", true);
            return;
        }

        // Controllo che non permette l'uso di caratteri speciali
        if (!System.Text.RegularExpressions.Regex.IsMatch(nuovoTitoloEsame, @"^[a-zA-Z0-9\s]+$"))
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Inserisci solo lettere per l\\'esame')", true); // I due \\ servono per impedire la chiusura prematura della stringa dell'alert
            return;
        }

        // Controllo CFU numerico intero e maggiore di 0
        byte CFU;
        if (!byte.TryParse(nuovoTitoloCFU, out CFU) || CFU <= 0)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Inserisci un numero intero valido per i CFU')", true);
            return;
        }        

        ESAMI es = new ESAMI();
        es.K_Esame = id;
        es.TitoloEsame = char.ToUpper(nuovoTitoloEsame[0]) + nuovoTitoloEsame.Substring(1);
        es.CFU = CFU;
        es.K_Docente = Guid.Parse(ddlTitoloDocente.SelectedValue);

        // Controllo duplicato
        DataTable dt = es.VerificaDuplicato();

        // Controllo duplicato
        if (dt.Rows.Count == 1)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Esame già presente')", true);
            return;
        }

        es.Modifica();

        CaricaEsami();
    }
}