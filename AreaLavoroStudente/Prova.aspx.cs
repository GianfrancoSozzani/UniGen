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
            Guid K_Prova = Guid.Parse(Request.QueryString["prova"]);
            Guid Cod = Guid.Parse(Session["Cod"].ToString());
            // Carica i dati necessari per la pagina
            CaricaDati(K_Prova);
            CaricaStudente(K_Prova, Cod);
        }
    }

    public void CaricaDati(Guid K_Prova)
    {
        // Carica i dati della prova
        DB db = new DB();
        db.query = "Prova_SelectByChiave";
        db.cmd.Parameters.AddWithValue("@k_prova", K_Prova);
        rptDomande.DataSource = db.SQLselect();
        rptDomande.DataBind();
    }

    protected void btnInviaRisposte_Click(object sender, EventArgs e)
    {
        DB db = new DB();
        Guid K_Prova = Guid.Parse(Request.QueryString["prova"]);
        Guid Cod = Guid.Parse(Session["Cod"].ToString());
        db.query = "Valutazioni_Insert";
        db.cmd.Parameters.AddWithValue("@prova", K_Prova);
        db.cmd.Parameters.AddWithValue("@studente", Cod);
        db.SQLcommand();
        Response.Redirect("Appelli_Gestione.aspx");
    }

    public void CaricaStudente(Guid K_Prova, Guid Cod)
    {
        // Carica i dati dello studente
        DB db = new DB();
        db.query = "Prova_SelectStudente";
        db.cmd.Parameters.AddWithValue("@k_stu", Cod);
        DataTable dt = db.SQLselect();
        lblStudente.Text = dt.Rows[0]["Nome"].ToString() + " " + dt.Rows[0]["Cognome"].ToString();
        lblMatricola.Text = dt.Rows[0]["Matricola"].ToString();
        dt.Clear();
        db.cmd.Parameters.Clear();
        db.query = "Prova_SelectEsame";
        db.cmd.Parameters.AddWithValue("@k_prova", K_Prova);
        dt = db.SQLselect();
        lblCorso.Text = dt.Rows[0]["TitoloCorso"].ToString();
        lblEsame.Text = dt.Rows[0]["TitoloEsame"].ToString();

    }
}