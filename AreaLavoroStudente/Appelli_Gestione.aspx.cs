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
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string matricola = "123556"; // prendi dal login/sessione
            CaricaAppelli(matricola);

        }
        
    }

    private void CaricaAppelli(string matricola)
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
            string matricola = "353794A3-1CF8-45F6-87F5-4F35A447F425"; // recupera da sessione se disponibile
            CaricaAppelli(matricola);

            lblMessaggio.Text = "Prenotazione eliminata con successo.";
            lblMessaggio.CssClass = "alert alert-success mt-3";
            lblMessaggio.Visible = true;
        }

    }
}
