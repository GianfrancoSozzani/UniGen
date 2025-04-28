using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.EnterpriseServices;
using LibreriaClassi;
using System.ComponentModel;
using System.Activities.Expressions;


//VotiSelectMatricola
public partial class _Default : System.Web.UI.Page
{
    public int matricolaTest;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            //if (Session["Matricola"] == null)
            //{
            //    ClientScript.RegisterStartupScript(this.GetType(), "ShowLoginModal", "showLoginModal();", true);

            //}
            //else
            //{
            //    CaricaCorso(matricolaTest);
            //    CaricaMedia(matricolaTest);
            //    CaricaEsami(matricolaTest);
            //}

            matricolaTest = 1;

            if (matricolaTest == 0)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "ShowLoginModal", "showLoginModal();", true);
                //ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Utente non loggato')", true);
                //return;
            }
            else
            {
                CaricaMedia(matricolaTest);
                CaricaCFU(matricolaTest);
                CaricaEsami(matricolaTest);
            }



        }
    }

    private void CaricaEsami(int matricolaTest)
    {
        DB db = new DB();
        db.query = "Libretti_EsamiByMatricola";
        db.cmd.Parameters.Clear();
        db.cmd.Parameters.AddWithValue("@Matricola", matricolaTest);

        DataTable dt = db.SQLselect();

        rptVoti.DataSource = dt;
        rptVoti.DataBind();
    }

    private void CaricaMedia(int matricolaTest)
    {
        DB db = new DB();
        db.query = "Libretti_MediaVotiByMatricola";
        db.cmd.Parameters.Clear();
        db.cmd.Parameters.AddWithValue("@Matricola", matricolaTest);

        DataTable dt = db.SQLselect();

        if (dt.Rows.Count > 0)
        {
            // Prepara il dato da associare (la media ponderata)
            var mediaPonderata = dt.Rows[0]["MediaPonderata"];

            // Associa il DataTable al FormView
            formMedia.DataSource = dt;
            formMedia.DataBind();

            // Dopo DataBind, usa FindControl per ottenere il controllo e impostare il valore
            Label lblMedia = (Label)formMedia.FindControl("lblMedia");
            if (lblMedia != null)
            {
                lblMedia.Text = mediaPonderata.ToString();
            }
        }
        else
        {
            // Gestisci il caso in cui non ci siano risultati (ad esempio, nessun voto trovato)
            Label lblMedia = (Label)formMedia.FindControl("lblMedia");
            if (lblMedia != null)
            {
                lblMedia.Text = "Nessuna media disponibile";
            }
        }

    }

    private void CaricaCFU(int matricolaTest)
    {

        DB db = new DB();
        db.query = "Libretti_TotCFUByMatricola";
        db.cmd.Parameters.Clear();
        db.cmd.Parameters.AddWithValue("@Matricola", matricolaTest);

        DataTable dt = db.SQLselect();

        if (dt.Rows.Count > 0)
        {

            var totaleCFU = dt.Rows[0]["TotaleCFU"];

            formCFU.DataSource = dt;
            formCFU.DataBind();

            Label lblCFU = (Label)formCFU.FindControl("lblCFU");
            if (lblCFU != null)
            {
                lblCFU.Text = totaleCFU.ToString() + " CFU";
            }
        }
        else
        {
            Label lblCFU = (Label)formCFU.FindControl("lblMedia");
            if (lblCFU != null)
            {
                lblCFU.Text = "Nessuna media disponibile";
            }
        }

    }

}
    












