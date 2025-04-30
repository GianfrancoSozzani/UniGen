<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Homepage.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .card-small {
            width: 14rem;
        }

        .card-img-top {
            height: 100px;
            object-fit: contain;
        }

        .card-title,
        .card-text,
        .card-body a {
            text-align: center;
        }

        .card-title {
            font-size: 1rem;
        }

        .card-text {
            font-size: 0.85rem;
        }

        @media (max-width: 1200px) {
            .flex-wrap-cards {
                flex-wrap: wrap;
            }
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <!--TITOLO-->
    <div class="container my-5">
        <%--<div class="mt-1" style="margin-top: -50px !important;">
        </div>--%>

        <!--TITOLO-->
        <div class="mb-1">
            <h2 class="text-start">Pannello studente</h2>
            <div class="d-flex text-start gap-2 my-3 flex-wrap  fs-6">
                <asp:Label ID="lblAnno" runat="server"></asp:Label>
                <span>-</span>
                <asp:Label ID="lblCorso" runat="server"></asp:Label>
                <span>-</span>
                <asp:Label ID="lblFacolta" runat="server"></asp:Label>
            </div>
        </div>

        <div class="d-flex flex-wrap-cards justify-content-center gap-3">
            <!-- LEZIONI/MATERIALI (solo abilitati)-->
            <!-- da collegare a pagina lezioni e materiali-->
            <div id="divLezioni" runat="server" class="card shadow-sm card-small">
                <img src="resources/1.png" class="card-img-top" alt="Lezioni e materiali">
                <div class="card-body d-flex flex-column">
                    <h5 class="card-title">Lezioni e materiali</h5>
                    <p class="card-text">Scarica le lezioni del tuo corso di studi e i relativi materiali.</p>
                    <a href="#" class="btn btn-primary mt-auto">Vai</a>
                </div>
            </div>

            <!-- APPELLI (solo abilitati)-->
            <div id="divAppelli" runat="server" class="card shadow-sm card-small">
                <img src="resources/2.png" class="card-img-top" alt="Appelli">
                <div class="card-body d-flex flex-column">
                    <h5 class="card-title">Appelli</h5>
                    <p class="card-text">Gestisci e visualizza le prenotazioni agli esami.</p>
                    <a href="#" class="btn btn-primary mt-auto">Vai</a>
                </div>
            </div>

            <!-- PIANO DI STUDIO-->
            <!-- da collegare a pagina visualizzazione piano studio-->
            <div class="card shadow-sm card-small">
                <img src="resources/3.png" class="card-img-top" alt="Piano di studio">
                <div class="card-body d-flex flex-column">
                    <h5 class="card-title">Piano di studio</h5>
                    <p class="card-text">Visualizza il tuo piano di studio.</p>
                    <a href="#" class="btn btn-primary mt-auto">Vai</a>
                </div>
            </div>

            <!-- CARRIERA -->
            <!-- da collegare a visualizzazione AvanzamentoVoti-->
            <div class="card shadow-sm card-small">
                <img src="resources/4.png" class="card-img-top" alt="Carriera">
                <div class="card-body d-flex flex-column">
                    <h5 class="card-title">La tua carriera</h5>
                    <p class="card-text">Visualizza gli esami verbalizzati, la tua media e i CFU conseguiti.</p>
                    <a href="AvanzamentoVoti.aspx" class="btn btn-primary mt-auto">Vai</a>
                </div>
            </div>

            <!-- PAGAMENTI -->
            <!-- da collegare a visualizzazione pagamenti -->
            <div class="card shadow-sm card-small">
                <img src="resources/5.png" class="card-img-top" alt="Pagamenti">
                <div class="card-body d-flex flex-column">
                    <h5 class="card-title">Pagamenti</h5>
                    <p class="card-text">Visualizza i tuoi pagamenti.</p>
                    <a href="#" class="btn btn-primary mt-auto">Vai</a>
                </div>
            </div>

            <!-- COMUNICAZIONI (solo abilitati)-->
            <div id="divComunicazioni" runat="server" class="card shadow-sm card-small">
                <img src="resources/6.png" class="card-img-top" alt="Comunicazioni">
                <div class="card-body d-flex flex-column">
                    <h5 class="card-title">Comunicazioni</h5>
                    <p class="card-text">Accedi alle comunicazioni relative al tuo corso e agli esami.</p>
                    <a href="#" class="btn btn-primary mt-auto">Vai</a>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
