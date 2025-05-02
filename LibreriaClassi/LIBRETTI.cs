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
        public int Matricola { get; set; }
        /// <summary>
        /// l'esito è positivo o negativo? (N) negativo, (P) positivo
        /// </summary>
        public string Esito { get; set; }


        public LIBRETTI()
        {

        }

        public void Inserimento()           //Inserimento nuovo libretto
        {
            DB db = new DB();
            db.query = "Libretti_Insert";
            //db.cmd.Parameters.AddWithValue("@voto", VotoEsame);
            //db.cmd.Parameters.AddWithValue("@esito", Esito);
            db.cmd.Parameters.AddWithValue("@k_studente", K_Studente);
            db.cmd.Parameters.AddWithValue("@k_appello", K_Appello);
            db.SQLcommand();
        }

        public DataTable SelezionaTutto()       //Libretti SelectAll
        {
            DB dB = new DB();
            dB.query = "Libretti_SelectAll";
            return dB.SQLselect();
        }

        public DataTable SelezionaChiaveLibretti()      //Seleziona tramite chiave
        {
            DB db = new DB();
            db.query = "Libretti_FindByChiave";
            db.cmd.Parameters.AddWithValue("@chiave", K_Libretto);
            return db.SQLselect();
        }

        public void Modifica()          //Modfiica di un libretto
        {
            DB dB = new DB();
            dB.query = "Libretti_Update";
            dB.cmd.Parameters.AddWithValue("@chiave", K_Libretto);
            dB.cmd.Parameters.AddWithValue("@voto", VotoEsame);
            dB.cmd.Parameters.AddWithValue("@k_appello", K_Appello);
            dB.cmd.Parameters.AddWithValue("@esito", Esito);
            dB.cmd.Parameters.AddWithValue("@k_studente", K_Studente);
            dB.SQLcommand();
        }

        public DataTable SelezionaCognomeNome(string Cognome, string Nome)      //Seleziona tramite Cognome e Nome
        {
            DB db = new DB();
            db.query = "Libretti_FindByCognomeNome";
            db.cmd.Parameters.AddWithValue("@Cognome", Cognome);
            db.cmd.Parameters.AddWithValue("@Nome", Nome);
            return db.SQLselect();
        }

        public DataTable SelezionaMatricola(int Matricola)      //Seleziona tramite matricola
        {
            DB db = new DB();
            db.query = "Libretti_FindByMatricola";
            db.cmd.Parameters.AddWithValue("@Matricola", Matricola);
            return db.SQLselect();
        }


        public DataTable SelezionaMedia(int Matricola)
        {
            DB db = new DB();
            db.query = "Libretti_MediaVotiByMatricola";
               db.cmd.Parameters.AddWithValue("@Matricola", Matricola);
            return db.SQLselect();
        }

        //prenotazione appelli 
        public void PrenotazioneAppelli()
        {
            DB db = new DB();
            db.query = "Prenotazione_Insert";
            db.cmd.Parameters.AddWithValue("@k_studente", K_Studente);
            db.cmd.Parameters.AddWithValue("@k_appello", K_Appello);
            db.SQLcommand();
        }

        //lista prenotazioni appelli 
        public DataTable ListaPrenotazioni()
        {
            
            DB db = new DB();
            db.query = "Appelli_Prenotati";
            db.cmd.Parameters.AddWithValue("@Matricola", Matricola);
            return db.SQLselect();
        }


        public DataTable SelezionaTOTCFU(int Matricola)
        {

            DB db = new DB();
            db.query = "Libretti_TotCFUByMatricola";
            db.cmd.Parameters.AddWithValue("@Matricola", Matricola);
            return db.SQLselect();
        }

        public DataTable SelezionaEsami(int Matricola)
        {
            DB db = new DB();
            db.query = "Libretti_EsamiByMatricola";
            db.cmd.Parameters.AddWithValue("@Matricola", Matricola);
            return db.SQLselect();


        }
        public DataTable SelezionaCFU(int Matricola)
        {
            DB db = new DB();
            db.query = "Libretti_TotCFUByMatricola";
            db.cmd.Parameters.AddWithValue("@Matricola", Matricola);
            return db.SQLselect();


        }


        //eliminazione prenotazione appelli 
        public void EliminazioneAppelli()
        {
            DB db = new DB();
            db.query = "Prenotazione_Delete";
            db.cmd.Parameters.AddWithValue("@k_libretto", K_Libretto);
            db.SQLcommand();
        }

        //controllo prenotazioni doppioni
        public DataTable ControlloDoppioni( )
        {
            DB db = new DB();
            db.query = "Prenotazione_Duplicati";
            db.cmd.Parameters.AddWithValue("@k_studente", K_Studente); //abbiamo cambiato matricola con studente 
            db.cmd.Parameters.AddWithValue("@k_appello", K_Appello);
            return db.SQLselect();
        }

        //recupera K_Studente
        public DataTable RecuperaKStudenteDaMatricola(int matricola)
        {
            DB db = new DB();
            db.query = "Recupera_KStudente";
            db.cmd.Parameters.AddWithValue("@matricola", matricola);
            return db.SQLselect();
        }

    }



















}
