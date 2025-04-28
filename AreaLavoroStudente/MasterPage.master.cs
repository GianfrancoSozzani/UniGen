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
using System.Security.Cryptography.X509Certificates;


public partial class MasterPage : System.Web.UI.MasterPage
{
    public int matricolaTest;
    protected void Page_Load(object sender, EventArgs e, ClientScriptManager clientScriptManager)

    {
        matricolaTest = 1;

        if (matricolaTest == 0)
        {
            clientScriptManager.RegisterStartupScript(this.GetType(), "ShowLoginModal", "showLoginModal();", true);
            //ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Utente non loggato')", true);
            //return;
        }
        else
        {
            STUDENTI studente = new STUDENTI();
            studente.Matricola = matricolaTest;
            DataTable dt = studente.SelezionaPerMatricola();

            if (dt.Rows.Count > 0)
            {
                string nome = dt.Rows[0]["Nome"].ToString();
                string cognome = dt.Rows[0]["Cognome"].ToString();
                lblStudente.Text = nome + cognome;
            }
        }

    }


    }

   

