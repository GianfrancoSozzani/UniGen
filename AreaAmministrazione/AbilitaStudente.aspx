<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AbilitaStudente.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Abilita/Disattiva Studenti</title>
    <meta name="viewport" content="width=device-width, initial-scale=1">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="container mt-3">
        <h1>Gestione Studenti</h1>

        <div class="form mb-3">
            <div class="row g-3 align-items-center">
                <div class="col-auto">
                    <label class="mr-2" for="txtRicercaMatricola">Ricerca Matricola</label>
                </div>
                <div class="col-auto">
                    <asp:TextBox ID="txtRicercaMatricola" runat="server" CssClass="form-control mr-2" TextMode="Number"></asp:TextBox>
                </div>
                <div class="col-auto">
                    <asp:LinkButton ID="btnRicerca" runat="server" CssClass="btn btn-primary" OnClick="btnRicerca_Click" Style="box-shadow: 0px 4px 12px #21212115;">
                <i class="bi bi-search"></i>
                    </asp:LinkButton>
                </div>
                <div class="col-auto">
                    <span style="margin-left: 1em;">
                        <asp:Label ID="lblErrore" runat="server" CssClass="text-danger mt-3" Text="" Visible="False"></asp:Label>
                    </span>
                </div>
            </div>
        </div>

        <h3>Elenco Studenti</h3>
        <asp:Repeater ID="rptStudenti" runat="server" OnItemCommand="rptStudenti_ItemCommand">
            <HeaderTemplate>
                <table class="table table-striped shadow">
                    <thead>
                        <tr>
                            <th>Matricola</th>
                            <th>Cognome</th>
                            <th>Nome</th>
                            <th>Stato</th>
                            <th>Azioni</th>
                        </tr>
                    </thead>
                    <tbody>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td><%# Eval("Matricola") %></td>
                    <td><%# Eval("Cognome") %></td>
                    <td><%# Eval("Nome") %></td>
                    <td>
                        <%# Eval("Abilitato").ToString() == "S" ? "Abilitato" : "Disattivato" %>
                    </td>
                    <td>
                        <asp:Button ID="btnAbilita" runat="server" Text="Abilita"
                            Visible='<%# Eval("Abilitato") != null && string.Equals(Eval("Abilitato").ToString(), "N", StringComparison.OrdinalIgnoreCase) %>'
                            CommandArgument='<%# Eval("Matricola") %>' CommandName="Abilita"
                            Style="box-shadow: 0px 4px 12px #21212115;" CssClass="btn btn-success btn-sm mr-2" />
                        <asp:Button ID="btnDisabilita" runat="server" Text="Disabilita"
                            Visible='<%# Eval("Abilitato") != null && string.Equals(Eval("Abilitato").ToString(), "S", StringComparison.OrdinalIgnoreCase) %>'
                            CommandArgument='<%# Eval("Matricola") %>' CommandName="Disabilita"
                            Style="box-shadow: 0px 4px 12px #21212115;" CssClass="btn btn-danger btn-sm" />
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </tbody>
                </table>
           
            </FooterTemplate>
        </asp:Repeater>
<%--        repeater per la paginazione--%>
        <asp:Repeater ID="rptPaginazione" runat="server" OnItemCommand="rptPaginazione_ItemCommand">
    <ItemTemplate>
        <asp:LinkButton ID="lnkPagina" runat="server"
            CommandName="CambiaPagina"
            CommandArgument='<%# Container.DataItem %>'
            CssClass='<%# (Convert.ToInt32(Container.DataItem) == GetPaginaCorrente() + 1) 
                ? "btn btn-primary btn-sm m-1 active" 
                : "btn btn-outline-primary btn-sm m-1" %>'>
            <%# Container.DataItem %>
        </asp:LinkButton>
    </ItemTemplate>
</asp:Repeater>



    </div>
</asp:Content>

