using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LibreriaClassi
{
    internal class FACOLTA
    {
        public Guid K_Facolta { get; set; }
        public string TitoloFacolta { get; set; }

        public FACOLTA()
        {
             
        }

        public void Inserimento()
        {
            DB db = new DB();
            db.query = "";
            db.cmd.Parameters.AddWithValue("", TitoloFacolta);
            db.SQLcommand();
        }

        public DataTable SelezionaTutto()
        {
            DB dB = new DB();
            dB.query = "";
            return dB.SQLselect();
        }

        public DataTable SelezionaChiave()
        {
            DB db = new DB();
            db.query = "";
            db.cmd.Parameters.AddWithValue("", K_Facolta);
            return db.SQLselect();
        }

        public void Modifica()
        {
            DB dB = new DB();
            dB.query = "";
            dB.cmd.Parameters.AddWithValue("", K_Facolta);
            dB.cmd.Parameters.AddWithValue("", TitoloFacolta);
            dB.SQLcommand();
        }
    }
}
