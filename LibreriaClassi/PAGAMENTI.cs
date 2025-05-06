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

        //PAGAMENTO NUOVO CHE SI INSERISCE DOPO AVER PAGATO IL PRIMO
        public void Inserimento()
        {
            DB db = new DB();
            db.query = "Pagamenti_Insert";
            db.cmd.Parameters.AddWithValue("@k_pagamento", K_Pagamento);
            db.cmd.Parameters.AddWithValue("@k_studente", K_Studente);
            db.cmd.Parameters.AddWithValue("@anno", Anno);
            db.cmd.Parameters.AddWithValue("@datapagamento", DataPagamento);
            db.cmd.Parameters.AddWithValue("@importo", Importo);
            db.cmd.Parameters.AddWithValue("@stato", Stato);
            db.SQLcommand();
        }

        public DataTable SelezionaTutto()
        {
            DB dB = new DB();
            dB.query = "Pagamenti_SelectAll";
            return dB.SQLselect();
        }

        public DataTable SelezionaChiave()
        {
            DB db = new DB();
            db.query = "Pagamenti_SelectKey";
            db.cmd.Parameters.AddWithValue("@chiave", K_Pagamento);
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

        //richiamo anno, importo, data
        public DataTable PagamentiDatiMancanti(Guid K_Pagamento)
        {
            DB db = new DB();
            db.query = "Pagamenti_DatiMancanti";
            db.cmd.Parameters.AddWithValue("@k_pagamento", K_Pagamento);
            return db.SQLselect();
        }

        public DataTable Pagamenti_VerificaPagamenti(int Matricola)
        {
            DB db = new DB();
            db.query = "Pagamenti_VerificaTasse";
            db.cmd.Parameters.AddWithValue("@matricola", Matricola);
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
            db.query = "Pagamenti_FindStimatiCorso";
            return db.SQLselect();
        }


    }
}
