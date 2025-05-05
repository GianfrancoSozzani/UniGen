<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ModificaPianoStudi.aspx.cs" Inherits="ModificaPianoStudi" %>

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
            border-radius: 15px;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
        }
        .card-header {
            border-top-left-radius: 15px;
            border-top-right-radius: 15px;
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
            border-radius: 50px;
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
        <div class="card">
            <div class="card-header bg-primary text-white">
                <h2>Modifica Esame nel Piano di Studio</h2>
            </div>
            <div class="card-body form-container">
                <asp:Panel ID="pnlMessaggio" runat="server" Visible="false" CssClass="alert alert-dismissible fade show alert-message">
                    <button type="button" class="btn-close" data-bs-dismiss="alert"></button>
                    <asp:Literal ID="litMessaggio" runat="server"></asp:Literal>
                </asp:Panel>

                <div class="mb-4">
                    <label class="form-label">Esame</label>
                    <asp:DropDownList ID="ddlEsame" runat="server" CssClass="form-select" Enabled="false">
                        <asp:ListItem Text="-- Seleziona Esame --" Value=""></asp:ListItem>
                    </asp:DropDownList>
                </div>
                
                <div class="mb-4">
                    <label class="form-label">Anno Accademico</label>
                    <asp:TextBox ID="txtAnnoAccademico" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvAnnoAccademico" runat="server" 
                        ControlToValidate="txtAnnoAccademico" ErrorMessage="Inserisci l'anno accademico" 
                        CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
                </div>
             
                
                <div class="d-flex justify-content-between mt-4">
                    <asp:Button ID="btnSalva" runat="server" Text="Salva Modifiche" CssClass="btn btn-primary" OnClick="btnSalva_Click" />
                    <div>
                        <asp:Button ID="btnAnnulla" runat="server" Text="Annulla" CssClass="btn btn-secondary" 
                            OnClick="btnAnnulla_Click" CausesValidation="false" />
                        <asp:Button ID="btnElimina" runat="server" Text="Elimina Esame" CssClass="btn btn-danger" 
                            OnClick="btnElimina_Click" CausesValidation="false" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>