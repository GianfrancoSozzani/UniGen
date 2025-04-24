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
            dB.query = "";
            dB.cmd.Parameters.AddWithValue("", K_Esame);
            dB.cmd.Parameters.AddWithValue("", Titolo);
            dB.cmd.Parameters.AddWithValue("", Video);
            dB.SQLcommand();
        }
        public DataTable SelezionaTutto()
        {
            DB dB = new DB();
            dB.query = "Lezioni_SelectAll";
            return dB.SQLselect();
        }
        public DataTable SelezionaPerNome()
        {
            DB dB = new DB();
            dB.query = "Lezioni_FindByNome";
            dB.cmd.Parameters.AddWithValue("@Nome", Titolo);
            return dB.SQLselect();
        }
        public void Modifica()
        {
            DB dB = new DB();
            dB.query = "";
            dB.cmd.Parameters.AddWithValue("", K_Lezione);
            dB.cmd.Parameters.AddWithValue("", K_Esame);
            dB.cmd.Parameters.AddWithValue("", Titolo);
            dB.cmd.Parameters.AddWithValue("", Video);
            dB.SQLcommand();
        }
        public DataTable SelezionaChiave()
        {
            DB dB = new DB();
            dB.query = "";
            dB.cmd.Parameters.AddWithValue("", K_Lezione);
            return dB.SQLselect();
        }
    }
}
