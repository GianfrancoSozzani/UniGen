<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="GestioneDocenti.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container">
        <div class="mb-5">
            <h3>Elenco Docenti</h3>
        </div>

        <div class="row mt-3 mb-5">
            <div class="col-auto d-flex align-items-center">
                <h4>Ricerca - </h4>
            </div>
            <div class="col-auto d-flex align-items-center">
                <asp:Label ID="litCognome" runat="server" CssClass="me-2">Cognome:</asp:Label>
                <asp:TextBox ID="txtCognome" runat="server"></asp:TextBox>
            </div>
            <div class="col-auto d-flex align-items-center">
                <asp:Label ID="litNome" runat="server" CssClass="me-2">Nome:</asp:Label>
                <asp:TextBox ID="txtNome" runat="server"></asp:TextBox>
            </div>
            <div class="col-auto">
                <button id="btnCerca" runat="server" class="btn btn-primary" onclick="btnCerca_ServerClick">
                    <i class="bi bi-search"></i>
                </button>
            </div>
        </div>

        <div class="row mt-3 mb-5">
            <div class="col-auto d-flex align-items-center">
                <h4>Inserisci nuovo docente - </h4>
            </div>
            <div class="col-auto">
                <asp:Button ID="btnNuovaPagina" runat="server" CssClass="btn btn-primary" Text="+" OnClick="btnNuovaPagina_Click" />
            </div>
        </div>

        <div class="mt-5">
            <asp:Repeater ID="rpDocenti" runat="server">
                <HeaderTemplate>
                    <div class="mt-5">
                        <table class="table table-striped shadow">
                            <thead>
                                <tr>
                                    <th>Cognome</th>
                                    <th>Nome</th>
                                    <th>Data di Nascita</th>
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
                        <td><%# Eval("Cognome") %></td>
                        <td><%# Eval("Nome") %></td>
                        <td><%# Eval("DataNascita") %></td>
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
                            <asp:Button ID="btnAbilita" Class="btn btn-success" runat="server" Text="Abilita" Visible='<%# Eval("Abilitato") != null && string.Equals(Eval("Abilitato").ToString(), "N", StringComparison.OrdinalIgnoreCase) %>' CommandName="Abilita" CommandArgument='<%# Eval("K_Docente") %>' OnCommand="Selected_Command" />
                            <asp:Button ID="btnDisabilita" Class="btn btn-danger" runat="server" Text="Disabilita" Visible='<%# Eval("Abilitato") != null && string.Equals(Eval("Abilitato").ToString(), "S", StringComparison.OrdinalIgnoreCase) %>' CommandName="Disabilita" CommandArgument='<%# Eval("K_Docente") %>' OnCommand="Selected_Command" />
                        </td>
                    </tr>
                </ItemTemplate>

                <FooterTemplate>
                    </tbody>
                </table>
                </div>
                </FooterTemplate>

            </asp:Repeater>
        </div>
    </div>
</asp:Content>

