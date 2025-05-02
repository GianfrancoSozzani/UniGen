<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Lezioni_Materiali.aspx.cs" Inherits="Default2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet">
    <div class="container mt-4">
        <div class="row mb-3">
            <div class="col-md-6">
                <asp:Label ID="lblFacolta" runat="server" CssClass="form-label fs-4 fw-bold"></asp:Label>
            </div>
            <div class="col-md-6">
                <asp:Label ID="lblCorso" runat="server" CssClass="form-label fs-4 fw-bold"></asp:Label>
            </div>
        </div>

        <div class="row">
            <div class="col-12">
                <h4 class="mb-3">Videolezioni</h4>
                <asp:Repeater ID="rptVideolezioni" runat="server">
                    <ItemTemplate>
                        <div class="card mb-2">
                            <div class="card-body">
                                <h5 class="card-title"><%# Eval("Titolo") %></h5>
                                <a href='<%# ResolveUrl(Eval("Video").ToString()) %>' target="_blank" class="btn btn-primary">Guarda lezione</a>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>

                <!--Messaggio da visualizzare se non ci sono lezioni -->
                <asp:Label ID="lblMessaggio" runat="server" CssClass="text-danger fs-5" Visible="false"></asp:Label>
            </div>
        </div>

        <div class="row mt-4">
            <div class="col-12">
                <h4 class="mb-3">Dispense</h4>
                <%-- <asp:Repeater ID="rptDispense" runat="server">
                    <ItemTemplate>
                        <div class="card mb-2">
                            <div class="card-body d-flex justify-content-between align-items-center">
                                <h5 class="card-title mb-0"><%# Eval("Titolo") %></h5>
                                <asp:Button ID="btnScarica" runat="server" Text="Download" OnCommand="btnScarica_Command" <%# Eval("K_Materiale") %> />
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>--%>
                <asp:Repeater ID="rptDispense" runat="server">
                    <ItemTemplate>
                        <div class="card mb-2">
                            <div class="card-body d-flex justify-content-between align-items-center">
                                <h5 class="card-title mb-0"><%# Eval("Titolo") %></h5>
                                <asp:Button
                                    ID="btnScarica"
                                    runat="server"
                                    Text="Download"
                                    CommandName="Scarica"
                                    CommandArgument='<%# Eval("K_Materiale") %>'
                                    OnCommand="btnScarica_Command" />
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>

            </div>
        </div>
    </div>
</asp:Content>

