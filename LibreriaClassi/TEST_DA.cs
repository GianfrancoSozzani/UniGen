using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibreriaClassi
{
    /// <summary>
    /// Classe per la gestione delle domande a risposta aperta
    /// </summary>
    internal class TEST_DA
    {
        public Guid K_Test_DA { get; set; }
        public int Numero_Domanda { get; set; }
        public string Domanda { get; set; }
        public string Risposta { get; set; }
        public Guid Codice_Test { get; set; }

        public void Inserimento()
        {
            DB dB = new DB();
            dB.query = "TEST_DA_Inserimento";
            dB.cmd.Parameters.AddWithValue("@Numero_Domanda", Numero_Domanda);
            dB.cmd.Parameters.AddWithValue("@Domanda", Domanda);
            dB.cmd.Parameters.AddWithValue("@Risposta", Risposta);
            dB.cmd.Parameters.AddWithValue("@Codice_Test", Codice_Test);
            dB.SQLcommand();
        }
        public void Modifica()
        {
            DB dB = new DB();
            dB.query = "TEST_DA_Modifica";
            dB.cmd.Parameters.AddWithValue("@K_Test_DA", K_Test_DA);
            dB.cmd.Parameters.AddWithValue("@Numero_Domanda", Numero_Domanda);
            dB.cmd.Parameters.AddWithValue("@Domanda", Domanda);
            dB.cmd.Parameters.AddWithValue("@Risposta", Risposta);
            dB.cmd.Parameters.AddWithValue("@Codice_Test", Codice_Test);
            dB.SQLcommand();
        }
        public DataTable SelezionaTutto()
        {

            DB dB = new DB();
            dB.query = "TEST_DA_SelezionaTutto";
            return dB.SQLselect();
        }
        public DataTable SelezionaChiave()
        {
            DB dB = new DB();
            dB.query = "TEST_DA_SelezionaChiave";
            dB.cmd.Parameters.AddWithValue("@K_Test_DA", K_Test_DA);
            return dB.SQLselect();
        }
    }
}
