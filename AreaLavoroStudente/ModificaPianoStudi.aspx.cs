using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using LibreriaClassi;

public partial class ModificaPianoStudi : System.Web.UI.Page
{
    private Guid K_PianoStudioPersonale;

    protected void Page_Load(object sender, EventArgs e)
    {
        ValidationSettings.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
        if (!IsPostBack)
        {
            //if (Session["Matricola"] == null)
            //{
            //    Response.Redirect("Login.aspx");
            //}

            if (!Guid.TryParse(Request.QueryString["idPiano"], out K_PianoStudioPersonale))
            {
                MostraErrore("ID piano di studio non valido.");
                return;
            }

            CaricaDatiPianoStudio();
        }
    }

    private void CaricaDatiPianoStudio()
    {
        try
        {
            PIANISTUDIOPERSONALI piano = new PIANISTUDIOPERSONALI();
            piano.K_PianoStudioPersonale = K_PianoStudioPersonale;
            DataTable dt = piano.SelezionaChiavePianiStudioPersonale();

            if (dt.Rows.Count == 0)
            {
                MostraErrore("Piano di studio non trovato.");
                return;
            }

            DataRow row = dt.Rows[0];

            
            txtAnnoAccademico.Text = row["AnnoAccademico"].ToString();
            txtKPianoPersonale.Text = row["K_PianoStudioPersonale"].ToString();


            ddlEsame.Items.Clear();
            ddlEsame.Items.Add(new ListItem(row["TitoloEsame"].ToString(), row["K_Esame"].ToString()));
            ddlEsame.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            MostraErrore("Errore durante il caricamento dei dati: " + ex.Message);
        }
    }

    protected void btnSalva_Click(object sender, EventArgs e)
    {
        
        if (!Page.IsValid) return;

        try
        {
            PIANISTUDIOPERSONALI piano = new PIANISTUDIOPERSONALI
            {
                K_PianoStudioPersonale = new Guid(txtKPianoPersonale.Text),
                K_Esame = new Guid(ddlEsame.SelectedValue),
                AnnoAccademico = txtAnnoAccademico.Text,
                K_Studente = new Guid(Session["cod"].ToString())
            };

            piano.Modifica();
            MostraSuccesso("Piano di studio aggiornato con successo!");
            Response.AddHeader("REFRESH", "2;URL=PianoStudiPersonale.aspx");
        }
        catch (Exception ex)
        {
            MostraErrore("Errore durante il caricamento dei dati: " + ex.Message);
        }
    }

    protected void btnElimina_Click(object sender, EventArgs e)
    {
        try
        {
            PIANISTUDIOPERSONALI piano = new PIANISTUDIOPERSONALI();
            

            piano.Elimina();
            MostraSuccesso("Esame rimosso con successo dal piano di studio!");
            Response.AddHeader("REFRESH", "2;URL=PianoStudiPersonale.aspx");
        }
        catch (Exception ex)
        {
            MostraErrore("Errore durante il caricamento dei dati: " + ex.Message);
        }
    }

    protected void btnAnnulla_Click(object sender, EventArgs e)
    {
        Response.Redirect("PianoStudiPersonale.aspx");
    }

    private void MostraSuccesso(string messaggio)
    {
        pnlMessaggio.Visible = true;
        pnlMessaggio.CssClass = "alert alert-success alert-dismissible fade show alert-message";
        litMessaggio.Text = messaggio;
    }

    private void MostraErrore(string messaggio)
    {
        pnlMessaggio.Visible = true;
        pnlMessaggio.CssClass = "alert alert-danger alert-dismissible fade show alert-message";
        litMessaggio.Text = messaggio;
    }
}