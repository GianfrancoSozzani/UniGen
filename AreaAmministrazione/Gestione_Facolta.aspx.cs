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
            GeneraTabellaFacolta();
        }
    }

    private void GeneraTabellaFacolta()
    {
        FACOLTA f = new FACOLTA();
        DataTable dt = f.SelezionaTutto();

        var html = new StringBuilder();

        html.Append("<table class='table table-striped table-bordered'>");
        html.Append("<thead><tr><th>ID</th><th>Facoltà</th></tr></thead>");
        html.Append("<tbody>");

        foreach (DataRow row in dt.Rows)
        {
            string id = row["K_Facolta"].ToString();
            string titolo = row["TitoloFacolta"].ToString();
            html.AppendFormat("<tr><td>{0}</td><td>{1}</td></tr>", id, titolo);
        }

        html.Append("</tbody></table>");
        litFacolta.Text = html.ToString();
    }
}