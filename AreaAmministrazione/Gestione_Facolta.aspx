<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Gestione_Facolta.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h1>Lista Facoltà</h1>

    <div class="container mt-5">
        <h3>Elenco Facoltà</h3>
        <asp:Literal ID="litFacolta" runat="server"></asp:Literal>
    </div>

</asp:Content>

