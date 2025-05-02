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
    public int Matricola;
    protected void Page_Load(object sender, EventArgs e)

    { //VERSIONE MATRICOLA DA SESSION
        //if (Session["Matricola"] != null)
        //{
        //    int Matricola = Convert.ToInt32(Session["Matricola"]);
        //CaricaDati(Matricola)
        //}

        //else

        //{
        //    // SE LA MATRICOLA NON E' PRESENTE, RIMANDA A LOGIN
        //    Response.Write("Utente non trovato.");
        //    Response.Redirect("~/Login.aspx");
        //}

        Matricola = 123562;
        CaricaDati(Matricola);

    }

    protected void btnConfermaRinuncia_Click(object sender, EventArgs e)
    {
        STUDENTI stu = new STUDENTI();
        stu.Disabilita(Matricola);
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