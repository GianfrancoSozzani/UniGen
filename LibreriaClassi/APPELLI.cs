using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LibreriaClassi
{
    /// <summary>
    /// Classe per la gestione degli appelli
    /// </summary>
    public class APPELLI
    {

        public Guid K_Appello { get; set; }
        public Guid K_Esame { get; set; }
        public DateTime DataAppello { get; set; }
        public DateTime DataVerbalizzazione { get; set; }
        /// <summary>
        /// Tipo di appello: O = Orale, S=Scritto, L=Laurea
        /// </summary>
        public char Tipo { get; set; }
        public string Link { get; set; }
        public DateTime DataOrale { get; set; }

        public void Inserimento()                                     //GESTIONE APPELLI TRAMITE API
        {
            DB dB = new DB();
            dB.query = "APPELLI_Inserimento";
            dB.cmd.Parameters.AddWithValue("@K_Esame", K_Esame);
            dB.cmd.Parameters.AddWithValue("@DataAppello", DataAppello);
            dB.cmd.Parameters.AddWithValue("@DataVerbalizzazione", DataVerbalizzazione);
            dB.cmd.Parameters.AddWithValue("@Tipo", Tipo);
            dB.cmd.Parameters.AddWithValue("@Link", Link);
            dB.SQLcommand();
        }
        public void Modifica()
        {
            DB dB = new DB();
            dB.query = "APPELLI_Modifica";
            dB.cmd.Parameters.AddWithValue("@K_Appello", K_Appello);
            dB.cmd.Parameters.AddWithValue("@K_Esame", K_Esame);
            dB.cmd.Parameters.AddWithValue("@DataAppello", DataAppello);
            dB.cmd.Parameters.AddWithValue("@DataVerbalizzazione", DataVerbalizzazione);
            dB.cmd.Parameters.AddWithValue("@Tipo", Tipo);
            dB.cmd.Parameters.AddWithValue("@Link", Link);
            dB.SQLcommand();
        }
        public DataTable SelezionaTutto()
        {
            DB dB = new DB();
            dB.query = "APPELLI_SelezionaTutto";
            return dB.SQLselect();
        }
        public DataTable SelezionaChiave()
        {
            DB dB = new DB();
            dB.query = "APPELLI_SelezionaChiave";
            dB.cmd.Parameters.AddWithValue("@K_Appello", K_Appello);
            return dB.SQLselect();
        }
        


        //lista appelli 
        public DataTable ListaAppelli(Guid K_Studente)
        {
            
            DB db = new DB();
            db.query = "Appelli_SelectMat";
            db.cmd.Parameters.AddWithValue("@k_studente", K_Studente);
            return db.SQLselect();
        }
        public DataTable CaricaEsamiMeseCorrente()
        {
            DB dB = new DB();
            dB.query = "APPELLI_CountEsamiMeseCorrente";
            return dB.SQLselect();
        }

    }
}
