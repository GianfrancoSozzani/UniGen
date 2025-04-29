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
    public class ESAMI
    {
        public Guid K_Esame { get; set; }
        public Guid K_PianoStudio { get; set; }
        public string TitoloEsame { get; set; }
        public int CFU { get; set; }
        public Guid K_Docente { get; set; }


        public void Inserimento()
        {
            DB dB = new DB();
            dB.query = "";
            dB.cmd.Parameters.AddWithValue("", TitoloEsame);
            dB.cmd.Parameters.AddWithValue("", K_PianoStudio);
            dB.cmd.Parameters.AddWithValue("", CFU);
            dB.cmd.Parameters.AddWithValue("", K_Docente);
            dB.SQLcommand();
        }

        public DataTable SelezionaTutto()
        {
            DB dB = new DB();
            dB.query = "Esami_SelectAll";
            return dB.SQLselect();
        }

        public DataTable SelezionaPerTitoloEsame()
        {
            DB dB = new DB();
            dB.query = "Esami_FindByNome";
            dB.cmd.Parameters.AddWithValue("@Nome", TitoloEsame);
            return dB.SQLselect();
        }

        public void Modifica()
        {
            DB dB = new DB();
            dB.query = "";
            dB.cmd.Parameters.AddWithValue("", K_Esame);
            dB.cmd.Parameters.AddWithValue("", K_PianoStudio);
            dB.cmd.Parameters.AddWithValue("", TitoloEsame);
            dB.cmd.Parameters.AddWithValue("", CFU);
            dB.cmd.Parameters.AddWithValue("", K_Docente);
            dB.SQLcommand();
        }
        public DataTable SelezionaChiave()
        {
            DB dB = new DB();
            dB.query = "";
            dB.cmd.Parameters.AddWithValue("", K_Esame);
            return dB.SQLselect();
        }


    }
}
