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
            string Matricola = Session["mat"].ToString();
            Session["mat"] = Matricola;
            string K_Studente = Session["cod"].ToString();
            Session["cod"]= K_Studente;
            

            CaricaDdl();
            CaricaFacoltaECorso();
            CaricaVideolezioni();
            CaricaDispense();

            //Carica dati dello studente
            CaricaAA(int.Parse(Matricola));
        }

    }

    public void CaricaAA(int Matricola)
    {
        STUDENTI studente = new STUDENTI();
        studente.Matricola = Matricola;
        DataTable dt = studente.SelezionaAnnoAccademico(Matricola);

        if (dt.Rows.Count >= 1)
        {
            string annoAccademico = dt.Rows[0]["AnnoAccademico"].ToString();
            string corso = dt.Rows[0]["TitoloCorso"].ToString();
            string facolta = dt.Rows[0]["TitoloFacolta"].ToString();

            lblAnno.Text = "Anno Accademico " + annoAccademico;
            lblF.Text = "Facoltà: " + facolta;
            lblC.Text = "Corso: " + corso;
        }
    }

    private void CaricaDdl()
    {
        

        Guid K_Studente = new Guid((string)Session["cod"]);
        ESAMI e = new ESAMI();
        ddlCaricaEsami.DataSource = e.SelezionaPsp(K_Studente);
        ddlCaricaEsami.DataValueField = "K_Esame";
        ddlCaricaEsami.DataTextField = "TitoloEsame";
        ddlCaricaEsami.DataBind();

        if (ddlCaricaEsami.Items.Count > 0)
        {
            K_Esame = new Guid(ddlCaricaEsami.SelectedValue);
            ViewState["K_Esame"] = ddlCaricaEsami.SelectedValue;
        }

    }


    private void CaricaFacoltaECorso()
    {
        //Recupero matricola da session
        if (Session["mat"] == null)
        {
            // Se non c'è la session reindirizza al login
            Response.Redirect("Login.aspx");
            return;
        }

        int matricola = Convert.ToInt32(Session["mat"]);


        DB db = new DB();
        db.query = "GetFacoltaCorsoByMatricola";
        db.cmd.Parameters.Clear();
        db.cmd.Parameters.AddWithValue("@Matricola", matricola);
        DataTable dt = db.SQLselect();

        //if (dt.Rows.Count > 0)
        //{
        //    //Prendo i valori e li carico nelle label
        //    lblFacolta.Text = dt.Rows[0]["Facolta"].ToString();
        //    lblCorso.Text = dt.Rows[0]["Corso"].ToString();
        //}
        //else
        //{
        //    lblFacolta.Text = "Facoltà non trovata";
        //    lblCorso.Text = "Corso non trovato";
        //}
    }

    private System.Web.SessionState.HttpSessionState GetSession()
    {
        return Session;
    }


    private void CaricaVideolezioni()
    {
        if (Session["mat"] == null)
        {
            Response.Redirect("Login.aspx");
            return;
        }

        LEZIONI lezioni = new LEZIONI();
        Guid K_Studente = new Guid((string)Session["cod"]);
        DataTable dt = lezioni.SelezionaPerMatricola(K_Studente, K_Esame);

        if (dt.Rows.Count > 0)
        {
            rptVideolezioni.DataSource = dt;
            rptVideolezioni.DataBind();
            lblMessaggio1.Visible = false;
        }
        else
        {
            rptVideolezioni.DataSource = null;
            rptVideolezioni.DataBind();
            lblMessaggio1.Visible = true;
            
        }
    }


    private void CaricaDispense()
    {
        if (Session["mat"] == null)
        {
            Response.Redirect("Login.aspx");
            return;
        }

        MATERIALI materiali = new MATERIALI();
        Guid K_Studente = new Guid((string)Session["cod"]);
        DataTable dt = materiali.DispensaPerMatricola(K_Studente, K_Esame);

        if (dt.Rows.Count > 0)
        {
            rptDispense.DataSource = dt;
            rptDispense.DataBind();
            lblMessaggio2.Visible = false; // Nascondi la label se ci sono dispense
        }
        else
        {      

            rptDispense.DataSource = null; // Svuota il Repeater
            rptDispense.DataBind();
            lblMessaggio2.Visible = true;  // Mostra il messaggio

        }
    }



    //protected void btnScarica_Command(object sender, CommandEventArgs e)
    //{

    //    // Recupera K_Esame dal ViewState
    //    if (ViewState["K_Esame"] != null)
    //        K_Esame = new Guid((string)ViewState["K_Esame"]);
    //    else
    //    {
    //        lblMessaggio2.Visible = true;
    //        lblMessaggio2.Text = "Errore: esame non selezionato.";
    //        return;
    //    }

    //    string SalvaK_Materiale = e.CommandArgument.ToString();
    //    MATERIALI m = new MATERIALI();
    //    Guid K_Studente = new Guid((string)Session["cod"]);
    //    DataTable dt = m.DispensaPerMatricola(K_Studente, K_Esame);

    //    if (dt != null && dt.Rows.Count > 0)
    //    {
    //        byte[] fileData = (byte[])dt.Rows[0]["Materiale"];
    //        string fileName = dt.Rows[0]["Titolo"].ToString();

    //        if (fileData != null)
    //        {
    //            Response.Clear();
    //            Response.ContentType = "application/pdf";
    //            Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName);
    //            Response.BinaryWrite(fileData);
    //            Response.End();
    //        }
    //    }
    //    else
    //    {
    //        lblMessaggio2.Visible = true;
    //        lblMessaggio2.Text = "Il materiale richiesto non è disponibile.";
    //    }
    //}

    protected void btnScarica_Command(object sender, CommandEventArgs e)
    {
        if (ViewState["K_Esame"] != null)
            K_Esame = new Guid((string)ViewState["K_Esame"]);
        else
        {
            lblMessaggio2.Visible = true;
            lblMessaggio2.Text = "Errore: esame non selezionato.";
            return;
        }

        string SalvaK_Materiale = e.CommandArgument.ToString();
        MATERIALI m = new MATERIALI();
        Guid K_Studente = new Guid((string)Session["cod"]);
        DataTable dt = m.DispensaPerMatricola(K_Studente, K_Esame);

        if (dt != null && dt.Rows.Count > 0)
        {
            byte[] fileData = (byte[])dt.Rows[0]["Materiale"];
            string fileNameWithoutExt = dt.Rows[0]["Titolo"].ToString();
            string mimeType = dt.Rows[0]["Tipo"].ToString();

            string fileExtension = MimeTypeToExtension(mimeType); // funzione definita sotto
            string fullFileName = fileNameWithoutExt + fileExtension;

            if (fileData != null)
            {
                Response.Clear();
                Response.ContentType = mimeType;
                Response.AddHeader("Content-Disposition", "attachment; filename=" + fullFileName);
                Response.BinaryWrite(fileData);
                Response.End();
            }
        }
        else
        {
            lblMessaggio2.Visible = true;
            lblMessaggio2.Text = "Il materiale richiesto non è disponibile.";
        }
    }

    private string MimeTypeToExtension(string mimeType)
    {
        switch (mimeType.ToLower())
        {
            case "application/pdf": return ".pdf";
            case "text/plain": return ".txt";
            case "image/png": return ".png";
            case "image/jpeg": return ".jpg";
            case "application/msword": return ".doc";
            case "application/vnd.openxmlformats-officedocument.wordprocessingml.document": return ".docx";
            case "application/rtf": return ".rtf";
            default: return ".bin"; // estensione di fallback
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







