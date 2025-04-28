<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Gestione_Facolta.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:Repeater ID="rpFacolta" runat="server">
        <HeaderTemplate>
            <div class="container mt-4">
                <table class="table">
                        <tr>
                            <th>Facoltà</th>
                            <th>Azioni</th>
                        </tr>
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
        </div>
        </FooterTemplate>
    </asp:Repeater>

    <a href ="InserimentoFacolta.aspx">INSERIMENTO</a>

</asp:Content>

