<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="GestioneFacolta2.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="d-flex flex-column align-items-center mt-4">

        <h1>Elenco delle facoltà</h1>

        <div class="row justify-content-center w-25">
            <div class="col">
                <asp:GridView ID="GrigliaFacolta" runat="server"
                    AutoGenerateColumns="False"
                    DataKeyNames="K_Facolta"
                    OnSelectedIndexChanged="GrigliaFacolta_SelectedIndexChanged"
                    CssClass="table table-striped table-bordered"
                    HeaderStyle-CssClass="table-primary"
                    RowStyle-CssClass="align-middle"
                    AlternatingRowStyle-CssClass="table-light">
                    <columns>
                        <asp:BoundField DataField="TitoloFacolta" HeaderText="Facoltà" SortExpression="TitoloFacolta" />
                        <asp:CommandField ButtonType="Button" HeaderText="" ShowHeader="true" ShowSelectButton="true" />
                    </columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>

