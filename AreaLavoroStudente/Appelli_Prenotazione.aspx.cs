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
    


    protected void Page_Load(object sender, EventArgs e)
    {
        //rptAppelli.ItemCommand += rptAppelli_ItemCommand;
        // Carica gli appelli solo al primo caricamento della pagina(non su PostBack, ad esempio dopo un click)
        if (!IsPostBack)
        {
            //string Matricola = Request.QueryString["mat"];
            string Matricola = Session["mat"].ToString();
            Session["mat"] = Matricola;
            CaricaAA(int.Parse(Matricola));
            CaricaAppelli();

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
            lblFacolta.Text = "Facoltà " + facolta;
            lblCorso.Text = "Corso " + corso;
        }
    }

    protected void CaricaAppelli()
    {
        
        APPELLI m = new APPELLI();
        rptAppelli.DataSource = m.ListaAppelli((Guid)Session["cod"]);
        rptAppelli.DataBind();

    }
    


    protected void btnPrenotaSelezionati_Click(object sender, EventArgs e)
    {
        // Recupera gli identificativi fissi (da sostituire poi con quelli della sessione utente)
        

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

                LIBRETTI m = new LIBRETTI();
                int Matricola = int.Parse(Session["mat"].ToString());
                m.Matricola = Matricola;
                m.K_Appello = K_Appello;
                m.RecuperaKStudenteDaMatricola(Matricola);

                if (m.RecuperaKStudenteDaMatricola(Matricola).Rows.Count > 0 && m.RecuperaKStudenteDaMatricola(Matricola).Columns.Count > 0)
                {
                    Guid primoGuid = (Guid)m.RecuperaKStudenteDaMatricola(Matricola).Rows[0][0];
                    m.K_Studente = primoGuid;
                }


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
            string Matricola = Session["mat"].ToString();
            //RICARICA GLI APPELLI DISPONIBILI
            CaricaAppelli(int.Parse(Matricola));
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



