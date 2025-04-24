<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="InserimentoFacolta.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>



<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   <%-- <div class="container">
        <asp:GridView runat="server" id="grdView" AllowPaging="True" AllowSorting="True" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" DataKeyNames="K_Facolta" GridLines="Vertical">

            <alternatingrowstyle backcolor="#DCDCDC" />

            <footerstyle backcolor="#CCCCCC" forecolor="Black" />
            <headerstyle backcolor="#000084" font-bold="True" forecolor="White" />
            <pagerstyle backcolor="#999999" forecolor="Black" horizontalalign="Center" />
            <rowstyle forecolor="Black" backcolor="#EEEEEE" />
            <selectedrowstyle backcolor="#008A8C" font-bold="True" forecolor="White" />
            <sortedascendingcellstyle backcolor="#F1F1F1" />
            <sortedascendingheaderstyle backcolor="#0000A9" />
            <sorteddescendingcellstyle backcolor="#CAC9C9" />
            <sorteddescendingheaderstyle backcolor="#000065" />

        </asp:GridView>

    </div>--%>
    <div class="container">
        <table>
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="Titolo Facoltà: "></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTitoloFacolta" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td></td>
                <td></td>
            </tr>
            <tr>
                <td></td>
                <td class="adx">
                    <asp:Button ID="btnInserisci" runat="server" Text="Inserisci" OnClick="btnInserisci_Click" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

