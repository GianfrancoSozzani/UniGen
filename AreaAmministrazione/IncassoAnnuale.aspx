<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="IncassoAnnuale.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="container mt-4">
    <div class="row justify-content-center">
        <div class="col-md-8">
            <asp:Chart ID="Chart1" runat="server" Width="100%" Height="400px">
                <ChartAreas>
                    <asp:ChartArea Name="ChartArea1" />
                </ChartAreas>
                <Series>
                    <asp:Series Name="Series1" ChartType="Column" />
                </Series>
            </asp:Chart>
        </div>
    </div>
</div>
</asp:Content>

