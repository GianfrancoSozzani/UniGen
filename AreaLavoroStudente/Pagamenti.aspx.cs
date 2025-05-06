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
            lblFacolta.Text = "Facoltà " + facolta;
            lblCorso.Text = "Corso " + corso;
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


        //    int countPagamenti = 0; // Contatore dei pagamenti prenotati con successo

        //    // Scorre ogni elemento del Repeater
        //    foreach (RepeaterItem item in rptPagamenti.Items)
        //    {

        //        CheckBox chkSeleziona = (CheckBox)item.FindControl("chkSeleziona");
        //        HiddenField hfKPagamento = (HiddenField)item.FindControl("hfKPagamento");

        //        if (chkSeleziona != null && hfKPagamento != null && chkSeleziona.Checked)
        //        {
        //            Guid K_Pagamento = Guid.Parse(hfKPagamento.Value);
        //            PAGAMENTI p = new PAGAMENTI();
        //            p.K_Pagamento = K_Pagamento;
        //            p.ModificaStatoPagamento();
        //            countPagamenti++;
        //            //inserimento pagamento nuovo 
        //            string matricola = Session["mat"].ToString();
        //            DataTable dt = p.PagamentiDatiMancanti(int.Parse(matricola));

        //            string Anno;
        //            DateTime DataPagamento;
        //            Decimal Importo;
        //            Guid k_studente;

        //            DataRow row = dt.Rows[0];
        //            Anno = row["Anno"].ToString();
        //            DataPagamento = DateTime.Parse(row["DataPagamento"].ToString());
        //            Importo = Decimal.Parse(row["Importo"].ToString());
        //            k_studente = Guid.Parse(row["K_Studente"].ToString());

        //            DataTable dt2 = p.Pagamenti_VerificaPagamenti(int.Parse(matricola));
        //            DataRow row2 = dt2.Rows[0];
        //            if ((int)row2[0] >= 2)
        //            {
        //                string anno = Anno;

        //                int annocorretto = int.Parse(anno.Substring(0, 4));


        //                annocorretto++;

        //                string nuovoanno = annocorretto.ToString();
        //                p.Anno = nuovoanno;
        //            }
        //            else
        //            {
        //                p.Anno = Anno;
        //            }

        //            p.DataPagamento = DataPagamento;
        //            p.Importo = (Importo / 2);
        //            p.K_Studente = k_studente;
        //            p.Stato = 'N';
        //            p.K_Pagamento = Guid.NewGuid();
        //            p.Inserimento();
        //        }
        //    }


        //    // Dopo aver terminato il ciclo, verifica se sono stati selezionati/effettuati pagamenti 7
        //    if (countPagamenti > 0)
        //    {
        //        // Mostra messaggio di successo
        //        lblMessaggio.Text = string.Format("{0} Pagamento/i eseguito/i con successo!", countPagamenti);
        //        lblMessaggio.CssClass = "alert alert-success mt-3";
        //        lblMessaggio.Visible = true;

        //        //CaricaPagamenti(int.Parse(matricola));

        //    }
        //    else
        //    {
        //        // Nessun appello selezionato: mostra messaggio di avviso
        //        lblMessaggio.Text = "Nessun pagamento selezionato.";
        //        lblMessaggio.CssClass = "alert alert-warning mt-3";
        //        lblMessaggio.Visible = true;
        //    }


        //    string Matricola = Session["mat"].ToString();
        //    Response.Redirect("Pagamenti.aspx");
        //}

        int countSelezionati = 0;
        RepeaterItem itemSelezionato = null;

        // Conta quante checkbox sono selezionate e salva l'item selezionato
        foreach (RepeaterItem item in rptPagamenti.Items)
        {
            CheckBox chkSeleziona = (CheckBox)item.FindControl("chkSeleziona");

            if (chkSeleziona != null && chkSeleziona.Checked)
            {
                countSelezionati++;
                itemSelezionato = item;
            }
        }

        // Se nessun pagamento selezionato
        if (countSelezionati == 0)
        {
            lblMessaggio.Text = "Nessun pagamento selezionato.";
            lblMessaggio.CssClass = "alert alert-warning mt-3";
            lblMessaggio.Visible = true;
            return;
        }

        // Se più di uno selezionato
        if (countSelezionati > 1)
        {
            lblMessaggio.Text = "Puoi selezionare solo un pagamento alla volta.";
            lblMessaggio.CssClass = "alert alert-danger mt-3";
            lblMessaggio.Visible = true;
            return;
        }

        // Elaborazione del pagamento selezionato
        HiddenField hfKPagamento = (HiddenField)itemSelezionato.FindControl("hfKPagamento");
        if (hfKPagamento != null)
        {
            Guid K_Pagamento = Guid.Parse(hfKPagamento.Value);
            PAGAMENTI p = new PAGAMENTI();
            p.K_Pagamento = K_Pagamento;
            DataTable dtAnno = p.SelezionaChiave();
            DataRow rowAnno = dtAnno.Rows[0];
            int annoCorrente = DateTime.Now.Year;

            if (rowAnno["Anno"].ToString().Trim() != annoCorrente.ToString().Trim())
            {
                lblMessaggio.Text = "Non è possibile effettuare il pagamento per il prossimo anno accademico.";
                lblMessaggio.CssClass = "alert alert-danger mt-3";
                lblMessaggio.Visible = true;
                return;
            }
            //if (rowAnno["Anno"].ToString() != DateTime.Now.Year.ToString())
            //{
            //    lblMessaggio.Text = "Non è possibile effettuare il pagamento per il prossimo anno accademico";
            //    return;
            //}
            p.ModificaStatoPagamento();

            if (string.IsNullOrEmpty(p.K_Pagamento.ToString()) )return;

            DataTable dt = p.PagamentiDatiMancanti(p.K_Pagamento);
            if (dt.Rows.Count == 0) return;

            DataRow row = dt.Rows[0];
            string Anno = row["Anno"].ToString();
            DateTime DataPagamento = DateTime.Parse(row["DataPagamento"].ToString());
            Decimal Importo = Decimal.Parse(row["Importo"].ToString());
            Guid k_studente = Guid.Parse(row["K_Studente"].ToString());

            string matricola = Session["mat"].ToString();
            DataTable dt2 = p.Pagamenti_VerificaPagamenti(int.Parse(matricola));
            if (dt2.Rows.Count > 0 && Convert.ToInt32(dt2.Rows[0][0]) >= 2)
            {
                int annocorretto = int.Parse(Anno.Substring(0, 4)) + 1;
                p.Anno = annocorretto.ToString();
            }
            else
            {
                p.Anno = Anno;
            }

            p.DataPagamento = DataPagamento;
            p.Importo = Importo;
            p.K_Studente = k_studente;
            p.Stato = 'N';
            p.K_Pagamento = Guid.NewGuid();
            p.Inserimento();

            // Mostra messaggio di successo
            lblMessaggio.Text = "Pagamento eseguito con successo!";
            lblMessaggio.CssClass = "alert alert-success mt-3";
            lblMessaggio.Visible = true;

            // Puoi eventualmente ricaricare la lista dei pagamenti aggiornati qui
            // CaricaPagamenti(int.Parse(matricola));
            dt.Clear();
            dt2.Clear();
            Response.Redirect("Pagamenti.aspx");

        }
    }
}