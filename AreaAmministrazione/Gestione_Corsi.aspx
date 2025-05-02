<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Gestione_Corsi.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="container mt-5">
        <h1>Gestione Corsi</h1>

        <div class="mb-4">
            <div id="icona" class="row g-3 align-items-center justify-content-end">
                <div class="col-auto">
                    <asp:Label ID="Label2" runat="server" Text="Inserisci un nuovo corso" CssClass="fw-bold fs-5"></asp:Label>
                </div>
                <div class="col-auto">
                    <i class="bi bi-plus-circle btn btn-primary"></i>
                </div>
            </div>

            <div id="insert" class="d-none" runat="server">
                <div class="row align-items-center justify-content-end mb-3">
                    <div class="col-auto d-flex align-items-center column-gap-3">
                        <asp:Label ID="Label4" runat="server" Text="Corso"></asp:Label>
                        <asp:TextBox ID="txtCorso" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-auto d-flex align-items-center column-gap-3">
                        <asp:Label ID="Label1" runat="server" Text="Facoltà"></asp:Label>
                        <asp:DropDownList ID="ddlFacolta" CssClass="form-select" runat="server"></asp:DropDownList>
                    </div>
                    <div class="col-auto d-flex align-items-center column-gap-3">
                        <asp:Label ID="Label3" runat="server" Text="Tipo&nbsp;Corso"></asp:Label>
                        <asp:DropDownList ID="ddlTipoCorso" CssClass="form-select" runat="server"></asp:DropDownList>
                    </div>
                </div>
                <div class="row align-items-center justify-content-end mb-3">
                    <div class="col-auto d-flex align-items-center column-gap-3">
                        <asp:Label ID="Label5" runat="server" Text="MinimoCFU"></asp:Label>
                        <asp:TextBox ID="txtMinimoCFU" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-auto d-flex align-items-center column-gap-3">
                        <asp:Label ID="Label6" runat="server" Text="CostoAnnuale"></asp:Label>
                        <asp:TextBox ID="txtCostoAnnuale" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="row align-items-center justify-content-end">
                    <div class="col-auto">
                        <asp:Button CssClass="btn btn-primary" ID="btnSalva" runat="server" Text="Inserisci" OnClick="btnSalva_Click" />
                    </div>
                </div>
            </div>
        </div>

        <div>
            <asp:Repeater ID="rpCorso" runat="server">
                <headertemplate>
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
                </headertemplate>

                <itemtemplate>
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
                </itemtemplate>

                <footertemplate>
                    </tbody>
            </table>
                </footertemplate>
            </asp:Repeater>
        </div>
    </div>

    <%--Modal--%>
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

