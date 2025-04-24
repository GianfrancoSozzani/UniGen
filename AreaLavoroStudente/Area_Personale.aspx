<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Area_Personale.aspx.cs" Inherits="AreaPersonale" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Area Personale</title>
    <!-- Link to Bootstrap CSS -->
    <link href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet" />

    <style>
        .section {
            margin-top: 20px;
        }
        .materiale, .lezione {
            margin-left: 15px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container mt-5">
            <!-- Card container for Area Personale -->
            <div class="card shadow-sm">
                <div class="card-body">
                    <h2 class="card-title">Area Personale</h2>

                    <!-- Selezione della facoltà -->
                    <div class="section">
                        <label for="ddlFacolta" class="font-weight-bold">Facoltà:</label>
                        <asp:DropDownList ID="ddlFacolta" runat="server" AutoPostBack="true" 
                            OnSelectedIndexChanged="ddlFacolta_SelectedIndexChanged" class="form-control" />
                    </div>

                    <!-- Selezione del corso -->
                    <div class="section">
                        <label for="ddlCorsi" class="font-weight-bold">Corso:</label>
                        <asp:DropDownList ID="ddlCorsi" runat="server" AutoPostBack="true" 
                            OnSelectedIndexChanged="ddlCorsi_SelectedIndexChanged" class="form-control" />
                    </div>

                    <!-- Dettagli del corso selezionato -->
                    <div class="section">
                        <asp:Label ID="lblCorso" runat="server" Font-Bold="true" Font-Size="Large" />
                    </div>

                    <!-- Elenco delle lezioni -->
                    <div class="section">
                        <h3>Lezioni:</h3>
                        <asp:Repeater ID="rptLezioni" runat="server">
                            <ItemTemplate>
                                <div class="lezione">• <%# Eval("Titolo") %> - <%# (bool)Eval("Vista") ? "✓" : "✗" %></div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>

                    <!-- Elenco dei materiali -->
                    <div class="section">
                        <h3>Materiali:</h3>
                        <asp:Repeater ID="rptMateriali" runat="server">
                            <ItemTemplate>
                                <div class="materiale">• <%# Eval("Titolo") %> (<%# Eval("Tipo") %>)</div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
            </div>
        </div>

        <!-- Link to Bootstrap JS & Popper.js (for Bootstrap functionalities) -->
        <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>
        <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.5.2/dist/umd/popper.min.js"></script>
        <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
    </form>
</body>
</html>
