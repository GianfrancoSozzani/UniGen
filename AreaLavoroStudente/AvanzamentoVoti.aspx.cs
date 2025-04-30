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
using System.Security.Cryptography.X509Certificates;

//VotiSelectMatricola
public partial class _Default : System.Web.UI.Page
{
    public int matricola = 123562;

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
          

            if (matricola == 0)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "ShowLoginModal", "showLoginModal();", true);
                //ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Utente non loggato')", true);
                //return;
            }
            else
            {
                CaricaMedia(matricola);
                CaricaCFU(matricola);
                CaricaEsami(matricola);
            }
        }
    }

    private void CaricaEsami(int matricola)
    {
        LIBRETTI lb = new LIBRETTI();
        DataTable dt = lb.SelezionaEsami(matricola);
        rptVoti.DataSource = dt;
        rptVoti.DataBind();
    }

    private void CaricaMedia(int matricola)
    {
        DB db = new DB();
        db.query = "Libretti_MediaVotiByMatricola";
        db.cmd.Parameters.Clear();
        db.cmd.Parameters.AddWithValue("@Matricola", matricola);

        DataTable dt = db.SQLselect();

        if (dt.Rows.Count > 0)
        {
            var mediaPonderata = dt.Rows[0]["MediaPonderata"];
            formMedia.DataSource = dt;
            formMedia.DataBind();

            Label lblMedia = (Label)formMedia.FindControl("lblMedia");
            if (lblMedia != null)
            {
                lblMedia.Text = mediaPonderata.ToString();
            }
        }
        else
        {
            Label lblMedia = (Label)formMedia.FindControl("lblMedia");
            if (lblMedia != null)
            {
                lblMedia.Text = "Nessuna media disponibile";
            }
        }
    }

    private void CaricaCFU(int matricola)
    {
        DB db = new DB();
        db.query = "Libretti_TotCFUByMatricola";
        db.cmd.Parameters.Clear();
        db.cmd.Parameters.AddWithValue("@Matricola", matricola);

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
