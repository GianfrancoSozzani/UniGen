using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
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
            CaricaNumDocenti();
            CaricaEsamiMeseCorrente();
            CaricaTassaMedia();
        }
    }
    protected void btnAggiorna_OnClick(object sender, EventArgs e)
    {

        CaricaStudenti();
        CaricaIncasso();
        CaricaNumCorsi();
        CaricaNumDocenti();
        CaricaEsamiMeseCorrente();
        CaricaTassaMedia();
    }
    protected void CaricaStudenti()
    {

        STUDENTI s = new STUDENTI();
        DataTable dt = s.StudentiIscritti();
        if (dt.Rows.Count == 0)
        {
            litStudentii.Text = "0";
            return;
        }
        litStudentii.Text = dt.Rows.Count.ToString();

    }
    protected void CaricaIncasso()
    {

        PAGAMENTI p = new PAGAMENTI();
        DataTable dt = p.IncassoAnnoCorrente();
        if (dt.Rows.Count == 0)
        {
            litIncassoC.Text = "0";
            return;
        }
        decimal incasso = Convert.ToDecimal(dt.Rows[0][0]);
        litIncassoC.Text = incasso.ToString("N2");
        //litIncassoC.Text = dt.Rows[0][0].ToString();

    }
    protected void CaricaNumCorsi()
    {

        CORSI c = new CORSI();
        DataTable dt = c.CorsiAttivi();
        if (dt.Rows.Count == 0)
        {
            litCorsiA.Text = "0";
            return;
        }
        litCorsiA.Text = dt.Rows[0][0].ToString();

    }

    protected void CaricaNumDocenti()
    {

        DOCENTI d = new DOCENTI();
        DataTable dt = d.DocentiAttivi();
        if (dt.Rows.Count == 0)
        {
            litDocenti.Text = "0";
            return;
        }
        litDocenti.Text = dt.Rows[0][0].ToString();

    }

    protected void CaricaEsamiMeseCorrente()
    {

        APPELLI a = new APPELLI();
        DataTable dt = a.CaricaEsamiMeseCorrente();
        if (dt.Rows.Count == 0)
        {
            litEsami.Text = "0";
            return;
        }
        litEsami.Text = dt.Rows[0][0].ToString();

    }
    protected void CaricaTassaMedia()
    {

        CORSI c = new CORSI();
        DataTable dt = c.TassaMediaAnnuale();
        if (dt.Rows[0][0].ToString() == "")
        {
            litTassaM.Text = "0";
            return;
        }

        decimal tassa = Convert.ToDecimal(dt.Rows[0][0]);
        litTassaM.Text = tassa.ToString("N2");

        //Massimo due cifre decimali
        //litTassaM.Text = Decimal.Parse(dt.Rows[0][0].ToString()).ToString("0.00");
        //litTassaM.Text = String.Format("{0:0,##}", dt.Rows[0][0].ToString());


    }

}
