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
        public short MinimoCFU { get; set; }
        public decimal CostoAnnuale { get; set; }

        public CORSI()
        {

        }

        public void Inserimento()
        {
            DB db = new DB();
            db.query = "Corsi_Insert";
            db.cmd.Parameters.AddWithValue("@titolocorso", TitoloCorso);
            db.cmd.Parameters.AddWithValue("@minimoCFU", MinimoCFU);
            db.cmd.Parameters.AddWithValue("@costoannuale", CostoAnnuale);
            db.cmd.Parameters.AddWithValue("@k_facolta", K_Facolta);
            db.cmd.Parameters.AddWithValue("@k_tipocorso", K_TipoCorso);
            db.SQLcommand();
        }

        public DataTable SelezionaTutto()
        {
            DB dB = new DB();
            dB.query = "Corsi_SelectAll";
            dB.query = "Corsi_SelectAll";
            return dB.SQLselect();
        }

        public DataTable VerificaDuplicato()
        {
            DB dB = new DB();
            dB.query = "Corsi_VerificaDuplicato";
            dB.cmd.Parameters.AddWithValue("@k_facolta", K_Facolta);
            dB.cmd.Parameters.AddWithValue("@titolocorso", TitoloCorso);
            dB.cmd.Parameters.AddWithValue("@k_tipocorso", K_TipoCorso);
            return dB.SQLselect();
        }

        public DataTable VerificaDuplicatoModifica()
        {
            DB dB = new DB();
            dB.query = "Corsi_VerificaDuplicatoModifica";
            dB.cmd.Parameters.AddWithValue("@k_corso", K_Corso);
            dB.cmd.Parameters.AddWithValue("@k_facolta", K_Facolta);
            dB.cmd.Parameters.AddWithValue("@titolocorso", TitoloCorso);
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
            dB.query = "Corsi_Update";
            dB.cmd.Parameters.AddWithValue("@k_corso", K_Corso);
            dB.cmd.Parameters.AddWithValue("@titolocorso", TitoloCorso);
            dB.cmd.Parameters.AddWithValue("@k_facolta", K_Facolta);
            dB.cmd.Parameters.AddWithValue("@k_tipocorso", K_TipoCorso);
            dB.cmd.Parameters.AddWithValue("@minimoCFU", MinimoCFU);
            dB.cmd.Parameters.AddWithValue("@costoannuale", CostoAnnuale);
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
