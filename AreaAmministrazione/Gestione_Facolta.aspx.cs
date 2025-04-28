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
        html.Append("<thead><tr><th>ID</th><th>Facoltà</th><th>Azioni</th></tr></thead>");
        html.Append("<tbody>");

        foreach (DataRow riga in dt.Rows)
        {
            string id = riga["K_Facolta"].ToString();
            string titolo = riga["TitoloFacolta"].ToString();

            html.Append("<tr>");
            html.AppendFormat("<td>{0}</td>", id);
            html.AppendFormat("<td>{0}</td>", titolo);
            html.AppendFormat("<td><a href='Gestione_FacoltaMod.aspx?id={0}' class='btn btn-warning btn-sm'>Modifica</a></td>",
                id
            );
            html.Append("</tr>");
        }

        html.Append("</tbody></table>");
        litFacolta.Text = html.ToString();

    }
}