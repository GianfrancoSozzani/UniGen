using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace LibreriaClassi
{
    internal class PIANISTUDIOPERSONALI
    {
        public Guid K_PianoStudioPersonale { get; set; }
        public Guid K_Esame { get; set; }
        public Guid K_Studente { get; set; }
        public string AnnoAccademico { get; set; }
        /// <summary>
        /// è obbligatorio? (S) si (N) no
        /// </summary>
        public string Obbligatorio { get; set; }


        public PIANISTUDIOPERSONALI()
        {
            
        }

        public void Inserimento()
        {
            DB db = new DB();
            db.query = "";
            db.cmd.Parameters.AddWithValue("", AnnoAccademico);
            db.cmd.Parameters.AddWithValue("", Obbligatorio);
            db.SQLcommand();
        }

        public DataTable SelezionaTutto()
        {
            DB dB = new DB();
            dB.query = "";
            return dB.SQLselect();
        }

        public DataTable SelezionaChiavePianiStudioPersonale()
        {
            DB db = new DB();
            db.query = "";
            db.cmd.Parameters.AddWithValue("", K_PianoStudioPersonale);
            return db.SQLselect();
        }

        public void Modifica()
        {
            DB dB = new DB();
            dB.query = "";
            dB.cmd.Parameters.AddWithValue("", K_PianoStudioPersonale);
            dB.cmd.Parameters.AddWithValue("", AnnoAccademico);
            dB.cmd.Parameters.AddWithValue("", Obbligatorio);
            dB.SQLcommand();
        }
    }
}
