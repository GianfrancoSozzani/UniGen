<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Pagamenti.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container">

        <h2 class="mb-4 ">I tuoi pagamenti in scadenza</h2>

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
        </div>

        <%--tabella pagamenti--%>

        <table class="table table-bordered mt-3">
            <thead class="thead-dark">
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

        <asp:Button ID="btnPaga" runat="server"
            Text="Paga"
            CommandArgument='<%# Eval("IDPagamento") %>'
            Visible='<%# Eval("StatoPagamento").ToString() == "Non Pagato" %>'
            CssClass="btn btn-primary btn-sm"
            OnClick="btnPaga_Click" />


        <asp:Label ID="lblMessaggio" runat="server" Visible="false" CssClass="alert mt-3" />

    </div>
     <div style="height: 18vh;">
     &nbsp
 </div>
    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.5.2/dist/umd/popper.min.js"></script>
</asp:Content>

