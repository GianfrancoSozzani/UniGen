using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
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
            CaricaFacolta();
        }
    }
    protected void CaricaFacolta()
    {
        FACOLTA f = new FACOLTA();
        DataTable dt = f.SelezionaTutto();
        PopolaListaConPaginazione(dt, rpFacolta);
    }

    protected void btnSalvaInserimento_Click(object sender, EventArgs e)
    {
        string titoloFacolta = txtTitoloFacoltaIns.Text.Trim();

        // Controllo che il campo non sia vuoto
        if (String.IsNullOrEmpty(titoloFacolta))
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Il campo non può essere vuoto')", true);
            return;
        }

        // Controllo che non permette l'uso di numeri o caratteri speciali
        if (!System.Text.RegularExpressions.Regex.IsMatch(titoloFacolta, @"^[a-zA-Z\s]+$"))
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Inserire solo lettere')", true);
            return;
        }

        FACOLTA f = new FACOLTA();

        // Rendo la prima lettera scritta sempre maiuscola
        f.TitoloFacolta = char.ToUpper(titoloFacolta[0]) + titoloFacolta.Substring(1);

        DataTable dt = f.VerificaDuplicato();

        // Controllo duplicato
        if (dt.Rows.Count == 1)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Facoltà già presente')", true);
            return;
        }
        
        f.Inserimento();

        CaricaFacolta();

        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Inserimento avvenuto con successo')", true);
        return;
    }

    protected void btnSalvaModifica_Click(object sender, EventArgs e)
    {
        Guid id = Guid.Parse(hiddenIdFacolta.Value);
        string nuovoTitolo = txtTitoloFacolta.Text.Trim();

        // Controllo che il campo non sia vuoto
        if (string.IsNullOrEmpty(nuovoTitolo))
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Il titolo non può essere vuoto')", true);
            return;
        }

        // Controllo che non permette l'uso di numeri o caratteri speciali
        if (!System.Text.RegularExpressions.Regex.IsMatch(nuovoTitolo, @"^[a-zA-Z\s]+$"))
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Inserire solo lettere')", true);
            return;
        }

        FACOLTA f = new FACOLTA();
        f.K_Facolta = id;
        f.TitoloFacolta = char.ToUpper(nuovoTitolo[0]) + nuovoTitolo.Substring(1);

        DataTable dt = f.VerificaDuplicato();

        // Controllo duplicato
        if (dt.Rows.Count == 1)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Facoltà già presente')", true);
            return;
        }

        f.Modifica();

        CaricaFacolta();
    }
        // Proprietà che gestisce l'indice corrente di pagina (ViewState)
private int PaginaCorrente
    {
        get { return ViewState["PaginaCorrente"] != null ? (int)ViewState["PaginaCorrente"] : 0; }
        set { ViewState["PaginaCorrente"] = value; }
    }

    // Metodo helper per usarlo nel markup ASPX
    public int GetPaginaCorrente()
    {
        return PaginaCorrente;
    }

    // Metodo per caricare la lista e gestire la paginazione
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
            pagine.Add(i + 1); // 1-based
        }

        rptPaginazione.DataSource = pagine;
        rptPaginazione.DataBind();
    }

    // Evento richiamato quando clicchi su una pagina
    protected void rptPaginazione_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "CambiaPagina")
        {
            PaginaCorrente = Convert.ToInt32(e.CommandArgument) - 1; // 0-based
                                                                     // Ricarica i dati
            CaricaFacolta();
        }
    }

   

}


