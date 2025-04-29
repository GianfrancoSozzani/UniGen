using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibreriaClassi
{
    /// <summary>
    /// Classe per la gestione delle prove
    /// </summary>
    public class PROVE
    {
        public Guid K_Prova { get; set; }
        public Guid K_Appello { get; set; }
        public string Link { get; set; }
        /// <summary>
        /// Domanda a risposta aperta o chiusa
        /// </summary>
        public string Tipologia { get; set; }


        //public void Inserimento()             //Gestito tramite MVC nell'area docenti
        //{
        //    DB dB = new DB();
        //    dB.query = "PROVE_Inserimento";
        //    dB.cmd.Parameters.AddWithValue("@K_Appello", K_Appello);
        //    dB.cmd.Parameters.AddWithValue("@Link", Link);
        //    dB.cmd.Parameters.AddWithValue("@Tipologia", Tipologia);
        //    dB.SQLcommand(); 
        //}
        //public void Modifica()
        //{
        //    DB dB = new DB();
        //    dB.query = "PROVE_Modifica";
        //    dB.cmd.Parameters.AddWithValue("@K_Prova", K_Prova);
        //    dB.cmd.Parameters.AddWithValue("@K_Appello", K_Appello);
        //    dB.cmd.Parameters.AddWithValue("@Link", Link);
        //    dB.cmd.Parameters.AddWithValue("@Tipologia", Tipologia);
        //    dB.SQLcommand();
        //}
        //public DataTable SelezionaTutto()
        //{
        //    DB dB = new DB();
        //    dB.query = "PROVE_SelezionaTutto";
        //    return dB.SQLselect();
        //}
        //public DataTable SelezionaChiave()
        //{
        //    DB dB = new DB();
        //    dB.query = "PROVE_SelezionaChiave";
        //    dB.cmd.Parameters.AddWithValue("@K_Prova", K_Prova);
        //    return dB.SQLselect();
        //}
    }
}
