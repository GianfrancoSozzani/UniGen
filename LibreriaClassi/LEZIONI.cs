using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibreriaClassi
{
    /// <summary>
    /// Classe per la gestione delle lezioni.
    /// </summary>
    internal class LEZIONI
    {
        public Guid K_Lezione { get; set; }
        public Guid K_Esame { get; set; }
        public string Titolo { get; set; }
        /// <summary>
        /// Link al video della lezione.
        /// </summary>
        public string Video { get; set; }

        public void Inserimento()
        {
            DB dB = new DB();
            dB.query = "LEZIONI_Inserimento";
            dB.cmd.Parameters.AddWithValue("@K_Esame", K_Esame);
            dB.cmd.Parameters.AddWithValue("@Titolo", Titolo);
            dB.cmd.Parameters.AddWithValue("@Video", Video);
            dB.SQLcommand();
        }
        public DataTable SelezionaTutto()
        {
            DB dB = new DB();
            dB.query = "LEZIONI_SelezionaTutto";
            return dB.SQLselect();
        }
        public void Modifica()
        {
            DB dB = new DB();
            dB.query = "LEZIONI_Modifica";
            dB.cmd.Parameters.AddWithValue("@K_Lezione", K_Lezione);
            dB.cmd.Parameters.AddWithValue("@K_Esame", K_Esame);
            dB.cmd.Parameters.AddWithValue("@Titolo", Titolo);
            dB.cmd.Parameters.AddWithValue("@Video", Video);
            dB.SQLcommand();
        }
        public DataTable SelezionaChiave()
        {
            DB dB = new DB();
            dB.query = "LEZIONI_SelezionaChiave";
            dB.cmd.Parameters.AddWithValue("@K_Lezione", K_Lezione);
            return dB.SQLselect();
        }
    }
}
