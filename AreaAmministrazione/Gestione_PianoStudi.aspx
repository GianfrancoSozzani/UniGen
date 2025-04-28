<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Gestione_PianoStudi.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style>
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="container">
    <h1 class="mb-3">Gestione Piano di Studi</h1>

    <h2>Inserimento piano di studio</h2>
   <%-- ----------------DDL FACOLTA e CORSO---------------------%>
    <div class="row w-25 me-2">
        <asp:Label ID="lblFacolta" runat="server" Text="Facoltà"></asp:Label>
    <asp:DropDownList ID="ddlFacolta" runat="server"></asp:DropDownList>
</div>
    <div class="row w-25 mb-3"
        <asp:Label ID="lblCorso" runat="server" Text="Corso di laurea"></asp:Label>
        <asp:DropDownList ID="ddlCorso" runat="server"></asp:DropDownList>
    </div>
    
   <%-- ----------------SELEZIONA ESAMI OBBLIGATORI---------------------%>
    <div class="row">
          <div class="col-6 mb-3"> 
      <h4>Seleziona esami obbligatori</h4>
      <form class="d-flex" role="search">
  <input class="form-control" type="search" placeholder="Search" aria-label="Cerca">
  <button class="btn btn-outline-success" type="submit">Cerca</button>
</form>


        <asp:ListBox ID="ListBox1" runat="server" DataSourceID="SqlEsami" DataTextField="TitoloEsame" DataValueField="TitoloEsame" Width="277px"></asp:ListBox>
        <asp:SqlDataSource ID="SqlEsami" runat="server" ConnectionString="<%$ ConnectionStrings:GENERATIONConnectionString %>" 
            ProviderName="<%$ ConnectionStrings:GENERATIONConnectionString.ProviderName %>" 
            SelectCommand="SELECT [TitoloEsame], [CFU] FROM [ESAMI] ORDER BY [TitoloEsame]"></asp:SqlDataSource>

        <asp:Button ID="btnAggiungiEsame" runat="server" Text="Aggiungi" OnClick="btnAggiungiEsame_Click" />


  </div>
          <%-- ----------------SELEZIONA ESAMI FACOLTATIVI---------------------%>
        <div class="col-6 mb-3"> 
            <h4>Seleziona esami facoltativi</h4>
            <form class="d-flex" role="search">
        <input class="form-control me-2" type="search" placeholder="Search" aria-label="Cerca">
        <button class="btn btn-outline-success" type="submit">Cerca</button>
      </form>
            
        <asp:ListBox ID="ListBox2" runat="server" DataSourceID="SqlEsami" DataTextField="TitoloEsame" DataValueField="TitoloEsame" Width="277px"></asp:ListBox>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:GENERATIONConnectionString %>" 
            ProviderName="<%$ ConnectionStrings:GENERATIONConnectionString.ProviderName %>" 
            SelectCommand="SELECT [TitoloEsame], [CFU] FROM [ESAMI] ORDER BY [TitoloEsame]"></asp:SqlDataSource>

        <asp:Button ID="Button1" runat="server" Text="Aggiungi" OnClick="btnAggiungiEsame_Click" />  <%--popup conferma aggiunta e colore dell'esame in grigio--%>

        </div>
       
    </div>
       

    <div>
   <a class="btn btn-primary mb-3" href="Home.aspx" role="button">Torna alla Home</a>
</div>
        </div>
</asp:Content>

