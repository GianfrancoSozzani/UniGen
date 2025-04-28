<%@ Page Title="Home" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Home.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
         

        .layout-wrapper {
            display: flex;
            min-height: calc(100vh - 230px); /* Adatta in base all'altezza di header+footer */
        }

        .sidebar {
            width: 250px;
            background-color: #1b456d;
            color: white;
            padding-top: 1rem;
            flex-shrink: 0;
        }

        .sidebar a {
            display: block;
            padding: 12px 20px;
            color: white;
            text-decoration: none;
        }

        .sidebar a:hover {
            background-color: #0d2236;
        }

        .main-content {
            flex-grow: 1;
            padding: 20px;
            background-color: #f8f9fa;
        }
        img{
            width: -webkit-fill-available;
        }
        .btn:hover{
                         box-shadow: 13px 6px 5px 0px rgba(0,0,0,0.23);
-webkit-box-shadow: 13px 6px 5px 0px rgba(0,0,0,0.23);
-moz-box-shadow: 13px 6px 5px 0px rgba(0,0,0,0.23);
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="layout-wrapper">



        <!-- Contenuto principale -->
        <main class="main-content">
            <h1>Area Amministrazione</h1>

            <%-----------------RIEPILOGO STUDENTI ISCRITTI, INCASSO ANNO CORRENTE E CORSI ATTIVI---------------------%>
            <div class="row mt-5">
                <div class="col-md-4 mb-3">
                    <div class="card border-primary shadow-sm">
                        <div class="card-body">
                            <h5 class="card-title"><i class="bi bi-people-fill me-2"></i>Studenti iscritti</h5>
                            <p class="card-text display-6 fw-bold">
                                <asp:Literal ID="litStudentii" runat="server"></asp:Literal>
                            </p>

                        </div>
                    </div>
                </div>
                <div class="col-md-4 mb-3">
                    <div class="card border-success shadow-sm">
                        <div class="card-body">
                            <h5 class="card-title"><i class="bi bi-cash-stack me-2"></i>Incasso anno corrente</h5>
                            <p class="card-text display-6 fw-bold">
                                <asp:Literal ID="litIncassoC" runat="server"></asp:Literal>€
                            </p>
                        </div>
                    </div>
                </div>
                <div class="col-md-4 mb-3">
                    <div class="card border-info shadow-sm">
                        <div class="card-body">
                            <h5 class="card-title"><i class="bi bi-bar-chart-line me-2"></i>Corsi attivi</h5>
                            <p class="card-text display-6 fw-bold">
                                <asp:Literal ID="litCorsiA" runat="server"></asp:Literal>
                            </p>
                        </div>
                    </div>
                </div>
                <div class="col-md-4 mb-3">
                    <div class="card border-info shadow-sm">
                        <div class="card-body">
                            <h5 class="card-title"><i class="bi bi-people-fill me-2"></i>Docenti attivi</h5>
                            <p class="card-text display-6 fw-bold">
                                <asp:Literal ID="litDocenti" runat="server"></asp:Literal>
                            </p>
                        </div>
                    </div>
                </div>
                <div class="col-md-4 mb-3">
                    <div class="card border-info shadow-sm">
                        <div class="card-body">
                            <h5 class="card-title"><i class="bi bi-clipboard2 me-2"></i>Appelli esami mese corrente</h5>
                            <p class="card-text display-6 fw-bold">
                                <asp:Literal ID="litEsami" runat="server"></asp:Literal>
                            </p>
                        </div>
                    </div>
                </div>
                <div class="col-md-4 mb-3">
                    <div class="card border-info shadow-sm">
                        <div class="card-body">
                            <h5 class="card-title"><i class="bi bi-piggy-bank me-2"></i>Tassa media annua</h5>
                            <p class="card-text display-6 fw-bold">
                                <asp:Literal ID="litTassaM" runat="server">€</asp:Literal>
                            </p>
                        </div>
                    </div>
                </div>
            </div>
            <div>
                <asp:Button runat="server" Text="Aggiorna" class="btn btn-outline-primary" ID="btnAggiorna" OnClick="btnAggiorna_OnClick" />
            </div>

            <%----------------SHORTCUT PER AZIONI FREQUENTI---------------------%>
            <div class="mt-5">
                <h3>Azioni rapide</h3>
                <div class="btn-group">
                    <a class="btn btn-outline-primary" href="#"><i class="bi bi-plus-circle me-1"></i>Nuovo corso</a>
                    <a class="btn btn-outline-info" href="#"><i class="bi bi-person-plus me-1"></i>Nuovo studente</a>
                    <a class="btn btn-outline-success" href="#"><i class="bi bi-file-earmark-arrow-down me-1"></i>Esporta dati</a>
                </div>
            </div>


            <%----------------DOCUMENTI UTILI---------------------%>
            <div class="mt-5 mb-3 ">
                <h5>Documenti utili</h5>
                <ul class="list-group">
                    <li class="list-group-item"><i class="bi bi-file-earmark-pdf me-2 text-danger"></i><a href="resources/Regolamento_Didattico_2025_UniGen.pdf">Regolamento didattico 2025</a></li>
                    <li class="list-group-item"><i class="bi bi-file-earmark-pdf me-2 text-danger"></i><a href="resources/Linee%20guida%20esami.pdf">Linee guida esami</a></li>
                </ul>
            </div>

        </main>
    </div>

</asp:Content>
