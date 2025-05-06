<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="GestioneDocenti.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="container mt-3">
        <h1>Elenco Docenti</h1>

        <div class="mb-4">
            <div class="row g-3 align-items-center justify-content-end">
                <div class="col-auto">
                    <button type="button" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#modalRicercaDocente">
                        <i class="bi bi-search"></i>
                    </button>
                </div>
                <div class="col-auto">
                    <asp:LinkButton ID="btnNuovaPagina" runat="server" CssClass="btn btn-primary" OnClick="btnNuovaPagina_Click">
                        <i class="bi bi-plus-circle"></i>
                    </asp:LinkButton>
                    <%--<asp:Button ID="btnNuovaPagina" runat="server" CssClass="btn btn-primary" Text="+" OnClick="btnNuovaPagina_Click" />--%>
                </div>
            </div>
        </div>

        <div>
            <asp:Repeater ID="rpDocenti" runat="server">
                <HeaderTemplate>
                    <div>
                        <table class="table table-striped shadow">
                            <thead>
                                <tr>
                                    <th>Cognome</th>
                                    <th>Nome</th>
                                    <th>Data di Nascita</th>
                                    <th>Stato</th>
                                    <%--                                    <th>Titolo Corso</th>
                                    <th>Titolo Esame</th>--%>
                                    <th>Azioni</th>
                                </tr>
                            </thead>
                            <tbody>
                </HeaderTemplate>

                <ItemTemplate>
                    <tr>
                        <td><%# Eval("Cognome") %></td>
                        <td><%# Eval("Nome") %></td>
                        <td><%# Eval("DataNascita","{0:dd/M/yyyy}") %></td>
                        <td>
                            <%# Eval("Abilitato").ToString() == "S" ? "Abilitato" : "Disabilitato" %>
                        </td>
                        <%--                        <td>
                            <%# Eval("TitoloCorso") %>
                        </td>
                        <td>
                            <%# Eval("TitoloEsame") %>
                        </td>--%>
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

    <%--Modal ricerca--%>
    <div class="modal fade" id="modalRicercaDocente" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">

                <div class="modal-header">
                    <h5 class="modal-title fw-bold" id="exampleModalLabel1">Ricerca Docente</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Chiudi"></button>
                </div>

                <div class="modal-body">
                    <div class="mb-3">
                        <label for="txtCognome" class="form-label fw-bold">Cognome</label>
                        <asp:TextBox ID="txtCognome" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="mb-3">
                        <label for="txtNome" class="form-label fw-bold">Nome</label>
                        <asp:TextBox ID="txtNome" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>

                <div class="modal-footer">
                    <asp:LinkButton ID="btnCerca" runat="server" CssClass="btn btn-primary" OnClick="btnCerca_Click">
                        Cerca
                    </asp:LinkButton>
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Chiudi</button>
                </div>

            </div>
        </div>
    </div>
</asp:Content>

