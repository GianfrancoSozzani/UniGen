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

    }

    protected void btnSalva_Click(object sender, EventArgs e)
    {
        string Email = txtEmail.Text.Trim();
        string PWD = txtPWD.Text.Trim();
        string Cognome = txtCognome.Text.Trim();
        string Nome = txtNome.Text.Trim();
        string dataNascitaString = txtDataDiNascita.Text;
        DateTime DataDiNascita;
        if (!DateTime.TryParse(dataNascitaString, out DataDiNascita))
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Data non valida')", true);
            //Response.Write("Data non valida.");
            return;
        }
        // Controllo che la data di nascita non sia futura
        if (DataDiNascita > DateTime.Now)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Data di nascita non valida')", true);
            //Response.Write("Data di nascita non valida.");
            return;
        }
        DataDiNascita = DateTime.Parse(dataNascitaString).Date;
        string Indirizzo = txtIndirizzo.Text.Trim();
        string Citta = txtCitta.Text.Trim();
        string CAP = txtCAP.Text.Trim();
        if (CAP.Length != 5) 
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('CAP non valido')", true);
            //Response.Write("CAP non valido.");
            return;
        }
        string Provincia = txtProvincia.Text.Trim().ToUpper();
        if (Provincia.Length != 2)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Provincia non valida')", true);
            //Response.Write("Provincia non valida.");
            return;
        }
        byte[] imgData = fuFotoProfilo.FileBytes;
        if (!fuFotoProfilo.HasFile)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Inserire una foto profilo valida')", true);
            //Response.Write("Inserire una foto profilo valida.");
            return;
        }
        string Tipo = fuFotoProfilo.PostedFile.ContentType;
        txtTipo.Text = Tipo;
        string Abilitato = CheckBoxAbilitato.Checked ? "S" : "N"; 
        if (String.IsNullOrEmpty(Cognome) ||
            String.IsNullOrEmpty(Nome) ||
            String.IsNullOrEmpty(txtDataDiNascita.Text) ||
            String.IsNullOrEmpty(Indirizzo) ||
            String.IsNullOrEmpty(Citta) ||
            String.IsNullOrEmpty(CAP) ||
            String.IsNullOrEmpty(Provincia) ||
            String.IsNullOrEmpty(Abilitato))
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Attenzione! Non possono essere lasciati campi vuoti')", true);
            return;
        }

        // Rendo la prima lettera scritta sempre maiuscola
        Cognome = char.ToUpper(Cognome[0]) + Cognome.Substring(1);
        Nome = char.ToUpper(Nome[0]) + Nome.Substring(1);
        Citta = char.ToUpper(Citta[0]) + Citta.Substring(1);



        

        // Controllo che non permette l'uso di numeri o caratteri speciali
        if (!System.Text.RegularExpressions.Regex.IsMatch(Cognome, @"^[a-zA-Z\s]+$") ||
            !System.Text.RegularExpressions.Regex.IsMatch(Nome, @"^[a-zA-Z\s]+$") ||
            !System.Text.RegularExpressions.Regex.IsMatch(Citta, @"^[a-zA-Z\s]+$") ||
            !System.Text.RegularExpressions.Regex.IsMatch(Provincia, @"^[a-zA-Z\s]+$"))
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Attenzione! Inserire solo lettere')", true);
            return;
        }

        DOCENTI d = new DOCENTI();
        d.Email = Email;
        d.PWD = PWD;
        d.Cognome = Cognome;
        d.Nome = Nome;
        d.DataNascita = DataDiNascita;
        d.Indirizzo = Indirizzo;
        d.CAP = CAP;
        d.Citta = Citta;
        d.Provincia = Provincia;
        d.ImmagineProfilo = imgData;
        d.Tipo = Tipo;
        d.DataRegistrazione = DateTime.Now;
        d.Abilitato = Abilitato;

        //DataTable dt = d.VerificaDoppione();

        //// Controllo duplicato
        //if (dt.Rows.Count != 0)
        //{
        //    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Docente già registrato')", true);
        //    return;
        //}

        d.Inserimento();
        Response.Redirect("GestioneDocenti.aspx");
    }
}