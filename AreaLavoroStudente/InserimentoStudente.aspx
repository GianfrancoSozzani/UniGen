<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InserimentoStudente.aspx.cs" Inherits="InserimentoStudente" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:FileUpload ID="fuFoto" runat="server" />
            <asp:Button ID="btnCarica" runat="server" Text="Carica" OnClick="btnCarica_Click"/>
            <hr />
            <asp:Button ID="btnLeggi" runat="server" Text="Mostra" OnClick="btnLeggi_Click" />
            <asp:Literal ID="lit" runat="server"></asp:Literal>





        </div>
    </form>
</body>
</html>
