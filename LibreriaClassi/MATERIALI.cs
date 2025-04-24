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
            dB.query = "";
            dB.cmd.Parameters.AddWithValue("", Titolo);
            dB.cmd.Parameters.AddWithValue("", Materiale);
            dB.cmd.Parameters.AddWithValue("", Tipo);
            dB.cmd.Parameters.AddWithValue("", K_Esame);
            dB.SQLcommand();
        }
        public DataTable SelezionaTutto()
        {
            DB dB = new DB();
            dB.query = "Materiali_SelectAll";
            return dB.SQLselect();
        }
        public DataTable SelezionaPerNome()
        {
            DB dB = new DB();
            dB.query = "Materiali_FindByNome";
            dB.cmd.Parameters.AddWithValue("@Nome", Titolo);
            return dB.SQLselect();
        }
        public void Modifica()
        {
            DB dB = new DB();
            dB.query = "";
            dB.cmd.Parameters.AddWithValue("", K_Materiale);
            dB.cmd.Parameters.AddWithValue("", Titolo);
            dB.cmd.Parameters.AddWithValue("", Materiale);
            dB.cmd.Parameters.AddWithValue("", Tipo);
            dB.cmd.Parameters.AddWithValue("", K_Esame);
            dB.SQLcommand();
        }
        public DataTable SelezionaChiave()
        {
            DB dB = new DB();
            dB.query = "";
            dB.cmd.Parameters.AddWithValue("", K_Materiale);
            return dB.SQLselect();
        }
    }
}
