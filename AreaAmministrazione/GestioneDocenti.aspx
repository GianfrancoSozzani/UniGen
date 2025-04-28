<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="GestioneDocenti.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h3>Elenco Docenti</h3>
    <asp:Literal ID="litCognome" runat="server">Cognome:</asp:Literal>
    <asp:TextBox ID="txtCognome" runat="server"></asp:TextBox>
    <br />
    <asp:Literal ID="litNome" runat="server">Nome:</asp:Literal>
    <asp:TextBox ID="txtNome" runat="server"></asp:TextBox>
    <br />
    <asp:Button ID="btnCerca" runat="server" Text="Cerca" OnClick="btnCerca_Click" />
    <br />
    <asp:Repeater ID="rpDocenti" runat="server">
        <HeaderTemplate>
            <div class="container mt-4">
                <table class="table table-striped">
                    <thead>
                        <tr>
                            <th>Foto Profilo</th>
                            <th>Cognome</th>
                            <th>Nome</th>
                            <th>Stato</th>
                            <th>Titolo Corso</th>
                            <th>Titolo Esame</th>
                            <th>Azioni</th>
                        </tr>
                    </thead>
                    <tbody>
        </HeaderTemplate>

        <ItemTemplate>
            <tr>
                <td><%# Eval("ImmagineProfilo") %></td>
                <td><%# Eval("Cognome") %></td>
                <td><%# Eval("Nome") %></td>
                <td>
                    <%# Eval("Abilitato").ToString() == "S" ? "Abilitato" : "Disattivato" %>
                    </td>
                <td>
                    <%# Eval("TitoloCorso") %>
                </td>
                <td>
                    <%# Eval("TitoloEsame") %>
                </td>
                <td>
                    <asp:Button ID="btnAbilita" runat="server" Text="Abilita" Visible='<%# Eval("Abilitato") != null && string.Equals(Eval("Abilitato").ToString(), "N", StringComparison.OrdinalIgnoreCase) %>' commandname="Abilita" CommandArgument='<%# Eval("K_Docente") %>' OnCommand="Selected_Command" />
                    <asp:Button ID="btnDisabilita" runat="server" Text="Disabilita" Visible='<%# Eval("Abilitato") != null && string.Equals(Eval("Abilitato").ToString(), "S", StringComparison.OrdinalIgnoreCase) %>' commandname="Disabilita" CommandArgument='<%# Eval("K_Docente") %>' OnCommand="Selected_Command" />
                </td>
            </tr>
        </ItemTemplate>

        <FooterTemplate>
            </tbody>
                </table>
                </div>
        </FooterTemplate>

    </asp:Repeater>
</asp:Content>

