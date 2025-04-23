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

        // Inserimento
        public void Inserimento()
        {
            DB db = new DB();
            db.query = "";
            db.cmd.Parameters.AddWithValue("",);
            db.SQLcommand();
        }

        // Seleziona tutti
        public DataTable SelezionaTutti()
        {
            DB dB = new DB();
            dB.query = "MARCHE_SelectAll";
            return dB.SQLselect();
        }

        // Seleziona per chiave
        public DataTable SelezionaChiave()
        {
            DB db = new DB();
            db.query = "MARCHE_SelezionaChiave";
            db.cmd.Parameters.AddWithValue("@chiave", K_Marca);
            return db.SQLselect();
        }

        // Modifica
        public void Modifica()
        {
            DB dB = new DB();
            dB.query = "MARCHE_Update";
            dB.cmd.Parameters.AddWithValue("@chiave", K_Marca);
            dB.cmd.Parameters.AddWithValue("@marca", Marca);
            dB.SQLcommand();
        }
    }
}
