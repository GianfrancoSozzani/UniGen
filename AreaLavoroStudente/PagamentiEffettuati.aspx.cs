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
    public int matricola;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //if (Session["matricola"] == null || !int.TryParse(Session["matricola"].ToString(), out matricola) || matricola == 0)
            //{
            //    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Utente non loggato');", true);
            //    Response.Redirect("~/Login.aspx");
            //  
            //}

            matricola = 123546;//verrà sostituita con la session 
            CaricaAA(matricola);
            CaricaPagamentiEffettuati();

        }
    }

    public void CaricaAA(int matricola)
    {
        matricola = 123546;
        STUDENTI studente = new STUDENTI();
        studente.Matricola = matricola;
        DataTable dt = studente.SelezionaAnnoAccademico();

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

    public void CaricaPagamentiEffettuati()
    {
        matricola = 123546; //da sostituire con la session
        PAGAMENTI m = new PAGAMENTI();
        rptPagamenti.DataSource = m.ListaPagamentiEffettuati(matricola);
        rptPagamenti.DataBind();
    }
}