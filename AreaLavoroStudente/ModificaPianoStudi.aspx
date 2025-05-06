<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ModificaPianoStudi.aspx.cs" Inherits="ModificaPianoStudi" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .form-label {
            font-weight: 600;
        }

        .alert-message {
            margin-bottom: 1rem;
        }

        .container {
            padding-top: 30px;
            padding-bottom: 30px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container">
        <div class="row">
            <!-- COLONNA SINISTRA: Modulo -->
            <div class="col-12 col-lg-8 mb-4">
                <h2 class="fw-bold mb-4">Modifica Piano di Studi</h2>

                <!-- Messaggio -->
                <asp:Panel ID="pnlMessaggio" runat="server" Visible="false" CssClass="alert alert-dismissible fade show alert-message">
                    <button type="button" class="btn-close" data-bs-dismiss="alert"></button>
                    <asp:Literal ID="litMessaggio" runat="server"></asp:Literal>
                </asp:Panel>

                <!-- Form -->
                <div class="mb-4">
                    <label for="ddlEsame" class="form-label">Esame</label>
                    <asp:DropDownList ID="ddlEsame" runat="server" CssClass="form-select" Enabled="false">
                        <asp:ListItem Text="-- Seleziona Esame --" Value=""></asp:ListItem>
                    </asp:DropDownList>
                </div>

                <div class="mb-4">
                    <label for="txtAnnoAccademico" class="form-label">Anno Accademico</label>
                    <asp:TextBox ID="txtAnnoAccademico" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:TextBox ID="txtKPianoPersonale" runat="server" CssClass="form-control" Visible="False"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvAnnoAccademico" runat="server"
                        ControlToValidate="txtAnnoAccademico" ErrorMessage="Inserisci l'anno accademico"
                        CssClass="text-danger" Display="Dynamic" />
                </div>

                <!-- Bottoni -->
                <div class="d-flex justify-content-between mt-4">
                    <asp:Button ID="btnSalva" runat="server" Text="Salva Modifiche" CssClass="btn btn-primary" OnClick="btnSalva_Click" />
                    <div class="d-flex gap-2">
                        <asp:Button ID="btnAnnulla" runat="server" Text="Annulla" CssClass="btn btn-secondary"
                            OnClick="btnAnnulla_Click" CausesValidation="false" />
                        <asp:Button ID="btnElimina" runat="server" Text="Elimina Esame" CssClass="btn btn-danger"
                            OnClick="btnElimina_Click" CausesValidation="false" />
                    </div>
                </div>
            </div>

            <!-- COLONNA DESTRA: Card descrittiva -->
            <div class="col-12 col-lg-4 mt-5">
                <div class="card shadow-sm">
                    <div class="card-body">
                        <h3 class="fw-bold mb-2">Come funziona?</h3>
                        <p class="text-muted fs-6">
                            In questa pagina puoi modificare l’anno accademico associato a un esame nel tuo piano di studi.
                            <br />
                            <br />
                            Una volta aggiornati i dati, premi <strong>“Salva Modifiche”</strong> per confermare, oppure usa i pulsanti per annullare o eliminare l'esame.
                        </p>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
