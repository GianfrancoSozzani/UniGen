<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ModificaFacolta.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="container d-flex flex-column align-items-center mt-5">
        <h1 class="mb-4 fw-bold">Modifica Facoltà</h1>

        <div class="card shadow p-4" style="max-width: 500px; width: 100%;">
            <div class="mb-3">
                <label for="txtFacolta" class="form-label fw-bold">Nome Facoltà</label>
                <asp:TextBox ID="txtFacolta" runat="server" CssClass="form-control"></asp:TextBox>
            </div>

            <div class="text-end">
                <asp:Button ID="btnSalva" runat="server" Text="Salva" CssClass="btn btn-primary" OnClick="btnSalva_Click" />
            </div>
        </div>

    </div>

</asp:Content>

