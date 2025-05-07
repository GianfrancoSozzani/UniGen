using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace LibreriaClassi
{
    public class PIANISTUDIOPERSONALI
    {
        public Guid K_PianoStudioPersonale { get; set; }
        public Guid K_Esame { get; set; }
        public Guid K_Studente { get; set; }
        /// <summary>
        /// inteso come anno accademico corrente (es. 2024/2025)
        /// </summary>
        public string AnnoAccademico { get; set; }
        /// <summary>
        /// è obbligatorio? (S) si (N) no
        /// </summary>
        public char Obbligatorio { get; set; }


        public PIANISTUDIOPERSONALI()
        {
            
        }

        public void Inserimento()
        {
            DB db = new DB();
            db.query = "PianoStudioPersonale_Insert";
            db.cmd.Parameters.AddWithValue("@K_PianoStudioPersonale", K_PianoStudioPersonale);
            db.cmd.Parameters.AddWithValue("@AnnoAccademico", AnnoAccademico);
            db.cmd.Parameters.AddWithValue("@K_Esame", K_Esame);
            db.cmd.Parameters.AddWithValue("@K_Studente", K_Studente);
            db.SQLcommand();
        }

        //public DataTable SelezionaTutto()
        //{
        //    DB dB = new DB();
        //    dB.query = "";
        //    return dB.SQLselect();
        //}

        public DataTable SelezionaChiavePianiStudioPersonale()
        {
            DB db = new DB();
            db.query = "PianoStudioPersonale_FindByKey";
            db.cmd.Parameters.AddWithValue("@chiave", K_PianoStudioPersonale);
            return db.SQLselect();
        }

        public DataTable SelezionaStudentePianiStudioPersonale(Guid K_Studente)
        {
            DB db = new DB();
            db.query = "PianoStudioPersonale_FindByStudente";
            db.cmd.Parameters.AddWithValue("@K_Studente", K_Studente);
            return db.SQLselect();
        }

        public void Modifica()
        {
            DB db = new DB();
            db.query = "PianoStudioPersonale_Update";
            db.cmd.Parameters.AddWithValue("@chiave", K_PianoStudioPersonale);
            db.cmd.Parameters.AddWithValue("@annoaccademico", AnnoAccademico);
            db.cmd.Parameters.AddWithValue("@k_esame", K_Esame);
            db.cmd.Parameters.AddWithValue("@k_studente", K_Studente);
            db.SQLcommand();
        }

        public void Elimina()
        {
            DB db = new DB();
            db.query = "PianoStudioPersonale_Delete";
            db.cmd.Parameters.AddWithValue("@K_PianoStudioPersonale", K_PianoStudioPersonale);
            db.SQLcommand();
        }

        public DataTable GetEsamiDisponibili(Guid K_Studente)
        {
            DB db = new DB();
            db.query = "Esami_SelectDisponibili";
            db.cmd.Parameters.AddWithValue("@K_Studente", K_Studente);
            return db.SQLselect();
        }

    }

}
