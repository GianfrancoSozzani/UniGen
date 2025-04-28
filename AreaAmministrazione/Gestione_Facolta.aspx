<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Gestione_Facolta.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="container mt-5">
        <h1>Gestione Facoltà</h1>

        <div class="mb-4 text-end">
            <div id="icona">
                <asp:Label ID="Label2" runat="server" Text="Inserisci una nuova Facoltà" CssClass="fw-bold"></asp:Label>
                <i class="bi bi-plus-circle btn btn-primary"></i>
            </div>

            <div id="insert" runat="server" style="display: none;">
                <asp:TextBox ID="txtFacolta" runat="server"></asp:TextBox>
                <asp:Button ID="btnSalva" runat="server" Text="Inserisci" OnClick="btnSalva_Click" />
            </div>
        </div>

        <div>
            <asp:Repeater ID="rpFacolta" runat="server">
                <HeaderTemplate>
                    <table class="table table-hover shadow">
                        <thead>
                            <tr>
                                <th>Facoltà</th>
                                <th>Azioni</th>
                            </tr>
                        </thead>
                        <tbody>
                </HeaderTemplate>

                <ItemTemplate>
                    <tr>
                        <td><%# Eval("TitoloFacolta") %></td>
                        <td>
                            <a href='<%# "ModificaFacolta.aspx?id=" + Eval("K_Facolta") %>' class="btn btn-sm btn-primary">Modifica
                            </a>
                        </td>
                    </tr>
                </ItemTemplate>

                <FooterTemplate>
                    </tbody>
            </table>
                </FooterTemplate>
            </asp:Repeater>
        </div>
    </div>

    <%--Cliccando l'icona viene nascosta e gli elementi per l'inserimento diventano visibili--%>
    <script>
        document.addEventListener("DOMContentLoaded", function () {
            var btnMostra = document.getElementById("icona");
            var divInsert = document.getElementById("<%= insert.ClientID %>");

            btnMostra.addEventListener("click", function () {
                btnMostra.style.display = "none"; // Nascondo l'icona
                divInsert.style.display = "block"; // Mostro il div
            });
        });
    </script>
</asp:Content>

