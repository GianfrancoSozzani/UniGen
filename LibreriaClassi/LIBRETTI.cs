using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibreriaClassi
{
    public class LIBRETTI
    {
        public Guid K_Libretto { get; set; }
        public Guid K_Studente { get; set; }
        public Guid K_Appello { get; set; }
        public int VotoEsame { get; set; }
        /// <summary>
        /// l'esito è positivo o negativo? (N) negativo, (P) positivo
        /// </summary>
        public string Esito { get; set; }


        public LIBRETTI()
        {

        }

        public void Inserimento()
        {
            DB db = new DB();
            db.query = "";
            db.cmd.Parameters.AddWithValue("", VotoEsame);
            db.cmd.Parameters.AddWithValue("", Esito);
            db.SQLcommand();
        }

        public DataTable SelezionaTutto()
        {
            DB dB = new DB();
            dB.query = "Libretti_SelectAll";
            return dB.SQLselect();
        }

        public DataTable SelezionaChiaveLibretti()
        {
            DB db = new DB();
            db.query = "";
            db.cmd.Parameters.AddWithValue("", K_Libretto);
            return db.SQLselect();
        }

        public void Modifica()
        {
            DB dB = new DB();
            dB.query = "";
            dB.cmd.Parameters.AddWithValue("", K_Libretto);
            dB.cmd.Parameters.AddWithValue("", VotoEsame);
            dB.cmd.Parameters.AddWithValue("", Esito);
            dB.SQLcommand();
        }
    }
}
