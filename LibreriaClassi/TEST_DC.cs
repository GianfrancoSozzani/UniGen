using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace LibreriaClassi
{
    /// <summary>
    /// Classe per la gestione delle domande a risposta chiusa
    /// </summary>
    internal class TEST_DC
    {
        public Guid K_Test_DC { get; set; }
        public int Numero_Domanda { get; set; }
        public string Domanda { get; set; }
        public string Risposte { get; set; }
        public string RispostaCorretta { get; set; }
        public string RispostaData { get; set; }
        /// <summary>
        /// Codice Test uniqueidentifier per ogni domanda di uno stesso test
        /// </summary>
        public Guid Codice_Test { get; set; }

        public void Inserimento()
        {
            DB dB = new DB();
            dB.query = "TEST_DC_Inserimento";
            dB.cmd.Parameters.AddWithValue("@Numero_Domanda", Numero_Domanda);
            dB.cmd.Parameters.AddWithValue("@Domanda", Domanda);
            dB.cmd.Parameters.AddWithValue("@Risposte", Risposte);
            dB.cmd.Parameters.AddWithValue("@RispostaCorretta", RispostaCorretta);
            dB.cmd.Parameters.AddWithValue("@RispostaData", RispostaData);
            dB.cmd.Parameters.AddWithValue("@Codice_Test", Codice_Test);
            dB.SQLcommand();
        }
        public void Modifica()
        {
            DB dB = new DB();
            dB.query = "TEST_DC_Modifica";
            dB.cmd.Parameters.AddWithValue("@K_Test_DC", K_Test_DC);
            dB.cmd.Parameters.AddWithValue("@Numero_Domanda", Numero_Domanda);
            dB.cmd.Parameters.AddWithValue("@Domanda", Domanda);
            dB.cmd.Parameters.AddWithValue("@Risposte", Risposte);
            dB.cmd.Parameters.AddWithValue("@RispostaCorretta", RispostaCorretta);
            dB.cmd.Parameters.AddWithValue("@RispostaData", RispostaData);
            dB.cmd.Parameters.AddWithValue("@Codice_Test", Codice_Test);

            dB.SQLcommand();
        }
        public DataTable SelezionaTutto()
        {

            DB dB = new DB();
            dB.query = "TEST_DC_SelezionaTutto";
            return dB.SQLselect();
        }
        public DataTable SelezionaChiave()
        {
            DB dB = new DB();
            dB.query = "TEST_DC_SelezionaChiave";
            dB.cmd.Parameters.AddWithValue("@K_Test_DC", K_Test_DC);
            return dB.SQLselect();
        }
    }
}
