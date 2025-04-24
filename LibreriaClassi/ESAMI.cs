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
    /// Classe per la gestione degli esami
    /// </summary>
    internal class ESAMI
    {
        public Guid K_Esame { get; set; }
        public string TitoloEsame { get; set; }
        public int CFU { get; set; }
        public Guid K_Docente { get; set; }
    

        public void Inserimento()
        {
            DB dB = new DB();
            dB.query = "ESAMI_Inserimento";
            dB.cmd.Parameters.AddWithValue("@TitoloEsame", TitoloEsame);
            dB.cmd.Parameters.AddWithValue("@CFU", CFU);
            dB.cmd.Parameters.AddWithValue("@K_Docente", K_Docente);
            dB.SQLcommand();
        }
        public DataTable SelezionaTutto()
        {
            DB dB = new DB();
            dB.query = "ESAMI_SelezionaTutto";
            return dB.SQLselect();
        }
        public void Modifica()
        {
            DB dB = new DB();
            dB.query = "ESAMI_Modifica";
            dB.cmd.Parameters.AddWithValue("@K_Esame", K_Esame);
            dB.cmd.Parameters.AddWithValue("@TitoloEsame", TitoloEsame);
            dB.cmd.Parameters.AddWithValue("@CFU", CFU);
            dB.cmd.Parameters.AddWithValue("@K_Docente", K_Docente);
            dB.SQLcommand();
        }
        public DataTable SelezionaChiave()
        {
            DB dB = new DB();
            dB.query = "ESAMI_SelezionaChiave";
            dB.cmd.Parameters.AddWithValue("@K_Esame", K_Esame);
            return dB.SQLselect();
        }

    }
}
