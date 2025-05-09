<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" EnableEventValidation="true" AutoEventWireup="true" CodeFile="Gestione_PianoStudi.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.11.3/font/bootstrap-icons.min.css">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/tingle/0.15.3/tingle.min.css" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/tingle/0.15.3/tingle.min.js"></script>
    <style>
        #container {
            margin-left: 50px;
        }

        .btn:hover {
            box-shadow: 14px 10px 5px -3px rgba(0,0,0,0.06);
            -webkit-box-shadow: 14px 10px 5px -3px rgba(0,0,0,0.06);
            -moz-box-shadow: 14px 10px 5px -3px rgba(0,0,0,0.06);
        }

        .btn-icon::before {
            content: "\f4d8"; /* codice icona "plus" in Bootstrap Icons */
            font-family: "Bootstrap Icons";
            font-weight: normal;
            padding-right: 6px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="container">
        <h1 class="mb-5">Gestione Piani di Studio</h1>

        <div class="row">
            <%-- Dropdown Facoltà --%>
            <div class="col-4 col-sm-8 col-md-4">
                <asp:Label ID="lblFacolta" runat="server" Text="Facoltà"></asp:Label>
                <asp:DropDownList ID="ddlFacolta" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlFacolta_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
            </div>
            <%-- Dropdown Corso --%>
            <div class="col-5 col-sm-8 col-md-4">
                <asp:Label ID="lblCorso" runat="server" Text="Corso di laurea"></asp:Label>
                <asp:DropDownList ID="ddlCorso" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlCorso_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
            </div>
        </div>

        <div class="row">
            <%-- Dropdown Tipologia Corso --%>
            <div class="col-4 mt-4 col-sm-8 col-md-4">
                <asp:Label ID="Label2" runat="server" Text="Tipologia Corso"></asp:Label>
                <asp:DropDownList ID="ddlTipoCorso" runat="server" CssClass="form-control">
                    <%--                  <asp:ListItem Text="Triennale" Value="Triennale"></asp:ListItem>
                    <asp:ListItem Text="Magistrale" Value="Magistrale"></asp:ListItem>
                    <asp:ListItem Text="Ciclo Unico" Value="Ciclo Unico"></asp:ListItem>--%>
                </asp:DropDownList>
            </div>
            <%-- Dropdown Anno accademico --%>
            <div class="col-5 mt-4 col-sm-8 col-md-4">
                <asp:Label ID="Label1" runat="server" Text="Anno Accademico"></asp:Label>
                <asp:DropDownList ID="ddlAnnoAccademico" runat="server" CssClass="form-control">
                    <asp:ListItem Text="2024/2025" Value="2024/2025"></asp:ListItem>
                    <asp:ListItem Text="2025/2026" Value="2025/2026"></asp:ListItem>
                    <asp:ListItem Text="2026/2027" Value="2026/2027"></asp:ListItem>
                    <asp:ListItem Text="2027/2028" Value="2027/2028"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <%-- BOTTONE Cerca Esami  --%>

            <div class="col-2 mt-5">
                <asp:Label ID="Label3" runat="server" Text=""></asp:Label>
                <asp:Button ID="btnCerca" runat="server" Text="Cerca" CssClass="btn btn-primary" OnClick="btnCerca_Click" />
            </div>
        </div>


        <div style="height: 1px; background-color: midnightblue; width: 100%; margin: 30px 0;"></div>

        <%-- BOTTONE Aggiungi Esami  --%>
        <div class="row  ">
            <div class="col-6">
                <h3>Elenco Esami</h3>
            </div>
            <div class="col-6 mb-3 text-end">
                <asp:Button ID="btnApriModalEsami" runat="server"
                    CssClass="btn btn-primary btn-icon"
                    Text="Aggiungi Esami"
                    OnClick="btnApriModalEsami_Click" />
            </div>
        </div>

        <%------------------------- MOSTRA ELENCO ESAMI INSERITI NEL PIANO DI STUDI------------------------%>

        <asp:Repeater ID="rpEsamiInseriti" runat="server">
            <HeaderTemplate>
                <table class="table">
                    <thead>
                        <tr>
                            <th>Esame</th>
                            <th>CFU</th>
                            <th>Tipologia</th>
                            <th>Docente</th>
                            <th>Azioni</th>
                        </tr>
                    </thead>
                    <tbody>
            </HeaderTemplate>

            <ItemTemplate>
                <tr>
                    <td><%# Eval("TitoloEsame") %></td>
                    <td><%# Eval("CFU") %></td>
                    <td><%# Eval("Tipologia") %></td>
                    <td><%# Eval("Cognome") %> <%# Eval("Nome") %></td>
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
            </ItemTemplate>

            <FooterTemplate>
                </tbody>
        </table>
            </FooterTemplate>
        </asp:Repeater>
        <%--        repeater per la paginazione--%>
        <asp:Repeater ID="rptPaginazione" runat="server" OnItemCommand="rptPaginazione_ItemCommand">
            <ItemTemplate>
                <asp:LinkButton ID="lnkPagina" runat="server" CommandName="CambiaPagina" CommandArgument='<%# Container.DataItem %>'
                    CssClass='<%# (Convert.ToInt32(Container.DataItem) == GetPaginaCorrente() + 1) 
     ? "btn btn-primary btn-sm m-1 active" 
     : "btn btn-outline-primary btn-sm m-1" %>'
                    Text='<%# Container.DataItem %>' />
            </ItemTemplate>
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
                            <HeaderTemplate>
                                <table class="table">
                                    <thead>
                                        <tr>
                                            <th>Titolo</th>
                                            <th>CFU</th>
                                            <th>Tipologia</th>
                                            <th>Docente</th>
                                            <th>Azioni</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                            </HeaderTemplate>

                            <ItemTemplate>
                                <tr>
                                    <td><%# Eval("TitoloEsame") %></td>
                                    <td><%# Eval("CFU") %></td>
                                    <td>
                                        <asp:CheckBox ID="chkFacoltativo" runat="server" CssClass="form-check-input" />
                                        <label class="form-check-label">Facoltativo</label>
                                    </td>
                                    <td><%# Eval("Cognome") %> <%# Eval("Nome") %></td>
                                    <td>
                                        <asp:LinkButton ID="btnAggiungiEsame" runat="server"
                                            CssClass="btn btn-primary"
                                            CommandName="AggiungiEsame"
                                            CommandArgument='<%# Eval("K_Esame") %>'>
                                            Aggiungi
                                        </asp:LinkButton>
                                    </td>
                                </tr>
                            </ItemTemplate>

                            <FooterTemplate>
                                </tbody>
                      </table>
                            </FooterTemplate>
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

    </div>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js"></script>
</asp:Content>

