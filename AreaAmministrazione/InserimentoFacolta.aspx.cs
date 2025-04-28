using System;
using System.Collections.Generic;
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
            FACOLTA facolta = new FACOLTA();
            //grdView.DataSource = facolta.SelezionaTutto();
            //grdView.DataBind();
        }

    }

    protected void btnInserisci_Click(object sender, EventArgs e)
    {
        if (String.IsNullOrEmpty(txtTitoloFacolta.Text))
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Dati non validi');", true);
            return;
        }
        FACOLTA facolta = new FACOLTA();
        facolta.TitoloFacolta = txtTitoloFacolta.Text;
        if (facolta.VerificaDuplicato().Rows.Count > 0)
        {

            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Facoltà già presente');", true);
            return;

        }
        facolta.Inserimento();
    }
}