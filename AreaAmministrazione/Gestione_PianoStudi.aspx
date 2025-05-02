<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" EnableEventValidation="true" AutoEventWireup="true" CodeFile="Gestione_PianoStudi.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.11.3/font/bootstrap-icons.min.css">
    <style>
        #container {
            margin-left: 50px;
        }

        .btn:hover {
            box-shadow: 13px 6px 5px 0px rgba(0,0,0,0.23);
            -webkit-box-shadow: 13px 6px 5px 0px rgba(0,0,0,0.23);
            -moz-box-shadow: 13px 6px 5px 0px rgba(0,0,0,0.23);
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="container">
        <h1 class="mb-5">Gestione Piano di Studi</h1>

        <h2>Cerca e modifica Piani di studio</h2>

        <div class="row">
            <%-- Dropdown Facoltà --%>
            <div class="col-4 mt-4 col-sm-8 col-md-4">
                <asp:Label ID="lblFacolta" runat="server" Text="Facoltà"></asp:Label>
                <asp:DropDownList ID="ddlFacolta" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlFacolta_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
            </div>
            <%-- Dropdown Corso --%>
            <div class="col-5 mt-4 col-sm-8 col-md-4">
                <asp:Label ID="lblCorso" runat="server" Text="Corso di laurea"></asp:Label>
                <asp:DropDownList ID="ddlCorso" runat="server" CssClass="form-control"></asp:DropDownList>
            </div>
        </div>

        <div class="row">
            <%-- Dropdown Tipologia Corso --%>
            <div class="col-4 mt-4 col-sm-8 col-md-4">
                <asp:Label ID="Label2" runat="server" Text="Tipologia Corso"></asp:Label>
                <asp:DropDownList ID="ddlTipoCorso" runat="server" CssClass="form-control">
                    <asp:ListItem Text="Triennale" Value="Triennale"></asp:ListItem>
                    <asp:ListItem Text="Magistrale" Value="Magistrale"></asp:ListItem>
                    <asp:ListItem Text="Ciclo Unico" Value="Ciclo Unico"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <%-- Dropdown Anno accademico --%>
            <div class="col-5 mt-4 col-sm-8 col-md-4">
                <asp:Label ID="Label1" runat="server" Text="Anno accademico"></asp:Label>
                <asp:DropDownList ID="ddlAnnoAccademico" runat="server" CssClass="form-control">
                    <asp:ListItem Text="2024/2025" Value="2024/2025"></asp:ListItem>
                    <asp:ListItem Text="2025/2026" Value="2025/2026"></asp:ListItem>
                    <asp:ListItem Text="2026/2027" Value="2026/2027"></asp:ListItem>
                    <asp:ListItem Text="2027/2028" Value="2027/2028"></asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>


        <%-- BOTTONE Cerca Esami  --%>
        <div class="row">
            <div class="col-4">
                <asp:Button ID="btnCerca" runat="server" Text="Cerca" CssClass="btn btn-primary mt-4" OnClick="btnCerca_Click" />
            </div>
        </div>

        <%-- BOTTONE Aggiungi Esami  --%>
        <div class="row">
            <div class="col-7 mb-3">
                <asp:Button ID="btnApriModalEsami" runat="server"
                    CssClass="btn btn-outline-success mt-4"
                    Text="Aggiungi Esami"
                    OnClick="btnApriModalEsami_Click" />
            </div>
        </div>

        <%------------------------- MOSTRA ELENCO ESAMI INSERITI NEL PIANO DI STUDI------------------------%>

        <asp:Repeater ID="rpEsamiInseriti" runat="server">
            <headertemplate>
                <table class="table">
                    <thead>
                        <tr>
                            <th>Esame</th>
                            <th>CFU</th>
                            <th>Tipologia</th>
                            <th>Azioni</th>
                        </tr>
                    </thead>
                    <tbody>
            </headertemplate>

            <itemtemplate>
                <tr>
                    <td><%# Eval("TitoloEsame") %></td>
                    <td><%# Eval("CFU") %></td>
                    <td><%# Eval("Tipologia") %></td>
                    <td>
                        <asp:LinkButton ID="btnRimuoviEsame" runat="server"
                            OnClick="btnRimuoviEsame_OnClick"
                            CssClass="btn btn-danger"
                            CommandName="RimuoviEsame"
                            CommandArgument='<%# Eval("K_PianoStudio") %>'>
                            Rimuovi
                        </asp:LinkButton>
                    </td>
                </tr>
            </itemtemplate>

            <footertemplate>
                </tbody>
        </table>
            </footertemplate>
        </asp:Repeater>



        <!------------------------------------------ M O D A L E: Elenco Esami -------------------------------------------------->
        <div class="modal fade" id="modalEsamiDisponibili" tabindex="-1" aria-labelledby="modalEsamiDisponibiliLabel" aria-hidden="true" data-bs-backdrop="static">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="modalEsamiDisponibiliLabel">Elenco Esami disponibili</h5>
                        <asp:Button ID="btnChiudiModal" runat="server" CssClass="btn-close"
                            OnClick="btnChiudiModal_Click"
                            OnClientClick="$('#modalEsamiDisponibili').modal('hide');" aria-label="Chiudi" />

                    </div>
                    <div class="modal-body">
                        <nav class="navbar bg-body-tertiary">
                            <div class="container-fluid">

                                <%--BOTTONE DI RICERCA PER NOME ESAME--%>
                                <div class="row mb-3">
                                    <div class="col-7">
                                        <asp:TextBox runat="server" ID="txtSearchEsame" CssClass="form-control" Placeholder="Cerca esame" />
                                    </div>
                                    <div class="col-5">
                                        <asp:Button ID="btnCercaEsame" runat="server" Text="Cerca" CssClass="btn btn-primary" OnClick="btnCercaEsame_Click" />
                                    </div>
                                </div>

                            </div>
                        </nav>
                        <asp:Repeater ID="rpEsami" runat="server" OnItemCommand="rpEsami_ItemCommand">
                            <headertemplate>
                                <table class="table">
                                    <thead>
                                        <tr>
                                            <th>Titolo</th>
                                            <th>CFU</th>
                                            <th>Tipologia</th>
                                            <th>Azioni</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                            </headertemplate>

                            <itemtemplate>
                                <tr>
                                    <td><%# Eval("TitoloEsame") %></td>
                                    <td><%# Eval("CFU") %></td>
                                    <td>
                                        <asp:CheckBox ID="chkObbligatorio" runat="server" CssClass="form-check-input" />
                                        <label class="form-check-label">Obbligatorio</label>
                                    </td>
                                    <td>
                                        <asp:LinkButton ID="btnAggiungiEsame" runat="server"
                                            CssClass="btn btn-primary"
                                            CommandName="AggiungiEsame"
                                            CommandArgument='<%# Eval("K_Esame") %>'>
                                            Aggiungi
                                        </asp:LinkButton>
                                    </td>
                                </tr>
                            </itemtemplate>

                            <footertemplate>
                                </tbody>
                      </table>
                            </footertemplate>
                        </asp:Repeater>

                        <script>
                            function aggiornaValoreCheckbox(checkbox) {
                                checkbox.value = checkbox.checked ? "S" : "N";
                            }
                        </script>

                    </div>
                    <div class="modal-footer">
                        <asp:Button ID="btnChiudiModal2" runat="server" CssClass="btn-secondary"
                            OnClick="btnChiudiModal2_Click"
                            OnClientClick="$('#modalEsamiDisponibili').modal('hide');" aria-label="Chiudi"
                            Text="Chiudi" />
                    </div>
                </div>
            </div>
        </div>

        <%--       Bottone per tornare alla Home--%>
        <div class="row w-25">
            <div class="col-text-end">
                <a class="btn btn-primary mt-4 mb-3 ms-50" id="tornahome" href="Home.aspx" role="button">Torna alla Home</a>
            </div>
        </div>

    </div>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js"></script>
</asp:Content>

