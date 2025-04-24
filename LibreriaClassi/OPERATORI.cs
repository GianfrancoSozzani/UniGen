using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibreriaClassi
{
    internal class OPERATORI
    {
        public Guid K_Operatore { get; set; }
        public string USR { get; set; }
        public string PWD { get; set; }
        public string Cognome { get; set; }
        public string Nome { get; set; }


        public OPERATORI()
        {
            
        }

        public void Inserimento()
        {
            DB db = new DB();
            db.query = "";
            db.cmd.Parameters.AddWithValue("", USR);
            db.cmd.Parameters.AddWithValue("", PWD);
            db.cmd.Parameters.AddWithValue("", Cognome);
            db.cmd.Parameters.AddWithValue("", Nome);
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
            db.cmd.Parameters.AddWithValue("", K_Operatore);
            return db.SQLselect();
        }

        public void Modifica()
        {
            DB dB = new DB();
            dB.query = "";
            dB.cmd.Parameters.AddWithValue("", K_Operatore);
            dB.cmd.Parameters.AddWithValue("", USR);
            dB.cmd.Parameters.AddWithValue("", PWD);
            dB.cmd.Parameters.AddWithValue("", Cognome);
            dB.cmd.Parameters.AddWithValue("", Nome);
            dB.SQLcommand();
        }
    }
}

