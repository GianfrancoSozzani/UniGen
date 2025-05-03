<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Appelli_Gestione.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container">
        <div class="row">
            <!-- COLONNA SINISTRA: Contenuto -->
            <div class="col-12 col-lg-8 mb-4">
                <h2 class="fw-bold mb-4">Gestione Appelli</h2>

                <!-- Etichette dinamiche -->
                <div class="d-flex text-start gap-2 my-3 flex-wrap fs-6">
                    <asp:Label ID="lblAnno" runat="server"></asp:Label>
                    <span>-</span>
                    <asp:Label ID="lblCorso" runat="server"></asp:Label>
                    <span>-</span>
                    <asp:Label ID="lblFacolta" runat="server"></asp:Label>
                </div>

                <!-- Tabella appelli -->
                <div class="table-responsive">
                    <table class="table table-striped table-bordered align-middle">
                        <thead>
                            <tr>
                                <th class="fw-bold">Seleziona</th>
                                <th class="fw-bold">Titolo Esame</th>
                                <th class="fw-bold">Obbligatorio</th>
                                <th class="fw-bold">Data Appello</th>
                                <th class="fw-bold">Tipo</th>
                                <th class="fw-bold">Link</th>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:Repeater ID="rptAppelli" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <asp:CheckBox ID="chkSeleziona" runat="server" />
                                            <asp:HiddenField ID="hfKLibretto" runat="server" Value='<%# Eval("K_Libretto") %>' />
                                        </td>
                                        <td><%# Eval("TitoloEsame") %></td>
                                        <td><%# Eval("Obbligatorio").ToString().ToLower() == "true" ? "Obbligatorio" : "Facoltativo" %></td>
                                        <td><%# Eval("DataAppello", "{0:dd/MM/yyyy}") %></td>
                                        <td><%# Eval("Tipo") %></td>
                                        <td>
                                            <a href='<%# Eval("Link") %>' target="_blank" class="link-primary">Vai al link</a>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </tbody>
                    </table>
                </div>

                <!-- Pulsante Elimina -->
                <asp:Button ID="btnEliminaPrenotazione" runat="server" Text="Elimina" CssClass="btn btn-danger mt-3" OnClick="btnEliminaPrenotazione_Click" />

                <!-- Label messaggio -->
                <div class="mt-3" style="min-height: 2.5rem;">
                    <asp:Label ID="lblMessaggio" runat="server" CssClass="alert d-none"></asp:Label>
                </div>
            </div>

            <!-- COLONNA DESTRA: Card descrittiva -->
            <div class="col-12 col-lg-4 mt-5">
                <div class="card shadow-sm">
                    <div class="card-body">
                        <h3 class="fw-bold mb-2">Come funziona?</h3>
                        <p class="text-muted fs-6">
                            In questa pagina puoi gestire le tue prenotazioni agli appelli disponibili per gli esami del tuo corso.
                            <br><br>
                            Seleziona uno o più appelli per eliminarli, cliccando sul pulsante <strong>“Elimina”</strong>.
                        </p>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
