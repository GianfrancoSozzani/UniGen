using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibreriaClassi
{
    internal class PAGAMENTI
    {
        public Guid K_Pagamento { get; set; }
        public Guid K_Studente { get; set; }
        /// <summary>
        /// inteso come anno accademico corrente (es. 2024/2025)
        /// </summary>
        public string Anno { get; set; }
        public DateTime DataPagamento { get; set; }
        public decimal Importo { get; set; }


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
            dB.query = "";
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
    }
}
