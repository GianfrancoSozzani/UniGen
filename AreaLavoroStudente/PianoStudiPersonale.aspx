<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="PianoStudiPersonale.aspx.cs" Inherits="PianoStudiPersonale" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Piano di Studio Personale</title>
    <style type="text/css">
        .table-header {
            background-color: #f8f9fa;
        }

        .empty-data {
            padding: 20px;
            text-align: center;
            border: 1px dashed #dee2e6;
            border-radius: 5px;
            margin-bottom: 20px;
        }
        
        .btn.disabled {
            opacity: 0.65;
            cursor: not-allowed;
            pointer-events: none;
        }
        
        .modal-iframe {
            width: 100%;
            height: 500px;
            border: none;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container mt-2 mb-4">
        <!-- INTESTAZIONE -->
        <div class="py-4 border-bottom">
            <h2 class="text-start mb-1">Piano di Studio Personale</h2>
            <div class="d-flex text-start gap-2 flex-wrap fs-6 mb-2">
                <asp:Label ID="lblAnno" runat="server"></asp:Label>
                <span>-</span>
                <asp:Label ID="lblCorso" runat="server"></asp:Label>
                <span>-</span>
                <asp:Label ID="lblFacolta" runat="server"></asp:Label>
            </div>
        </div>

        <div class="row align-items-start mt-4">
            <!-- COLONNA PRINCIPALE -->
            <div class="col-12 col-lg-8 mb-4">
                <!-- MESSAGGI -->
                <asp:Panel ID="pnlMessaggio" runat="server" Visible="false" CssClass="alert alert-dismissible fade show mb-3">
                    <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
                    <asp:Literal ID="litMessaggio" runat="server"></asp:Literal>
                </asp:Panel>

                <!-- TABELLA -->
                <div class="table-responsive">
                    <asp:Repeater ID="rptPianoStudio" runat="server" OnItemCommand="rptPianoStudio_ItemCommand">
                        <HeaderTemplate>
                            <table class="table table-striped table-bordered align-middle">
                                <thead>
                                    <tr>
                                        <th class="fw-bold">Esame</th>
                                        <th class="fw-bold">Anno Accademico</th>
                                        <th class="fw-bold">Obbligatorio</th>
                                        <th class="fw-bold text-center">Azioni</th>
                                    </tr>
                                </thead>
                                <tbody>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td><%# Eval("TitoloEsame") %></td>
                                <td><%# Eval("AnnoAccademico") %></td>
                                <td><%# Eval("Obbligatorio").ToString() == "S" ? "Sì" : "No" %></td>
                                <td class="text-center">
                                    <asp:HiddenField ID="hfObbligatorio" runat="server" Value='<%# Eval("Obbligatorio") %>' />
                                    <asp:Button ID="btnElimina" runat="server" Text="Elimina"
                                        CommandName="Elimina" CommandArgument='<%# Eval("K_PianoStudioPersonale") %>'
                                        OnClientClick='<%# Eval("Obbligatorio").ToString() == "S" ? "alert(\"Non puoi eliminare un esame obbligatorio!\"); return false;" : "return confirm(\"Sei sicuro di voler eliminare questo esame dal tuo piano di studio?\")" %>'
                                        CssClass='<%# Eval("Obbligatorio").ToString() == "S" ? "btn btn-danger btn-sm disabled" : "btn btn-danger btn-sm" %>'
                                        Enabled='<%# Eval("Obbligatorio").ToString() != "S" %>' />
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </tbody>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                </div>

                <!-- NESSUN DATO -->
                <asp:Panel ID="pnlNessunDato" runat="server" CssClass="empty-data" Visible="false">
                    <p class="mb-3 fs-5">Nessun esame presente nel tuo piano di studio.</p>
                    <asp:Button ID="btnAggiungiPrimo" runat="server" Text="Aggiungi il primo esame"
                        OnClick="btnNuovoPiano_Click" CssClass="btn btn-success mt-2" />
                </asp:Panel>

                <!-- PULSANTE AGGIUNGI -->
                <div class="mt-3">
                    <asp:Button ID="btnNuovoPiano" runat="server" Text="Aggiungi Esame"
                        CssClass="btn btn-primary" onClick="btnNuovoPiano_Click" />
                </div>

                <!-- MESSAGGI -->
                <div class="mt-3" style="min-height: 2.5rem;">
                    <asp:Label ID="lblEsito" runat="server" CssClass="alert d-none"></asp:Label>
                </div>
            </div>

            <!-- COLONNA SECONDARIA -->
            <div class="col-12 col-lg-4 mb-4">
                <div class="card shadow-sm h-100">
                    <div class="card-body">
                        <h3 class="fw-bold mb-3">Come funziona?</h3>
                        <p class="text-muted fs-6">
                            Questa pagina mostra il tuo piano di studi personalizzato.<br />
                            <br />
                            Puoi aggiungere, modificare o rimuovere gli esami che intendi sostenere.<br />
                            <br />
                            Gli esami obbligatori sono indicati automaticamente dal sistema e non possono essere rimossi.
                        </p>
                    </div>
                </div>
            </div>
        </div>
    </div>




</asp:Content>