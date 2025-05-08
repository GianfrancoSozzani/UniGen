<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Contatti.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   
    <div class="container mt-2">

    <!-- TITOLO -->
    <div class="py-4 mb-4 border-bottom">
        <h2 class="text-start mb-1">Contatti</h2>
    </div>

    <!-- RESTO DELLA PAGINA -->
    <div class="row">
        <!-- Colonna sinistra: contatti -->
        <div class="col-md-8">
            <div class="d-flex flex-column align-items-start">
                <div class="w-100">
                    <h4 class="fw-normal mt-4">Centralino</h4>
                    <h5 class="fw-light">Tel +39 0123 456 789<br>
                        email: centralino@unigen.it
                    </h5>

                    <h4 class="fw-normal mt-4">URP Unità Relazioni con il Pubblico</h4>
                    <h5 class="fw-light">Tel +39 0123 456 789<br>
                        email: urp@unigen.it
                    </h5>

                    <h4 class="fw-normal mt-4">Studenti e Post-laurea</h4>
                    <h5 class="fw-light">Tel +39 0123 456 789<br>
                        email: studentipostlaurea@unigen.it
                    </h5>

                    <h4 class="fw-normal mt-4">Ricerca</h4>
                    <h5 class="fw-light">Tel +39 0123 456 789<br>
                        email: ricerca@unigen.it
                    </h5>

                    <h4 class="fw-normal mt-4">International</h4>
                    <h5 class="fw-light">Tel +39 0123 456 789<br>
                        email: international@unigen.it<br>
                        Studenti stranieri Erasmus: erasmus@unigen.it
                    </h5>
                </div>
            </div>
        </div>

        <!-- Colonna destra: mappa -->
        <div class="col-md-4">
            <div class="d-flex justify-content-end">
                <div class="border rounded shadow-sm mt-4" style="width: 100%;">
                    <h5 class="text-center g-2">Dove ci troviamo</h5>
                    <iframe src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d11185.819500467893!2d9.211718482826837!3d45.50092108719124!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x4786c719ac51810b%3A0xbf03100040d10f0d!2s20100%20Milan%2C%20Metropolitan%20City%20of%20Milan!5e0!3m2!1sen!2sit!4v1746616181626!5m2!1sen!2sit" width="100%" height="450" style="border: 0;" allowfullscreen="" loading="lazy" referrerpolicy="no-referrer-when-downgrade"></iframe>
                </div>
            </div>
        </div>
    </div>
</div>kd


</asp:Content>

