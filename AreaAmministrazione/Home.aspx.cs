using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.DynamicData;
using System.Web.UI;
using System.Web.UI.WebControls;
using LibreriaClassi;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CaricaStudenti();
            CaricaIncasso();
            CaricaNumCorsi();
        }
    }
    protected void btnAggiorna_OnClick(object sender, EventArgs e)
    {

        CaricaStudenti();
        CaricaIncasso();
        CaricaNumCorsi();
    }
    protected void CaricaStudenti()
    {

        STUDENTI s = new STUDENTI();
        DataTable dt = s.StudentiIscritti();
        litStudenti.Text = dt.Rows[0][0].ToString();

    }
    protected void CaricaIncasso()
    {

        PAGAMENTI p = new PAGAMENTI();
        DataTable dt = p.IncassoAnnoCorrente();
        litIncasso.Text = dt.Rows[0][0].ToString();

    }
    protected void CaricaNumCorsi()
    {

        CORSI c = new CORSI();
        DataTable dt = c.CorsiAttivi();
        litCorsi.Text = dt.Rows[0][0].ToString();

    }

}