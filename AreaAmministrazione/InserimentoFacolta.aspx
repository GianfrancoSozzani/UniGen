<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="InserimentoFacolta.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="d-flex flex-column align-items-center mt-4">

        <h1>Inserimento Facoltà</h1>

        <div class="container mt-5 mb-5">
            <table class="table">
                <tr>
                    <td>
                        <asp:Label ID="Label1" runat="server" Text="Facoltà"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtFacolta" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <asp:Button ID="btnSalva" runat="server" Text="Salva" OnClick="btnSalva_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>

