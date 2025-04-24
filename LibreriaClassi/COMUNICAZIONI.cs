using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibreriaClassi
{
    /// <summary>
    /// Classe per la gestione delle comunicazioni
    /// </summary>
    public class COMUNICAZIONI
    {
        public Guid K_Comunicazione { get; set; }
        /// <summary>
        /// Codice per identificare la comunicazione per tenere traccia dei messaggi
        /// </summary>
        public Guid Codice_Comunicazione { get; set; }
        public DateTime DataOraComunicazione { get; set; }
        /// <summary>
        /// Chi ha iniziato la comunicazione
        /// S = Studente  D = Docente  A = Amministrazione
        /// </summary>
        public char Soggetto { get; set; }
        /// <summary>
        /// Chiave del soggetto che ha iniziato la comunicazione
        /// </summary>
        public Guid K_Soggetto { get; set; }
        public string Testo { get; set; }
        /// <summary>
        /// Chiave dello studente che RISPONDE alla comunicazione
        /// </summary>
        public Guid K_Studente { get; set; }
        /// <summary>
        /// Chiave del docente che RISPONDE alla comunicazione
        /// </summary>
        public Guid K_Docente { get; set; }

        //public void Inserimento()                                     //GESTIONE COMUNICAZIONI TRAMITE API
        //{
        //    DB dB = new DB();
        //    dB.query = "COMUNICAZIONI_Inserimento";
        //    dB.cmd.Parameters.AddWithValue("@Codice_Comunicazione", Codice_Comunicazione);
        //    dB.cmd.Parameters.AddWithValue("@DataOraComunicazione", DataOraComunicazione);
        //    dB.cmd.Parameters.AddWithValue("@Soggetto", Soggetto);
        //    dB.cmd.Parameters.AddWithValue("@K_Soggetto", K_Soggetto);
        //    dB.cmd.Parameters.AddWithValue("@Testo", Testo);
        //    dB.cmd.Parameters.AddWithValue("@K_Studente", K_Studente);
        //    dB.cmd.Parameters.AddWithValue("@K_Docente", K_Docente);
        //    dB.SQLcommand();
        //}

        //public void Modifica()
        //{
        //    DB dB = new DB();
        //    dB.query = "COMUNICAZIONI_Modifica";
        //    dB.cmd.Parameters.AddWithValue("@K_Comunicazione", K_Comunicazione);
        //    dB.cmd.Parameters.AddWithValue("@Codice_Comunicazione", Codice_Comunicazione);
        //    dB.cmd.Parameters.AddWithValue("@DataOraComunicazione", DataOraComunicazione);
        //    dB.cmd.Parameters.AddWithValue("@Soggetto", Soggetto);
        //    dB.cmd.Parameters.AddWithValue("@K_Soggetto", K_Soggetto);
        //    dB.cmd.Parameters.AddWithValue("@Testo", Testo);
        //    dB.cmd.Parameters.AddWithValue("@K_Studente", K_Studente);
        //    dB.cmd.Parameters.AddWithValue("@K_Docente", K_Docente);
        //    dB.SQLcommand();
        //}

        //public DataTable SelezionaTutto()
        //{
        //    DB dB = new DB();
        //    dB.query = "COMUNICAZIONI_SelezionaTutto";
        //    return dB.SQLselect();
        //}
        //public DataTable SelezionaChiave()
        //{
        //    DB dB = new DB();
        //    dB.query = "COMUNICAZIONI_SelezionaChiave";
        //    dB.cmd.Parameters.AddWithValue("@K_Comunicazione", K_Comunicazione);
        //    return dB.SQLselect();
        //}

    }
}
