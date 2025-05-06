<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Pagamenti.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container mt-2 mb-4">

        <!-- TITOLO + ETICHETTE ACCADEMICHE -->
        <div class="py-4 mb-4 border-bottom">
            <h2 class="text-start mb-1">I tuoi pagamenti</h2>
            <div class="d-flex text-start gap-2 flex-wrap fs-6 mb-2">
                <asp:Label ID="lblAnno" runat="server"></asp:Label>
                <span>-</span>
                <asp:Label ID="lblCorso" runat="server"></asp:Label>
                <span>-</span>
                <asp:Label ID="lblFacolta" runat="server"></asp:Label>
            </div>
        </div>

        <div class="row">
            <!-- COLONNA SINISTRA -->
            <div class="col-12 col-lg-8 mb-4">
                <!-- TABELLA PAGAMENTI -->
                <div class="table-responsive">
                    <table class="table table-bordered table-striped align-middle">
                        <thead class="table-light">
                            <tr>
                                <th>Seleziona</th>
                                <th>Importo</th>
                                <th>Scadenza</th>
                                <th>Anno</th>
                                <th>Stato</th>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:Repeater ID="rptPagamenti" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <asp:CheckBox ID="chkSeleziona" runat="server" />
                                            <asp:HiddenField ID="hfKPagamento" runat="server" Value='<%# Eval("IDPagamento") %>' />
                                        </td>
                                        <td>€ <%# Eval("Importo", "{0:N2}") %></td>
                                        <td><%# Eval("Scadenza", "{0:dd/MM/yyyy}") %></td>
                                        <td><%# Eval("AnnoPagamento") %></td>
                                        <td><%# Eval("StatoPagamento") %></td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </tbody>
                    </table>
                </div>

                <!-- PULSANTE + MESSAGGIO -->
                <asp:Button ID="btnPaga" runat="server"
                    Text="Paga"
                    CommandArgument='<%# Eval("IDPagamento") %>'
                    Visible='<%# Eval("StatoPagamento").ToString() == "Non Pagato" %>'
                    CssClass="btn btn-primary btn-sm mt-3"
                    OnClick="btnPaga_Click" />

                <asp:Label ID="lblMessaggio" runat="server" Visible="false" CssClass="alert mt-3" />
            </div>

            <!-- COLONNA DESTRA -->
            <div class="col-12 col-lg-4">
                <div class="card shadow-sm">
                    <div class="card-body">
                        <h3 class="fw-bold mb-2">Come funziona?</h3>
                        <p class="text-muted fs-6">
                            In questa sezione puoi visualizzare e selezionare i pagamenti da effettuare per l’anno accademico selezionato.
                            <br /><br />
                            Dopo aver selezionato le voci desiderate, clicca su <strong>"Paga"</strong> per procedere.
                            Verifica sempre le scadenze indicate.
                        </p>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div style="height: 20vh;">&nbsp;</div>

    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.5.2/dist/umd/popper.min.js"></script>
</asp:Content>
