using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using LibreriaClassi;

public partial class _Default : System.Web.UI.Page
{
    Guid k_pay = Guid.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Form["ajaxCall"] == "true" && !string.IsNullOrEmpty(Request.Form["orderId"]))
        {
            ConfirmPayment();

            Response.ContentType = "application/json";
            Response.Write("{\"status\":\"ok\"}");
            Response.End();
            return;
        }
        else if (!IsPostBack)
        {
            string Matricola = Session["mat"].ToString();
            Session["mat"] = Matricola;
            CaricaAA(int.Parse(Matricola));
            CaricaPagamenti(int.Parse(Matricola));
            if (Request.QueryString["res"] == "1")
            {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Pagamento effettuato con successo')", true);
            }
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

    protected void CaricaPagamenti(int Matricola)
    {

        PAGAMENTI m = new PAGAMENTI();
        rptPagamenti.DataSource = m.ListaPagamenti(Matricola);
        rptPagamenti.DataBind();
    }



    protected void btnPaga_Click(object sender, EventArgs e)
    {

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

        HiddenField hfKPagamento = (HiddenField)itemSelezionato.FindControl("hfKPagamento");
        if (hfKPagamento != null)
        {
            Guid K_Pagamento = Guid.Parse(hfKPagamento.Value);
            PAGAMENTI p = new PAGAMENTI();
            k_pay = K_Pagamento;
            Session["k_pay"] = k_pay;
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
            decimal importo = Convert.ToDecimal(rowAnno["Importo"]);
            string importoFormatted = importo.ToString("0.00", CultureInfo.InvariantCulture);

            paypal_container.Style["display"] = "block"; // paypal_container è il div che contiene PayPal

            string script = string.Format("mostraPayPal('{0}');", importoFormatted);
            ClientScript.RegisterStartupScript(this.GetType(), "avviaPaypal", script, true);


        }
    }
    public void ConfirmPayment()
    {
        PAGAMENTI p = new PAGAMENTI();
        p.K_Pagamento = Guid.Parse(Session["k_pay"].ToString());
        p.ModificaStatoPagamento();

        if (string.IsNullOrEmpty(p.K_Pagamento.ToString())) return;

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
        dt.Clear();
        dt2.Clear();
        return;
    }
}