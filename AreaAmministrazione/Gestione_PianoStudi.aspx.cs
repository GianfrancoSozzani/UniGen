using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using LibreriaClassi;

public partial class _Default : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        //carica le ddl con le facoltà e i corsi disponibili
        if (!IsPostBack)
        {
            FACOLTA f = new FACOLTA();
            ddlFacolta.DataSource = f.SelezionaTutto();
            ddlFacolta.DataTextField = "TitoloFacolta";
            ddlFacolta.DataValueField = "K_Facolta";
            ddlFacolta.DataBind();
            ddlFacolta.Items.Insert(0, new ListItem("Seleziona una facoltà...", ""));
            ddlCorso.Items.Insert(0, new ListItem("Seleziona prima una facoltà...", ""));

        }
        //metodo per mostrare il Popup con gli esami disponibili da aggiungere (TUTTI)


    }



    protected void MostraTuttiEsami()
    {

        ESAMI es = new ESAMI();
        DataTable dt = es.SelezionaEsami();

        rpEsami.DataSource = dt;
        rpEsami.DataBind();

        // Mostra il modale con uno script JS
        ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "$('#modalEsamiDisponibili').modal('show');", true);
    }
    //carica solo i corsi associati alla facoltà selezionata
    protected void ddlFacolta_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlCorso.Items.Clear(); // Pulisce la lista dei corsi
        if (!string.IsNullOrEmpty(ddlFacolta.SelectedValue))
        {
            DB db = new DB();
            db.query = "Corsi_SelectbyFacolta";
            db.cmd.Parameters.AddWithValue("@chiave", Guid.Parse(ddlFacolta.SelectedValue));
            ddlCorso.DataSource = db.SQLselect();
            ddlCorso.DataTextField = "TitoloCorso";
            ddlCorso.DataValueField = "TitoloCorso";
            ddlCorso.DataBind();
        }
        if(ddlFacolta.SelectedIndex == 0)
        {
            ddlCorso.Items.Insert(0, new ListItem("Seleziona prima una facoltà...", ""));
            return;
        }

        ddlCorso.Items.Insert(0, new ListItem("Seleziona un corso di laurea...", ""));

    }
    protected void btnCerca_Click(object sender, EventArgs e)
    {
        if (ddlFacolta.SelectedValue == "")
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Selezionare una Facoltà');", true);
            return;
        }
        if (ddlCorso.SelectedValue == "")
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Selezionare un Corso di Laurea');", true);
            return;

        }
        else
        {
            CaricaEsamiPianiStudio();
        }
    }
    //Visualizza esami inseriti nel piano di studi
    protected void CaricaEsamiPianiStudio()
    {

        {
            // Verifica che l'utente abbia selezionato tutti i filtri
            if (!string.IsNullOrEmpty(ddlFacolta.SelectedValue) &&
                !string.IsNullOrEmpty(ddlCorso.SelectedValue) &&
                !string.IsNullOrEmpty(ddlTipoCorso.SelectedValue) &&
                !string.IsNullOrEmpty(ddlAnnoAccademico.SelectedValue))
            {

                Guid kFacolta = Guid.Parse(ddlFacolta.SelectedValue);
                DB db = new DB();
                db.query = "Corsi_SelectbyTitoloTipo";
                db.cmd.Parameters.AddWithValue("@TitoloCorso", ddlCorso.SelectedValue);
                db.cmd.Parameters.AddWithValue("@K_TipoCorso", Guid.Parse(ddlTipoCorso.SelectedValue));               
                Guid kCorso = Guid.Parse(db.SQLselect().Rows[0]["K_Corso"].ToString());
                string tipoCorso = ddlTipoCorso.SelectedValue;
                string annoAccademico = ddlAnnoAccademico.SelectedValue;

                // Istanzia l'oggetto e richiama la stored procedure tramite metodo
                PIANISTUDIO ps = new PIANISTUDIO();
                DataTable esami = ps.ElencoEsamiInseriti(kFacolta, kCorso, tipoCorso, annoAccademico);

                if (esami != null && esami.Rows.Count > 0)
                {
                    rpEsamiInseriti.DataSource = esami;
                    rpEsamiInseriti.DataBind();
                }
                else
                {
                    rpEsamiInseriti.DataSource = null;
                    rpEsamiInseriti.DataBind();
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Nessun esame trovato per il piano di studi selezionato');", true);

                }
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Seleziona tutti i filtri prima di caricare gli esami');", true);
            }
        }



    }
    //apri il modale con gli esami disponibili
    protected void btnApriModalEsami_Click(object sender, EventArgs e)
    {
        bool validazioneOk = true;

        if (string.IsNullOrEmpty(ddlFacolta.SelectedValue) ||
            string.IsNullOrEmpty(ddlCorso.SelectedValue) ||
            string.IsNullOrEmpty(ddlTipoCorso.SelectedValue) ||
            string.IsNullOrEmpty(ddlAnnoAccademico.SelectedValue))
        {
            validazioneOk = false;
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Seleziona tutti i filtri prima di caricare gli esami');", true);
        }

        if (validazioneOk)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "setTimeout(function() { $('#modalEsamiDisponibili').modal('show'); }, 100);", true);
           
            MostraTuttiEsami();
        }

    }
    //Cerca gli esami in base al titolo inserito dall'utente
    protected void btnCercaEsame_Click(object sender, EventArgs e)
    {
        string termine = txtSearchEsame.Text.Trim();
        if (string.IsNullOrEmpty(termine))
        {
            MostraTuttiEsami();
            return;
        }
        PIANISTUDIO ps = new PIANISTUDIO();
        DataTable dt = ps.CercaEsame(termine);
        //se l'utente ha inserito un input e la ricerca ha trovato una corrispondenza
        if (dt != null && dt.Rows.Count > 0)
        {

            rpEsami.DataSource = dt;
            rpEsami.DataBind();
            //riapre il modale dopo il postback, altrimenti scompare
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "setTimeout(function() { $('#modalEsamiDisponibili').modal('show'); }, 100);", true);
          
        }
        //se non ci sono corrispondenze
        else
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Esame non trovato');", true);
            txtSearchEsame.Text = "";
        }

    }
    protected void btnChiudiModal_Click(object sender, EventArgs e)
    {
        MostraTuttiEsami();
    }
    protected void btnChiudiModal2_Click(object sender, EventArgs e)
    {
        MostraTuttiEsami();
    }

    //salva nel db il piano di studi con i nuovi esami
    protected void rpEsami_ItemCommand(object source, RepeaterCommandEventArgs e)
    {

        if (e.CommandName == "AggiungiEsame")
        {
            // Mappa la stringa del tipo corso al suo Guid
            //Guid tipoCorsoGuid;

            //switch (ddlTipoCorso.SelectedValue)
            //{
            //    case "Triennale":
            //        tipoCorsoGuid = Guid.Parse("22b89603-2e76-416d-ba56-40ade89fa44e");
            //        break;
            //    case "Magistrale":
            //        tipoCorsoGuid = Guid.Parse("4ca2f725-30fe-4ef1-8c10-1713f4d1acd0");
            //        break;
            //    case "Ciclo Unico":
            //        tipoCorsoGuid = Guid.Parse("fc3cbdf5-fe6a-494c-ad32-88ea5f5e5da2");
            //        break;
            //    default:
            //        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Tipo di corso non valido');", true);
            //        return;
            //}
            // Recupera l'esame dalla riga cliccata
            string kEsame = e.CommandArgument.ToString();

            // Prepara l’oggetto completo per la verifica
            PIANISTUDIO ps = new PIANISTUDIO();
            ps.K_Esame = Guid.Parse(kEsame);
            ps.K_Facolta = Guid.Parse(ddlFacolta.SelectedValue);
            DB db = new DB();
            db.query = "Corsi_SelectbyTitoloTipo";
            db.cmd.Parameters.AddWithValue("@TitoloCorso",  ddlCorso.SelectedValue);
            db.cmd.Parameters.AddWithValue("@K_TipoCorso", Guid.Parse(ddlTipoCorso.SelectedValue));
            ps.K_Corso = Guid.Parse(db.SQLselect().Rows[0]["K_Corso"].ToString());
            ps.K_TipoCorso = Guid.Parse(ddlTipoCorso.SelectedValue);
            ps.AnnoAccademico = ddlAnnoAccademico.SelectedValue;

            // Verifica esistenza
            DataTable dt = ps.ControlloEsami();

            if (dt != null && dt.Rows.Count > 0 && Convert.ToInt32(dt.Rows[0]["CONTROLLOPIANI"]) > 0)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Esame già presente nel Piano di Studi');", true);
            }
            else
            {
                CheckBox chkFacoltativo = (CheckBox)e.Item.FindControl("chkFacoltativo");
                ps.Obbligatorio = chkFacoltativo != null && chkFacoltativo.Checked ? 'N' : 'S';
                ps.AggiungiEsami();
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Esame aggiunto correttamente al Piano di Studi');", true);
                CaricaEsamiPianiStudio();
                MostraTuttiEsami();
            }
        }

    }

    protected void btnRimuoviEsame_OnClick(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)sender;
        Guid kPianoStudio = Guid.Parse(btn.CommandArgument);

        PIANISTUDIO ps = new PIANISTUDIO();
        ps.K_PianoStudio = kPianoStudio;
        ps.CancellaEsameDalPiano();
        //ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Esame rimosso correttamente dal Piano di Studi');", true);
        CaricaEsamiPianiStudio();
    }

    protected void ddlCorso_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlTipoCorso.Items.Clear(); // Pulisce la lista dei corsi
        if (!string.IsNullOrEmpty(ddlCorso.SelectedValue))
        {
            DB db = new DB();
            db.query = "Corsi_SelectTipoByCorso";
            db.cmd.Parameters.AddWithValue("@TitoloCorso", ddlCorso.SelectedValue);
            ddlTipoCorso.DataSource = db.SQLselect();
            ddlTipoCorso.DataTextField = "Tipo";
            ddlTipoCorso.DataValueField = "K_TipoCorso";
            ddlTipoCorso.DataBind();
        }
        if (ddlCorso.SelectedIndex == 0)
        {
            ddlTipoCorso.Items.Insert(0, new ListItem("Seleziona prima un Corso di Laurea...", ""));
            return;
        }
        ddlTipoCorso.Items.Insert(0, new ListItem("Seleziona un tipo di corso...", ""));

    }
}
