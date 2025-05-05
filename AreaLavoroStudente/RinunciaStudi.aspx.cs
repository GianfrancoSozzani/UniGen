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

        string Matricola = Session["mat"].ToString();
        string Abilitazione = Session["a"].ToString();
        CaricaDati(int.Parse(Matricola));

    }

    protected void btnConfermaRinuncia_Click(object sender, EventArgs e)
    {
        STUDENTI stu = new STUDENTI();
        string Matricola = Session["mat"].ToString();
        stu.Disabilita(int.Parse(Matricola));
        Session["a"] = "N";
        Response.Redirect("Homepage.aspx");

    }

    protected void CaricaDati(int Matricola)
    {
        STUDENTI s = new STUDENTI();
        DataTable dt = s.SelezionaDatiRinuncia(Matricola);

        if (dt.Rows.Count == 1)
        {
            lblMatricola.Text = dt.Rows[0]["Matricola"].ToString();
            lblNome.Text = dt.Rows[0]["Nome"].ToString();
            lblCognome.Text = dt.Rows[0]["Cognome"].ToString();
            lblCorso.Text = dt.Rows[0]["TitoloCorso"].ToString();
            lblDataRichiesta.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }
    }

   
}