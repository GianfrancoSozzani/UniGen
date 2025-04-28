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
        btnCerca_Click(sender, e);
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
        rpDocenti.DataBind();
    }

    protected void Selected_Command(object sender, CommandEventArgs e)
    {
        string action = e.CommandName; // "Abilita" o "Disabilita"
        string K_docente = e.CommandArgument.ToString(); // GUID del docente

        if (action == "Abilita")
        {
            // Richiama il metodo per impostare Abilitato = 'Y' in Docenti in base alla K_docente
            AbilitaDocente(K_docente);
            return;
        }
        else if (action == "Disabilita")
        {
            //  Richiama il metodo per impostare Abilitato = 'N' in Docenti in base alla K_docente
            DisabilitaDocente(K_docente);
            return;
        }
    }

    protected void AbilitaDocente(string K_Docente)
    {
        DOCENTI d = new DOCENTI();
        d.Abilita();
    }

    protected void DisabilitaDocente(string K_Docente)
    {
       DOCENTI d = new DOCENTI();
        d.Disabilita();
    }
   
}