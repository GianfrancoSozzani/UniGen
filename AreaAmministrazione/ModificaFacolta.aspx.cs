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
    static string chiave;
    protected void Page_Load(object sender, EventArgs e)
    {
        chiave = Request.QueryString["id"].ToString();

        if (!IsPostBack)
        {
            // richiamo le facoltà dal database
            FACOLTA f = new FACOLTA();
            f.K_Facolta = Guid.Parse(chiave);

            DataTable DT = new DataTable();
            DT = f.SelezionaChiave();

            txtFacolta.Text = DT.Rows[0]["TitoloFacolta"].ToString();

        }
    }

    protected void btnSalva_Click(object sender, EventArgs e)
    {
        string titoloFacolta = txtFacolta.Text.Trim();

        if (String.IsNullOrEmpty(titoloFacolta))
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Il campo non può essere vuoto')", true);
            return;
        }

        // Rendo la prima lettera scritta sempre maiuscola
        titoloFacolta = char.ToUpper(titoloFacolta[0]) + titoloFacolta.Substring(1);

        // Aggiorno il campo TextBox con la stringa modificata
        txtFacolta.Text = titoloFacolta;

        // controllo che non permette l'uso di numeri o caratteri speciali
        if (!System.Text.RegularExpressions.Regex.IsMatch(titoloFacolta, @"^[a-zA-Z\s]+$"))
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Inserire solo lettere')", true);
            return;
        }

        FACOLTA f = new FACOLTA();
        f.TitoloFacolta = titoloFacolta;
        f.K_Facolta = Guid.Parse(chiave);

        f.Modifica();
        Response.Redirect("Gestione_Facolta.aspx");
    }
}