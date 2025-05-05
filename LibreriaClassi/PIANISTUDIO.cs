using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibreriaClassi
{
    public class PIANISTUDIO
    {
        public Guid K_PianoStudio { get; set; }
        public Guid K_Corso { get; set; }
        public Guid K_Esame { get; set; }
        /// <summary>
        /// inteso come anno accademico corrente (es. 2024/2025)
        /// </summary>
        public string AnnoAccademico { get; set; }
        /// <summary>
        /// è obbligatorio? (S) si (N) no
        /// </summary>
        public char Obbligatorio { get; set; }

        [ForeignKey ("K_Facolta")]
        public Guid K_Facolta { get; set; }
        [ForeignKey("K_TipoCorso")]
        public Guid K_TipoCorso { get; set; }

        public PIANISTUDIO()
        {

        }

        public void Inserimento()
        {
            DB db = new DB();
            db.query = "PianiStudio_Insert";
            db.cmd.Parameters.AddWithValue("@k_corso", K_Corso);
            db.cmd.Parameters.AddWithValue("@k_esame", K_Esame);
            db.cmd.Parameters.AddWithValue("@annoaccademico", AnnoAccademico);
            db.cmd.Parameters.AddWithValue("@obbligatorio", Obbligatorio);
            db.SQLcommand();
        }

        public DataTable SelezionaTutto()
        {
            DB dB = new DB();
            dB.query = "PianiStudio_SelectAll";
            return dB.SQLselect();
        }

        public DataTable SelezionaChiavePianoStudio()
        {
            DB db = new DB();
            db.query = "PianiStudio_FindByKey";
            db.cmd.Parameters.AddWithValue("@chiave", K_PianoStudio);
            return db.SQLselect();
        }
        public DataTable SelezionaCorsoPianoStudio()
        {
            DB db = new DB();
            db.query = "PianiStudio_FindByCorso";
            db.cmd.Parameters.AddWithValue("@corso", K_Corso);
            return db.SQLselect();
        }

        public DataTable SelezionaCorsoAnnoPianoStudio()
        {
            DB db = new DB();
            db.query = "PianiStudio_FindByCorsoAnno";
            db.cmd.Parameters.AddWithValue("@corso", K_Corso);
            db.cmd.Parameters.AddWithValue("@annoaccademico", AnnoAccademico);
            return db.SQLselect();
        }

        public void Modifica()
        {
            DB dB = new DB();
            dB.query = "PianiStudio_Update";
            dB.cmd.Parameters.AddWithValue("@K_PianoStudio", K_PianoStudio);
            dB.cmd.Parameters.AddWithValue("@Annoaccademico", AnnoAccademico);
            dB.cmd.Parameters.AddWithValue("@Obbligatorio", Obbligatorio);
            dB.cmd.Parameters.AddWithValue("@K_Corso", Obbligatorio);
            dB.cmd.Parameters.AddWithValue("@K_Esame", Obbligatorio);
            dB.SQLcommand();
        }

        public DataTable ElencoEsamiInseriti(Guid kFacolta, Guid kCorso, string tipoCorso, string annoAccademico)
        {
            DB db = new DB();
            db.query = "PianiStudio_SelectEsamiByFiltro";
            db.cmd.Parameters.AddWithValue("@K_PianoStudio", K_PianoStudio);
            db.cmd.Parameters.AddWithValue("@K_Facolta", kFacolta);
            db.cmd.Parameters.AddWithValue("@K_Corso", kCorso);
            db.cmd.Parameters.AddWithValue("@TipoCorso", tipoCorso);
            db.cmd.Parameters.AddWithValue("@AnnoAccademico", annoAccademico);
            return db.SQLselect();
        }
        public void CancellaEsameDalPiano()
        {
            DB db = new DB();
            db.query = "PianiStudio_DeleteEsame";
            db.cmd.Parameters.AddWithValue("@K_PianoStudio", K_PianoStudio);
            db.SQLcommand();
        }
        public void AggiungiEsami()
        {
            DB db = new DB();
            db.query = "PianiStudio_Insert";
            //db.cmd.Parameters.AddWithValue("K_PianiStudio", Guid.NewGuid());
            db.cmd.Parameters.AddWithValue("K_Corso", K_Corso);
            db.cmd.Parameters.AddWithValue("K_Esame", K_Esame);
            db.cmd.Parameters.AddWithValue("AnnoAccademico", AnnoAccademico);
            db.cmd.Parameters.AddWithValue("Obbligatorio", Obbligatorio);
            db.SQLcommand();
        }

        public DataTable CercaEsame(string termine)
        {
            DB database = new DB();
            database.query = "PianiStudio_SearchByEsame";
            database.cmd.Parameters.AddWithValue("@termine", termine);
            return database.SQLselect();

        }
        public DataTable ControlloEsami()
        {
            DB database = new DB();
            database.query = "PianiStudio_Controllo";
            database.cmd.Parameters.AddWithValue("@K_Facolta", this.K_Facolta);
            database.cmd.Parameters.AddWithValue("@K_Corso", this.K_Corso );
            database.cmd.Parameters.AddWithValue("@K_Esame", K_Esame);
            database.cmd.Parameters.AddWithValue("@K_TipoCorso", this.K_TipoCorso);
            database.cmd.Parameters.AddWithValue("@AnnoAccademico", this.AnnoAccademico);
            return database.SQLselect();

        }

    }
}
