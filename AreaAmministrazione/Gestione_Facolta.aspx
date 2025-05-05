<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Gestione_Facolta.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="container mt-5">
        <h1>Gestione Facoltà</h1>

        <div class="mb-4">
            <div id="icona" class="row g-3 align-items-center justify-content-end">
                <div class="col-auto">
                    <button type="button" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#modalInserimentoFacolta">
                        <i class="bi bi-plus-circle"></i>
                    </button>
                </div>
            </div>

        </div>

        <div>
            <asp:Repeater ID="rpFacolta" runat="server">
                <headertemplate>
                    <table class="table table-striped shadow">
                        <thead>
                            <tr>
                                <th>Facoltà</th>
                                <th>Azioni</th>
                            </tr>
                        </thead>
                        <tbody>
                </headertemplate>

                <itemtemplate>
                    <tr>
                        <td><%# Eval("TitoloFacolta") %></td>
                        <td>
                            <a href="#" class="btn btn-sm btn-primary" onclick="apriModal('<%# Eval("K_Facolta") %>', '<%# Eval("TitoloFacolta") %>')">Modifica</a>

                        </td>
                    </tr>
                </itemtemplate>

                <footertemplate>
                    </tbody>
            </table>
                </footertemplate>
            </asp:Repeater>
        </div>
    </div>

    <%--Modal inserimento--%>
    <div class="modal fade" id="modalInserimentoFacolta" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">

                <div class="modal-header">
                    <h5 class="modal-title fw-bold" id="example2ModalLabel">Inserisci Facoltà</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Chiudi"></button>
                </div>

                <div class="modal-body">
                    <asp:HiddenField ID="hiddenFacoltaIns" runat="server" />

                    <div class="mb-3">
                        <label for="txtTitoloFacoltaIns" class="form-label fw-bold">Facoltà</label>
                        <asp:TextBox ID="txtTitoloFacoltaIns" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>

                <div class="modal-footer">
                    <asp:Button ID="btnSalvaInserimento" runat="server" Text="Salva Modifiche" CssClass="btn btn-primary" OnClick="btnSalvaInserimento_Click" />
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Chiudi</button>
                </div>

            </div>
        </div>
    </div>

    <%--Modal modifica--%>
    <div class="modal fade" id="modalModificaFacolta" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">

                <div class="modal-header">
                    <h5 class="modal-title fw-bold" id="exampleModalLabel">Modifica Facoltà</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Chiudi"></button>
                </div>

                <div class="modal-body">
                    <asp:HiddenField ID="hiddenIdFacolta" runat="server" />

                    <div class="mb-3">
                        <label for="txtTitoloFacolta" class="form-label fw-bold">Facoltà</label>
                        <asp:TextBox ID="txtTitoloFacolta" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>

                <div class="modal-footer">
                    <asp:Button ID="btnSalvaModifica" runat="server" Text="Salva Modifiche" CssClass="btn btn-primary" OnClick="btnSalvaModifica_Click" />
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Chiudi</button>
                </div>

            </div>
        </div>
    </div>

    <%--Script per il modal inserimento--%>
    <script>
        function apriModalIns(id, titolo) {
            document.getElementById('<%= hiddenFacoltaIns.ClientID %>').value = id;
            document.getElementById('<%= txtTitoloFacoltaIns.ClientID %>').value = titolo;

            var myModal = new bootstrap.Modal(document.getElementById('modalInserimentoFacolta'));
            myModal.show();
        }
    </script>

    <%--Script per il modal modifica--%>
    <script>
        function apriModal(id, titolo) {
            document.getElementById('<%= hiddenIdFacolta.ClientID %>').value = id;
            document.getElementById('<%= txtTitoloFacolta.ClientID %>').value = titolo;

            var myModal = new bootstrap.Modal(document.getElementById('modalModificaFacolta'));
            myModal.show();
        }
    </script>

</asp:Content>
