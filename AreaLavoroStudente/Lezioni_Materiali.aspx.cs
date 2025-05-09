using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LibreriaClassi;

public partial class Default2 : System.Web.UI.Page
{
    Guid K_Esame;
    Guid K_Studente;

    protected void Page_Load(object sender, EventArgs e)
    {
        K_Studente = Guid.Parse(Session["cod"].ToString());
        if (!IsPostBack)
        {
            string Matricola = Session["mat"].ToString();


            CaricaDdl(K_Studente);
            CaricaFacoltaECorso();
            CaricaVideolezioni(K_Studente);
            CaricaDispense(K_Studente);
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

    private void CaricaDdl(Guid K_Studente)
    {
        //Guid K_Studente = new Guid((string)Session["cod"]);
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
        if (Session["mat"] == null)
        {
            Response.Redirect("Login.aspx");
            return;
        }

        int matricola = Convert.ToInt32(Session["mat"]);

        DB db = new DB();
        db.query = "GetFacoltaCorsoByMatricola";
        db.cmd.Parameters.Clear();
        db.cmd.Parameters.AddWithValue("@Matricola", matricola);
        DataTable dt = db.SQLselect();
    }

    private void CaricaVideolezioni(Guid K_Studente)
    {
        if (Session["mat"] == null)
        {
            Response.Redirect("Login.aspx");
            return;
        }

        LEZIONI lezioni = new LEZIONI();
        //Guid K_Studente = new Guid((string)Session["cod"]);
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

    private void CaricaDispense(Guid K_Studente)
    {
        if (Session["mat"] == null)
        {
            Response.Redirect("Login.aspx");
            return;
        }

        MATERIALI materiali = new MATERIALI();
        //Guid K_Studente = new Guid((string)Session["cod"]);
        DataTable dt = materiali.DispensaPerMatricola(K_Studente, K_Esame);

        if (dt.Rows.Count > 0)
        {
            rptDispense.DataSource = dt;
            rptDispense.DataBind();
            lblMessaggio2.Visible = false;
        }
        else
        {
            rptDispense.DataSource = null;
            rptDispense.DataBind();
            lblMessaggio2.Visible = true;
        }
    }

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

        Guid kMateriale = new Guid(e.CommandArgument.ToString());
        MATERIALI m = new MATERIALI();
        Guid K_Studente = new Guid((string)Session["cod"]);
        DataTable dt = m.DispensaPerMatricola(K_Studente, K_Esame);

        DataRow[] rows = dt.Select("K_Materiale = '" + kMateriale + "'");

        if (rows.Length > 0)
        {
            DataRow r = rows[0];
            byte[] fileData = (byte[])r["Materiale"];
            string fileNameWithoutExt = r["Titolo"].ToString();
            string mimeType = r["Tipo"].ToString();

            string fileExtension = MimeTypeToExtension(mimeType);
            string fullFileName = fileNameWithoutExt + fileExtension;

            if (fileData != null)
            {
                Response.Clear();
                Response.ContentType = mimeType;
                string encodedFileName = HttpUtility.UrlPathEncode(fullFileName);
                Response.AddHeader("Content-Disposition", "attachment; filename=\"" + encodedFileName + "\"");
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
            default: return ".bin";
        }
    }

    protected void ddlCaricaEsami_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["K_Esame"] = ddlCaricaEsami.SelectedValue;
        K_Esame = new Guid((string)ViewState["K_Esame"]);
        CaricaVideolezioni(K_Studente);
        CaricaDispense(K_Studente);
    }
}








