<%--<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="IncassoAnnuale.aspx.cs" Inherits="_Default" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<h1 class="text-center mb-5">Sezione Incassi</h1>

<div class="row align-items-center">
    <!-- Colonna per il grafico (a sinistra) -->
    <div class="col-md-6 d-flex justify-content-center mb-4 mb-md-0">
        <asp:Chart ID="Chart1" runat="server" Width="800px" Height="500px">
            <ChartAreas>
                <asp:ChartArea Name="ChartArea1" />
            </ChartAreas>
            <Series>
                <asp:Series Name="Series1" ChartType="Column" />
            </Series>
        </asp:Chart>
    </div>

    <!-- Sezione a destra -->
    <div class="col-md-6 d-flex justify-content-center">
        <div class="border p-5 rounded shadow w-75">
            <h5 class="text-center mb-4">Seleziona Anno</h5>

            <div class="text-center mb-4">
                <asp:DropDownList 
                    ID="ddlAnno" 
                    runat="server" 
                    CssClass="form-control w-75 d-inline-block" 
                    AutoPostBack="true" 
                    OnSelectedIndexChanged="ddlAnno_SelectedIndexChanged">
                </asp:DropDownList>
            </div>

            <div class="text-center">
                <asp:Button 
                    ID="btnRiepilogo" 
                    runat="server" 
                    Text="Mostra Riepilogo" 
                    CssClass="btn btn-primary px-5 py-2" 
                    OnClick="btnRicerca_Click"
                    Style="box-shadow: 0px 4px 12px rgba(0,0,0,0.2); border-radius: 8px;" />
            </div>
        </div>
    </div>
</div>
</asp:Content>


</asp:Content>
--%>
