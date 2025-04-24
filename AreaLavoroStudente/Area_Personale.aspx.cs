using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI.WebControls;

public partial class AreaPersonale : System.Web.UI.Page
{
    // Stringa di connessione al database SQL Server
    private static readonly string connectionString = "Data Source=.;Initial Catalog=GENERATION;Integrated Security=True"; // Modificare in base alla configurazione del tuo SQL Server

    protected void Page_Load(object sender, EventArgs e)
    {
        // Popola la dropdown delle facoltà solo al primo caricamento della pagina
        if (!IsPostBack)
        {
            CaricaFacolta(); // Chiama metodo per caricare le facoltà dal DB
            ddlFacolta.Items.Insert(0, new ListItem("-- Seleziona una facoltà --", ""));
            ddlCorsi.Items.Insert(0, new ListItem("-- Seleziona un corso --", ""));
        }
    }

    // Metodo per ottenere l'elenco delle facoltà dal database tramite stored procedure
    private void CaricaFacolta()
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        using (SqlCommand cmd = new SqlCommand("sp_GetFacolta", conn)) // Stored procedure: sp_GetFacolta
        {
            cmd.CommandType = CommandType.StoredProcedure;
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            ddlFacolta.DataSource = reader;
            ddlFacolta.DataTextField = "TitoloFacolta"; // Nome da mostrare
            ddlFacolta.DataValueField = "K_Facolta"; // Valore da usare per query successive
            ddlFacolta.DataBind();
        }
    }

    // Evento scatenato dalla selezione di una facoltà
    protected void ddlFacolta_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlCorsi.Items.Clear();
        CaricaCorsi(ddlFacolta.SelectedValue); // Carica corsi associati alla facoltà selezionata
        ddlCorsi.Items.Insert(0, new ListItem("-- Seleziona un corso --", ""));
        lblCorso.Text = "";
        rptLezioni.DataSource = null;
        rptLezioni.DataBind();
        rptMateriali.DataSource = null;
        rptMateriali.DataBind();
    }

    // Metodo per ottenere i corsi associati a una facoltà
    private void CaricaCorsi(string facoltaId)
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        using (SqlCommand cmd = new SqlCommand("sp_GetCorsiByFacolta", conn)) // Stored procedure: sp_GetCorsiByFacolta
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@FacoltaId", Guid.Parse(facoltaId));
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            ddlCorsi.DataSource = reader;
            ddlCorsi.DataTextField = "Nome";
            ddlCorsi.DataValueField = "Id";
            ddlCorsi.DataBind();
        }
    }

    // Evento scatenato dalla selezione di un corso
    protected void ddlCorsi_SelectedIndexChanged(object sender, EventArgs e)
    {
        string corsoId = ddlCorsi.SelectedValue;
        lblCorso.Text = "Corso: " + ddlCorsi.SelectedItem.Text;

        // Popola i repeater con lezioni e materiali
        rptLezioni.DataSource = GetLezioni(corsoId);
        rptLezioni.DataBind();

        rptMateriali.DataSource = GetMateriali(corsoId);
        rptMateriali.DataBind();
    }

    // Metodo per ottenere le lezioni associate al corso selezionato
    private List<Lezione> GetLezioni(string corsoId)
    {
        var lezioni = new List<Lezione>();
        using (SqlConnection conn = new SqlConnection(connectionString))
        using (SqlCommand cmd = new SqlCommand("sp_GetLezioniByCorso", conn)) // Stored procedure: sp_GetLezioniByCorso
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CorsoId", corsoId);
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                lezioni.Add(new Lezione
                {
                    Titolo = reader["Titolo"].ToString(),
                    Vista = Convert.ToBoolean(reader["Vista"])
                });
            }
        }
        return lezioni;
    }

    // Metodo per ottenere i materiali didattici associati al corso selezionato
    private List<Materiale> GetMateriali(string corsoId)
    {
        var materiali = new List<Materiale>();
        using (SqlConnection conn = new SqlConnection(connectionString))
        using (SqlCommand cmd = new SqlCommand("sp_GetMaterialiByCorso", conn)) // Stored procedure: sp_GetMaterialiByCorso
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CorsoId", corsoId);
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                materiali.Add(new Materiale
                {
                    Titolo = reader["Titolo"].ToString(),
                    UrlFile = reader["UrlFile"].ToString(),
                    Tipo = reader["Tipo"].ToString()
                });
            }
        }
        return materiali;
    }

    // Classe che rappresenta una lezione
    public class Lezione
    {
        public string Titolo { get; set; }
        public bool Vista { get; set; }
    }

    // Classe che rappresenta un materiale didattico
    public class Materiale
    {
        public string Titolo { get; set; }
        public string UrlFile { get; set; }
        public string Tipo { get; set; }
    }
}
