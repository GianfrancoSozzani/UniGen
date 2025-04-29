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
            string Matricola = "4491B5BD-CE09-4519-A511-57701E047FCB"; //verrà sostituita con la session 
            CaricaAppelli(Matricola);

        }
    }

    protected void CaricaAppelli(string Matricola)
    {
        DB db = new DB();
        db.query = "Appelli_SelectMat";
        db.cmd.Parameters.AddWithValue("@Matricola", Matricola);
        DataTable dt = db.SQLselect();
        rptAppelli.DataSource = dt;
        rptAppelli.DataBind();
    }



    protected void btnPrenotaSelezionati_Click(object sender, EventArgs e)
    {
        // Recupera gli identificativi fissi (da sostituire poi con quelli della sessione utente)
        Guid K_Studente = Guid.Parse("6B787310-8260-4517-A2F4-B7132DE47C2E");
        Guid K_Libretto = Guid.Parse("C12E1195-736F-4AE5-AD43-13BFE0532190");
        string Esito = "prenotato"; // Stato da inserire nel database

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

                // Crea un nuovo oggetto DB per la prenotazione
                DB db = new DB();
                db.query = "Prenotazione_Insert"; // Nome della stored procedure per la prenotazione
                db.cmd.CommandType = CommandType.StoredProcedure; // Specifica che è una stored procedure
                db.cmd.Parameters.AddWithValue("@k_libretto", K_Libretto); // Parametri da passare
                db.cmd.Parameters.AddWithValue("@k_studente", K_Studente);
                db.cmd.Parameters.AddWithValue("@k_appello", K_Appello);
                db.cmd.Parameters.AddWithValue("@esito", Esito);

                try
                {
                    db.SQLcommand(); // Esegue la prenotazione
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


