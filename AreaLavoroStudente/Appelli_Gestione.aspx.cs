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
            Guid Cod = Guid.Parse(Session["cod"].ToString());
            string Usr = Session["usr"] as string;
            string Matricola = Session["mat"] as string;
            string Abilitato = Session["ab"] as string;

            Session["mat"] = Matricola;
            CaricaAA(int.Parse(Matricola));
            CaricaAppelli(Cod);

            //salvo nella session 


        }

    }


    //mostra identificativo studente per trovare la matricola
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

    //mostrami le prenotazioni fatte in base alla matricola
    private void CaricaAppelli(Guid Cod)
    {
        LIBRETTI m = new LIBRETTI();
        DataTable dt = m.ListaPrenotazioni(Cod);
        foreach (DataRow dr in dt.Rows)
        {
            int i = 0;
            DB db = new DB();
            db.query = "Valutazioni_SelectByStudente";
            db.cmd.Parameters.AddWithValue("@k_prova", Guid.Parse(dt.Rows[i]["K_Prova"].ToString()));
            db.cmd.Parameters.AddWithValue("@k_stu", Guid.Parse(Session["cod"].ToString()));
            DataTable dt2 = db.SQLselect();
            if (dt2.Rows.Count != 0) {
                dt.Rows[i]["Link"] = "superato";
            }
            i++;
        }
        rptAppelli.DataSource = dt;
        rptAppelli.DataBind();
    }

    //elimina la prenotazione
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
            Guid Cod = Guid.Parse(Session["cod"].ToString());
            CaricaAppelli(Cod);
            lblMessaggio.Text = "Prenotazione eliminata con successo.";
            lblMessaggio.CssClass = "alert alert-success mt-3";
            lblMessaggio.Visible = true;
        }

    }


    protected void btnProva_Command(object sender, CommandEventArgs e)
    {
        string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
        Guid k_Prova = Guid.Parse(commandArgs[0]);
        DateTime data = DateTime.Parse(commandArgs[1]);
        DB db = new DB();
        if (k_Prova == null)
        {
            lblMessaggio.Text = "Prova momentaneamente non disponibile.";
            lblMessaggio.CssClass = "alert alert-danger mt-3";
            lblMessaggio.Visible = true;
            return;
        }
        else if (data > DateTime.Now)
        {
            lblMessaggio.Text = "Attenzione: non è più possibile eseguire la prova.";
            lblMessaggio.CssClass = "alert alert-danger mt-3";
            lblMessaggio.Visible = true;
            return;
        }
        Response.Redirect("Prova.aspx?prova=" + k_Prova);
    }
}

