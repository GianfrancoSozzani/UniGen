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
        //carica le ddl con le facoltà e i corsi disponibili
        if (!IsPostBack)
        {
            CaricaEsami();
            FACOLTA f = new FACOLTA();
            ddlFacolta.DataSource = f.SelezionaTutto();
            ddlFacolta.DataTextField = "TitoloFacolta";
            ddlFacolta.DataValueField = "K_Facolta";
            ddlFacolta.DataBind();

            CORSI c = new CORSI();
            ddlCorso.DataSource = c.SelezionaTutto();
            ddlCorso.DataTextField = "TitoloCorso";
            ddlCorso.DataValueField = "K_Corso";
            ddlCorso.DataBind();
        }
    }

  
    //Visualizza esami disponibili
    protected void CaricaEsami()
    {
        ESAMI e = new ESAMI();

        rpEsami.DataSource = e.SelezionaTutto();
        rpEsami.DataBind();
    }
    // Seleziona esami da inserire nel piano studi
    protected void btnSeleziona_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        // Recupera l'ID dell'esame dal CommandArgument
        Guid K_Esame = Guid.Parse(btn.CommandArgument);

        // Recupera l'item del repeater (la riga)
        RepeaterItem item = (RepeaterItem)btn.NamingContainer;

        // Recupera il valore del CheckBox
        CheckBox chkObbligatorio = (CheckBox)item.FindControl("chkObbligatorio");
        bool obbligatorio = chkObbligatorio != null && chkObbligatorio.Checked;

        //Inserisci nel database il piano di studi
        PIANISTUDIO ps = new PIANISTUDIO();
        ps.K_Corso = Guid.Parse(ddlCorso.SelectedValue);
        ps.K_Esame = K_Esame;
        ps.AnnoAccademico = ddlAnnoAccademico.DataValueField;
        ps.Obbligatorio = obbligatorio ? 'S' : 'N';
        DataTable dt = new DataTable();
        dt = ps.Controllo();

        int count = Convert.ToInt32(dt.Rows[0]["CONTROLLOPIANI"]);
        if (count == 0)
        {
        ps.Inserimento();


            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Esame aggiunto correttamente al Piano di Studi');", true);

        }
        else
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Esame già presente nel Piano di Studi');", true);
        }
    }
}
