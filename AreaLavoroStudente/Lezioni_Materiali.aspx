
<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Lezioni_Materiali.aspx.cs" Inherits="Default2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        .card-body .card-title.custom-subtitle {
            font-weight: normal;
            font-size: 0.95rem;
            margin-bottom: 0.25rem;
        }

        .card h4 {
            font-weight: bold;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container mt-2">
        <!--TITOLO-->
        <div class="py-4 mb-4 border-bottom">
            <h2 class="text-start mb-1">Area Materiali</h2>

            <!-- Etichette dinamiche sotto al titolo -->
            <div class="d-flex text-start gap-2 flex-wrap fs-6 mb-2">
                <asp:Label ID="lblAnno" runat="server"></asp:Label>
                <span>-</span>
                <asp:Label ID="lblF" runat="server"></asp:Label>
                <span>-</span>
                <asp:Label ID="lblC" runat="server"></asp:Label>
            </div>

            <!-- Separatore mantenuto -->
            <hr />

            <div class="row align-items-start">
                <!-- COLONNA SINISTRA -->
                <div class="col-12 col-lg-8">
                    <!-- Dropdown esami -->
                    <h4 class="mb-3">Esame</h4>
                    <div class="mb-4">
                        <asp:DropDownList ID="ddlCaricaEsami" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlCaricaEsami_SelectedIndexChanged"></asp:DropDownList>
                    </div>

                    <!-- Videolezioni e Dispense -->
                    <div class="row mb-5">
                        <!-- Videolezioni -->
                        <div class="col-md-6 d-flex">
                            <div class="card w-100 h-100">
                                <div class="card-body">
                                    <h4 class="mb-3">Videolezioni</h4>
                                    <asp:Repeater ID="rptVideolezioni" runat="server">
                                        <ItemTemplate>
                                            <div class="d-flex justify-content-between align-items-center mb-2 border-bottom pb-2">
                                                <h5 class="card-title custom-subtitle mb-0"><%# Eval("Titolo") %></h5>
                                                <a href='<%# ResolveUrl(Eval("Video").ToString()) %>' target="_blank" class="btn btn-sm btn-primary">Guarda lezione</a>
                                            </div>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <asp:Label ID="lblMessaggio1" runat="server" CssClass="text-danger fs-5 mt-3 d-block" Visible="false" Text="Non ci sono videolezioni disponibili per il tuo esame"></asp:Label>
                                </div>
                            </div>
                        </div>

                        <!-- Dispense -->
                        <div class="col-md-6 d-flex">
                            <div class="card w-100 h-100">
                                <div class="card-body">
                                    <h4 class="mb-3">Elenco Dispense</h4>
                                    <asp:Repeater ID="rptDispense" runat="server">
                                        <ItemTemplate>
                                            <div class="d-flex justify-content-between align-items-center mb-2 border-bottom pb-2">
                                                <h5 class="card-title custom-subtitle ms-0"><%# Eval("Titolo") %></h5>
                                                <asp:Button
                                                    ID="btnScarica"
                                                    runat="server"
                                                    Text="Download"
                                                    CssClass="btn btn-sm btn-primary me-0"
                                                    CommandName="Scarica"
                                                    CommandArgument='<%# Eval("K_Materiale") %>'
                                                    Visible='<%# !string.IsNullOrEmpty(Eval("Titolo") as string) %>'
                                                    OnCommand="btnScarica_Command" />
                                            </div>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <asp:Label ID="lblMessaggio2" runat="server" CssClass="text-danger fs-5 mt-3 d-block" Visible="false" Text="Non ci sono dispense disponibili per il tuo esame"></asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <!-- COLONNA DESTRA: Card "Come funziona?" allineata alla dropdown -->
                <div class="col-12 col-lg-4 mt-0 mt-lg-5">
                    <div class="card shadow-sm w-100">
                        <div class="card-body">
                            <h3 class="fw-bold mb-2">Come funziona?</h3>
                            <p class="text-muted fs-6">
                                Questa pagina ti permette di consultare e scaricare i materiali didattici relativi agli esami del tuo piano di studi.
                                <br />
                                <br />
                                Seleziona l'esame dal menu a tendina per visualizzare le videolezioni disponibili e le dispense associate.
                            </p>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
