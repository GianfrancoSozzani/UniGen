﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="IncassoPerCorso.aspx.cs" Inherits="_Default" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="container mt-3">
        <h1>Incassi per Facoltà</h1>

        <div class="row mt-5">
            <!-- Colonna per il grafico (a sinistra) -->
            <div class="col-md-6 d-flex justify-content-center mb-4 mb-md-0">
                <div class="w-100 d-flex justify-content-center bg-white rounded shadow" style="border-radius: 12px;">
                    <asp:Chart ID="Chart1" runat="server" Style="width: 100%; height: 100%;" Width="700px" Height="600px">
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
            <div class="col-md-6 d-flex justify-content-center">
                <div class="border d-flex flex-column p-5 rounded shadow w-100">
                    <div class="row justify-content-center mb-4">
                        <div class="col-5">
                            <h5 class="mb-4">Seleziona Anno</h5>
                            <asp:DropDownList
                                ID="ddlAnno"
                                runat="server"
                                CssClass="form-control py-1 px-1"
                                AutoPostBack="true"
                                OnSelectedIndexChanged="ddlAnno_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                        <div class="col-6">
                            <h5 class="mb-4">Seleziona Facoltà</h5>
                            <asp:DropDownList
                                ID="ddlFacolta"
                                runat="server"
                                CssClass="form-control py-1 px-2"
                                AutoPostBack="true"
                                OnSelectedIndexChanged="ddlFacolta_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
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
                            <HeaderTemplate>
                                <table class="table table-bordered table-striped table-hover">
                                    <thead class="thead-dark">
                                        <tr>
                                            <th><%# (facolta == Guid.Empty ? "Facoltà" : "Corso") %></th>
                                            <th>Incasso</th>
                                            <th>Iscritti</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                            </HeaderTemplate>

                            <ItemTemplate>
                                <tr>
                                    <td><%# Eval(facolta == Guid.Empty ? "Facolta" : "Corso") %></td>
                                    <td><%# String.Format("{0:C}", Eval("Importo")) %></td>
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
    </div>
</asp:Content>

