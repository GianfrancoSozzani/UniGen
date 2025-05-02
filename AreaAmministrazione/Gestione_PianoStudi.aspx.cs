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
        FACOLTA f = new FACOLTA();
        ddlFacolta.DataSource = f.SelezionaTutto();
        ddlFacolta.DataTextField = "TitoloFacolta";
        ddlFacolta.DataValueField = "K_Facolta";
        ddlFacolta.DataBind();
    }

    protected void btnAggiungiEsame_Click(object sender, EventArgs e)
    {

    }
}