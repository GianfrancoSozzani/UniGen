using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Data;

namespace LibreriaClassi
{
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

        public string ImmagineProfilo { get; set; } //Da capire tipo di dato
        public string Tipo { get; set; }

        public DateTime DataRegistrazione { get; set; }
        public bool Abilitato { get; set; }

        public void Inserimento()
        {
            
            dB.storedProcedure = "DipendentiInserimento";
            dB.cmd.Parameters.AddWithValue("@Nome", Nome);
            dB.cmd.Parameters.AddWithValue("@Cognome", Cognome);
            dB.cmd.Parameters.AddWithValue("@Ruolo", Ruolo);
            dB.cmd.Parameters.AddWithValue("@K_Salone", K_Salone);
            dB.SQLCommand();
        }
        public DataTable SelectAll()
        {
            DB dB = new DB();
            dB.storedProcedure = "DIPENDENTI_SelectAll";
            return dB.SQLSelect();
        }

        public void Modifica()
        {
            DB dB = new DB();
            dB.storedProcedure = "DIPENDENTI_Modifica";
            dB.cmd.Parameters.AddWithValue("@Nome", Nome);
            dB.cmd.Parameters.AddWithValue("@Cognome", Cognome);
            dB.cmd.Parameters.AddWithValue("@Ruolo", Ruolo);
            dB.cmd.Parameters.AddWithValue("@K_Salone", K_Salone);
            dB.cmd.Parameters.AddWithValue("@chiave", K_Dipendente);
            dB.SQLCommand();
        }

        public DataTable SelectOne()
        {
            DB dB = new DB();
            dB.storedProcedure = "DIPENDENTI_SelectOne";
            dB.cmd.Parameters.AddWithValue("@chiave", K_Dipendente);
            return dB.SQLSelect();
        }




    }



}
