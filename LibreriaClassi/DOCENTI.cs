using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Data;

namespace LibreriaClassi
{
    /// <summary>
    /// Classe per la gestione dei docenti
    /// </summary>
    public class DOCENTI
    {

        public Guid K_Docente { get; set; }
        public string Nome { get; set; }
        public string Cognome { get; set; }
        public string Email { get; set; }
        public string PWD { get; set; }
        public DateTime DataNascita { get; set; }
        public string Indirizzo { get; set; }
        public string CAP { get; set; }
        public string Citta { get; set; }
        public string Provincia { get; set; }

        /// <summary>
        /// File immagine del profilo
        /// </summary>
        public byte[] ImmagineProfilo { get; set; } //Da capire tipo di dato byte

        /// <summary>
        /// Tipo del file immagine del profilo
        /// </summary>
        public string Tipo { get; set; }

        public DateTime DataRegistrazione { get; set; }

        /// <summary>
        /// Indica se il docente è abilitato
        /// </summary>
        public string Abilitato { get; set; }

        public void Inserimento()
        {
            DB dB = new DB();
            dB.query = "Docenti_Inserimento";
            dB.cmd.Parameters.AddWithValue("@Email", Email);
            dB.cmd.Parameters.AddWithValue("PWD", PWD);
            dB.cmd.Parameters.AddWithValue("@Cognome", Cognome);
            dB.cmd.Parameters.AddWithValue("@Nome", Nome);
            dB.cmd.Parameters.AddWithValue("@DataNascita", DataNascita);
            dB.cmd.Parameters.AddWithValue("@Indirizzo", Indirizzo);
            dB.cmd.Parameters.AddWithValue("@CAP", CAP);
            dB.cmd.Parameters.AddWithValue("@Citta", Citta);
            dB.cmd.Parameters.AddWithValue("@Provincia", Provincia);
            dB.cmd.Parameters.AddWithValue("@ImmagineProfilo", ImmagineProfilo);
            dB.cmd.Parameters.AddWithValue("@Tipo", Tipo);
            dB.cmd.Parameters.AddWithValue("@DataDiRegistrazione", DataRegistrazione);
            dB.cmd.Parameters.AddWithValue("@Abilitato", Abilitato);

            dB.SQLcommand();
        }
        public DataTable SelezionaTutto()
        {
            DB dB = new DB();
            dB.query = "Docenti_SelectAll";
            return dB.SQLselect();
        }

        public DataTable SelezionaNomeCompleto()
        {
            DB dB = new DB();
            dB.query = "Docenti_SelectNomeCompleto";
            return dB.SQLselect();
        }

        public DataTable SelezionaPerCognomeNome()
        {
            DB dB = new DB();
            dB.query = "Docenti_FindByCognomeNome";
            dB.cmd.Parameters.AddWithValue("@Cognome", Cognome);
            dB.cmd.Parameters.AddWithValue("@Nome", Nome);
            return dB.SQLselect();
        }

        public void Modifica()
        {
            DB dB = new DB();
            dB.query = "DOCENTI_Modifica";
            dB.cmd.Parameters.AddWithValue("@Nome", Nome);
            dB.cmd.Parameters.AddWithValue("@Cognome", Cognome);
            dB.cmd.Parameters.AddWithValue("@Email", Email);
            dB.cmd.Parameters.AddWithValue("@Password", PWD);
            dB.cmd.Parameters.AddWithValue("@DataNascita", DataNascita);
            dB.cmd.Parameters.AddWithValue("@Indirizzo", Indirizzo);
            dB.cmd.Parameters.AddWithValue("@CAP", CAP);
            dB.cmd.Parameters.AddWithValue("@Citta", Citta);
            dB.cmd.Parameters.AddWithValue("@ImmagineProfilo", ImmagineProfilo);
            dB.cmd.Parameters.AddWithValue("@Tipo", Tipo);
            dB.cmd.Parameters.AddWithValue("@DataRegistrazione", DataRegistrazione);
            dB.cmd.Parameters.AddWithValue("@Abilitato", Abilitato);
            dB.SQLcommand();
        }

        public DataTable SelezionaChiave()
        {
            DB dB = new DB();
            dB.query = "DOCENTI_SelezionaChiave";
            dB.cmd.Parameters.AddWithValue("@K_Docente", K_Docente);
            return dB.SQLselect();
        }

        public void Abilita()
        {
            DB db = new DB();
            db.query = "Docenti_Abilita";
            db.cmd.Parameters.AddWithValue("@Chiave", K_Docente);
            db.SQLcommand();
        }

        public void Disabilita()
        {
            DB db = new DB();
            db.query = "Docenti_Disabilita";
            db.cmd.Parameters.AddWithValue("@Chiave", K_Docente);
            db.SQLcommand();
        }

        public DataTable DocentiAttivi()
        {
            DB db = new DB();
            db.query = "Docenti_CountAttivi";
            return db.SQLselect();
        }

        public DataTable VerificaDoppione()
        {
            DB dB = new DB();
            dB.query = "Docenti_VerificaDoppione";
            dB.cmd.CommandType = CommandType.StoredProcedure;
            dB.cmd.Parameters.AddWithValue("@Cognome", Cognome);
            dB.cmd.Parameters.AddWithValue("@Nome", Nome);
            return dB.SQLselect();
        }
    }
}
