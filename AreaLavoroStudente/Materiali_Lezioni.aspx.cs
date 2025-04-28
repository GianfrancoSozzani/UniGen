using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.EnterpriseServices;
using LibreriaClassi;
using System.ComponentModel;
using System.Activities.Expressions;


public partial class Materiali_Lezioni : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Al primo caricamento della pagina, carica le facoltà
        if (!IsPostBack)
        {
            CaricaFacolta();
            ddlFacolta.Items.Insert(0, new ListItem("-- Seleziona una facoltà --", ""));
            ddlCorsi.Items.Insert(0, new ListItem("-- Seleziona un corso --", ""));
        }
    }

    // Carica le facoltà usando la classe DB e la stored procedure 
    public void CaricaFacolta()
    {
        DB db = new DB();
        db.query = "Facolta_SelectAll";
        DataTable dt = db.SQLselect();

        ddlFacolta.DataSource = dt;
        ddlFacolta.DataTextField = "TitoloFacolta"; // Colonna visibile
        ddlFacolta.DataValueField = "K_Facolta";    // Chiave primaria
        ddlFacolta.DataBind();
    }

    // Quando l'utente seleziona una facoltà
    protected void ddlFacolta_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlCorsi.Items.Clear();
        CaricaCorsi(ddlFacolta.SelectedValue);
        ddlCorsi.Items.Insert(0, new ListItem("-- Seleziona un corso --", ""));

        // Reset dei repeater e etichetta
        lblCorso.Text = "";
        rptLezioni.DataSource = null;
        rptLezioni.DataBind();
        rptMateriali.DataSource = null;
        rptMateriali.DataBind();
    }

    // Carica i corsi relativi alla session in modo da avere gia la facoltà dello studente
    private void CaricaCorsi(string facoltaId)
    {
        DB db = new DB();
        db.cmd.Parameters.AddWithValue("@FacoltaId", Guid.Parse(facoltaId));
        db.query = "";
        DataTable dt = db.SQLselect();

        ddlCorsi.DataSource = dt;
        ddlCorsi.DataTextField = "Nome";
        ddlCorsi.DataValueField = "Id";
        ddlCorsi.DataBind();
    }

    // Quando l'utente seleziona un corso
    protected void ddlCorsi_SelectedIndexChanged(object sender, EventArgs e)
    {
        string corsoId = ddlCorsi.SelectedValue;
        lblCorso.Text = "Corso: " + ddlCorsi.SelectedItem.Text;

        rptLezioni.DataSource = GetLezioni(corsoId);
        rptLezioni.DataBind();

        rptMateriali.DataSource = GetMateriali(corsoId);
        rptMateriali.DataBind();
    }

    // Ottieni lezioni tramite stored procedure 
    private List<Lezioni> GetLezioni(string corsoId)
    {
        var lezioni = new List<Lezioni>();
        DB db = new DB();
        db.cmd.Parameters.AddWithValue("@CorsoId", corsoId);
        db.query = "";
        DataTable dt = db.SQLselect();

        foreach (DataRow row in dt.Rows)
        {
            lezioni.Add(new Lezioni
            {
                Titolo = row["Titolo"].ToString(),
                Vista = Convert.ToBoolean(row["Vista"])
            });
        }

        return lezioni;
    }

    // Ottieni materiali tramite stored procedure 
    private List<Materiali> GetMateriali(string corsoId)
    {
        var materiali = new List<Materiali>();
        DB db = new DB();
        db.cmd.Parameters.AddWithValue("@CorsoId", corsoId);
        db.query = "MATERIALI_SelezionaTutto";
        DataTable dt = db.SQLselect();

        foreach (DataRow row in dt.Rows)
        {
            materiali.Add(new Materiali
            {
                Titolo = row["Titolo"].ToString(),
                UrlFile = row["UrlFile"].ToString(),
                Tipo = row["Tipo"].ToString()
            });
        }

        return materiali;
    }
}
