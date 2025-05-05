using System;
using System.Collections.Generic;
using System.Data;
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
            string Matricola = Session["mat"].ToString();
            Session["mat"] = Matricola;
            CaricaAA(int.Parse(Matricola));
            CaricaPagamentiEffettuati(int.Parse(Matricola));

        }
    }

    public void CaricaAA(int Matricola)
    {
        STUDENTI studente = new STUDENTI();
        studente.Matricola = Matricola;
        DataTable dt = studente.SelezionaAnnoAccademico(Matricola);

        if (dt.Rows.Count == 1)
        {
            string annoAccademico = dt.Rows[0]["AnnoAccademico"].ToString();
            string corso = dt.Rows[0]["TitoloCorso"].ToString();
            string facolta = dt.Rows[0]["TitoloFacolta"].ToString();

            lblAnno.Text = "Anno Accademico " + annoAccademico;
            lblCorso.Text = corso;
            lblFacolta.Text = facolta;
        }
    }

    public void CaricaPagamentiEffettuati(int Matricola)
    {
        
        PAGAMENTI m = new PAGAMENTI();
        rptPagamenti.DataSource = m.ListaPagamentiEffettuati(Matricola);
        rptPagamenti.DataBind();
    }
}