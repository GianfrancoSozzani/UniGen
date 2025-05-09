<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Gestione_Corsi.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="container mt-3">
        <h1>Gestione Corsi</h1>

        <div class="mb-4">
            <div id="icona" class="row g-3 align-items-center justify-content-end">
                <div class="col-auto">
                    <button type="button" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#modalRicercaCorso">
                        <i class="bi bi-search"></i>
                    </button>
                </div>
                <div class="col-auto">
                    <button type="button" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#modalInserimentoCorso">
                        <i class="bi bi-plus-circle"></i>
                    </button>
                </div>
            </div>
        </div>

        <asp:Repeater ID="rpCorso" runat="server">
            <HeaderTemplate>
                <table class="table table-striped shadow">
                    <thead>
                        <tr>
                            <th>Corsi</th>
                            <th>Facoltà</th>
                            <th>Tipo corso</th>
                            <th>Minimo CFU</th>
                            <th>Costi annuali</th>
                            <th>Azioni</th>
                        </tr>
                    </thead>
                    <tbody>
            </HeaderTemplate>

            <ItemTemplate>
                <tr>
                    <td><%# Eval("TitoloCorso") %></td>
                    <td><%# Eval("TitoloFacolta") %></td>
                    <td><%# Eval("Tipo") %></td>
                    <td><%# Eval("MinimoCFU") %></td>
                    <td><%# Eval("CostoAnnuale") %></td>
                    <td>
                        <a href="#" class="btn btn-sm btn-primary" onclick="apriModal(
                                '<%# Eval("K_Corso") %>',
                                '<%# Eval("K_Facolta") %>',
                                '<%# Eval("K_TipoCorso") %>',
                                '<%# Eval("TitoloCorso") %>',
                                '<%# Eval("MinimoCFU") %>',
                                '<%# Eval("CostoAnnuale") %>',)">Modifica</a>
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

    <%--Modal ricerca--%>
    <div class="modal fade" id="modalRicercaCorso" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">

                <div class="modal-header">
                    <h5 class="modal-title fw-bold" id="exampleModalLabel1">Ricerca Corso</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Chiudi"></button>
                </div>

                <div class="modal-body">
                    <div class="mb-3">
                        <label for="txtRicercaCorso" class="form-label fw-bold">Corso</label>
                        <asp:TextBox ID="txtRicercaCorso" runat="server" CssClass="form-control"></asp:TextBox>
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
    <div class="modal fade" id="modalInserimentoCorso" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">

                <div class="modal-header">
                    <h5 class="modal-title fw-bold" id="example2ModalLabel">Inserisci Corso</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Chiudi"></button>
                </div>

                <div class="modal-body">
                    <asp:HiddenField ID="hiddenCorsoIns" runat="server" />

                    <div class="mb-3">
                        <label for="ddlFacolta" class="form-label fw-bold">Facoltà</label>
                        <asp:DropDownList ID="ddlFacolta" CssClass="form-select" runat="server"></asp:DropDownList>
                    </div>
                    <div class="mb-3">
                        <label for="ddlTipoCorso" class="form-label fw-bold">Tipo Corso</label>
                        <asp:DropDownList ID="ddlTipoCorso" CssClass="form-select" runat="server"></asp:DropDownList>
                    </div>
                    <div class="mb-3">
                        <label for="txtCorso" class="form-label fw-bold">Corso</label>
                        <asp:TextBox ID="txtCorso" CssClass="form-control mio_width" runat="server"></asp:TextBox>
                    </div>
                    <div class="mb-3">
                        <label for="txtMinimoCFU" class="form-label fw-bold">Minimo CFU</label>
                        <asp:TextBox ID="txtMinimoCFU" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="mb-3">
                        <label for="txtCostoAnnuale" class="form-label fw-bold">Costo Annuale</label>
                        <asp:TextBox ID="txtCostoAnnuale" CssClass="form-control" runat="server"></asp:TextBox>
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
    <div class="modal fade" id="modalModificaCorso" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">

                <div class="modal-header">
                    <h5 class="modal-title fw-bold" id="exampleModalLabel">Modifica Corso</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Chiudi"></button>
                </div>

                <div class="modal-body">
                    <asp:HiddenField ID="hiddenIdCorso" runat="server" />

                    <div class="mb-3">
                        <label for="txtTitoloCorso" class="form-label fw-bold">Corso</label>
                        <asp:TextBox ID="txtTitoloCorso" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>

                    <div class="mb-3">
                        <label for="ddlTitoloFacolta" class="form-label fw-bold">Facoltà</label>
                        <asp:DropDownList ID="ddlTitoloFacolta" CssClass="form-select" runat="server"></asp:DropDownList>
                    </div>

                    <div class="mb-3">
                        <label for="ddlTitoloTipo" class="form-label fw-bold">Tipo corso</label>
                        <asp:DropDownList ID="ddlTitoloTipo" CssClass="form-select" runat="server"></asp:DropDownList>
                    </div>

                    <div class="mb-3">
                        <label for="txtTitoloMinimoCFU" class="form-label fw-bold">MinimoCFU</label>
                        <asp:TextBox ID="txtTitoloMinimoCFU" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>

                    <div class="mb-3">
                        <label for="txtTitoloCostoAnnuale" class="form-label fw-bold">CostoAnnuale</label>
                        <asp:TextBox ID="txtTitoloCostoAnnuale" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>

                <div class="modal-footer">
                    <asp:Button ID="btnSalvaModifica" runat="server" Text="Salva Modifiche" CssClass="btn btn-primary" OnClick="btnSalvaModifica_Click" />
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Chiudi</button>
                </div>

            </div>
        </div>
    </div>

    <%--Script per il modal--%>
    <script>
        function apriModal(kCorso, kFacolta, kTipoCorso, titolo, cfu, costo) {
            document.getElementById('<%= hiddenIdCorso.ClientID %>').value = kCorso;
            document.getElementById('<%= ddlTitoloFacolta.ClientID %>').value = kFacolta;
            document.getElementById('<%= ddlTitoloTipo.ClientID %>').value = kTipoCorso;
            document.getElementById('<%= txtTitoloCorso.ClientID %>').value = titolo;
            document.getElementById('<%= txtTitoloMinimoCFU.ClientID %>').value = cfu;
            document.getElementById('<%= txtTitoloCostoAnnuale.ClientID %>').value = costo;

            var myModal = new bootstrap.Modal(document.getElementById('modalModificaCorso'));
            myModal.show();
        }
    </script>
</asp:Content>

