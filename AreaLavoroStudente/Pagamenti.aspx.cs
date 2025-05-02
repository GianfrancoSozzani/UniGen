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

            matricola = 123551;//verrà sostituita con la session 
            CaricaAA(matricola);
            CaricaPagamenti();

        }
    }

    public void CaricaAA(int matricola)
    {
        matricola = 123551;
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

    protected void CaricaPagamenti()
    {
        matricola = 123551; //da sostituire con la session
        PAGAMENTI m = new PAGAMENTI();
        rptPagamenti.DataSource = m.ListaPagamenti(matricola);
        rptPagamenti.DataBind();
    }



    protected void btnPaga_Click(object sender, EventArgs e)
    {
        // Recupera gli identificativi fissi (da sostituire poi con quelli della sessione utente)
        int matricola = int.Parse("123551");

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


        // Dopo aver terminato il ciclo, verifica se sono stati prenotati appelli
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

        CaricaPagamenti();
    }
    
}