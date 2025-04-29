<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Visualizzazione_PianoStudi.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.11.3/font/bootstrap-icons.min.css">
    <style>
        #container {
            margin-left: 50px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="container">


        <h2>Visualizza Piani di studio compilati</h2>

    </div>


    <%-- Visualizza piani --%>
    <div class="row">
        <div class="col-8 mb-3">

            <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control w-75" placeholder="Cerca per Corso di Laurea"></asp:TextBox>
            <asp:Button ID="btnSearch" runat="server" Text="Cerca" CssClass="btn btn-outline-success my-2" />
        </div>
        <div class="col-4">
        </div>
    </div>

    <div class="row">


        <asp:Repeater ID="rpPianiStudio" runat="server">
            <HeaderTemplate>
                <div class="container mt-4">
                    <table class="table">
                        <tr>
                            <th>Anno Accademico</th>
                            <th>Facoltà</th>
                            <th>Corso di Laurea</th>
                            <th>Tipologia</th>
                            <th>Esami</th>

                        </tr>
                        <tbody>
            </HeaderTemplate>

            <ItemTemplate>
                <tr>
                    <td><%# Eval("Anno Accademico") %></td>
                    <td><%# Eval("Facoltà") %></td>
                    <td><%# Eval("Corso di Laurea") %></td>
                    <td><%# Eval("Tipologia") %></td>
                    <td>
                        <div class="accordion-item">
                            <h2 class="accordion-header">
                                <%--<button class="accordion-button" type="button" data-bs-toggle="collapse" data-bs-target="#collapseOne" aria-expanded="true" aria-controls="collapseOne">
                                    <i class="bi bi-arrow-down-circle me-2"></i>Espandi
                                </button>--%>
                                <button class="accordion-button" type="button" data-bs-toggle="collapse"
                                    data-bs-target='<%# "#collapse_" + Eval("K_PianoStudio") %>'
                                    aria-expanded="false"
                                    aria-controls='<%# "collapse_" + Eval("K_PianoStudio") %>'>
                                    <i class="bi bi-arrow-down-circle me-2"></i>Espandi
                                </button>

                            </h2>
                            <div id='<%# "collapse_" + Eval("K_PianoStudio").ToString() %>' class="accordion-collapse collapse" data-bs-parent="#accordionExample">
                                <%--<div id="collapseOne" class="accordion-collapse collapse show" data-bs-parent="#accordionExample">--%>
                                <div class="accordion-body">
                                    <p>
                                        <asp:Repeater ID="rpEsami" runat="server">
                                            <ItemTemplate>
                                               
                                                <p><%# Eval("TitoloEsame") %> <%--( <%# Eval("Tipologia") %> )--%> </p>

                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </p>
                                </div>
                            </div>
                        </div>
                    </td>

                </tr>
            </ItemTemplate>

            <FooterTemplate>
                </tbody>
        </table>
    </div>
            </FooterTemplate>
        </asp:Repeater>



    </div>

    <div class="row w-25">
        <a class="btn btn-primary mb-3" href="Home.aspx" role="button">Torna alla Home</a>
    </div>

    </div>
</asp:Content>

