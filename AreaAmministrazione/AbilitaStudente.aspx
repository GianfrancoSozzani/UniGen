<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AbilitaStudente.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Abilita/Disattiva Studenti</title>
    <meta name="viewport" content="width=device-width, initial-scale=1">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="container mt-3">
        <h1>Gestione Studenti</h1>

        <div class="mb-4">
            <div class="mb-3">
                <span>
                    <asp:Label ID="lblErrore" runat="server" CssClass="text-danger mt-3" Text="" Visible="False"></asp:Label>
                </span>
            </div>
            <div class="row g-3 align-items-center justify-content-end">
                <div class="col-auto">

                    <button type="button" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#modalRicercaMatricola">
                        <i class="bi bi-search"></i>
                    </button>
                </div>

            </div>
        </div>

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

        <%--Modal--%>
        <div class="modal fade" id="modalRicercaMatricola" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
            <div class="modal-dialog modal-dialog-centered">
                <div class="modal-content">

                    <div class="modal-header">
                        <h5 class="modal-title fw-bold" id="exampleModalLabel">Ricerca Matricola</h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Chiudi"></button>
                    </div>

                    <div class="modal-body">
                        <div class="mb-3">
                            <label for="txtRicercaMatricola" class="form-label fw-bold">Matricola</label>
                            <asp:TextBox ID="txtRicercaMatricola" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>

                    </div>

                    <div class="modal-footer">
                        <asp:LinkButton ID="btnRicerca" runat="server" CssClass="btn btn-primary" OnClick="btnRicerca_Click" Style="box-shadow: 0px 4px 12px #21212115;">
                            Cerca
                        </asp:LinkButton>
                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Chiudi</button>
                    </div>

                </div>
            </div>
        </div>
    </div>

</asp:Content>

