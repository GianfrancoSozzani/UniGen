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
            if (!IsPostBack)
            {
            string K_Studente = Session["cod"].ToString();
            Session["cod"] = K_Studente;

            CaricaPianoStudio();
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
                    Guid idPiano = new Guid(e.CommandArgument.ToString());
                    PIANISTUDIOPERSONALI piano = new PIANISTUDIOPERSONALI
                    {
                        K_PianoStudioPersonale = idPiano
                    };

                    piano.Elimina();
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
