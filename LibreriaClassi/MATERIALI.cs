using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibreriaClassi
{
    /// <summary>
    /// Classe per la gestione dei materiali didattici
    /// </summary>
    internal class MATERIALI
    {
        public Guid K_Materiale { get; set; }
        public string Titolo { get; set; }

        /// <summary>
        /// File del materiale didattico
        /// </summary>
        public string Materiale { get; set; }

        /// <summary>
        /// Tipo di file: Esempio: PDF, DOCX, PPTX, XLSX, ZIP
        /// </summary>
        public string Tipo { get; set; }

        public Guid K_Esame { get; set; }

        public void Inserimento()
        {
            DB dB = new DB();
            dB.query = "MATERIALI_Inserimento";
            dB.cmd.Parameters.AddWithValue("@Titolo", Titolo);
            dB.cmd.Parameters.AddWithValue("@Materiale", Materiale);
            dB.cmd.Parameters.AddWithValue("@Tipo", Tipo);
            dB.cmd.Parameters.AddWithValue("@K_Esame", K_Esame);
            dB.SQLcommand();
        }
        public DataTable SelezionaTutto()
        {
            DB dB = new DB();
            dB.query = "MATERIALI_SelezionaTutto";
            return dB.SQLselect();
        }
        public void Modifica()
        {
            DB dB = new DB();
            dB.query = "MATERIALI_Modifica";
            dB.cmd.Parameters.AddWithValue("@K_Materiale", K_Materiale);
            dB.cmd.Parameters.AddWithValue("@Titolo", Titolo);
            dB.cmd.Parameters.AddWithValue("@Materiale", Materiale);
            dB.cmd.Parameters.AddWithValue("@Tipo", Tipo);
            dB.cmd.Parameters.AddWithValue("@K_Esame", K_Esame);
            dB.SQLcommand();
        }
        public DataTable SelezionaChiave()
        {
            DB dB = new DB();
            dB.query = "MATERIALI_SelezionaChiave";
            dB.cmd.Parameters.AddWithValue("@K_Materiale", K_Materiale);
            return dB.SQLselect();
        }
    }
}
