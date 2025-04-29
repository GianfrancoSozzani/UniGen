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

        public void Inserimento() //AGGIUNTA
        {
            DB db = new DB();
            db.query = "PianiStudio_Insert";
            //db.cmd.Parameters.AddWithValue("K_PianiStudio", Guid.NewGuid());
            db.cmd.Parameters.AddWithValue("K_Corso", K_Corso);
            db.cmd.Parameters.AddWithValue("K_Esame", K_Esame);
            db.cmd.Parameters.AddWithValue("AnnoAccademico", AnnoAccademico);
            db.cmd.Parameters.AddWithValue("Obbligatorio", Obbligatorio);
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
            db.query = "";
            db.cmd.Parameters.AddWithValue("", K_PianoStudio);
            return db.SQLselect();
        }

        public void Modifica()
        {
            DB dB = new DB();
            dB.query = "";
            dB.cmd.Parameters.AddWithValue("", K_PianoStudio);
            dB.cmd.Parameters.AddWithValue("", AnnoAccademico);
            dB.cmd.Parameters.AddWithValue("", Obbligatorio);
            dB.SQLcommand();
        }
        
        public DataTable MostraLista() //AGGIUNTA
        {
            DB dB = new DB();
            dB.query = "PianiStudio_List";
            return dB.SQLselect();
        }
        public DataTable MostraEsami(string idPiano) //AGGIUNTA
        {
            DB dB = new DB();
            dB.query = "PianiStudio_MostraEsami";
            return dB.SQLselect();
        }
        public DataTable Controllo() //AGGIUNTA
        {
            DB database = new DB();
            database.query = "PianiStudio_Controllo";
            database.cmd.Parameters.AddWithValue("@K_Corso", K_Corso );
            database.cmd.Parameters.AddWithValue("@K_Esame", K_Esame);
            database.cmd.Parameters.AddWithValue("@AnnoAccademico", AnnoAccademico);
            return database.SQLselect();

        }

        public DataTable ElencoEsamiPianoStudio() //AGGIUNTA
        {
            DB db = new DB();
            db.query = "PianiStudio_ElencoEsami";
            db.cmd.Parameters.AddWithValue("@K_PianoStudio", K_PianoStudio);
            return db.SQLselect();
        }
    }
}
