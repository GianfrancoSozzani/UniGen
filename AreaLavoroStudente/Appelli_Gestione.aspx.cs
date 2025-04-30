using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
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

            matricola = 123556; // prendi dal login/sessione
            CaricaAA(matricola);
            CaricaAppelli(matricola);

        }
        
    }



    public void CaricaAA(int matricola)
    {
        matricola = 123556;
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

    private void CaricaAppelli(int matricola)
    {
        LIBRETTI m = new LIBRETTI();
        rptAppelli.DataSource = m.ListaPrenotazioni();
        rptAppelli.DataBind();
    }

    protected void btnEliminaPrenotazione_Click(object sender, EventArgs e)
    {

        bool eliminato = false;
        List<Guid> daEliminare = new List<Guid>();

        // Primo ciclo: raccoglie gli ID da eliminare
        foreach (RepeaterItem item in rptAppelli.Items)
        {
            CheckBox chkSeleziona = (CheckBox)item.FindControl("chkSeleziona");
            HiddenField hfKLibretto = (HiddenField)item.FindControl("hfKLibretto");

            if (chkSeleziona != null && hfKLibretto != null && chkSeleziona.Checked)
            {
                Guid K_Libretto = Guid.Parse(hfKLibretto.Value);
                daEliminare.Add(K_Libretto);
            }
        }

        //Se non ci sono selezioni, mostra messaggio e interrompe
        if (daEliminare.Count == 0)
        {
            lblMessaggio.Text = "Seleziona almeno un appello da eliminare.";
            lblMessaggio.CssClass = "alert alert-warning mt-3";
            lblMessaggio.Visible = true;
            return;
        }

        // Secondo ciclo: esegue le eliminazioni
        foreach (Guid id in daEliminare)
        {
            LIBRETTI m = new LIBRETTI();
            m.K_Libretto = id;

            try
            {
                m.EliminazioneAppelli();
                eliminato = true;
            }
            catch (Exception ex)
            {
                lblMessaggio.Text = "Errore durante l'eliminazione dell'appello: " + ex.Message;
                lblMessaggio.CssClass = "alert alert-danger mt-3";
                lblMessaggio.Visible = true;
                return;
            }
        }

        if (eliminato)
        {
            matricola = 123556; // recupera da sessione se disponibile
            CaricaAppelli( matricola);

            lblMessaggio.Text = "Prenotazione eliminata con successo.";
            lblMessaggio.CssClass = "alert alert-success mt-3";
            lblMessaggio.Visible = true;
        }

    }
}
