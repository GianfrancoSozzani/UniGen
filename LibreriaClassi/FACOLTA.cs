using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LibreriaClassi
{
    public class FACOLTA
    {
        public Guid K_Facolta { get; set; }
        public string TitoloFacolta { get; set; }

        public FACOLTA()
        {
             
        }

        public void Inserimento()
        {
            DB db = new DB();
            db.query = "Facolta_Insert";
            db.cmd.Parameters.AddWithValue("@titolofacolta", TitoloFacolta);
            db.SQLcommand();
        }

        public DataTable SelezionaTutto()
        {
            DB dB = new DB();
            dB.query = "Facolta_SelectAll";
            return dB.SQLselect();
        }

        public DataTable SelezionaChiave()
        {
            DB db = new DB();
            db.query = "Facolta_SelectKey";
            db.cmd.Parameters.AddWithValue("@chiave", K_Facolta);
            return db.SQLselect();
        }

        public void Modifica()
        {
            DB dB = new DB();
            dB.query = "Facolta_Update";
            dB.cmd.Parameters.AddWithValue("@chiave", K_Facolta);
            dB.cmd.Parameters.AddWithValue("@titolofacolta", TitoloFacolta);
            dB.SQLcommand();
        }
    }
}
