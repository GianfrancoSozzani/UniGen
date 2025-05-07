<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Gestione_Esami.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="container mt-3">
        <h1>Gestione Esami</h1>

        <div class="mb-4">
            <div id="icona" class="row g-3 align-items-center justify-content-end">
                <div class="col-auto">
                    <button type="button" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#modalRicercaEsame">
                        <i class="bi bi-search"></i>
                    </button>
                </div>
                <div class="col-auto">
                    <button type="button" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#modalInserimentoEsame">
                        <i class="bi bi-plus-circle"></i>
                    </button>
                </div>
            </div>
        </div>

        <div>
            <asp:Repeater ID="rpEsame" runat="server">
                <HeaderTemplate>
                    <table class="table table-striped shadow">
                        <thead>
                            <tr>
                                <th>Esami</th>
                                <th>Docenti</th>
                                <th>CFU</th>
                                <th>Azioni</th>
                            </tr>
                        </thead>
                        <tbody>
                </HeaderTemplate>

                <ItemTemplate>
                    <tr>
                        <td><%# Eval("TitoloEsame") %></td>
                        <td><%# Eval("Cognome") %> <%# Eval("Nome") %></td>
                        <td><%# Eval("CFU") %></td>
                        <td>
                            <a href="#" class="btn btn-sm btn-primary" onclick="apriModal(
                            '<%# Eval("K_Esame") %>',
                            '<%# Eval("K_Docente") %>',
                            '<%# Eval("TitoloEsame") %>',
                            '<%# Eval("CFU") %>')">Modifica</a>
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
    </div>

    <%--Modal ricerca--%>
    <div class="modal fade" id="modalRicercaEsame" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">

                <div class="modal-header">
                    <h5 class="modal-title fw-bold" id="exampleModalLabel1">Ricerca Esame</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Chiudi"></button>
                </div>

                <div class="modal-body">
                    <div class="mb-3">
                        <label for="txtRicercaEsame" class="form-label fw-bold">Esame</label>
                        <asp:TextBox ID="txtRicercaEsame" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="mb-3">
                        <span style="margin-left: 1em;">
                            <asp:Label ID="lblErrore" runat="server" CssClass="text-danger mt-3" Text="" Visible="False"></asp:Label>
                        </span>
                    </div>
                </div>

                <div class="modal-footer">
                    <asp:LinkButton ID="btnRicerca" runat="server" CssClass="btn btn-primary" Style="box-shadow: 0px 4px 12px #21212115;" OnClick="btnRicerca_Click">
                        Cerca
                    </asp:LinkButton>
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Chiudi</button>
                </div>

            </div>
        </div>
    </div>

    <%--Modal inserimento--%>
    <div class="modal fade" id="modalInserimentoEsame" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">

                <div class="modal-header">
                    <h5 class="modal-title fw-bold" id="example2ModalLabel">Inserisci Esame</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Chiudi"></button>
                </div>

                <div class="modal-body">

                    <div class="mb-3">
                        <label for="txtEsami" class="form-label fw-bold">Esame</label>
                        <asp:TextBox ID="txtEsami" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="mb-3">
                        <label for="ddlDocente" class="form-label fw-bold">Docente</label>
                        <asp:DropDownList ID="ddlDocente" CssClass="form-select" runat="server"></asp:DropDownList>
                    </div>
                    <div class="mb-3">
                        <label for="txtCFU" class="form-label fw-bold">CFU</label>
                        <asp:TextBox ID="txtCFU" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>                   

                </div>

                <div class="modal-footer">
                    <asp:Button CssClass="btn btn-primary" ID="btnSalva" runat="server" Text="Inserisci" OnClick="btnSalva_Click" />
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Chiudi</button>
                </div>

            </div>
        </div>
    </div>

    <%--Modal modifica--%>
    <div class="modal fade" id="modalModificaEsame" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">

                <div class="modal-header">
                    <h5 class="modal-title fw-bold" id="exampleModalLabel">Modifica Esame</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Chiudi"></button>
                </div>

                <div class="modal-body">
                    <asp:HiddenField ID="hiddenIdEsame" runat="server" />

                    <div class="mb-3">
                        <label for="txtTitoloEsame" class="form-label fw-bold">Esame</label>
                        <asp:TextBox ID="txtTitoloEsame" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>

                    <div class="mb-3">
                        <label for="ddlTitoloEsame" class="form-label fw-bold">Docente</label>
                        <asp:DropDownList ID="ddlTitoloDocente" CssClass="form-select" runat="server"></asp:DropDownList>
                    </div>

                    <div class="mb-3">
                        <label for="txtTitoloCFU" class="form-label fw-bold">CFU</label>
                        <asp:TextBox ID="txtTitoloCFU" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>

                <div class="modal-footer">
                    <asp:Button ID="btnSalvaModifica" runat="server" Text="Salva Modifiche" CssClass="btn btn-primary" OnClick="btnSalvaModifica_Click" />
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Chiudi</button>
                </div>

            </div>
        </div>
    </div>

    <%--Script per il modal modifica--%>
    <script>
        function apriModal(kesame, kdocente, esame, cfu) {
            document.getElementById('<%= hiddenIdEsame.ClientID %>').value = kesame;
            document.getElementById('<%= ddlTitoloDocente.ClientID %>').value = kdocente;
            document.getElementById('<%= txtTitoloEsame.ClientID %>').value = esame;
            document.getElementById('<%= txtTitoloCFU.ClientID %>').value = cfu;

            var myModal = new bootstrap.Modal(document.getElementById('modalModificaEsame'));
            myModal.show();
        }
    </script>
</asp:Content>
