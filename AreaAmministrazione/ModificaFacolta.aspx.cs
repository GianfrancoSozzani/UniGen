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
            // apro il collegamento al db
            FACOLTA f = new FACOLTA();
            f.K_Facolta = Guid.Parse(chiave);

            DataTable DT = new DataTable();
            DT = f.SelezionaChiave();

            // popolo la txt Cognome
            txtFacolta.Text = DT.Rows[0]["TitoloFacolta"].ToString();

        }
    }

    protected void btnSalva_Click(object sender, EventArgs e)
    {
        if (String.IsNullOrEmpty(txtFacolta.Text))
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Il campo non può essere vuoto')", true);
            return;
        }

        // controllo che non accetta numeri o caratteri speciali
        if (!System.Text.RegularExpressions.Regex.IsMatch(txtFacolta.Text, @"^[a-zA-Z\s]+$"))
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Inserire solo lettere')", true);
            return;
        }

        FACOLTA f = new FACOLTA();
        f.TitoloFacolta = txtFacolta.Text.Trim();
        f.K_Facolta = Guid.Parse(chiave);

        f.Modifica();
        Response.Redirect("GestioneFacolta2.aspx");
    }
}