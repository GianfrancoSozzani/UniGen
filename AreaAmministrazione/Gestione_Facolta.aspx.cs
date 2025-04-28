using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
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
            CaricaFacolta();
        }
    }

    protected void CaricaFacolta()
    {
        FACOLTA f = new FACOLTA();
        rpFacolta.DataSource = f.SelezionaTutto(); // Funzione che carica tutte le facoltà
        rpFacolta.DataBind();
    }

    protected void btnSalva_Click(object sender, EventArgs e)
    {
        string titoloFacolta = txtFacolta.Text.Trim();

        // Controllo che il campo non sia vuoto
        if (String.IsNullOrEmpty(titoloFacolta))
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Il campo non può essere vuoto')", true);
            return;
        }

        // Aggiorno il campo TextBox con la stringa modificata
        txtFacolta.Text = titoloFacolta;

        // Controllo che non permette l'uso di numeri o caratteri speciali
        if (!System.Text.RegularExpressions.Regex.IsMatch(titoloFacolta, @"^[a-zA-Z\s]+$"))
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Inserire solo lettere')", true);
            return;
        }

        FACOLTA f = new FACOLTA();

        // Rendo la prima lettera scritta sempre maiuscola
        f.TitoloFacolta = char.ToUpper(titoloFacolta[0]) + titoloFacolta.Substring(1);

        DataTable dt = f.VerificaDuplicato();

        // Controllo duplicato
        if (dt.Rows.Count == 1)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Facoltà già presente')", true);
            return;
        }

        f.Inserimento();
        CaricaFacolta();
    }

    protected void btnSalvaModifica_Click(object sender, EventArgs e)
    {
        Guid id = Guid.Parse(hiddenIdFacolta.Value);
        string nuovoTitolo = txtTitoloFacolta.Text.Trim();

        // Controllo che il campo non sia vuoto
        if (string.IsNullOrEmpty(nuovoTitolo))
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Il titolo non può essere vuoto')", true);
            return;
        }

        // Controllo che non permette l'uso di numeri o caratteri speciali
        if (!System.Text.RegularExpressions.Regex.IsMatch(nuovoTitolo, @"^[a-zA-Z\s]+$"))
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Inserire solo lettere')", true);
            return;
        }

        FACOLTA f = new FACOLTA();
        f.K_Facolta = id;
        f.TitoloFacolta = char.ToUpper(nuovoTitolo[0]) + nuovoTitolo.Substring(1);

        DataTable dt = f.VerificaDuplicato();

        // Controllo duplicato
        if (dt.Rows.Count == 1)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Facoltà già presente')", true);
            return;
        }

        f.Modifica();

        CaricaFacolta();
    }

}
