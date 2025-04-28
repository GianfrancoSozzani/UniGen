<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="IncassoAnnuale.aspx.cs" Inherits="_Default" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="mt-2">
    <h1 class="text-center mb-3">Incassi Annuali</h1>
    <div class="row">
        <!-- Colonna per il grafico (a sinistra) -->
        <div class="col-md-6">
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
        <div class="col-md-6">
            <div class="border p-3">
                <h5>Sezione a destra</h5>
                <p>DDL e Tabella da inserire.</p>
            </div>
        </div>
    </div>
</div>

</asp:Content>
