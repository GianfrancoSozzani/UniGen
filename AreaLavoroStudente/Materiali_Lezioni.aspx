<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Materiali_Lezioni.aspx.cs" Inherits="Materiali_Lezioni" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        .section {
            margin-top: 20px;
        }
        table th, table td {
            vertical-align: middle;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <form id="form1" runat="server">
        <div class="container mt-5">
            <h2 class="mb-4">Area Personale</h2>

            <!-- Selezione della facoltà -->
            <div class="section">
                <label for="ddlFacolta" runat="server" class="font-weight-bold">Facoltà:</label>
                <asp:DropDownList ID="ddlFacolta" runat="server" AutoPostBack="true" 
                    OnSelectedIndexChanged="ddlFacolta_SelectedIndexChanged" 
                    CssClass="form-control" />
            </div>

            <!-- Selezione del corso -->
            <div class="section">
                <label for="ddlCorsi" class="font-weight-bold">Corso:</label>
                <asp:DropDownList ID="ddlCorsi" runat="server" AutoPostBack="true" 
                    OnSelectedIndexChanged="ddlCorsi_SelectedIndexChanged" 
                    CssClass="form-control" />
            </div>

            <!-- Dettagli del corso selezionato -->
            <div class="section">
                <asp:Label ID="lblCorso" runat="server" Font-Bold="true" Font-Size="Large" />
            </div>

            <!-- Tabella delle lezioni -->
            <div class="section">
                <h3>Lezioni:</h3>
                <asp:Repeater ID="rptLezioni" runat="server">
                    <HeaderTemplate>
                        <table class="table table-bordered table-striped">
                            <thead class="thead-dark">
                                <tr>
                                    <th>Titolo</th>
                                    <th>Stato</th>
                                </tr>
                            </thead>
                            <tbody>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td><%# Eval("Titolo") %></td>
                            <td><%# (bool)Eval("Vista") ? "✓" : "✗" %></td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                            </tbody>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
            </div>

            <!-- Tabella dei materiali -->
            <div class="section">
                <h3>Materiali:</h3>
                <asp:Repeater ID="rptMateriali" runat="server">
                    <HeaderTemplate>
                        <table class="table table-bordered table-striped">
                            <thead class="thead-dark">
                                <tr>
                                    <th>Titolo</th>
                                    <th>Tipo</th>
                                </tr>
                            </thead>
                            <tbody>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td><%# Eval("Titolo") %></td>
                            <td><%# Eval("Tipo") %></td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                            </tbody>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
            </div>
        </div>

        <!-- Bootstrap JS e dipendenze -->
        <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>
        <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.5.2/dist/umd/popper.min.js"></script>
        <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
    </form>
</asp:Content>