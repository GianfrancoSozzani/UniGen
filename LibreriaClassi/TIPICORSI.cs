using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibreriaClassi
{
    internal class TIPICORSI
    {
        public Guid K_TipoCorso { get; set; }
        public string Tipo { get; set; }
        public string Durata { get; set; }

        public TIPICORSI()
        {

        }

        public void Inserimento()
        {
            DB db = new DB();
            db.query = "";
            db.cmd.Parameters.AddWithValue("", Tipo);
            db.cmd.Parameters.AddWithValue("", Durata);
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
            db.cmd.Parameters.AddWithValue("", K_TipoCorso);
            return db.SQLselect();
        }

        public void Modifica()
        {
            DB dB = new DB();
            dB.query = "";
            dB.cmd.Parameters.AddWithValue("", K_TipoCorso);
            dB.cmd.Parameters.AddWithValue("", Tipo);
            dB.cmd.Parameters.AddWithValue("", Durata);
            dB.SQLcommand();
        }
    }
}
