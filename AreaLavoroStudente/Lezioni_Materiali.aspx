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
    </div>
    <asp:DropDownList ID="ddlCaricaEsami" runat="server" OnSelectedIndexChanged="ddlCaricaEsami_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>


    <div class="row">
        <div class="col-12">
            <h4 class="mb-3">Videolezioni</h4>

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
                        <!-- closes card-body -->
                    </div>    
                        <!-- closes card -->
                </FooterTemplate>
            </asp:Repeater>

            <!--Messaggio da visualizzare se non ci sono lezioni -->
       <%--     <asp:Label ID="lblMessaggio" runat="server" CssClass="text-danger fs-5" Visible="false"></asp:Label>
        </div>
    </div>--%>

    


    <%-- <div class="row">
            <div class="col-12">
                <h4 class="mb-3">Videolezioni</h4>
                <asp:Repeater ID="rptVideolezioni" runat="server">
                    <HeaderTemplate>
                        CardTitle - Nome LezioneCardBody - Nome Esame, Nome Corso, Nome Facoltà 
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div class="card mb-2">
                            <div class="card-body">
                                <h5 class="card-title"><%# Eval("Titolo") %></h5>
                                <a href='<%# ResolveUrl(Eval("Video").ToString()) %>' target="_blank" class="btn btn-primary">Guarda lezione</a>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>--%>

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
                        <!-- closes card-body -->
            </div>    
                        <!-- closes card -->
        </FooterTemplate>

    </asp:Repeater>



    <!--Messaggio da visualizzare se non ci sono lezioni -->
           <asp:Label ID="lblMessaggio" runat="server" CssClass="text-danger fs-5" Visible="false"></asp:Label>
            </div>
        </div>

    <%--        <div class="row mt-4">
            <div class="col-12">
                <h4 class="mb-3">Dispense</h4>
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
                </asp:Repeater>--%>

    <%--      </div>
        </div>
    </div>--%>
</asp:Content>

