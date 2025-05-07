using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using LibreriaClassi;
using System.Security.Cryptography.X509Certificates;

public partial class InserisciPianoStudi : Page
    {
        protected void Page_Load(object sender, EventArgs e)

        {
        ValidationSettings.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
        if (!IsPostBack)
            {
            string Matricola = Session["mat"].ToString();
            Session["mat"] = Matricola;
            string K_Studente = Session["cod"].ToString();
            Session["cod"] = K_Studente;
            try
                {
                    CaricaEsamiDisponibili(Guid.Parse(K_Studente));
                }
                catch (SqlException sqlEx)
                {
                    MostraErrore("Errore database:" + sqlEx.Message);
                }
                catch (Exception ex)
                {
                    MostraErrore("Errore database:" + ex.Message);
                }
            }
        }

        private void CaricaEsamiDisponibili(Guid K_Studente)
        {
            PIANISTUDIOPERSONALI piano = new PIANISTUDIOPERSONALI();
             
            DataTable dt = piano.GetEsamiDisponibili(K_Studente);

            ddlEsame.DataSource = dt;
            ddlEsame.DataTextField = "TitoloEsame";
            ddlEsame.DataValueField = "K_Esame";
            
            
            ddlEsame.DataBind();
        }

        protected void btnSalva_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;

            try
            {

            PIANISTUDIOPERSONALI dati = new PIANISTUDIOPERSONALI
            {
                K_Esame = new Guid(ddlEsame.SelectedValue)
            };
            DataTable dt = dati.GetDatiEsame();

            PIANISTUDIOPERSONALI piano = new PIANISTUDIOPERSONALI
            {
                K_PianoStudioPersonale = Guid.NewGuid(),
                K_Esame = new Guid(ddlEsame.SelectedValue),
                K_Studente = new Guid((string)Session["cod"]),
                Obbligatorio = Convert.ToChar(dt.Rows[0]["Obbligatorio"]),
                AnnoAccademico = dt.Rows[0]["AnnoAccademico"].ToString()                
            };

                piano.Inserimento();
                MostraSuccesso("Esame aggiunto con successo al piano di studio!");
                Response.AddHeader("REFRESH", "2;URL=PianoStudiPersonale.aspx");
            }
            catch (SqlException sqlEx)
            {
                MostraErrore("Errore database durante il salvataggio: " + sqlEx.Message);
            }
            catch (Exception ex)
            {
                MostraErrore("Errore database durante il salvataggio: " + ex.Message);
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
