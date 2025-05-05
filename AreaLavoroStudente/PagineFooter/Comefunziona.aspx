<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Comefunziona.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container">
        <div class="row">
            <div class="col-12 col-lg-8">
                <div class="mt-5">
                    <h1 class="text-start">Come funziona il pannello studente?</h1>

                    <ol start="1">
                        <li>
                            <p><strong>Navbar (Barra di navigazione in alto)</strong></p>
                            <ul>
                                <li>
                                    <p><strong>Logo e Home:</strong> In alto a sinistra trovi il logo; cliccandolo verrai reindirizzato alla home page del pannello studente, da cui iniziare ogni attività.</p>
                                </li>
                                <li>
                                    <p><strong>Menu Principale:</strong> Contiene diverse voci suddivise per area:</p>
                                    <ul>
                                        <li>
                                            <p><strong>Home:</strong> Collegamento diretto alla pagina iniziale.</p>
                                        </li>
                                        <li>
                                            <p><strong>Didattica:</strong></p>
                                            <ul>
                                                <li><strong>Materiali:</strong> Area dove puoi visualizzare e scaricare i materiali didattici.</li>
                                                <li><strong>Lezioni:</strong> Sezione per consultare l’orario delle lezioni o accedere a registrazioni (se disponibili).</li>
                                            </ul>
                                        </li>
                                        <li>
                                            <p><strong>Carriera:</strong></p>
                                            <ul>
                                                <li><strong>Piano di studi:</strong> Consulta o modifica il tuo piano formativo nei periodi abilitati.</li>
                                                <li><strong>Voti:</strong> Visualizza gli esiti degli esami sostenuti.</li>
                                            </ul>
                                        </li>
                                        <li>
                                            <p><strong>Esami:</strong></p>
                                            <ul>
                                                <li><strong>Appelli:</strong> Prenotati agli appelli disponibili.</li>
                                            </ul>
                                        </li>
                                        <li>
                                            <p><strong>Amministrazione:</strong></p>
                                            <ul>
                                                <li><strong>Pagamenti:</strong> Verifica le tasse da pagare, salda online o stampa ricevute.</li>
                                                <li><strong>Rinuncia agli studi:</strong> Accedi al modulo per rinunciare al corso di laurea, se necessario.</li>
                                            </ul>
                                        </li>
                                    </ul>
                                </li>
                            </ul>
                        </li>
                    </ol>

                    <ol start="2">
                        <li>
                            <p><strong>Footer (Piè di pagina)</strong></p>
                            <ul>
                                <li>
                                    <p><strong>Sezione "Chi Siamo":</strong> Contiene link informativi e utili:</p>
                                    <ul>
                                        <li><strong>Contatti</strong></li>
                                        <li><strong>Come Funziona</strong></li>
                                        <li><strong>FAQ</strong></li>
                                        <li><strong>Privacy</strong></li>
                                    </ul>
                                    <p>Questi collegamenti aiutano a orientarsi nella piattaforma e a ricevere supporto.</p>
                                </li>
                            </ul>
                        </li>
                    </ol>

                    <ol start="3">
                        <li>
                            <p><strong>Home Cards (Funzionalità rapide nella homepage)</strong></p>
                            <ul>
                                <li>
                                    <p><strong>Accesso diretto alle funzioni principali:</strong> Sulla home trovi una serie di card che rappresentano le aree fondamentali per la tua carriera universitaria:</p>
                                    <ul>
                                        <li><strong>Materiali:</strong> Scarica i documenti e le slide messi a disposizione dai docenti.</li>
                                        <li><strong>Lezioni:</strong> Consulta orari o registra presenza alle lezioni (se richiesto).</li>
                                        <li><strong>Appelli:</strong> Prenotati facilmente agli esami.</li>
                                        <li><strong>Voti:</strong> Visualizza rapidamente i risultati degli esami.</li>
                                        <li><strong>Piano di studi:</strong> Accedi alla struttura del tuo percorso formativo.</li>
                                        <li><strong>Pagamenti:</strong> Gestisci in modo semplice le tasse universitarie.</li>
                                        <li><strong>Rinuncia:</strong> In caso di ritiro, accedi alla procedura ufficiale.</li>
                                    </ul>
                                </li>
                                <li>
                                    <p><strong>Esperienza semplice e organizzata:</strong> Le card rendono più veloce l’accesso alle sezioni principali, con un’interfaccia intuitiva pensata per studenti.</p>
                                </li>
                            </ul>
                        </li>
                    </ol>
                </div>
            </div>
        </div>
    </div>
    <div style="height: 5vh;">
        &nbsp
    </div>
</asp:Content>

