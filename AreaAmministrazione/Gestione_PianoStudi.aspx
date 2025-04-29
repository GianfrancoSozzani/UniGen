<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Gestione_PianoStudi.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style>
    #container{
        margin-left: 50px;
    }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<div class="container">
    <h1 class="mb-3">Gestione Piano di Studi</h1>

    <h2>Inserimento piano di studio</h2>

     <%-- Dropdown Anno accademico --%>
 <div class="row w-25 me-2 mb-3">
     <asp:Label ID="Label1" runat="server" Text="Anno accademico"></asp:Label>
    <asp:DropDownList ID="ddlAnnoAccademico" runat="server" CssClass="form-control">
        <asp:ListItem Text="2024/2025" Value="2025/2026"></asp:ListItem>
        <asp:ListItem Text="2025/2026" Value="2025/2026"></asp:ListItem>
        <asp:ListItem Text="2026/2027" Value="2026/2027"></asp:ListItem>
        <asp:ListItem Text="2027/2028" Value="2027/2028"></asp:ListItem>
       
    </asp:DropDownList>
 </div>

    <%-- Dropdown Facoltà --%>
    <div class="row w-25 me-2 mb-3">
        <asp:Label ID="lblFacolta" runat="server" Text="Facoltà"></asp:Label>
        <asp:DropDownList ID="ddlFacolta" runat="server"></asp:DropDownList>
    </div>

    <%-- Dropdown Corso --%>
    <div class="row w-25 mb-3">
        <asp:Label ID="lblCorso" runat="server" Text="Corso di laurea"></asp:Label>
        <asp:DropDownList ID="ddlCorso" runat="server"></asp:DropDownList>
    </div>

    <div class="row">
       

        <%-- Sezione Esami Facoltativi --%>
            <h4>Seleziona esami</h4>
        <div class="col-8 mb-3">

            <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control w-75" placeholder="Cerca esame"></asp:TextBox>
            <asp:Button ID="btnSearch" runat="server" Text="Cerca" CssClass="btn btn-outline-success my-2" />
        </div>
            <div class="col-4">

            </div>

    </div>
    <div class="row">



            <%--<asp:ListBox ID="ListBox2" runat="server" CssClass="form-control mb-6 w-75" DataSourceID="SqlDataSource1" DataTextField="TitoloEsame" DataValueField="TitoloEsame" Width="277px"></asp:ListBox>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:GENERATIONConnectionString %>" 
                ProviderName="<%$ ConnectionStrings:GENERATIONConnectionString.ProviderName %>"
                SelectCommand="SELECT [TitoloEsame], [CFU] FROM [ESAMI] ORDER BY [TitoloEsame]">
            </asp:SqlDataSource>--%>
        <asp:Repeater ID="rpEsami" runat="server">
    <HeaderTemplate>
        <div class="container mt-4">
            <table class="table">
                    <tr>
                        <th>Esami</th>
                        <th>CFU</th>
                        <th>Azioni</th>
                        <th>Obbligatorio</th>
                    </tr>
                <tbody>
    </HeaderTemplate>

    <ItemTemplate>
        <tr>
            <td><%# Eval("TitoloEsame") %></td>
            <td><%# Eval("CFU") %></td>
            <td>
                <asp:Button ID="btnSeleziona" runat="server" Text="Seleziona" />
            </td>
            <td><input class="form-check-input" type="checkbox" role="switch" id="switchCheckDefault"></td>
        </tr>
    </ItemTemplate>

    <FooterTemplate>
        </tbody>
        </table>
    </div>
    </FooterTemplate>
</asp:Repeater>



    </div>

    <div class="row w-25">
        <a class="btn btn-primary mb-3" href="Home.aspx" role="button">Torna alla Home</a>
    </div>

</div>

</asp:Content>
