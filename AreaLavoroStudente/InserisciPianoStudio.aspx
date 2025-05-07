<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="InserisciPianoStudio.aspx.cs" Inherits="InserisciPianoStudi" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.5/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-SgOJa3DmI69IUzQ2PVdRZhwQ+dy64/BUtbMJw1MZ8t5HZApcHrRKUc4W0kG879m7" crossorigin="anonymous" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="container mt-4">
        <div class="row">
            <!-- COLONNA SINISTRA -->
            <div class="col-12 col-lg-8 mb-4">
                <div class="py-4 border-bottom mb-3">
                    <h2 class="text-start mb-1">Aggiungi Esame al Piano di Studio</h2>
                </div>

                <!-- Messaggio di errore -->
                <asp:Panel ID="pnlMessaggio" runat="server" Visible="false" CssClass="alert alert-warning alert-dismissible fade show mb-4">
                    <button type="button" class="btn-close" data-bs-dismiss="alert"></button>
                    <asp:Literal ID="litMessaggio" runat="server"></asp:Literal>
                </asp:Panel>

                <!-- Selezione Esame -->
                <div class="mb-4">
                    <label class="form-label fw-semibold">Seleziona Esame</label>
                    <asp:DropDownList ID="ddlEsame" runat="server" CssClass="form-select">
                        <asp:ListItem Text="-- Seleziona Esame --" Value="" />
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvEsame" runat="server" 
                        ControlToValidate="ddlEsame" ErrorMessage="Seleziona un esame" 
                        CssClass="text-danger" Display="Dynamic" />
                </div>

                <!-- Bottoni -->
                <div class="d-flex gap-3 mt-4">
                    <asp:Button ID="btnSalva" runat="server" Text="Salva" CssClass="btn btn-primary" OnClick="btnSalva_Click" />
                    <asp:Button ID="btnAnnulla" runat="server" Text="Annulla" CssClass="btn btn-secondary" 
                        OnClick="btnAnnulla_Click" CausesValidation="false" />
                </div>
            </div>

            <!-- COLONNA DESTRA -->
            <div class="col-12 col-lg-4 mt-5">
                <div class="card shadow-sm">
                    <div class="card-body">
                        <h3 class="fw-bold mb-3">Come funziona?</h3>
                        <p class="text-muted fs-6">
                            In questa pagina puoi aggiungere un esame al tuo piano di studio.
                            <br /><br />
                            Seleziona l'esame e clicca su <strong>“Salva”</strong>.
                        </p>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>