<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Prova.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="container mt-2 mb-4">
        <div class="py-4 border-bottom">
            <h2 class="text-start mb-1">Gestione Appelli</h2>
            <div class="d-flex text-start gap-2 flex-wrap fs-6 mb-2">
                <asp:Label ID="lblCorso" runat="server"></asp:Label>
                <span>-</span>
                <asp:Label ID="lblEsame" runat="server"></asp:Label>
                <span>-</span>
                <asp:Label ID="lblStudente" runat="server"></asp:Label>
                <span>-</span>
                <asp:Label ID="lblMatricola" runat="server"></asp:Label>
            </div>
        </div>
        <!-- RIGA: tabella e card allineate -->
        <div class="row align-items-start mt-4">


            <asp:Repeater ID="rptDomande" runat="server">
                <ItemTemplate>
                    <div class="card mb-3 shadow-sm">
                        <div class="card-body">
                            <h6 class="card-subtitle mb-2 text-muted">Domanda <%# Eval("NumeroDomanda") %></h6>
                            <p class="card-text"><%# Eval("Domanda") %></p>

                            <asp:TextBox
                                ID="txtRisposta"
                                runat="server"
                                TextMode="MultiLine"
                                CssClass="form-control"
                                Rows="4"
                                placeholder="Scrivi la tua risposta qui...">
                            </asp:TextBox>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
            <div class="text-center">
                <asp:Button ID="btnInviaRisposte" runat="server" Text="Invia Risposte" CssClass="btn btn-primary" OnClick="btnInviaRisposte_Click" />
            </div>
        </div>
        </div>
</asp:Content>

