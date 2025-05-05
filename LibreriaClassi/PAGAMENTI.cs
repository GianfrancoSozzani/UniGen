using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibreriaClassi
{
    public class PAGAMENTI
    {
        public Guid K_Pagamento { get; set; }
        public Guid K_Studente { get; set; }
        /// <summary>
        /// inteso come anno accademico corrente (es. 2024/2025)
        /// </summary>
        public string Anno { get; set; }
        public DateTime DataPagamento { get; set; }
        public decimal Importo { get; set; }
        /// <summary>
        /// Stato del pagamento: P=Pagato, N=Non pagato
        /// </summary>
        public char Stato { get; set; }


        public PAGAMENTI()
        {

        }

        public void Inserimento()
        {
            DB db = new DB();
            db.query = "";
            db.cmd.Parameters.AddWithValue("", Anno);
            db.cmd.Parameters.AddWithValue("", DataPagamento);
            db.cmd.Parameters.AddWithValue("", Importo);
            db.SQLcommand();
        }

        public DataTable SelezionaTutto()
        {
            DB dB = new DB();
            dB.query = "Pagamenti_SelectAll";
            return dB.SQLselect();
        }

        public DataTable SelezionaChiavePagamento()
        {
            DB db = new DB();
            db.query = "";
            db.cmd.Parameters.AddWithValue("", K_Pagamento);
            return db.SQLselect();
        }

        public void Modifica()
        {
            DB dB = new DB();
            dB.query = "";
            dB.cmd.Parameters.AddWithValue("", K_Pagamento);
            dB.cmd.Parameters.AddWithValue("", Anno);
            dB.cmd.Parameters.AddWithValue("", DataPagamento);
            dB.cmd.Parameters.AddWithValue("", Importo);
            dB.SQLcommand();
        }

        //lista pagamenti per lo studente in base alla matricola 
        public DataTable ListaPagamenti(int Matricola)
        {
            DB db = new DB();
            db.query = "Pagamenti_SelectMat";
            db.cmd.Parameters.AddWithValue("@Matricola", Matricola);
            return db.SQLselect();
        }

        //aggiorna stato pagamenti studente da N a S
        public void ModificaStatoPagamento()
        {
            DB db = new DB();
            db.query = "Pagamenti_AggiornaStato";
            db.cmd.Parameters.AddWithValue("@IDPagamento", K_Pagamento);
            db.SQLcommand();
        }

        //lista pagamenti effetuati dallo studente in base alla matricola
        public DataTable ListaPagamentiEffettuati(int Matricola)
        {
            DB db = new DB();
            db.query = "PagamentiEffettuati_SelectMat";
            db.cmd.Parameters.AddWithValue("@Matricola", Matricola);
             return db.SQLselect();
        }

        //---------------AGGIUNTA PER HOME AMMINISTRAZIONE
        public DataTable IncassoAnnoCorrente()
        {
            DB db = new DB();
            db.query = "Pagamenti_IncassoAnnoCorrente";
            return db.SQLselect();
        }

        public DataTable IncassiPerAnno()
        {
            DB db = new DB();
            db.cmd.Parameters.AddWithValue("@anno", Anno);
            db.query = "Pagamenti_SelectIncassoAnnuale";
            return db.SQLselect();
        }

        public DataTable IncassiGroupByAnno()
        {
            DB db = new DB();
            db.query = "Pagamenti_SelectGroupByAnno";
            return db.SQLselect();
        }

        public DataTable IncassiGroupByFacolta()
        {
            DB db = new DB();
            db.cmd.Parameters.AddWithValue("@anno", Anno);
            db.query = "Pagamenti_SelectGroupByFacolta";
            return db.SQLselect();
        }

        public DataTable IncassiPerCorso(Guid Facolta)
        {
            DB db = new DB();
            db.cmd.Parameters.AddWithValue("@anno", Anno);
            db.cmd.Parameters.AddWithValue("@facolta", Facolta);
            db.query = "Pagamenti_SelectGroupByCorso";
            return db.SQLselect();
        }

        public DataTable IncassiStimatiFacolta()
        {
            DB db = new DB();
            db.query = "Pagamenti_SelectStimatiFacolta";
            return db.SQLselect();
        }

        public DataTable IncassiStimatiCorso(Guid Facolta)
        {
            DB db = new DB();
            db.cmd.Parameters.AddWithValue("@facolta", Facolta);
            db.query = "Pagamenti_SelectStimatiCorso";
            return db.SQLselect();
        }
    }
}
