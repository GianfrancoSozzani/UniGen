<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="RinunciaStudi.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <!-- MODAL DI CONFERMA RINUNCIA -->
    <div class="modal fade" id="rinunciaModal" tabindex="-1" aria-labelledby="rinunciaModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="rinunciaModalLabel">Conferma rinuncia</h5>
                </div>
                <div class="modal-body">
                    Sei sicuro di voler rinunciare agli studi? Questa azione è irreversibile.
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btnConfermaRinuncia" runat="server" CssClass="btn btn-danger" OnClick="btnConfermaRinuncia_Click" Text="Conferma" />
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Annulla</button>
                </div>
            </div>
        </div>
    </div>

    <div class="container mt-2 mb-4">
        <div class="py-4 mb-4 border-bottom">
            <h2 class="text-start mb-1">Rinuncia agli studi</h2>
        </div>

        <div class="row">
            <!-- COLONNA SINISTRA -->
            <div class="col-12 col-lg-8 mb-4">
                <p>
                    Stai per inoltrare la <strong>richiesta di rinuncia agli studi</strong>. Questa operazione è <strong>irreversibile</strong> e comporta la disattivazione definitiva della tua carriera accademica.
                </p>

                <p>I tuoi dati:</p>
                <ul>
                    <li><strong>Matricola:</strong> <asp:Label ID="lblMatricola" runat="server" /></li>
                    <li><strong>Nome:</strong> <asp:Label ID="lblNome" runat="server" /></li>
                    <li><strong>Cognome:</strong> <asp:Label ID="lblCognome" runat="server" /></li>
                    <li><strong>Corso di Studi:</strong> <asp:Label ID="lblCorso" runat="server" /></li>
                    <li><strong>Data di richiesta:</strong> <asp:Label ID="lblDataRichiesta" runat="server" /></li>
                </ul>

                <p>
                    Confermando, il tuo profilo verrà <strong>disabilitato</strong> e non potrai più accedere ai servizi didattici online.
                </p>

                <button type="button" class="btn btn-danger" data-bs-toggle="modal" data-bs-target="#rinunciaModal">
                    Conferma
                </button>
            </div>

            <!-- COLONNA DESTRA -->
            <div class="col-12 col-lg-4">
                <div class="card shadow-sm">
                    <div class="card-body">
                        <h3 class="fw-bold mb-2">Come funziona?</h3>
                        <p class="text-muted fs-6">
                            Questa procedura consente di rinunciare definitivamente agli studi.
                            Una volta confermata, la tua carriera universitaria sarà interrotta e non potrai più accedere ai servizi riservati agli studenti.
                            <br /><br />
                            Assicurati di aver compreso le conseguenze prima di procedere.
                        </p>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div style="height: 20vh;">&nbsp;</div>
</asp:Content>
