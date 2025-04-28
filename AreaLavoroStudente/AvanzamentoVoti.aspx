<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AvanzamentoVoti.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.5/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-SgOJa3DmI69IUzQ2PVdRZhwQ+dy64/BUtbMJw1MZ8t5HZApcHrRKUc4W0kG879m7" crossorigin="anonymous">
    
    <%--MODAL STUDENTE NON LOGGATO--%>
    <div class="modal fade" id="loginModal" tabindex="-1" aria-labelledby="loginModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="loginModalLabel">Utente non loggato</h5>
                </div>
                <div class="modal-body">
                    Non hai effettuato il login. Verrai reindirizzato alla pagina di login tra qualche secondo.
                </div>
                <div class="modal-footer">
                    <a href="Login.aspx" class="btn btn-primary">Vai al login ora</a>
                </div>
            </div>
        </div>
    </div>

    <div class="container">
        <p class="fs-2 fw-bold">La tua carriera</p>
        <div class="card-body">
            <asp:FormView ID="formCFU" runat="server">
                <ItemTemplate>
                    <asp:Label ID="lblCFU" CssClass="fs-4" runat="server" Text='<%# Eval("TotaleCFU") %>' />
                </ItemTemplate>
            </asp:FormView>
        </div>

        <div class="card-body">
            <asp:FormView ID="formMedia" runat="server">
                <ItemTemplate>
                    <asp:Label ID="lblMedia" CssClass="fs-4" runat="server" Text='Media ponderata:<%# Eval("MediaPonderata") %>' />
                </ItemTemplate>
            </asp:FormView>
        </div>


        <asp:Repeater ID="rptVoti" runat="server">
            <HeaderTemplate>
                <div class="row">
            </HeaderTemplate>

            <ItemTemplate>
                <div class="col-md-4 mb-4">
                    <div class="card shadow-sm h-100">
                        <div class="card-body">
                            <h5 class="card-title text-primary">
                                <%# Eval("TitoloEsame") %>
                            </h5>
                            <p class="card-text">
                                <strong>Voto:</strong>
                                <span class="badge bg-success"><%# Eval("VotoEsame") %></span>
                                <br />
                                <strong>CFU:</strong>
                                <%# Eval("CFU") %><br />
                                <strong>Data Esame:</strong>
                                <%# Eval("DataOrale", "{0:dd/MM/yyyy}") %><br />
                                <strong>Data Verbalizzazione:</strong>
                                <%# Eval("DataVerbalizzazione", "{0:dd/MM/yyyy}") %>
                            </p>
                        </div>
                    </div>
                </div>
            </ItemTemplate>

            <FooterTemplate>
                </div>
                <!-- fine row -->
            </FooterTemplate>
        </asp:Repeater>

    </div>
    <script>
    function showLoginModal() {
        const loginModal = new bootstrap.Modal(document.getElementById('loginModal'));
        loginModal.show();
       
        setTimeout(() => {
            window.location.href = 'Login.aspx';
        }, 10000);
    }
    </script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.5/dist/js/bootstrap.bundle.min.js" integrity="sha384-k6d4wzSIapyDyv1kpU366/PK5hCdSbCRGRCMv+eplOQJWyd1fbcAu9OCUj5zNLiq" crossorigin="anonymous"></script>
</asp:Content>







