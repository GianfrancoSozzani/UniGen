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

        rpFacolta.DataSource = f.SelezionaTutto();
        rpFacolta.DataBind();
    }

    protected void btnSalva_Click(object sender, EventArgs e)
    {
        string titoloFacolta = txtFacolta.Text.Trim();

        // Controllo campo non vuoto
        if (String.IsNullOrEmpty(titoloFacolta))
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Il campo non può essere vuoto')", true);
            return;
        }

        // Rendo la prima lettera scritta sempre maiuscola
        titoloFacolta = char.ToUpper(titoloFacolta[0]) + titoloFacolta.Substring(1);

        // Aggiorno il campo TextBox con la stringa modificata
        txtFacolta.Text = titoloFacolta;

        // Controllo che non permette l'uso di numeri o caratteri speciali
        if (!System.Text.RegularExpressions.Regex.IsMatch(titoloFacolta, @"^[a-zA-Z\s]+$"))
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Inserire solo lettere')", true);
            return;
        }

        FACOLTA f = new FACOLTA();
        f.TitoloFacolta = titoloFacolta;

        DataTable dt = f.VerificaDuplicato();

        // Controllo duplicato
        if (dt.Rows.Count == 1)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Facoltà già presente')", true);
            return;
        }

        f.Inserimento();
        insert.Visible = false;
        CaricaFacolta();

    }
}