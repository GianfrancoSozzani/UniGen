using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibreriaClassi
{
    public class STUDENTI
    {
        public Guid K_Studente { get; set; }
        public Guid K_Corso { get; set; }
        public string Email { get; set; }
        public string PWD { get; set; }
        public string Cognome { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascita { get; set; }
        public string Indirizzo { get; set; }
        public string CAP { get; set; }
        public string Citta { get; set; }
        public string Provincia { get; set; }
        public string ImmagineProfilo { get; set; }
        public string Tipo { get; set; }
        public int Matricola { get; set; }
        public DateTime DataImmatricolazione { get; set; }
        public char Abilitato { get; set; }


        public STUDENTI()
        {

        }

        public void Inserimento()
        {
            DB db = new DB();
            db.query = "";
            db.cmd.Parameters.AddWithValue("", Email);
            db.cmd.Parameters.AddWithValue("", PWD);
            db.cmd.Parameters.AddWithValue("", Cognome);
            db.cmd.Parameters.AddWithValue("", Nome);
            db.cmd.Parameters.AddWithValue("", DataNascita);
            db.cmd.Parameters.AddWithValue("", Indirizzo);
            db.cmd.Parameters.AddWithValue("", CAP);
            db.cmd.Parameters.AddWithValue("", Citta);
            db.cmd.Parameters.AddWithValue("", Provincia);
            db.cmd.Parameters.AddWithValue("", ImmagineProfilo);
            db.cmd.Parameters.AddWithValue("", Tipo);
            db.cmd.Parameters.AddWithValue("", Matricola);
            db.cmd.Parameters.AddWithValue("", DataImmatricolazione);
            db.cmd.Parameters.AddWithValue("", Abilitato);
            db.SQLcommand();
        }

        public DataTable SelezionaTutto()
        {
            DB dB = new DB();
            dB.query = "Studenti_SelectAll";
            return dB.SQLselect();
        }
        //---------------AGGIUNTA PER HOME AMMINISTRAZIONE
        public DataTable StudentiIscritti()
        {
            DB db = new DB();
            db.query = "Studenti_SelectImmatricolati";
            return db.SQLselect();
        }

        public DataTable SelezionaPerMatricola()
        {
            DB db = new DB();
            db.query = "Studenti_SelectByMatricola";
            db.cmd.Parameters.AddWithValue("@Matricola", Matricola);
            return db.SQLselect();
        }

        public DataTable SelezionaAnno()
        {
            DB db = new DB();
            db.query = "Studenti_CountAnni";
            return db.SQLselect();
        }

        public DataTable SelezionaPerCorso()
        {
            DB db = new DB();
            db.query = "Studenti_PerCorsi";
            return db.SQLselect();
        }

        public DataTable SelezionaAnnoSingolo(string anno)
        {
            DB db = new DB();
            db.query = "Studenti_CountAnnoSingolo";
            db.cmd.Parameters.AddWithValue("@Anno", anno);
            return db.SQLselect();
        }

        public DataTable SelezionaPerCorsoAnnoSingolo(string anno)
        {
            DB db = new DB();
            db.query = "Studenti_PerCorsiAnnoSingolo";
            db.cmd.Parameters.AddWithValue("@Anno", anno);
            return db.SQLselect();
        }

        public DataTable AttivaStudenteList()
        {
            DB db = new DB();
            db.query = "Studenti_AbilitaList";
            return db.SQLselect();
        }

        public DataTable SelezionaChiaveStudente()
        {
            DB db = new DB();
            db.query = "";
            db.cmd.Parameters.AddWithValue("", K_Studente);
            return db.SQLselect();
        }

        public DataTable Attiva()
        {
            DB db = new DB();
            db.query = "Studenti_Abilita";
            db.cmd.Parameters.AddWithValue("@matricola", Matricola);
            return db.SQLselect();
        }

        public DataTable Disattiva()
        {
            DB db = new DB();
            db.query = "Studenti_Disabilita";
            db.cmd.Parameters.AddWithValue("@matricola", Matricola);
            return db.SQLselect();
        }

        public void Modifica()
        {
            DB dB = new DB();
            dB.query = "";
            dB.cmd.Parameters.AddWithValue("", K_Studente);
            dB.cmd.Parameters.AddWithValue("", Email);
            dB.cmd.Parameters.AddWithValue("", PWD);
            dB.cmd.Parameters.AddWithValue("", Cognome);
            dB.cmd.Parameters.AddWithValue("", Nome);
            dB.cmd.Parameters.AddWithValue("", DataNascita);
            dB.cmd.Parameters.AddWithValue("", Indirizzo);
            dB.cmd.Parameters.AddWithValue("", CAP);
            dB.cmd.Parameters.AddWithValue("", Citta);
            dB.cmd.Parameters.AddWithValue("", Provincia);
            dB.cmd.Parameters.AddWithValue("", ImmagineProfilo);
            dB.cmd.Parameters.AddWithValue("", Tipo);
            dB.cmd.Parameters.AddWithValue("", Matricola);
            dB.cmd.Parameters.AddWithValue("", DataImmatricolazione);
            dB.cmd.Parameters.AddWithValue("", Abilitato);
            dB.SQLcommand();
        }


    }
}
