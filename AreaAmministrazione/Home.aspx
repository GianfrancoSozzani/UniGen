<%@ Page Title="Home" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Home.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style>
        .layout-wrapper {
            display: flex;
            min-height: calc(100vh - 230px); /* Adatta in base all'altezza di header+footer */
        }

        .sidebar {
            width: 220px;
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
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="layout-wrapper">
        <!-- Sidebar -->
        <nav class="sidebar">
            <h5 class="text-center">Menu</h5>
            <a href="#"><i class="bi bi-house-door-fill me-2"></i> Home amministrazione</a>
            <a href="#"><i class="bi bi-person-check-fill"></i> Abilita/disabilita studente</a>
            <a href="#"><i class="bi bi-mortarboard-fill"></i> Gestione facoltà</a>
            <a href="#"><i class="bi bi-clipboard2"></i> Gestione corsi</a>
            <a href="#"><i class="bi bi-book"></i> Gestione Piano Studi</a>
            <a href="#"><i class="bi bi-people-fill"></i> Gestione Docenti</a>
            <div class="dropdown-sidebar">
    <a class="dropdown-toggle" href="#" data-bs-toggle="collapse" data-bs-target="#submenuEconomica" role="button" aria-expanded="false" aria-controls="submenuEconomica">
        <i class="bi bi-wallet2 me-2"></i> Gestione Economica
    </a>
    <div class="collapse" id="submenuEconomica">
        <a class="submenu-item" href="#"><i class="bi bi-cash-stack me-2"></i> Incasso Annuale</a>
        <a class="submenu-item" href="#"><i class="bi bi-receipt me-2"></i> Incasso per corso</a>
        <a class="submenu-item" href="#"><i class="bi bi-piggy-bank me-2"></i> Stima incassi dell'anno</a>
    </div>
</div>
            <div class="dropdown-sidebar">
    <a class="dropdown-toggle" href="#" data-bs-toggle="collapse" data-bs-target="#submenuStatistiche" role="button" aria-expanded="false" aria-controls="submenuEconomica">
        <i class="bi bi-bar-chart-line-fill me-2"></i> Statistiche studenti
    </a>
    <div class="collapse" id="submenuStatistiche">
        <a class="submenu-item" href="#"><i class="bi bi-people-fill"></i> Studenti laureati per anno</a>
        <a class="submenu-item" href="#"><i class="bi bi-receipt me-2"></i> Studenti iscritti per anno</a>
    </div>
</div>
           
        </nav>

        <!-- Contenuto principale -->
        <main class="main-content">
            <h1>Area amministrazione</h1>
            
            <%-----------------RIEPILOGO STUDENTI ISCRITTI, INCASSO ANNO CORRENTE E CORSI ATTIVI---------------------%>
            <div class="row">
    <div class="col-md-4 mb-3">
        <div class="card border-primary shadow-sm">
            <div class="card-body">
                <h5 class="card-title"><i class="bi bi-people-fill me-2"></i> Studenti iscritti</h5>
                <p class="card-text display-6 fw-bold">1.235</p>
            </div>
        </div>
    </div>
    <div class="col-md-4 mb-3">
        <div class="card border-success shadow-sm">
            <div class="card-body">
                <h5 class="card-title"><i class="bi bi-cash-stack me-2"></i> Incasso anno corrente</h5>
                <p class="card-text display-6 fw-bold">€ 285.000</p>
            </div>
        </div>
    </div>
    <div class="col-md-4 mb-3">
        <div class="card border-info shadow-sm">
            <div class="card-body">
                <h5 class="card-title"><i class="bi bi-bar-chart-line me-2"></i> Corsi attivi</h5>
                <p class="card-text display-6 fw-bold">42</p>
            </div>
        </div>
    </div>
</div>
           
         <%----------------SHORTCUT PER AZIONI FREQUENTI---------------------%>
            <div class="mt-5">
    <h3>Azioni rapide</h3>
    <div class="btn-group">
        <a class="btn btn-outline-primary" href="#"><i class="bi bi-plus-circle me-1"></i> Nuovo corso</a>
        <a class="btn btn-outline-secondary" href="#"><i class="bi bi-person-plus me-1"></i> Nuovo studente</a>
        <a class="btn btn-outline-success" href="#"><i class="bi bi-file-earmark-arrow-down me-1"></i> Esporta dati</a>
    </div>
</div>

        </main>
    </div>

</asp:Content>
