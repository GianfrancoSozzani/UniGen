﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="FAQ.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container mt-2">
        <!-- HEADER -->
        <div class="py-4 mb-3 border-bottom">
            <h2 class="text-start mb-1">FAQ</h2>
        </div>

        <!-- ROW: Accordion e Card affiancati -->
        <div class="row align-items-start">
            <!-- COLONNA SINISTRA: FAQ -->
            <div class="col-12 col-lg-8 mb-4">
                <div class="accordion" id="faqAccordion">
                    <!-- FAQ 1 -->
                    <div class="accordion-item">
                        <h2 class="accordion-header" id="headingOne">
                            <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#collapseOne">
                                Come posso iscrivermi agli esami?
                            </button>
                        </h2>
                        <div id="collapseOne" class="accordion-collapse collapse" data-bs-parent="#faqAccordion">
                            <div class="accordion-body">
                                <strong>Puoi iscriverti agli esami tramite il portale studenti, sezione “Appelli”. Ricorda di rispettare le scadenze!</strong>
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
                                <strong>Il calendario accademico è disponibile sul sito ufficiale dell’università nella sezione "Calendari".</strong>
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
                                <strong>Puoi contattare la segreteria studenti o il tutor didattico del tuo corso di laurea.</strong>
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
                                <strong>La rinuncia agli studi si effettua compilando il modulo specifico disponibile sul sito dell’università.
                                Il modulo va consegnato alla segreteria studenti insieme a una marca da bollo e, in alcuni casi,
                                al pagamento di eventuali tasse arretrate. Dopo la rinuncia, non potrai più sostenere esami o
                                recuperare la carriera.</strong>
                            </div>
                        </div>
                    </div>

                    <!-- FAQ 5 -->
                    <div class="accordion-item">
                        <h2 class="accordion-header" id="headingFive">
                            <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#collapseFive">
                                Posso modificare il piano di studi?
                            </button>
                        </h2>
                        <div id="collapseFive" class="accordion-collapse collapse" data-bs-parent="#faqAccordion">
                            <div class="accordion-body">
                                <strong>Sì, puoi modificarlo nei periodi previsti dal calendario accademico, accedendo al portale studenti nella sezione "Piano carriera".</strong>
                            </div>
                        </div>
                    </div>

                    <!-- FAQ 6 -->
                    <div class="accordion-item">
                        <h2 class="accordion-header" id="headingSix">
                            <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#collapseSix">
                                Come recupero la password del portale studenti?
                            </button>
                        </h2>
                        <div id="collapseSix" class="accordion-collapse collapse" data-bs-parent="#faqAccordion">
                            <div class="accordion-body">
                                <strong>Puoi recuperarla cliccando su "Password dimenticata?" nella pagina di login del portale. Riceverai istruzioni via email.</strong>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <!-- COLONNA DESTRA: CARD -->
            <div class="col-12 col-lg-4 mb-4">
                <div class="card shadow-sm h-100">
                    <div class="card-body">
                        <h3 class="fw-bold mb-2">Cos'è questa sezione?</h3>
                        <p class="text-muted fs-6">
                            La sezione FAQ (Frequently Asked Questions) raccoglie le domande più comuni che gli studenti universitari pongono durante il loro percorso accademico.
                            <br><br>
                            Qui puoi trovare risposte rapide su esami, scadenze, carriera e procedure amministrative, senza dover contattare direttamente la segreteria.
                        </p>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div style="height: 14vh;">&nbsp;</div>
</asp:Content>
