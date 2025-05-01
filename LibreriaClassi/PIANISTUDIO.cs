using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibreriaClassi
{
    public class PIANISTUDIO
    {
        public Guid K_PianoStudio { get; set; }
        public Guid K_Corso { get; set; }
        public Guid K_Esame { get; set; }
        /// <summary>
        /// inteso come anno accademico corrente (es. 2024/2025)
        /// </summary>
        public string AnnoAccademico { get; set; }
        /// <summary>
        /// è obbligatorio? (S) si (N) no
        /// </summary>
        public char Obbligatorio { get; set; }


        public PIANISTUDIO()
        {

        }

        public void Inserimento()
        {
            DB db = new DB();
            db.query = "PianiStudio_Insert";
            db.cmd.Parameters.AddWithValue("@k_corso", K_Corso);
            db.cmd.Parameters.AddWithValue("@k_esame", K_Esame);
            db.cmd.Parameters.AddWithValue("@annoaccademico", AnnoAccademico);
            db.cmd.Parameters.AddWithValue("@obbligatorio", Obbligatorio);
            db.SQLcommand();
        }

        public DataTable SelezionaTutto()
        {
            DB dB = new DB();
            dB.query = "PianiStudio_SelectAll";
            return dB.SQLselect();
        }

        public DataTable SelezionaChiavePianoStudio()
        {
            DB db = new DB();
            db.query = "PianiStudio_FindByKey";
            db.cmd.Parameters.AddWithValue("@chiave", K_PianoStudio);
            return db.SQLselect();
        }
        public DataTable SelezionaCorsoPianoStudio()
        {
            DB db = new DB();
            db.query = "PianiStudio_FindByCorso";
            db.cmd.Parameters.AddWithValue("@corso", K_Corso);
            return db.SQLselect();
        }

        public DataTable SelezionaCorsoAnnoPianoStudio()
        {
            DB db = new DB();
            db.query = "PianiStudio_FindByCorsoAnno";
            db.cmd.Parameters.AddWithValue("@corso", K_Corso);
            db.cmd.Parameters.AddWithValue("@annoaccademico", AnnoAccademico);
            return db.SQLselect();
        }

        public void Modifica()
        {
            DB dB = new DB();
            dB.query = "PianiStudio_Update";
            dB.cmd.Parameters.AddWithValue("@K_PianoStudio", K_PianoStudio);
            dB.cmd.Parameters.AddWithValue("@Annoaccademico", AnnoAccademico);
            dB.cmd.Parameters.AddWithValue("@Obbligatorio", Obbligatorio);
            dB.cmd.Parameters.AddWithValue("@K_Corso", Obbligatorio);
            dB.cmd.Parameters.AddWithValue("@K_Esame", Obbligatorio);
            dB.SQLcommand();
        }
    }
}
