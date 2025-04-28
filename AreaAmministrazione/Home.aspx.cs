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
        litStudentii.Text = dt.Rows[0][0].ToString();

    }
    protected void CaricaIncasso()
    {

        PAGAMENTI p = new PAGAMENTI();
        DataTable dt = p.IncassoAnnoCorrente();
        litIncassoC.Text = dt.Rows[0][0].ToString();

    }
    protected void CaricaNumCorsi()
    {

        CORSI c = new CORSI();
        DataTable dt = c.CorsiAttivi();
        litCorsiA.Text = dt.Rows[0][0].ToString();

    }

    protected void CaricaNumDocenti()
    {

        DOCENTI d = new DOCENTI();
        DataTable dt = d.DocentiAttivi();
        litDocenti.Text = dt.Rows[0][0].ToString();

    }

    protected void CaricaEsamiMeseCorrente()
    {

        APPELLI a = new APPELLI();
        DataTable dt = a.CaricaEsamiMeseCorrente();
        litEsami.Text = dt.Rows[0][0].ToString();

    }
    protected void CaricaTassaMedia()
    {

        CORSI c = new CORSI();
        DataTable dt = c.TassaMediaAnnuale();
        litTassaM.Text = dt.Rows[0][0].ToString();

    }

}
