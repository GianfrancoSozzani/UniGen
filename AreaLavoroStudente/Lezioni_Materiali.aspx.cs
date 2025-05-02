using System;
using System.Activities.Expressions;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LibreriaClassi;

public partial class Default2 : System.Web.UI.Page
{
    Guid K_Esame;
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            //Simulazione della session

            Session["Matricola"] = 123563;
            Session["K_Studente"] = "382a8129-4725-4723-aecd-963b5e840509";

            CaricaDdl();
            CaricaFacoltaECorso();
            CaricaVideolezioni();
            CaricaDispense();

        }

    }

    private void CaricaDdl()
    {
        Guid K_Studente = new Guid((string)Session["K_Studente"]);
        ESAMI e = new ESAMI();
        ddlCaricaEsami.DataSource = e.SelezionaPsp(K_Studente);
        ddlCaricaEsami.DataValueField = ("K_Esame");
        ddlCaricaEsami.DataTextField = ("TitoloEsame");
        ddlCaricaEsami.DataBind();
        K_Esame = new Guid((string)ddlCaricaEsami.SelectedValue);
    }


    private void CaricaFacoltaECorso()
    {
        //Recupero matricola da session
        if (Session["Matricola"] == null)
        {
            // Se non c'è la session reindirizza al login
            Response.Redirect("Login.aspx");
            return;
        }

        int matricola = Convert.ToInt32(Session["Matricola"]);


        DB db = new DB();
        db.query = "GetFacoltaCorsoByMatricola";
        db.cmd.Parameters.Clear();
        db.cmd.Parameters.AddWithValue("@Matricola", matricola);
        DataTable dt = db.SQLselect();

        if (dt.Rows.Count > 0)
        {
            //Prendo i valori e li carico nelle label
            lblFacolta.Text = dt.Rows[0]["Facolta"].ToString();
            lblCorso.Text = dt.Rows[0]["Corso"].ToString();
        }
        else
        {
            lblFacolta.Text = "Facoltà non trovata";
            lblCorso.Text = "Corso non trovato";
        }
    }

    private System.Web.SessionState.HttpSessionState GetSession()
    {
        return Session;
    }


    private void CaricaVideolezioni()
    {
        if (Session["Matricola"] == null)
        {
            Response.Redirect("Login.aspx");
            return;
        }

        LEZIONI lezioni = new LEZIONI();
        Guid K_Studente = new Guid((string)Session["K_Studente"]);
        DataTable dt = lezioni.SelezionaPerMatricola(K_Studente, K_Esame);

        if (dt.Rows.Count > 0)
        {
            rptVideolezioni.DataSource = dt;
            rptVideolezioni.DataBind();
        }
        else
        {
            //lblMessaggio.Visible = true;
            //lblMessaggio.Text = "Non ci sono videolezioni disponibili per il tuo corso.";
            rptVideolezioni.DataSource = null;
            rptVideolezioni.DataBind();
        }
    }

    private void CaricaDispense()
    {
        if (Session["Matricola"] == null)
        {
            Response.Redirect("Login.aspx");
            return;
        }

        MATERIALI lezioni = new MATERIALI();
        Guid K_Studente = new Guid((string)Session["K_Studente"]);

      
        DataTable dt = lezioni.DispensaPerMatricola(K_Studente, K_Esame);

        if (dt.Rows.Count > 0)
        {
            rptDispense.DataSource = dt;
            rptDispense.DataBind();
        }
        else
        {
            //lblMessaggio.Visible = true;
            //lblMessaggio.Text = "Non ci sono dispense disponibili per il tuo corso.";
            rptDispense.DataSource = null;
            rptDispense.DataBind();
        }
    }


    protected void btnScarica_Command(object sender, CommandEventArgs e)
    {

        // Recupera K_Esame dal ViewState
        if (ViewState["K_Esame"] != null)
            K_Esame = new Guid((string)ViewState["K_Esame"]);
        else
        {
            lblMessaggio.Visible = true;
            lblMessaggio.Text = "Errore: esame non selezionato.";
            return;
        }

        string SalvaK_Materiale = e.CommandArgument.ToString();
        MATERIALI m = new MATERIALI();
        Guid K_Studente = new Guid((string)Session["K_Studente"]);
        DataTable dt = m.DispensaPerMatricola(K_Studente, K_Esame);

        if (dt != null && dt.Rows.Count > 0)
        {
            byte[] fileData = (byte[])dt.Rows[0]["Materiale"];
            string fileName = dt.Rows[0]["Titolo"].ToString();

            if (fileData != null)
            {
                Response.Clear();
                Response.ContentType = "application/pdf";
                Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName);
                Response.BinaryWrite(fileData);
                Response.End();
            }
        }
        else
        {
            lblMessaggio.Visible = true;
            lblMessaggio.Text = "Il materiale richiesto non è disponibile.";
        }
    }
   
    protected void ddlCaricaEsami_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["K_Esame"] = ddlCaricaEsami.SelectedValue;
        K_Esame = new Guid((string)ViewState["K_Esame"]);
        CaricaVideolezioni();
        CaricaDispense();
    }
}






