using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibreriaClassi
{
    internal class PIANISTUDIO
    {
        public Guid K_PianoStudio { get; set; }
        public Guid K_Corso { get; set; }
        public Guid K_Esame { get; set; }
        public string AnnoAccademico { get; set; }
        public string Obbligatorio { get; set; }


        public PIANISTUDIO()
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
    }
}
