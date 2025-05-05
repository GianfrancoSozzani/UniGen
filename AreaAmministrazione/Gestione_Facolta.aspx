<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Gestione_Facolta.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="container mt-5">
        <h1>Gestione Facoltà</h1>

        <div class="mb-4">
            <div id="icona" class="row g-3 align-items-center justify-content-end">
                <div class="col-auto">
                    <asp:Label ID="Label2" runat="server" Text="Inserisci una nuova Facoltà" CssClass="fw-bold fs-5"></asp:Label>
                </div>
                <div class="col-auto">
                    <i class="bi bi-plus-circle btn btn-primary"></i>
                </div>
            </div>

            <div id="insert" class="d-none row g-3 align-items-center justify-content-end" runat="server">
                <div class="col-auto">
                    <asp:TextBox ID="txtFacolta" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
                <div class="col-auto">
                    <asp:Button CssClass="btn btn-primary" ID="btnSalva" runat="server" Text="Inserisci" OnClick="btnSalva_Click" />
                </div>
            </div>
        </div>

        <div>
            <asp:Repeater ID="rpFacolta" runat="server">
                <HeaderTemplate>
                    <table class="table table-striped shadow">
                        <thead>
                            <tr>
                                <th>Facoltà</th>
                                <th>Azioni</th>
                            </tr>
                        </thead>
                        <tbody>
                </HeaderTemplate>

                <ItemTemplate>
                    <tr>
                        <td><%# Eval("TitoloFacolta") %></td>
                        <td>
                            <a href="#" class="btn btn-sm btn-primary" onclick="apriModal('<%# Eval("K_Facolta") %>', '<%# Eval("TitoloFacolta") %>')">Modifica</a>

                        </td>
                    </tr>
                </ItemTemplate>

                <FooterTemplate>
                    </tbody>
            </table>
                </FooterTemplate>
            </asp:Repeater>
        </div>
    </div>

    <%--Modal--%>
    <div class="modal fade" id="modalModificaFacolta" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog">
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


    <%--Script per nascondere l'icona e rendere visibili gli elementi per l'inserimento--%>
    <script>
        document.addEventListener("DOMContentLoaded", function () {
            var btnMostra = document.getElementById("icona");
            var divInsert = document.getElementById("<%= insert.ClientID %>");

            btnMostra.addEventListener("click", function () {
                btnMostra.classList.add("d-none");
                divInsert.classList.remove("d-none");
            });
        });
    </script>

    <%--Script per il modal--%>
    <script>
        function apriModal(id, titolo) {
            document.getElementById('<%= hiddenIdFacolta.ClientID %>').value = id;
            document.getElementById('<%= txtTitoloFacolta.ClientID %>').value = titolo;

            var myModal = new bootstrap.Modal(document.getElementById('modalModificaFacolta'));
            myModal.show();
        }
    </script>

</asp:Content>
