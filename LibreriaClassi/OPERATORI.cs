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
            db.query = "Operatori_Insert";
            db.cmd.Parameters.AddWithValue("@user", USR);
            db.cmd.Parameters.AddWithValue("@pwd", PWD);
            db.cmd.Parameters.AddWithValue("@cognome", Cognome);
            db.cmd.Parameters.AddWithValue("@nome", Nome);
            db.SQLcommand();
        }

        public DataTable SelezionaTutto()
        {
            DB dB = new DB();
            dB.query = "Operatori_SelectAll";
            return dB.SQLselect();
        }

        public DataTable SelezionaChiave()
        {
            DB db = new DB();
            db.query = "Operatori_FindByKey";
            db.cmd.Parameters.AddWithValue("@chiave", K_Operatore);
            return db.SQLselect();
        }

        public void Modifica()
        {
            DB dB = new DB();
            dB.query = "Operatori_Update";
            dB.cmd.Parameters.AddWithValue("@chiave", K_Operatore);
            dB.cmd.Parameters.AddWithValue("@user", USR);
            dB.cmd.Parameters.AddWithValue("@pwd", PWD);
            dB.cmd.Parameters.AddWithValue("@cognome", Cognome);
            dB.cmd.Parameters.AddWithValue("@nome", Nome);
            dB.SQLcommand();
        }
    }
}

