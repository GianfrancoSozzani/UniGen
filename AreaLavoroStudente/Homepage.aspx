<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Homepage.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container my-5" style="max-width: 960px;">
        <div class="mb-5">
            <h1 class="text-center">Pannello studente</h1>
        </div>

        <div class="row row-cols-1 row-cols-md-2 row-cols-lg-3 g-1 justify-content-center">
            <!-- Card 1 -->
            <div class="col d-flex justify-content-center">
                <div class="card shadow h-100" style="width: 16rem;">
                    <img src="resources/1.png" class="card-img-top" alt="Card 1 Image">
                    <div class="card-body d-flex flex-column">
                        <h5 class="card-title">Lezioni e materiali</h5>
                        <p class="card-text">Scarica le lezioni del tuo corso di studi e i relativi materiali.</p>
                        <a href="#" class="btn btn-primary mt-auto">Vai</a>
                    </div>
                </div>
            </div>

            <!-- Card 2 -->
            <div class="col d-flex justify-content-center">
                <div class="card shadow h-100" style="width: 16rem;">
                    <img src="resources/2.png" class="card-img-top" alt="Card 2 Image">
                    <div class="card-body d-flex flex-column">
                        <h5 class="card-title">Appelli</h5>
                        <p class="card-text">Gestisci e visualizza le prenotazioni agli esami.</p>
                        <a href="#" class="btn btn-primary mt-auto">Vai</a>
                    </div>
                </div>
            </div>

            <!-- Card 3 -->
            <div class="col d-flex justify-content-center">
                <div class="card shadow h-100" style="width: 16rem;">
                    <img src="resources/3.png" class="card-img-top" alt="Card 3 Image">
                    <div class="card-body d-flex flex-column">
                        <h5 class="card-title">Piano di studio</h5>
                        <p class="card-text">Gestisci e visualizza il tuo piano di studio.</p>
                        <a href="#" class="btn btn-primary mt-auto">Vai</a>
                    </div>
                </div>
            </div>

            <!-- Card 4 -->
            <div class="col d-flex justify-content-center">
                <div class="card shadow h-100" style="width: 16rem;">
                    <img src="resources/4.png" class="card-img-top" alt="Card 4 Image">
                    <div class="card-body d-flex flex-column">
                        <h5 class="card-title">La tua carriera</h5>
                        <p class="card-text">Visualizza gli esami verbalizzati, la tua media e i CFU conseguiti.</p>
                        <a href="#" class="btn btn-primary mt-auto">Vai</a>
                    </div>
                </div>
            </div>

            <!-- Card 5 -->
            <div class="col d-flex justify-content-center">
                <div class="card shadow h-100" style="width: 16rem;">
                    <img src="resources/5.png" class="card-img-top" alt="Card 5 Image">
                    <div class="card-body d-flex flex-column">
                        <h5 class="card-title">Pagamenti</h5>
                        <p class="card-text">Gestisci e visualizza i tuoi pagamenti.</p>
                        <a asp-area="" asp-controller="Materiali" asp-action="Add" class="btn btn-primary mt-auto">Vai</a>
                    </div>
                </div>
            </div>

            <!-- Card 6 -->
            <div class="col d-flex justify-content-center">
                <div class="card shadow h-100" style="width: 16rem;">
                    <img src="resources/6.png" class="card-img-top" alt="Card 6 Image">
                    <div class="card-body d-flex flex-column">
                        <h5 class="card-title">Comunicazioni</h5>
                        <p class="card-text">Accedi alle tue comunicazioni relative al tuo corso e ai tuoi esami.</p>
                        <a href="#" class="btn btn-primary mt-auto">Vai</a>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
