using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibreriaClassi
{
    public class CORSI
    {
        public Guid K_Corso { get; set; }
        public Guid K_Facolta { get; set; }
        public Guid K_TipoCorso { get; set; }
        public string TitoloCorso { get; set; }
        public string MinimoCFU { get; set; }
        public decimal CostoAnnuale { get; set; }

        public CORSI()
        {

        }

        public void Inserimento()
        {
            DB db = new DB();
            db.query = "";
            db.cmd.Parameters.AddWithValue("", TitoloCorso);
            db.cmd.Parameters.AddWithValue("", MinimoCFU);
            db.cmd.Parameters.AddWithValue("", CostoAnnuale);
            db.SQLcommand();
        }

        public DataTable SelezionaTutto()
        {
            DB dB = new DB();
            dB.query = "Corsi_SelectAll";
            return dB.SQLselect();
        }

        public DataTable SelezionaChiaveCorso()
        {
            DB db = new DB();
            db.query = "";
            db.cmd.Parameters.AddWithValue("", K_Corso);
            return db.SQLselect();
        }

        public void Modifica()
        {
            DB dB = new DB();
            dB.query = "";
            dB.cmd.Parameters.AddWithValue("", K_Corso);
            dB.cmd.Parameters.AddWithValue("", TitoloCorso);
            dB.cmd.Parameters.AddWithValue("", MinimoCFU);
            dB.cmd.Parameters.AddWithValue("", CostoAnnuale);
            dB.SQLcommand();
        }
        //---------------AGGIUNTE PER HOME AMMINISTRAZIONE
        public DataTable CorsiAttivi()
        {
            DB db = new DB();
            db.query = "Corsi_CountCorsi";
            return db.SQLselect();
        }
      
        public DataTable TassaMediaAnnuale()
        {
            DB db = new DB();
            db.query = "Corsi_TassaMediaAnn";
            return db.SQLselect();
        }

    }
}
