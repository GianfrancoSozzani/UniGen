using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LibreriaClassi;

public partial class _Default : System.Web.UI.Page
{
    Guid K_Studente;
    string Anno;
    DateTime DataPagamento;
    char Stato;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string Matricola = Session["mat"].ToString();
            Session["mat"] = Matricola;
            CaricaAA(int.Parse(Matricola));
            CaricaPagamenti(int.Parse(Matricola));

        }
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
            lblCorso.Text = corso;
            lblFacolta.Text = facolta;
        }
    }

    protected void CaricaPagamenti(int Matricola)
    {
        
        PAGAMENTI m = new PAGAMENTI();
        rptPagamenti.DataSource = m.ListaPagamenti(Matricola);
        rptPagamenti.DataBind();
    }



    protected void btnPaga_Click(object sender, EventArgs e)
    {
       

        int countPagamenti = 0; // Contatore dei pagamenti prenotati con successo

        // Scorre ogni elemento del Repeater
        foreach (RepeaterItem item in rptPagamenti.Items)
        {
            
            CheckBox chkSeleziona = (CheckBox)item.FindControl("chkSeleziona");
            HiddenField hfKPagamento = (HiddenField)item.FindControl("hfKPagamento");

            if (chkSeleziona != null && hfKPagamento != null && chkSeleziona.Checked)
            {
                Guid K_Pagamento = Guid.Parse(hfKPagamento.Value);
                PAGAMENTI p = new PAGAMENTI();
                p.K_Pagamento = K_Pagamento;
                p.ModificaStatoPagamento();
                countPagamenti++;
            }
        }


        // Dopo aver terminato il ciclo, verifica se sono stati selezionati/effettuati pagamenti 7
        if (countPagamenti > 0)
        {
            // Mostra messaggio di successo
            lblMessaggio.Text = string.Format("{0} Pagamento/i eseguito/i con successo!", countPagamenti);
            lblMessaggio.CssClass = "alert alert-success mt-3";
            lblMessaggio.Visible = true;
        }
        else
        {
            // Nessun appello selezionato: mostra messaggio di avviso
            lblMessaggio.Text = "Nessun pagamento selezionato.";
            lblMessaggio.CssClass = "alert alert-warning mt-3";
            lblMessaggio.Visible = true;
        }

        //dopo aver fatto l'update che segnala il pagamento si deve rinserire il pagamento nuovo che deve essere fatto tra 6 mesi 
        string Matricola = Session["mat"].ToString();
        PAGAMENTI d = new PAGAMENTI();
        d.K_Studente = K_Studente;
        d.Anno = Anno;
        d.DataPagamento = DataPagamento;
        d.Stato = Stato;
        d.Inserimento(int.Parse(Matricola));

        //ricarica la pagina con i dati aggiornati 
        CaricaPagamenti(int.Parse(Matricola));
    }
   
}