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
        CaricaDati();
    }
    protected void CaricaDati()
    {
        FACOLTA f = new FACOLTA();

        GrigliaFacolta.DataSource = f.SelezionaTutto();
        GrigliaFacolta.DataBind();
    }

    protected void GrigliaFacolta_SelectedIndexChanged(object sender, EventArgs e)
    {
        string id = GrigliaFacolta.SelectedValue.ToString();

        Response.Redirect("ModificaFacolta.aspx?id=" + id);
    }
}