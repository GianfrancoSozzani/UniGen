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
        <asp:Button ID="btnCerca" runat="server" Text="Cerca" OnClick="btnCerca_Click"/>
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
                    <td><%# Eval("Cognome") %></td>
                    <td><%# Eval("Nome") %></td>
                    <td>
                        <%# Eval("Abilitato").ToString() == "S" ? "Abilitato" : "Disattivato" %>
                    </td>

                <input type="submit" ID="btnAbilita" runat="server" Text="Abilita" Visible='<%# Eval("Abilitato") != null && string.Equals(Eval("Abilitato").ToString(), "S", StringComparison.OrdinalIgnoreCase) %>' CommandName="Abilita" CommandArgument='<%# Eval("K_Docente") %>' OnCommand="Selected_Command" />
                <input type="submit" ID="btnDisabilita" runat="server" Text="Disabilita" Visible='<%# Eval("Abilitato") != null && string.Equals(Eval("Abilitato").ToString(), "N", StringComparison.OrdinalIgnoreCase) %>' CommandName="Disabilita" CommandArgument='<%# Eval("K_Docente") %>' OnCommand="Selected_Command" />
            </ItemTemplate>

            <FooterTemplate>
                </tbody>
                </table>
                </div>
            </FooterTemplate>

        </asp:Repeater>
</asp:Content>

