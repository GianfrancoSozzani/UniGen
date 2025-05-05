<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="FAQ.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!DOCTYPE html>




    <div class="container mt-5">
        <div class="row">

            <!-- COLONNA SINISTRA: FAQ -->
            <div class="col-12 col-lg-8">
                <h1 class="mb-4 text-center text-lg-start">Domande Frequenti (FAQ)</h1>

                <div class="accordion" id="faqAccordion">

                    <!-- FAQ 1 -->
                    <div class="accordion-item">
                        <h2 class="accordion-header" id="headingOne">
                            <button class="accordion-button" type="button" data-bs-toggle="collapse" data-bs-target="#collapseOne">
                                Come posso iscrivermi agli esami?
                            </button>
                        </h2>
                        <div id="collapseOne" class="accordion-collapse collapse show" data-bs-parent="#faqAccordion">
                            <div class="accordion-body">
                                Puoi iscriverti agli esami tramite il portale studenti, sezione “Appelli”. Ricorda di rispettare le scadenze!
                            </div>
                        </div>
                    </div>

                    <!-- FAQ 2 -->
                    <div class="accordion-item">
                        <h2 class="accordion-header" id="headingTwo">
                            <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#collapseTwo">
                                Dove trovo il calendario accademico?
                            </button>
                        </h2>
                        <div id="collapseTwo" class="accordion-collapse collapse" data-bs-parent="#faqAccordion">
                            <div class="accordion-body">
                                Il calendario accademico è disponibile sul sito ufficiale dell’università nella sezione "Calendari".
                            </div>
                        </div>
                    </div>

                    <!-- FAQ 3 -->
                    <div class="accordion-item">
                        <h2 class="accordion-header" id="headingThree">
                            <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#collapseThree">
                                A chi posso rivolgermi per problemi con la carriera universitaria?
                            </button>
                        </h2>
                        <div id="collapseThree" class="accordion-collapse collapse" data-bs-parent="#faqAccordion">
                            <div class="accordion-body">
                                Puoi contattare la segreteria studenti o il tutor didattico del tuo corso di laurea.
                            </div>
                        </div>
                    </div>

                    <!-- FAQ 4 -->
                    <div class="accordion-item">
                        <h2 class="accordion-header" id="headingFour">
                            <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#collapseFour">
                                Come posso fare rinuncia agli studi?
                            </button>
                        </h2>
                        <div id="collapseFour" class="accordion-collapse collapse" data-bs-parent="#faqAccordion">
                            <div class="accordion-body">
                                La rinuncia agli studi si effettua compilando il modulo specifico disponibile sul sito dell’università.
                Il modulo va consegnato alla segreteria studenti insieme a una marca da bollo e, in alcuni casi,
                al pagamento di eventuali tasse arretrate. Dopo la rinuncia, non potrai più sostenere esami o
                recuperare la carriera.
                            </div>
                        </div>
                    </div>

                </div>
            </div>

            <!-- COLONNA DESTRA: CARD INFORMATIVA -->
            <div class="col-12 col-lg-4 mt-5 mt-lg-5 pt-lg-5">
                <div class="card shadow-sm">
                    <div class="card-body">
                        <h3 class="fw-bold mb-2">Cos'è questa sezione?</h3>
                        <p class="text-muted fs-6">
                            La sezione FAQ (Frequently Asked Questions) raccoglie le domande più comuni che gli studenti universitari pongono durante il loro percorso accademico.
        <br>
                            <br>
                            Qui puoi trovare risposte rapide su esami, scadenze, carriera e procedure amministrative, senza dover contattare direttamente la segreteria.
                        </p>
                    </div>
                </div>
            </div>

        </div>
    </div>
     <div style="height: 14vh;">
     &nbsp
 </div>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js"></script>
</asp:Content>

