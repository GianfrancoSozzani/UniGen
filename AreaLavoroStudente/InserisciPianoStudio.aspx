<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="InserisciPianoStudio.aspx.cs" Inherits="InserisciPianoStudi" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style>
        .form-container { 
            max-width: 650px; 
            margin: 0 auto; 
            padding: 20px;
        }
        .alert-message { 
            margin-bottom: 20px; 
        }
        .card {
            border-radius: 5px; /* Modificato per bordi meno arrotondati */
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
        }
        .card-header {
            border-top-left-radius: 5px; /* Modificato per bordi meno arrotondati */
            border-top-right-radius: 5px; /* Modificato per bordi meno arrotondati */
        }
        .form-label {
            font-weight: 600;
        }
        .form-check-label {
            margin-left: 5px;
        }
        .form-check-input {
            margin-top: 2px;
        }
        .btn {
            border-radius: 5px; /* Modificato per bordi meno arrotondati */
        }
        .btn-primary {
            background-color: #007bff;
            border-color: #007bff;
        }
        .btn-secondary {
            background-color: #6c757d;
            border-color: #6c757d;
        }
        .btn-danger {
            background-color: #dc3545;
            border-color: #dc3545;
        }
        .btn:hover {
            opacity: 0.9;
        }
        .float-end {
            float: right;
        }
        .container {
            padding-top: 30px;
            padding-bottom: 30px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="container mt-4">
        <div class="row">
            <!-- COLONNA SINISTRA -->
            <div class="col-12 col-lg-8 mb-4">
                <h2 class="fw-bold mb-4">Aggiungi Esame al Piano di Studio</h2>

                <!-- Messaggio di errore -->
                <asp:Panel ID="pnlMessaggio" runat="server" Visible="false" CssClass="alert alert-dismissible fade show alert-message">
                    <button type="button" class="btn-close" data-bs-dismiss="alert"></button>
                    <asp:Literal ID="litMessaggio" runat="server"></asp:Literal>
                </asp:Panel>

                <!-- Selezione Esame -->
                <div class="mb-3">
                    <label class="form-label">Seleziona Esame</label>
                    <asp:DropDownList ID="ddlEsame" runat="server" CssClass="form-select">
                        <asp:ListItem Text="-- Seleziona Esame --" Value=""></asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvEsame" runat="server" 
                        ControlToValidate="ddlEsame" ErrorMessage="Seleziona un esame" 
                        CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
                </div>

                <!-- Anno Accademico -->
                <div class="mb-3">
                    <label class="form-label">Anno Accademico</label>
                    <asp:TextBox ID="txtAnnoAccademico" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvAnnoAccademico" runat="server" 
                        ControlToValidate="txtAnnoAccademico" ErrorMessage="Inserisci l'anno accademico" 
                        CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
                </div>

                <!-- Bottoni -->
                <div class="d-flex justify-content-start gap-2 mt-3">
                    <asp:Button ID="btnSalva" runat="server" Text="Salva" CssClass="btn btn-primary" OnClick="btnSalva_Click" />
                    <asp:Button ID="btnAnnulla" runat="server" Text="Annulla" CssClass="btn btn-secondary" 
                        OnClick="btnAnnulla_Click" CausesValidation="false" />
                </div>
            </div>

            <!-- COLONNA DESTRA -->
            <div class="col-12 col-lg-4 mt-5">
                <div class="card shadow-sm">
                    <div class="card-body">
                        <h3 class="fw-bold mb-2">Come funziona?</h3>
                        <p class="text-muted fs-6">
                            In questa pagina puoi aggiungere un esame al tuo piano di studio.
                            <br><br>
                            Seleziona l'esame e l'anno accademico, poi clicca su <strong>“Salva”</strong>.
                        </p>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
</asp:Content>

