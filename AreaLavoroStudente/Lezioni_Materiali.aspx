<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Lezioni_Materiali.aspx.cs" Inherits="Default2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet">
   
    <div class="container mt-4">
        <div class="row">
            <!-- COLONNA SINISTRA: TABELLONE ESAMI -->
            <div class="col-12 col-lg-8 mb-4">
                <!-- Titolo sezione -->
                <h2 class="fw-bold mb-4">Lezioni e materiali</h2>
                
                <!-- Etichette facoltà e corso -->
                <div class="row mb-3">
                    <div class="col-md-6">
                        <asp:Label ID="lblFacolta" runat="server" CssClass="form-label fs-4 fw-bold"></asp:Label>
                    </div>
                    <div class="col-md-6">
                        <asp:Label ID="lblCorso" runat="server" CssClass="form-label fs-4 fw-bold"></asp:Label>
                    </div>
                </div>

                <!-- Dropdown Esami -->
                <asp:DropDownList ID="ddlCaricaEsami" runat="server" OnSelectedIndexChanged="ddlCaricaEsami_SelectedIndexChanged" AutoPostBack="true" CssClass="form-select mb-3"></asp:DropDownList>

                <!-- Tabella lezioni -->
                <asp:Repeater ID="rptVideolezioni" runat="server">
                    <HeaderTemplate>
                        <div class="card mb-2">
                            <div class="card-body">
                    </HeaderTemplate>

                    <ItemTemplate>
                        <div class="mb-3">
                            <h5 class="card-title"><%# Eval("Titolo") %></h5>
                            <a href='<%# ResolveUrl(Eval("Video").ToString()) %>' target="_blank" class="btn btn-primary">Guarda lezione</a>
                        </div>
                    </ItemTemplate>

                    <FooterTemplate>
                        </div>
                        </div> <!-- closes card-body -->
                    </FooterTemplate>
                </asp:Repeater>

                <!-- Tabella dispense -->
                <asp:Repeater ID="rptDispense" runat="server">
                    <HeaderTemplate>
                        <div class="card mb-2">
                            <div class="card-header">
                                <strong>Elenco Dispense</strong>
                            </div>
                            <div class="card-body">
                    </HeaderTemplate>

                    <ItemTemplate>
                        <div class="d-flex justify-content-between align-items-center mb-2">
                            <h5 class="card-title mb-0"><%# Eval("Titolo") %></h5>
                            <asp:HiddenField ID="hfEsame" runat="server" Value='<%# Eval("K_Materiale") %>' />
                            <asp:Button
                                ID="btnScarica"
                                runat="server"
                                Text="Download"
                                CommandName="Scarica"
                                CommandArgument='<%# Eval("K_Materiale") %>'
                                OnCommand="btnScarica_Command" />
                        </div>
                    </ItemTemplate>

                    <FooterTemplate>
                        </div>
                        </div> <!-- closes card-body -->
                    </FooterTemplate>
                </asp:Repeater>

                <!-- Messaggio in caso di assenza materiale -->
                <asp:Label ID="lblMessaggio" runat="server" CssClass="text-danger fs-5" Visible="false"></asp:Label>
            </div>

            <!-- COLONNA DESTRA: Card informativa -->
            <div class="col-12 col-lg-4 mt-5">
                <div class="card shadow-sm">
                    <div class="card-body">
                        <h3 class="fw-bold mb-2">Come funziona?</h3>
                        <p class="text-muted fs-6">
                            In questa sezione puoi accedere alle videolezioni e scaricare le dispense relative ai tuoi esami.
                            <br><br>
                            Seleziona un esame dal menu a tendina per visualizzare il materiale disponibile.
                        </p>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
