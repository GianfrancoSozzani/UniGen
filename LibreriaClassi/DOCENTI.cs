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

        //public void Inserimento()             //Registrazione tramite API
        //{
        //    DB dB = new DB();
        //    dB.query = "";
        //    dB.cmd.Parameters.AddWithValue("", Nome);
        //    dB.cmd.Parameters.AddWithValue("", Cognome);
        //    dB.cmd.Parameters.AddWithValue("", Email);
        //    dB.cmd.Parameters.AddWithValue("", Password);
        //    dB.cmd.Parameters.AddWithValue("", DataNascita);
        //    dB.cmd.Parameters.AddWithValue("", Indirizzo);
        //    dB.cmd.Parameters.AddWithValue("", CAP);
        //    dB.cmd.Parameters.AddWithValue("", Citta);
        //    dB.cmd.Parameters.AddWithValue("", ImmagineProfilo);
        //    dB.cmd.Parameters.AddWithValue("", Tipo);
        //    dB.cmd.Parameters.AddWithValue("", DataRegistrazione);
        //    dB.cmd.Parameters.AddWithValue("", Abilitato);
        //    dB.SQLcommand();
        //}
        public DataTable SelezionaTutto()   //Docenti SelectAll
        {
            DB dB = new DB();
            dB.query = "Docenti_SelectAll";
            return dB.SQLselect();
        }

        public DataTable SelezionaPerCognomeNome()      //Docenti ricerca per cognome e nome
        {
            DB dB = new DB();
            dB.query = "Docenti_FindByCognomeNome";
            dB.cmd.Parameters.AddWithValue("@Cognome", Cognome);
            dB.cmd.Parameters.AddWithValue("@Nome", Nome);
            return dB.SQLselect();
        }

        //public void Modifica()            //Modifica tramite API
        //{
        //    DB dB = new DB();
        //    dB.query = "DOCENTI_Modifica";
        //    dB.cmd.Parameters.AddWithValue("@Nome", Nome);
        //    dB.cmd.Parameters.AddWithValue("@Cognome", Cognome);
        //    dB.cmd.Parameters.AddWithValue("@Email", Email);
        //    dB.cmd.Parameters.AddWithValue("@Password", Password);
        //    dB.cmd.Parameters.AddWithValue("@DataNascita", DataNascita);
        //    dB.cmd.Parameters.AddWithValue("@Indirizzo", Indirizzo);
        //    dB.cmd.Parameters.AddWithValue("@CAP", CAP);
        //    dB.cmd.Parameters.AddWithValue("@Citta", Citta);
        //    dB.cmd.Parameters.AddWithValue("@ImmagineProfilo", ImmagineProfilo);
        //    dB.cmd.Parameters.AddWithValue("@Tipo", Tipo);
        //    dB.cmd.Parameters.AddWithValue("@DataRegistrazione", DataRegistrazione);
        //    dB.cmd.Parameters.AddWithValue("@Abilitato", Abilitato);
        //    dB.SQLcommand();
        //}

        public DataTable SelezionaChiave()      //Ricerca tramite Chiave
        {
            DB dB = new DB();
            dB.query = "Docenti_FindByChiave";
            dB.cmd.Parameters.AddWithValue("@K_Docente", K_Docente);
            return dB.SQLselect();
        }

        public DataTable SelezionaCorso(string TitoloCorso)     //Seleziona docenti per corso (ATTENZIONE A PASSARE IL TITOLO DEL CORSO)
        {
            DB dB = new DB();
            dB.query = "Docenti_FindByCorso";
            dB.cmd.Parameters.AddWithValue("@Corso", TitoloCorso);
            return dB.SQLselect();
        }

        public void Abilita()       //Abilita docente
        {
            DB dB = new DB();
            dB.query = "Docenti_Abilita";
            dB.cmd.Parameters.AddWithValue("@K_Docente", K_Docente);
            dB.SQLcommand();
        }

        public void Disabilita()        //Disabilita docente
        {
            DB dB = new DB();
            dB.query = "Docenti_Disabilita";
            dB.cmd.Parameters.AddWithValue("@K_Docente", K_Docente);
            dB.SQLcommand();
        }
    }
}
