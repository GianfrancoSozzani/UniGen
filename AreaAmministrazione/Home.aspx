<%@ Page Title="Home" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Home.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style>
         .bg-black.pb-1, .navbar-custom {
            display: none !important;
        }

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
        .btn{
                        box-shadow: 0px 4px 12px #21212115
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="layout-wrapper">
        <!-- Sidebar -->
        <nav class="sidebar box-shadow: 0px 4px 12px #21212115">
            <img src="resources/logo.png" />
            <h5 class="text-center mt-3">Menu</h5>
            <a href="Home.aspx"><i class="bi bi-house-door-fill me-2"></i> Dashboard</a>
            <a href="#"><i class="bi bi-person-check-fill"></i> Gestione studente</a>
            <a href="Gestione_Facolta.aspx"><i class="bi bi-mortarboard-fill"></i> Gestione facoltà</a>
            <a href="#"><i class="bi bi-clipboard2"></i> Gestione corsi</a>
            <a href="Gestione_PianoStudi.aspx"><i class="bi bi-book"></i> Gestione Piano Studi</a>
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
        <a href="#"><i class="bi bi-envelope"></i> Comunicazioni</a>    
        </nav>

        <!-- Contenuto principale -->
        <main class="main-content">
            <h1>Area Amministrazione</h1>
            
            <%-----------------RIEPILOGO STUDENTI ISCRITTI, INCASSO ANNO CORRENTE E CORSI ATTIVI---------------------%>
            <div class="row mt-5">
    <div class="col-md-4 mb-3">
        <div class="card border-primary shadow-sm">
            <div class="card-body">
                <h5 class="card-title"><i class="bi bi-people-fill me-2"></i> Studenti iscritti</h5>
                <p class="card-text display-6 fw-bold"> <asp:Literal ID="litStudenti" runat="server" ></asp:Literal></p>
               
            </div>
        </div>
    </div>
    <div class="col-md-4 mb-3">
        <div class="card border-success shadow-sm">
            <div class="card-body">
                <h5 class="card-title"><i class="bi bi-cash-stack me-2"></i> Incasso anno corrente</h5>
                 <p class="card-text display-6 fw-bold"> <asp:Literal ID="litIncasso" runat="server" ></asp:Literal>€</p>
            </div>
        </div>
    </div>
    <div class="col-md-4 mb-3">
        <div class="card border-info shadow-sm">
            <div class="card-body">
                <h5 class="card-title"><i class="bi bi-bar-chart-line me-2"></i> Corsi attivi</h5>
                <p class="card-text display-6 fw-bold"> <asp:Literal ID="litCorsi" runat="server" ></asp:Literal> </p>
            </div>
        </div>
    </div>
                 <div class="col-md-4 mb-3">
     <div class="card border-info shadow-sm">
         <div class="card-body">
             <h5 class="card-title"><i class="bi bi-bar-chart-line me-2"></i> Corsi attivi</h5>
             <p class="card-text display-6 fw-bold"> <asp:Literal ID="Literal1" runat="server" ></asp:Literal> </p>
         </div>
     </div>
 </div>
                 <div class="col-md-4 mb-3">
     <div class="card border-info shadow-sm">
         <div class="card-body">
             <h5 class="card-title"><i class="bi bi-bar-chart-line me-2"></i> Corsi attivi</h5>
             <p class="card-text display-6 fw-bold"> <asp:Literal ID="Literal2" runat="server" ></asp:Literal> </p>
         </div>
     </div>
 </div>
                 <div class="col-md-4 mb-3">
     <div class="card border-info shadow-sm">
         <div class="card-body">
             <h5 class="card-title"><i class="bi bi-bar-chart-line me-2"></i> Corsi attivi</h5>
             <p class="card-text display-6 fw-bold"> <asp:Literal ID="Literal3" runat="server" ></asp:Literal> </p>
         </div>
     </div>
 </div>
</div>
            <div>
                <asp:Button runat="server" Text="Aggiorna" class="btn btn-outline-primary" id="btnAggiorna" OnClick="btnAggiorna_OnClick" />
            </div>
           
         <%----------------SHORTCUT PER AZIONI FREQUENTI---------------------%>
            <div class="mt-5">
    <h3>Azioni rapide</h3>
    <div class="btn-group">
        <a class="btn btn-outline-primary" href="#"><i class="bi bi-plus-circle me-1"></i> Nuovo corso</a>
        <a class="btn btn-outline-info" href="#"><i class="bi bi-person-plus me-1"></i> Nuovo studente</a>
        <a class="btn btn-outline-success" href="#"><i class="bi bi-file-earmark-arrow-down me-1"></i> Esporta dati</a>
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
