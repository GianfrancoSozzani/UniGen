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
}