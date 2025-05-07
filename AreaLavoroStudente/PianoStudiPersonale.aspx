<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="PianoStudiPersonale.aspx.cs" Inherits="PianoStudiPersonale" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title>Piano di Studio Personale</title>
    <style>
    .table-header { background-color: #f8f9fa; font-weight: bold; }
    .empty-data { text-align: center; padding: 20px; color: #6c757d; }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


    
    
        <div class="container mt-4">
            <div class="card">
                <div class="card-header bg-primary text-white">
                    <h2>Piano di Studio Personale</h2>
                </div>
                <div class="card-body">
                    
                    <asp:Panel ID="pnlMessaggio" runat="server" Visible="false" CssClass="alert alert-dismissible fade show mb-3">
                        <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
                        <asp:Literal ID="litMessaggio" runat="server"></asp:Literal>
                    </asp:Panel>
                    
                    <div class="mb-3">
                        <asp:Button ID="btnNuovoPiano" runat="server" Text="Aggiungi Esame" CssClass="btn btn-success" OnClick="btnNuovoPiano_Click" />
                    </div>
                    
                    
                    <asp:Repeater ID="rptPianoStudio" runat="server" OnItemCommand="rptPianoStudio_ItemCommand">
                        <HeaderTemplate>
                            <div class="table-responsive">
                                <table class="table table-striped table-bordered">
                                    <thead class="table-header">
                                        <tr>
                                            <th>Esame</th>
                                            <th>Anno Accademico</th>
                                            <th>Obbligatorio</th>
                                            <th>Azioni</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td><%# Eval("TitoloEsame") %></td>
                                <td><%# Eval("AnnoAccademico") %></td>
                                <td><%# Eval("Obbligatorio").ToString() == "S" ? "Sì" : "No" %></td>
                                <td>
                                    <asp:Button ID="btnModifica" runat="server" Text="Modifica" 
                                        CommandName="Modifica" CommandArgument='<%# Eval("K_PianoStudioPersonale") %>'
                                        CssClass="btn btn-primary btn-sm" />
                                    
                                    <asp:Button ID="btnElimina" runat="server" Text="Elimina" 
                                        CommandName="Elimina" CommandArgument='<%# Eval("K_PianoStudioPersonale") %>'
                                        OnClientClick="return confirm('Sei sicuro di voler eliminare questo esame dal tuo piano di studio?');"
                                        CssClass="btn btn-danger btn-sm" />
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                                    </tbody>
                                </table>
                            </div>
                        </FooterTemplate>
                    </asp:Repeater>
                    
                    
                    <asp:Panel ID="pnlNessunDato" runat="server" CssClass="empty-data" Visible="false">
                        <p>Nessun esame presente nel tuo piano di studio.</p>
                        <asp:Button ID="btnAggiungiPrimo" runat="server" Text="Aggiungi il primo esame" 
                            OnClick="btnNuovoPiano_Click" CssClass="btn btn-success" />
                    </asp:Panel>
                </div>
            </div>
        </div>
    
</asp:Content>

