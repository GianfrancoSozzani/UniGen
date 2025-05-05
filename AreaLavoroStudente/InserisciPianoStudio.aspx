<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="InserisciPianoStudio.aspx.cs" Inherits="InserisciPianoStudi" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style>
       
    .form-container { max-width: 600px; margin: 0 auto; }
    .alert-message { margin-bottom: 20px; }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
        <div class="container mt-4">
            <div class="card">
                <div class="card-header bg-primary text-white">
                    <h2>Aggiungi Esame al Piano di Studio</h2>
                </div>
                <div class="card-body form-container">
                    
                    <asp:Panel ID="pnlMessaggio" runat="server" Visible="false" CssClass="alert alert-dismissible fade show alert-message">
                        <button type="button" class="btn-close" data-bs-dismiss="alert"></button>
                        <asp:Literal ID="litMessaggio" runat="server"></asp:Literal>
                    </asp:Panel>
                    
                    
                    <div class="mb-3">
                        <label class="form-label">Seleziona Esame</label>
                        <asp:DropDownList ID="ddlEsame" runat="server" CssClass="form-select">
                            <asp:ListItem Text="-- Seleziona Esame --" Value=""></asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvEsame" runat="server" 
                            ControlToValidate="ddlEsame" ErrorMessage="Seleziona un esame" 
                            CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
                    </div>
                    
                    <div class="mb-3">
                        <label class="form-label">Anno Accademico</label>
                        <asp:TextBox ID="txtAnnoAccademico" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvAnnoAccademico" runat="server" 
                            ControlToValidate="txtAnnoAccademico" ErrorMessage="Inserisci l'anno accademico" 
                            CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
                    </div>
                    
                    
                    <div class="mb-3">
                        <asp:Button ID="btnSalva" runat="server" Text="Salva" CssClass="btn btn-primary" OnClick="btnSalva_Click" />
                        <asp:Button ID="btnAnnulla" runat="server" Text="Annulla" CssClass="btn btn-secondary" 
                            OnClick="btnAnnulla_Click" CausesValidation="false" />
                    </div>
                </div>
            </div>
        </div>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
</asp:Content>

