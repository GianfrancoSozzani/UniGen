using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {

        lblNomeUtente.Text = Request.QueryString["usr"] ?? "Amministratore";
        //lblNomeUtente.Text = Request.QueryString["usr"] != null ? "Benvenuto " + Request.QueryString["usr"].ToString() : "Benvenuto Amministratore";
        //bigger font

    }
}
