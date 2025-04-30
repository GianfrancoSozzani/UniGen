using System;
using System.Activities.Expressions;
using System.Activities.Statements;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.DynamicData;
using System.Web.UI;
using System.Web.UI.WebControls;
using LibreriaClassi;

public partial class _Default : System.Web.UI.Page
{
    public int matricola;

    protected void Page_Load(object sender, EventArgs e)
    {
        //rptAppelli.ItemCommand += rptAppelli_ItemCommand;
        // Carica gli appelli solo al primo caricamento della pagina(non su PostBack, ad esempio dopo un click)
        if (!IsPostBack)
        {
            //if (Session["matricola"] == null || !int.TryParse(Session["matricola"].ToString(), out matricola) || matricola == 0)
            //{
            //    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Utente non loggato');", true);
            //    Response.Redirect("~/Login.aspx");
            //  
            //}

            matricola = 123556;//verrà sostituita con la session 
            CaricaAA(matricola);
            CaricaAppelli();

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

    protected void CaricaAppelli()
    {
        matricola = 123556; //da sostituire con la session
        APPELLI m = new APPELLI();
        rptAppelli.DataSource = m.ListaAppelli( matricola);
        rptAppelli.DataBind();
       
    }


    protected void btnPrenotaSelezionati_Click(object sender, EventArgs e)
    {
        // Recupera gli identificativi fissi (da sostituire poi con quelli della sessione utente)
        int matricola = int.Parse("123556"); 
        //Guid K_Libretto = Guid.Parse("C12E1195-736F-4AE5-AD43-13BFE0532190");
        string Esito = "Prenotato"; // Stato da inserire nel database

        int countPrenotati = 0; // Contatore degli appelli prenotati con successo

        // Scorre ogni elemento del Repeater
        foreach (RepeaterItem item in rptAppelli.Items)
        {
            // Trova il CheckBox e l'HiddenField all'interno dell'item
            CheckBox chkSeleziona = (CheckBox)item.FindControl("chkSeleziona");
            HiddenField hfKAppello = (HiddenField)item.FindControl("hfKAppello");

            // Se il CheckBox è selezionato e l'HiddenField è presente
            if (chkSeleziona != null && hfKAppello != null && chkSeleziona.Checked)
            {
                // Recupera il GUID dell'appello selezionato
                Guid K_Appello = Guid.Parse(hfKAppello.Value);

                //Controllo prenotazioni duplicati 

                LIBRETTI m = new LIBRETTI();
                m.Matricola = matricola;
                m.K_Appello = K_Appello;
                m.Esito= Esito;

                if (m.ControlloDoppioni().Rows.Count == 1)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Prenotazione già presente')", true);
                    return;
                }

                // Inserire prenotazione

                try
                {
                    m.PrenotazioneAppelli();  // Esegue la prenotazione
                    countPrenotati++; // Incrementa il contatore dei successi
                }
                catch (Exception ex)
                {
                    // In caso di errore, mostra messaggio di errore e interrompe il processo
                    lblMessaggio.Text = "Errore durante la prenotazione di un appello: " + ex.Message;
                    lblMessaggio.CssClass = "alert alert-danger mt-3";
                    lblMessaggio.Visible = true;
                    return; // Esce dal metodo alla prima eccezione
                }
            }
        }

        // Dopo aver terminato il ciclo, verifica se sono stati prenotati appelli
        if (countPrenotati > 0)
        {
            // Mostra messaggio di successo
            lblMessaggio.Text = string.Format("{0} appello/i prenotato/i con successo!", countPrenotati);
            lblMessaggio.CssClass = "alert alert-success mt-3";
            lblMessaggio.Visible = true;
        }
        else
        {
            // Nessun appello selezionato: mostra messaggio di avviso
            lblMessaggio.Text = "Nessun appello selezionato.";
            lblMessaggio.CssClass = "alert alert-warning mt-3";
            lblMessaggio.Visible = true;
        }
    }
}


