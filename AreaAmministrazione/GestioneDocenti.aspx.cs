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
            CaricaDocenti();
        }
    }

    public void CaricaDocenti()
    {
        DOCENTI d = new DOCENTI();
        DataTable dt = d.SelezionaTutto();
        PopolaListaConPaginazione(dt, rpDocenti);

    }

    protected void btnCerca_Click(object sender, EventArgs e)
    {
        DOCENTI d = new DOCENTI();
        d.Cognome = txtCognome.Text.Trim();
        d.Nome = txtNome.Text.Trim();
        rpDocenti.DataSource = d.SelezionaPerCognomeNome();
        ViewState["Cognome"] = d.Cognome;
        ViewState["Nome"] = d.Nome;
        rpDocenti.DataBind();

    }

    protected void Selected_Command(object sender, CommandEventArgs e)
    {
        string command = e.CommandName; // "Abilita" o "Disabilita"

        string salvaK_docente = e.CommandArgument.ToString(); // GUID del docente

        Guid K_docente = Guid.Parse(salvaK_docente);

        if (command == "Abilita")
        {
            // Richiama il metodo per impostare Abilitato = 'Y' in Docenti in base alla K_docente
            AbilitaDocente(K_docente);
        }
        else if (command == "Disabilita")
        {
            //  Richiama il metodo per impostare Abilitato = 'N' in Docenti in base alla K_docente
            DisabilitaDocente(K_docente);
        }
        CaricaDocenti();
    }

    protected void AbilitaDocente(Guid K_Docente)
    {
        DOCENTI d = new DOCENTI();
        d.K_Docente = K_Docente;
        d.Abilita();
        rpDocenti.DataBind();
        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Docente abilitato con successo')", true);
        return;
    }

    protected void DisabilitaDocente(Guid K_Docente)
    {
        DOCENTI d = new DOCENTI();
        d.K_Docente = K_Docente;
        d.Disabilita();
        rpDocenti.DataBind();
        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Docente disabilitato con successo')", true);
        return;
    }

    protected void btnNuovaPagina_Click(object sender, EventArgs e)
    {
        Response.Redirect("InserimentoDocente.aspx?usr=" + Request.QueryString["usr"]);
    }

    //PAGINAZIONE
    private int PaginaCorrente
    {
        get { return ViewState["PaginaCorrente"] != null ? (int)ViewState["PaginaCorrente"] : 0; }
        set { ViewState["PaginaCorrente"] = value; }
    }
    public int GetPaginaCorrente()
    {
        return PaginaCorrente;
    }
    protected void PopolaListaConPaginazione(DataTable dati, Repeater rptDati)
    {
        PagedDataSource paged = new PagedDataSource();
        paged.DataSource = dati.DefaultView;
        paged.AllowPaging = true;
        paged.PageSize = 10;
        paged.CurrentPageIndex = PaginaCorrente;

        rptDati.DataSource = paged;
        rptDati.DataBind();

        // Pagine numeriche
        List<int> pagine = new List<int>();
        for (int i = 0; i < paged.PageCount; i++)
        {
            pagine.Add(i + 1); // Le pagine partono da 1
        }

        rptPaginazione.DataSource = pagine;
        rptPaginazione.DataBind();
    }
    protected void rptPaginazione_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "CambiaPagina")
        {
            PaginaCorrente = Convert.ToInt32(e.CommandArgument) - 1;
            CaricaDocenti();
        }
    }
}
