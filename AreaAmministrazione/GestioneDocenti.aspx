<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="GestioneDocenti.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Repeater ID="rpDocenti" runat="server">
        <HeaderTemplate>
                <table class="table table-striped">
                    <thead>
                        <tr>
                            <th>Foto Profilo</th>
                            <th>Cognome</th>
                            <th>Nome</th>
                            <th>Stato</th>
                            <th>Azioni</th>
                        </tr>
                    </thead>
                    <tbody>
            </HeaderTemplate>

        <ItemTemplate>
        <asp:Button ID="btnAbilita" runat="server" Text="Abilita" Visible='<%# Eval("Abilitato") != null && string.Equals(Eval("Abilitato").ToString(), "S", StringComparison.OrdinalIgnoreCase) %>' CommandName="Abilita" CommandArgument='<%# Eval("K_Docente") %>' OnCommand="Selected_Command"/>
        <asp:Button ID="btnDisabilita" runat="server" Text="Disabilita" Visible='<%# Eval("Abilitato") != null && string.Equals(Eval("Abilitato").ToString(), "N", StringComparison.OrdinalIgnoreCase) %>' CommandName="Disabilita" CommandArgument='<%# Eval("K_Docente") %>' OnCommand="Selected_Command"/>
    </ItemTemplate>
    </asp:Repeater>
</asp:Content>

