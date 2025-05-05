<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Gestione_Esami.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="container mt-5">
        <h1>Gestione Esami</h1>

        <div class="mb-4">
            <div id="icona" class="row g-3">
                <div class="col-auto">
                    <asp:Label ID="Label2" runat="server" Text="Inserisci un nuovo esame" CssClass="fw-bold fs-5"></asp:Label>
                </div>
                <div class="col-auto">
                    <i class="bi bi-plus-circle btn btn-sm btn-primary"></i>
                </div>
            </div>

            <div id="insert" class="d-none row" runat="server">
                <div class="col-auto">
                    <asp:Label ID="Label3" runat="server" Text="Esame"></asp:Label>
                    <asp:TextBox ID="txtEsami" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
                <div class="col-auto">
                    <asp:Label ID="Label1" runat="server" Text="Docente"></asp:Label>
                    <asp:DropDownList ID="ddlDocente" CssClass="form-select" runat="server"></asp:DropDownList>
                </div>
                <div class="col-auto">
                    <asp:Label ID="Label5" runat="server" Text="CFU"></asp:Label>
                    <asp:TextBox ID="txtCFU" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
                <div class="col-auto d-flex align-items-end">
                    <asp:Button CssClass="btn btn-primary" ID="btnSalva" runat="server" Text="Inserisci" OnClick="btnSalva_Click" />
                </div>
            </div>
        </div>

        <div class="form mb-3">
            <div class="row g-3 align-items-center">
                <div class="col-auto">
                    <label class="mr-2" for="lblRicercaEsame">Ricerca Esame</label>
                </div>
                <div class="col-auto">
                    <asp:TextBox ID="txtRicercaEsame" runat="server" CssClass="form-control mr-2"></asp:TextBox>
                </div>
                <div class="col-auto">
                    <asp:LinkButton ID="btnRicerca" runat="server" CssClass="btn btn-primary" Style="box-shadow: 0px 4px 12px #21212115;" OnClick="btnRicerca_Click">
  <i class="bi bi-search"></i>
                    </asp:LinkButton>
                </div>
                <div class="col-auto">
                    <span style="margin-left: 1em;">
                        <asp:Label ID="lblErrore" runat="server" CssClass="text-danger mt-3" Text="" Visible="False"></asp:Label>
                    </span>
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
                        <td><%# Eval("NomeCompleto") %></td>
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
        </div>
    </div>

    <%--Modal--%>
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
