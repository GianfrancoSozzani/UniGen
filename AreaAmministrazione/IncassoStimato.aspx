<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="IncassoStimato.aspx.cs" Inherits="_Default" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h1 class="ms-5 mb-5">Stima Incassi Annuali</h1>

    <div class="row align-items-center">
        <!-- Colonna per il grafico (a sinistra) -->
        <div class="col-md-6 d-flex justify-content-center mb-4 mb-md-0">
            <div class="p-3 bg-white rounded shadow" style="border-radius: 12px;">
                <asp:Chart ID="Chart1" runat="server" Width="500px" Height="380px">
                    <ChartAreas>
                        <asp:ChartArea Name="ChartArea1" />
                    </ChartAreas>
                    <Series>
                        <asp:Series Name="Series1" ChartType="Column" />
                    </Series>
                </asp:Chart>
            </div>
        </div>


        <!-- Sezione a destra -->
        <div class="col-md-6">
            <div class="border p-5 rounded shadow mb-4 ">
                <h5 class="text-center mb-4">Seleziona Facoltà</h5>
                <div class="d-flex justify-content-center">
                    <asp:DropDownList
                        ID="ddlFacolta"
                        runat="server"
                        CssClass="form-select form-select-sm mb-3"
                        Style="width: 200px;"
                        AutoPostBack="true"
                        OnSelectedIndexChanged="ddlFacolta_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>


                <%--<div class="text-center">
                    <asp:Button
                        ID="btnRiepilogo"
                        runat="server"
                        Text="Mostra Riepilogo"
                        CssClass="btn btn-primary px-5 py-2"
                        OnClick="btnRiepilogo_Click"
                        Style="box-shadow: 0px 4px 12px rgba(0,0,0,0.2); border-radius: 8px;" />
                </div>--%>
                <div class="mt-4">
                    <asp:Repeater ID="rptIncassiFacolta" runat="server">
                        <HeaderTemplate>
                            <table class="table table-bordered table-striped table-hover">
                                <thead class="thead-dark">
                                    <tr>
                                        <th><%# (facolta == Guid.Empty ? "Facoltà" : "Corso") %></th>
                                        <th>Incasso</th>
                                        <th>Stima</th>
                                        <th>Iscritti</th>
                                    </tr>
                                </thead>
                                <tbody>
                        </HeaderTemplate>

                        <ItemTemplate>
                            <tr>
                                <td><%# Eval(facolta == Guid.Empty ? "Facolta" : "Corso") %></td>
                                <td><%# String.Format("{0:C}", Eval("Incasso")) %></td>
                                <td><%# String.Format("{0:C}", Eval("Stima")) %></td>
                                <td><%# Eval("Iscritti") %></td>
                            </tr>
                        </ItemTemplate>

                        <FooterTemplate>
                            </tbody>
                </table>
                        </FooterTemplate>
                    </asp:Repeater>
                </div>
            </div>
        </div>

    </div>
</asp:Content>

