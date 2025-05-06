using System;
using System.Data;
using System.Web.UI;
using LibreriaClassi;  // Assicurati di avere la libreria corretta per interagire con i dati.

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //string Matricola = Request.QueryString["mat"];
            string Matricola = Session["mat"].ToString();
            Session["mat"] = Matricola;
            CaricaAA(int.Parse(Matricola));
            CaricaEsami(int.Parse(Matricola));
            CaricaMedia(int.Parse(Matricola));
            CaricaCFU(int.Parse(Matricola));
        }

    }

    private void CaricaEsami(int Matricola)
    {
        LIBRETTI lb = new LIBRETTI();
        DataTable dt = lb.SelezionaEsami(Matricola);

        // Controllo se la DataTable è vuota
        if (dt.Rows.Count > 0)
        {
            rptVoti.DataSource = dt;
        }
        else
        {
            ClientScript.RegisterStartupScript(this.GetType(), "showDatiModal", "showDatiModal();", true);
            
        }

        rptVoti.DataBind();
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
            lblFacolta.Text = "Facoltà " + facolta;
            lblCorso.Text = "Corso " + corso;
        }
    }

    private void CaricaMedia(int Matricola)
    {
        LIBRETTI lb = new LIBRETTI();
        DataTable dt = lb.SelezionaMedia(Matricola);

        if (dt.Rows.Count >= 1)
        {
            formMedia.DataSource = dt;
            formMedia.DataBind();
            lblMediaVuota.Visible = false;
        }
        else
        {
            formMedia.DataSource = null;
            formMedia.DataBind();
            lblMediaVuota.Visible = true;
        }
    }

    private void CaricaCFU(int Matricola)
    {
        LIBRETTI lb = new LIBRETTI();
        DataTable dt = lb.SelezionaTOTCFU(Matricola);

        if (dt.Rows.Count >= 1)
        {
            formCFU.DataSource = dt;
            formCFU.DataBind();
            lblCFUVuota.Visible = false;
        }
        else
        {
            formCFU.DataSource = null;
            formCFU.DataBind();
            lblCFUVuota.Visible = true;
        }
    }

}
