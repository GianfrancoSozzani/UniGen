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
    internal class DOCENTI
    {

        public Guid K_Docente { get; set; }
        public string Nome { get; set; }
        public string Cognome { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime DataNascita { get; set; }
        public string Indirizzo { get; set; }
        public string CAP { get; set; }
        public string Citta { get; set; }

        /// <summary>
        /// File immagine del profilo
        /// </summary>
        public string ImmagineProfilo { get; set; } //Da capire tipo di dato byte

        /// <summary>
        /// Tipo del file immagine del profilo
        /// </summary>
        public string Tipo { get; set; }

        public DateTime DataRegistrazione { get; set; }

        /// <summary>
        /// Indica se il docente è abilitato
        /// </summary>
        public bool Abilitato { get; set; }

        public void Inserimento()
        {
            DB dB = new DB();
            dB.query = "DOCENTI_Inserimento";
            dB.cmd.Parameters.AddWithValue("@Nome", Nome);
            dB.cmd.Parameters.AddWithValue("@Cognome", Cognome);
            dB.cmd.Parameters.AddWithValue("@Email", Email);
            dB.cmd.Parameters.AddWithValue("@Password", Password);
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
        public DataTable SelezionaTutto()
        {
            DB dB = new DB();
            dB.query = "DOCENTI_SelezionaTutto";
            return dB.SQLselect();
        }

        public void Modifica()
        {
            DB dB = new DB();
            dB.query = "DOCENTI_Modifica";
            dB.cmd.Parameters.AddWithValue("@Nome", Nome);
            dB.cmd.Parameters.AddWithValue("@Cognome", Cognome);
            dB.cmd.Parameters.AddWithValue("@Email", Email);
            dB.cmd.Parameters.AddWithValue("@Password", Password);
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

    }
}
