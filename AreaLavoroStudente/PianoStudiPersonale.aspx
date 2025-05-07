<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="PianoStudiPersonale.aspx.cs" Inherits="PianoStudiPersonale" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Piano di Studio Personale</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container mt-4 mb-5"> <!-- Aggiungi margine in basso per una distanza migliore -->
        <div class="row">
            <!-- COLONNA SINISTRA -->
            <div class="col-12 col-lg-8 mb-4">
                <h2 class="fw-bold mb-4">Piano Studio Personale</h2> <!-- Spazio sopra e sotto il titolo -->

                <!-- Messaggio dinamico -->
                <asp:Panel ID="pnlMessaggio" runat="server" Visible="false" CssClass="alert alert-dismissible fade show mb-3">
                    <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
                    <asp:Literal ID="litMessaggio" runat="server"></asp:Literal>
                </asp:Panel>

                <!-- Tabella piano di studio -->
                <div class="table-responsive mb-4"> <!-- Spazio tra la tabella e gli altri elementi -->
                    <asp:Repeater ID="rptPianoStudio" runat="server" OnItemCommand="rptPianoStudio_ItemCommand">
                        <HeaderTemplate>
                            <table class="table table-striped table-bordered align-middle">
                                <thead class="table-header">
                                    <tr>
                                        <th>Esame</th>
                                        <th>Anno Accademico</th>
                                        <th>Obbligatorio</th>
                                        <th>Azioni</th>
                                    </tr>
                                </thead>
                                <tbody>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td><%# Eval("TitoloEsame") %></td>
                                <td><%# Eval("AnnoAccademico") %></td>
                                <td><%# Eval("Obbligatorio").ToString() == "S" ? "Sì" : "No" %></td>
                                <td>
                                    <asp:Button ID="btnElimina" runat="server" Text="Elimina"
                                        CommandName="Elimina" CommandArgument='<%# Eval("K_PianoStudioPersonale") %>'
                                        OnClientClick="return confirm('Sei sicuro di voler eliminare questo esame dal tuo piano di studio?');"
                                        CssClass="btn btn-danger btn-sm" />
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </tbody>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                </div>

                <!-- Nessun dato -->
                <asp:Panel ID="pnlNessunDato" runat="server" CssClass="empty-data" Visible="false">
                    <p class="mb-3">Nessun esame presente nel tuo piano di studio.</p> <!-- Margine sotto il testo -->
                    <asp:Button ID="btnAggiungiPrimo" runat="server" Text="Aggiungi il primo esame"
                        OnClick="btnNuovoPiano_Click" CssClass="btn btn-success mt-2" />
                </asp:Panel>

                <!-- Pulsante aggiunta -->
                <div class="mt-4"> <!-- Spazio sopra il pulsante per un miglior allineamento -->
                    <asp:Button ID="btnNuovoPiano" runat="server" Text="Aggiungi Esame" CssClass="btn btn-primary" OnClick="btnNuovoPiano_Click" />
                </div>

                <!-- Spazio per eventuali messaggi -->
                <div class="mt-3" style="min-height: 2.5rem;">
                    <asp:Label ID="lblEsito" runat="server" CssClass="alert d-none"></asp:Label>
                </div>
            </div>

            <!-- COLONNA DESTRA -->
            <div class="col-12 col-lg-4 mt-5">
                <div class="card shadow-sm">
                    <div class="card-body">
                        <h3 class="fw-bold mb-3">Come funziona?</h3> <!-- Aggiungi un po' di spazio tra titolo e testo -->
                        <p class="text-muted fs-6">
                            Questa pagina mostra il tuo piano di studi personalizzato.<br />
                            <br />
                            Puoi aggiungere, modificare o rimuovere gli esami che intendi sostenere.
                        </p>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>