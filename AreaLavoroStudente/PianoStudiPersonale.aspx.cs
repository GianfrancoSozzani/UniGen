using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using LibreriaClassi;


    public partial class PianoStudiPersonale : Page
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
            CaricaAA(int.Parse(Matricola));
            CaricaPianoStudio();
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

    public void CaricaAA(int Matricola)
    {

        STUDENTI studente = new STUDENTI();
        studente.Matricola = Matricola;
        DataTable dt = studente.SelezionaAnnoAccademico(Matricola);

        if (dt.Rows.Count >= 1)
        {
            string annoAccademico = dt.Rows[0]["AnnoAccademico"].ToString();
            string corso = dt.Rows[0]["TitoloCorso"].ToString();
            string facolta = dt.Rows[0]["TitoloFacolta"].ToString();

            lblAnno.Text = "Anno Accademico " + annoAccademico;
            lblFacolta.Text = "Facoltà " + facolta;
            lblCorso.Text = "Corso " + corso;
        }
    }

    private void CaricaPianoStudio()
        {
            try
            {
            Guid K_Studente = new Guid((string)Session["cod"]);
            PIANISTUDIOPERSONALI piano = new PIANISTUDIOPERSONALI();
                DataTable dt = piano.SelezionaStudentePianiStudioPersonale(K_Studente);

                if (dt.Rows.Count > 0)
                {
                    rptPianoStudio.DataSource = dt;
                    rptPianoStudio.DataBind();
                    pnlNessunDato.Visible = false;
                }
                else
                {
                    rptPianoStudio.Visible = false;
                    pnlNessunDato.Visible = true;
                }
            }
            catch (SqlException sqlEx)
            {
                MostraErrore("Errore durante l'operazione: " + sqlEx.Message);
            }
            catch (Exception ex)
            {
                MostraErrore("Errore durante l'operazione: " + ex.Message);
            }   
        }

        protected void btnNuovoPiano_Click(object sender, EventArgs e)
        {
            Response.Redirect("InserisciPianoStudio.aspx");
    }

    protected void rptPianoStudio_ItemCommand(object source, System.Web.UI.WebControls.RepeaterCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Elimina")
            {
                Guid K_Studente = new Guid((string)Session["cod"]);
                Guid idPiano = new Guid(e.CommandArgument.ToString());
                PIANISTUDIOPERSONALI piano = new PIANISTUDIOPERSONALI
                {
                    K_PianoStudioPersonale = idPiano
                };

                piano.Elimina();
                CaricaEsamiDisponibili(K_Studente);
                MostraSuccesso("Esame rimosso con successo dal piano di studio.");
                CaricaPianoStudio();
            }
            else if (e.CommandName == "Modifica")
            {
                string idPiano = e.CommandArgument.ToString();
                Response.Redirect("ModificaPianoStudi.aspx?idPiano=" + idPiano);
            }
        }
        catch (SqlException sqlEx)
        {
            MostraErrore("Errore durante l'operazione: " + sqlEx.Message);
        }
        catch (Exception ex)
        {
            MostraErrore("Errore durante l'operazione: " + ex.Message);
        }
    }

    private void CaricaEsamiDisponibili(Guid K_Studente)
    {
        try
        {
            PIANISTUDIOPERSONALI piano = new PIANISTUDIOPERSONALI();
            DataTable dt = piano.GetEsamiDisponibili(K_Studente);

            ddlEsame.DataSource = dt;
            ddlEsame.DataTextField = "TitoloEsame";
            ddlEsame.DataValueField = "K_Esame";
            ddlEsame.DataBind();
        }
        catch (SqlException sqlEx)
        {
            MostraModalErrore("Errore database:" + sqlEx.Message);
        }
        catch (Exception ex)
        {
            MostraModalErrore("Errore database:" + ex.Message);
        }
    }

    protected void btnSalvaModal_Click(object sender, EventArgs e)
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

            CaricaEsamiDisponibili(piano.K_Studente);

            CaricaPianoStudio();
            
            ScriptManager.RegisterStartupScript(this, this.GetType(), "closeModal",
                "$('#modalInserisciEsame').modal('hide');", true);
            MostraSuccesso("Esame aggiunto con successo!");
        }
        catch (SqlException sqlEx)
        {
            MostraModalErrore("Errore database durante il salvataggio: " + sqlEx.Message);
        }
        catch (Exception ex)
        {
            MostraModalErrore("Errore database durante il salvataggio: " + ex.Message);
        }
    }


    private void MostraModalErrore(string messaggio)
    {
        pnlModalMessaggio.Visible = true;
        pnlModalMessaggio.CssClass = "alert alert-danger alert-dismissible fade show alert-message";
        litModalMessaggio.Text = messaggio;
    }

    private void MostraSuccesso(string messaggio)
        {
            pnlMessaggio.Visible = true;
            pnlMessaggio.CssClass = "alert alert-success alert-dismissible fade show mb-3";
            litMessaggio.Text = messaggio;
        }

        private void MostraErrore(string messaggio)
        {
            pnlMessaggio.Visible = true;
            pnlMessaggio.CssClass = "alert alert-danger alert-dismissible fade show mb-3";
            litMessaggio.Text = messaggio;
        }

        private void MostraInfo(string messaggio)
        {
            pnlMessaggio.Visible = true;
            pnlMessaggio.CssClass = "alert alert-info alert-dismissible fade show mb-3";
            litMessaggio.Text = messaggio;
        }
    }
