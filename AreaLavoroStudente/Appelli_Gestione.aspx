<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Appelli_Gestione.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="container ">
        <h2 class="mb-4 text-center">Gestione Prenotazione Appelli</h2>

        <div class="container my-5">
            <div class="mt-1" style="margin-top: -50px !important;">
                <div class="d-flex text-start gap-2 my-3 flex-wrap  fs-6">
                    <asp:Label ID="lblAnno" runat="server"></asp:Label>
                    <span>-</span>
                    <asp:Label ID="lblCorso" runat="server"></asp:Label>
                    <span>-</span>
                    <asp:Label ID="lblFacolta" runat="server"></asp:Label>
                </div>
            </div>

            <table class="table table-bordered table-striped">
                <thead class="table-light">
                    <tr>
                        <th>Seleziona</th>
                        <th>Titolo Esame</th>
                        <th>Obbligatorio</th>
                        <th>Data Appello</th>
                        <th>Tipo</th>
                        <th>Link</th>
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

            <asp:Button ID="btnEliminaPrenotazione" runat="server" Text="Elimina" CssClass="btn btn-primary mt-3" OnClick="btnEliminaPrenotazione_Click" />

            <asp:Label ID="lblMessaggio" runat="server" CssClass="mt-3 alert d-none"></asp:Label>
        </div>
        <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.5.2/dist/umd/popper.min.js"></script>
</asp:Content>

