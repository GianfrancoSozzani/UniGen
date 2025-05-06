<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Contatti.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="height: 11.7vh;"></div>
    <main class="container mt-2">
        <div class="row">
            <!-- Colonna sinistra: Contatti -->
            <div class="col-md-8">
                <div class="d-flex flex-column align-items-start">
                    <div class="w-100">
                        <h1 class="alert-dark mb-4">Contatti</h1>

                        <h4 class="alert-dark fw-normal mt-4">Centralino</h4>
                        <h5 class="fw-light">Tel +39 0123 456 789<br>
                            email: centralino@unigen.it
                        </h5>

                        <h4 class="alert-dark fw-normal mt-4">URP Unità Relazioni con il Pubblico</h4>
                        <h5 class="fw-light">Tel +39 0123 456 789<br>
                            email: urp@unigen.it
                        </h5>

                        <h4 class="alert-dark fw-normal mt-4">Studenti e Post-laurea</h4>
                        <h5 class="fw-light">Tel +39 0123 456 789<br>
                            email: studentipostlaurea@unigen.it
                        </h5>

                        <h4 class="alert-dark fw-normal mt-4">Ricerca</h4>
                        <h5 class="fw-light">Tel +39 0123 456 789<br>
                            email: ricerca@unigen.it
                        </h5>

                        <h4 class="alert-dark fw-normal mt-4">International</h4>
                        <h5 class="fw-light">Tel +39 0123 456 789<br>
                            email: international@unigen.it<br>
                            Studenti stranieri Erasmus: erasmus@unigen.it
                        </h5>
                    </div>
                </div>
            </div>

            <!-- Colonna destra: Mappa -->
            <div class="col-md-4">
                <div class="d-flex justify-content-end">
                    <div class="border rounded shadow-sm mt-4" style="width: 100%;">
                        <h5>Dove ci troviamo</h5>
                        <iframe src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d12071.048960148692!2d14.243788395393834!3d40.855143607443665!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x133b08f0393f7605%3A0x405e93e8c920f374!2sVia%20S.%20Gennaro%20Ad%20Antignano%2C%20123%2C%2080129%20Napoli%20NA!5e0!3m2!1sit!2sit!4v1746190462664!5m2!1sit!2sit"
                            width="100%"
                            height="450"
                            style="border: 0;"
                            allowfullscreen=""
                            loading="lazy"
                            referrerpolicy="no-referrer-when-downgrade"></iframe>
                    </div>
                </div>
            </div>
        </div>
        </main>

        <div style="height: 5vh;">
            &nbsp
        </div>
</asp:Content>

