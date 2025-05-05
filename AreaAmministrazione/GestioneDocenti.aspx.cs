using System;
using System.Collections.Generic;
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

        rpDocenti.DataSource = d.SelezionaTutto();
        rpDocenti.DataBind();
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
    }

    protected void DisabilitaDocente(Guid K_Docente)
    {
        DOCENTI d = new DOCENTI();
        d.K_Docente = K_Docente;
        d.Disabilita();
        rpDocenti.DataBind();
    }

    protected void btnNuovaPagina_Click(object sender, EventArgs e)
    {
        Response.Redirect("InserimentoDocente.aspx?usr=" + Request.QueryString["usr"]);
    }

}
