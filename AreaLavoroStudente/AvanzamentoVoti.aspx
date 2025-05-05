<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AvanzamentoVoti.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.5/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-SgOJa3DmI69IUzQ2PVdRZhwQ+dy64/BUtbMJw1MZ8t5HZApcHrRKUc4W0kG879m7" crossorigin="anonymous">

    <div class="modal fade" id="datiModal" tabindex="-1" aria-labelledby="datiModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="datiModalLabel">Attenzione! Dati non presenti</h5>
                </div>
                <div class="modal-footer">
                    <a href="Homepage.aspx" class="btn btn-primary" runat="server" onclick="window.location.href = 'Homepage.aspx';">Torna alla homepage</a>
                </div>
            </div>
        </div>
    </div>


    <%-- CONTENUTO PRINCIPALE --%>
    <div class="container mt-2 mb-2">
        <div class="row">
            <%-- COLONNA SINISTRA: TABELLONE ESAMI --%>
            <div class="col-12 col-lg-8 mb-4">
                 <h2 class="fw-bold mb-4">Prenotazione Appelli</h2>
                <p class="text-muted mb-4">In questa sezione puoi visualizzare l’elenco degli esami sostenuti, le relative votazioni e i dati riepilogativi del tuo percorso accademico.</p>

                <div class="table-responsive">
                    <table class="table table-striped table-bordered align-middle">
                        <thead class="table-light">
                            <tr>
                                <th>Esame</th>
                                <th>Votazione</th>
                                <th>Data</th>
                                <th>Data Verbalizzazione</th>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:Repeater ID="rptVoti" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td><%# Eval("TitoloEsame") %></td>
                                        <td><span class="badge text-bg-dark"><%# Eval("VotoEsame") %></span></td>
                                        <td><%# Eval("DataOrale", "{0:dd/MM/yyyy}") %></td>
                                        <td><%# Eval("DataVerbalizzazione", "{0:dd/MM/yyyy}") %></td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </tbody>
                    </table>
                </div>
            </div>

            <%--CFU E MEDIA --%>
            <div class="col-12 col-lg-4 mt-5">
                <div class="card">
                    <div class="card-body">
                        <h2 class="fw-bold mb-1">Statistiche carriera</h2>
                        <p class="text-muted mb-4 fs-6">Qui puoi consultare la media ponderata dei tuoi voti e il numero totale di crediti (CFU) ottenuti fino a questo momento.</p>

                        <table class="table table-borderless mb-4">
                            <thead>
                                <tr>
                                    <th class="fw-bold">Indicatore</th>
                                    <th class="fw-bold">Valore</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <td class="text-muted">Media ponderata:</td>
                                    <td>
                                        <asp:FormView ID="formMedia" runat="server">
                                            <ItemTemplate>
                                                <asp:Label ID="lblMedia" CssClass="fs-5" runat="server" Text='<%# Eval("MediaPonderata") %>' />
                                            </ItemTemplate>
                                        </asp:FormView>
                                        <asp:Label ID="lblMediaVuota" runat="server" CssClass="text-danger" Text="Dati non disponibili" Visible="true" />
                                    </td>
                                </tr>

                                <tr>
                                    <td class="text-muted">Totale CFU:</td>
                                    <td>
                                        <asp:FormView ID="formCFU" runat="server">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCFU" CssClass="fs-5" runat="server" Text='<%# Eval("TotaleCFU") %>' />
                                            </ItemTemplate>
                                        </asp:FormView>
                                        <asp:Label ID="lblCFUVuota" runat="server" CssClass="text-danger" Text="Dati non disponibili" Visible="true" />

                                    </td>
                                </tr>


                            </tbody>
                        </table>

                        <p class="text-muted fs-6 mb-0">La media ponderata calcola la media dei voti degli esami, tenendo conto del numero di crediti (CFU) associati a ciascun esame, attribuendo maggiore peso agli esami con più crediti.</p>
                    </div>
                </div>
            </div>







        </div>
    </div>
    
    <script>
        function showDatiModal() {
            const datiModal = new bootstrap.Modal(document.getElementById('datiModal'));
            datiModal.show();
            //window.location.href = 'Homepage.aspx';
        }
    </script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.5/dist/js/bootstrap.bundle.min.js" integrity="sha384-k6d4wzSIapyDyv1kpU366/PK5hCdSbCRGRCMv+eplOQJWyd1fbcAu9OCUj5zNLiq" crossorigin="anonymous"></script>
</asp:Content>







