<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="IncassoPerCorso.aspx.cs" Inherits="_Default" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="row">
    <!-- Colonna per il grafico (a sinistra) -->
    <div class="col-md-6 d-flex justify-content-center">
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
        <div class="border p-4 rounded w-75">
            <h5 class="text-center mb-4">Selezioni</h5>

            <!-- Due DropDownList affiancate -->
            <div class="row mb-3">
                <div class="col-6">
                    <asp:DropDownList ID="ddlFacolta" runat="server" CssClass="form-control">
                    </asp:DropDownList>
                </div>
                <div class="col-6">
                    <asp:DropDownList ID="ddlCorso" runat="server" CssClass="form-control">
                    </asp:DropDownList>
                </div>
            </div>

            <!-- DropDownList centrale sotto -->
            <div class="text-center">
                <asp:DropDownList ID="ddlAnno" runat="server" CssClass="form-control w-50 d-inline-block">
                </asp:DropDownList>
            </div>
        </div>
    </div>
</div>
</asp:Content>

