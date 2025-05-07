<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="InserisciPianoStudio.aspx.cs" Inherits="InserisciPianoStudi" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Aggiungi Esame - Piano di Studio</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container mt-2 mb-4">
        <!-- INTESTAZIONE CON DIVISORE (UGUALE A PIANOSTUDI) -->
        <div class="py-4 border-bottom">
            <h2 class="text-start mb-1">Aggiungi Esame al Piano di Studio</h2>
            <div class="d-flex text-start gap-2 flex-wrap fs-6 mb-2">
                <asp:Label ID="lblAnno" runat="server"></asp:Label>
                <span>-</span>
                <asp:Label ID="lblCorso" runat="server"></asp:Label>
                <span>-</span>
                <asp:Label ID="lblFacolta" runat="server"></asp:Label>
            </div>
        </div>

        <div class="row mt-4">
            <!-- COLONNA PRINCIPALE -->
            <div class="col-12 col-lg-8 mb-4">
                <!-- CONTENUTO ESISTENTE (INVARIATO) -->
                <asp:Panel ID="pnlMessaggio" runat="server" Visible="false" CssClass="alert alert-warning alert-dismissible fade show mb-4">
                    <button type="button" class="btn-close" data-bs-dismiss="alert"></button>
                    <asp:Literal ID="litMessaggio" runat="server"></asp:Literal>
                </asp:Panel>

                <div class="mb-4">
                    <label class="form-label fw-semibold">Seleziona Esame</label>
                    <asp:DropDownList ID="ddlEsame" runat="server" CssClass="form-select">
                        <asp:ListItem Text="-- Seleziona Esame --" Value="" />
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvEsame" runat="server"
                        ControlToValidate="ddlEsame" ErrorMessage="Seleziona un esame"
                        CssClass="text-danger" Display="Dynamic" />
                </div>

                <div class="d-flex gap-3 mt-4">
                    <asp:Button ID="btnSalva" runat="server" Text="Salva" CssClass="btn btn-primary" OnClick="btnSalva_Click" />
                    <asp:Button ID="btnAnnulla" runat="server" Text="Annulla" CssClass="btn btn-secondary"
                        OnClick="btnAnnulla_Click" CausesValidation="false" />
                </div>
            </div>

            <!-- COLONNA CARD (ALLINEATA COME IN PIANOSTUDI) -->
            <div class="col-12 col-lg-4">
                <div class="card shadow-sm mt-4 mt-lg-0 h-100">
                    <div class="card-body">
                        <h3 class="fw-bold mb-3">Come funziona?</h3>
                        <p class="text-muted fs-6">
                            In questa pagina puoi aggiungere un esame al tuo piano di studio.
                            <br />
                            <br />
                            Seleziona l'esame e clicca su <strong>“Salva”</strong>.
                            <br />
                            <br />
                            Gli esami obbligatori sono evidenziati automaticamente.
                        </p>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
