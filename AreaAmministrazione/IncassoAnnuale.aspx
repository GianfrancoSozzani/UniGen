<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="IncassoAnnuale.aspx.cs" Inherits="_Default" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="container mt-3">
        <h1>Incassi Annuali</h1>

        <div class="row mt-5">
            <!-- Colonna per il grafico (a sinistra) -->
            <div class="col-md-6 d-flex justify-content-center">
                <div class="w-100 d-flex justify-content-center bg-white rounded shadow" style="border-radius: 12px;">
                    <asp:Chart ID="Chart1" runat="server" style="width: 100%; height: 100%;" Width="700px" Height="600px">
                        <chartareas>
                            <asp:ChartArea Name="ChartArea1" />
                        </chartareas>
                        <series>
                            <asp:Series Name="Series1" ChartType="Column" />
                        </series>
                    </asp:Chart>
                </div>
            </div>


            <!-- Sezione a destra -->
            <div class="col-md-6 d-flex justify-content-center">
                <div class="border d-flex flex-column p-5 rounded shadow w-100">
                    <h5 class="text-center mb-4">Seleziona Anno</h5>

                    <div class="text-center mb-4">
                        <asp:DropDownList
                            ID="ddlAnno"
                            runat="server"
                            CssClass="form-control w-25 d-inline-block py-1 px-1"
                            AutoPostBack="true"
                            OnSelectedIndexChanged="ddlAnno_SelectedIndexChanged">
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
                    <div class="text-center mt-4">
                        <asp:Repeater ID="rptIncassiFacolta" runat="server">
                            <headertemplate>
                                <table class="table table-bordered table-striped table-hover">
                                    <thead class="thead-dark">
                                        <tr>
                                            <th>Facoltà</th>
                                            <th>Incasso</th>
                                            <th>Iscritti</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                            </headertemplate>

                            <itemtemplate>
                                <tr>
                                    <td><%# Eval("Facolta") %></td>
                                    <td><%# String.Format("{0:C}", Eval("Importo")) %></td>
                                    <td><%# Eval("Iscritti") %></td>
                                </tr>
                            </itemtemplate>

                            <footertemplate>
                                </tbody>
                          </table>
                            </footertemplate>
                        </asp:Repeater>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
