<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Appelli_Prenotazione.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.5/dist/css/bootstrap.min.css" rel="stylesheet" crossorigin="anonymous" />

    <div class="container mt-2 mb-4">
        <!-- TITOLO E ETICHETTE -->
        <div class="py-4 mb-4 border-bottom">
            <h2 class="text-start mb-1">Prenotazione Appelli</h2>
            <div class="d-flex text-start gap-2 flex-wrap fs-6 mb-2">
                <asp:Label ID="lblAnno" runat="server"></asp:Label>
                <span>-</span>
                <asp:Label ID="lblCorso" runat="server"></asp:Label>
                <span>-</span>
                <asp:Label ID="lblFacolta" runat="server"></asp:Label>
            </div>

        </div>

        <!-- ROW con tabella e card allineate -->
        <div class="row align-items-start">
            <!-- COLONNA SINISTRA: Tabella -->
            <div class="col-12 col-lg-8 mb-4">
                <div class="table-responsive">
                    <table class="table table-striped table-bordered align-middle">
                        <thead>
                            <tr>
                                <th class="fw-bold">Seleziona</th>
                                <th class="fw-bold">Titolo Esame</th>
                                <th class="fw-bold">Obbligatorio</th>
                                <th class="fw-bold">Data Appello</th>
                                <th class="fw-bold">Tipo</th>
                                <%--<th class="fw-bold">Link</th>--%>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:Repeater ID="rptAppelli" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <asp:CheckBox ID="chkSeleziona" runat="server" />
                                            <asp:HiddenField ID="hfKAppello" runat="server" Value='<%# Eval("K_Appello") %>' />
                                        </td>
                                        <td><%# Eval("TitoloEsame") %></td>
                                        <td><%# Eval("Obbligatorio").ToString().ToLower() == "true" ? "Obbligatorio" : "Facoltativo" %></td>
                                        <td><%# Eval("DataAppello", "{0:dd/MM/yyyy}") %></td>
                                        <td><%# Eval("Tipo") %></td>
                                       <%-- <td>
                                            <a href='<%# Eval("Link") %>' target="_blank" class="link-primary">Vai al link</a>
                                        </td>--%>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </tbody>
                    </table>
                </div>

                <!-- Bottone Prenota -->
                <asp:Button ID="btnPrenotaSelezionati" runat="server" Text="Prenota" CssClass="btn btn-primary mt-3"
                    CommandName="prenota"
                    CommandArgument='<%# Eval("K_Studente") %>'
                    OnClick="btnPrenotaSelezionati_Click" />

                <!-- Messaggio -->
                <div class="mt-3" style="min-height: 2.5rem;">
                    <asp:Label ID="lblMessaggio" runat="server" CssClass="alert d-none"></asp:Label>
                </div>
            </div>

            <!-- COLONNA DESTRA: Card -->
            <div class="col-12 col-lg-4 mb-4">
                <div class="card shadow-sm h-100">
                    <div class="card-body">
                        <h3 class="fw-bold mb-2">Come funziona?</h3>
                        <p class="text-muted fs-6">
                            Questa pagina ti consente di prenotarti agli appelli disponibili per gli esami del tuo piano di studi.
                            <br />
                            <br />
                            Seleziona uno o più appelli dalla tabella e clicca sul pulsante <strong>“Prenota”</strong> per completare l’operazione.
                            Ogni appello potrebbe avere un link utile per informazioni aggiuntive.
                        </p>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div style="height: 15vh;">&nbsp;</div>

    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.5.2/dist/umd/popper.min.js"></script>
</asp:Content>
