<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AbilitaStudente.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Abilita/Disattiva Studenti</title>
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css">
    <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.5.3/dist/umd/popper.min.js"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.11.3/font/bootstrap-icons.min.css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="container mt-3">
        <h2 class="form-inline">Gestione Studenti</h2>

        <div class="form-inline mb-3">
            <label class="mr-2" for="txtRicercaMatricola">Ricerca Matricola</label>
            <asp:TextBox ID="txtRicercaMatricola" runat="server" CssClass="form-control mr-2" TextMode="Number"></asp:TextBox>
            <button id="btnRicerca" runat="server" class="btn btn-primary" OnClick="btnRicerca_Click">
                <i class="bi bi-search"></i>
            </button>
            <span style="margin-left: 1em;">
                <asp:Label ID="lblErrore" runat="server" CssClass="text-danger mt-3" Text="" Visible="False"></asp:Label>
            </span>
        </div>

        <h3>Elenco Studenti</h3>
        <asp:Repeater ID="rptStudenti" runat="server" OnItemCommand="rptStudenti_ItemCommand">
            <headertemplate>
                <table class="table table-striped">
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
            </headertemplate>
            <itemtemplate>
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
            </itemtemplate>
            <footertemplate>
                </tbody>
                </table>
           
            </footertemplate>
        </asp:Repeater>


    </div>
</asp:Content>

