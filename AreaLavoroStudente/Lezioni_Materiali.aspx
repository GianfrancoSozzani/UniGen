<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Lezioni_Materiali.aspx.cs" Inherits="Default2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container mt-5">

        <!-- Intestazione Facoltà e Corso -->
        <div class="row mb-4">
            <div class="col-md-6">
                <asp:Label ID="lblFacolta" runat="server" CssClass="form-label fs-4 fw-bold"></asp:Label>
            </div>
            <div class="col-md-6 text-md-end">
                <asp:Label ID="lblCorso" runat="server" CssClass="form-label fs-4 fw-bold"></asp:Label>
            </div>
        </div>

        <!-- Dropdown selezione esame -->
        <div class="mb-4">
            <asp:DropDownList ID="ddlCaricaEsami" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlCaricaEsami_SelectedIndexChanged"></asp:DropDownList>
        </div>

        <!-- Videolezioni -->
        <div class="row mb-5">
            <div class="col-md-12">
                <h4 class="mb-3">Videolezioni</h4>
                <asp:Repeater ID="rptVideolezioni" runat="server">
                    <HeaderTemplate>
                        <div class="card">
                            <div class="card-body">
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div class="mb-3">
                            <h5 class="card-title"><%# Eval("Titolo") %></h5>
                            <a href='<%# ResolveUrl(Eval("Video").ToString()) %>' target="_blank" class="btn btn-outline-primary">Guarda lezione</a>
                        </div>
                    </ItemTemplate>
                    <FooterTemplate>
                        </div>
                        <!-- card-body -->
                        </div>
                        <!-- card -->
                    </FooterTemplate>
                </asp:Repeater>



                <!-- Messaggio se non ci sono videolezioni -->
                <asp:Label ID="lblMessaggio1" runat="server" CssClass="text-danger fs-5 mt-3 d-block" Visible="false" Text="Non ci sono videolezioni disponibili per il tuo corso."></asp:Label>
            </div>
        </div>

        <!-- Dispense-->
        <div class="row mb-5">
            <div class="col-md-12">
                <h4 class="mb-3">Elenco Dispense</h4>
                <asp:Repeater ID="rptDispense" runat="server">
                    <HeaderTemplate>
                        <div class="card">
                            <div class="card-body">
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div class="d-flex justify-content-between align-items-center mb-2 border-bottom pb-2">
                            <h5 class="mb-0"><%# Eval("Titolo") %></h5>
                            <asp:HiddenField ID="hfEsame" runat="server" Value='<%# Eval("K_Materiale") %>' />
                            <asp:Button
                                ID="btnScarica"
                                runat="server"
                                Text="Download"
                                CssClass="btn btn-sm btn-outline-primary"
                                CommandName="Scarica"
                                CommandArgument='<%# Eval("K_Materiale") %>'
                                OnCommand="btnScarica_Command" />
                        </div>
                    </ItemTemplate>
                    <FooterTemplate>
                        </div>
                        
                        </div>
                        
                    </FooterTemplate>
                </asp:Repeater>



                <!-- Messaggio se non ci sono videolezioni -->
                <asp:Label ID="lblMessaggio" runat="server" CssClass="text-danger fs-5 mt-3 d-block" Visible="false" Text="Non ci sono dispense disponibili per il tuo corso."></asp:Label>
            </div>
        </div>

     

</asp:Content>
