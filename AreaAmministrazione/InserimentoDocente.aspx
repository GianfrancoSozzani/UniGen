<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="InserimentoDocente.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container mt-4 mb-4">

        <div class="row justify-content-center mb-4">
            <div class="col-md-8 text-center">
                <h3>Inserimento Docente</h3>
            </div>
        </div>

        <!-- Email e Password -->
        <div class="row g-3 mb-5" style="box-shadow: 0px 4px 12px #21212115">
            <div class="col-md-6 d-flex align-items-center">
                <asp:Label ID="Label4" runat="server" Text="Email" CssClass="form-label w-25"></asp:Label>
                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control w-75"></asp:TextBox>
            </div>
            <div class="col-md-6 d-flex align-items-center">
                <asp:Label ID="Label5" runat="server" Text="Password" CssClass="form-label w-25"></asp:Label>
                <asp:TextBox ID="txtPWD" runat="server" CssClass="form-control w-75" TextMode="Password"></asp:TextBox>
            </div>
        </div>

        <!-- Cognome e Nome -->
        <div class="row g-3 mb-5" style="box-shadow: 0px 4px 12px #21212115">
            <div class="col-md-6 d-flex align-items-center">
                <asp:Label ID="Label1" runat="server" Text="Cognome" CssClass="form-label w-25"></asp:Label>
                <asp:TextBox ID="txtCognome" runat="server" CssClass="form-control w-75"></asp:TextBox>
            </div>
            <div class="col-md-6 d-flex align-items-center">
                <asp:Label ID="Label2" runat="server" Text="Nome" CssClass="form-label w-25"></asp:Label>
                <asp:TextBox ID="txtNome" runat="server" CssClass="form-control w-75"></asp:TextBox>
            </div>
        </div>

       <%-- <!-- Data di Nascita Centrata -->
        <div class="row g-3 mb-5 justify-content-center" style="box-shadow: 0px 4px 12px #21212115">
            <div class="col-md-6 d-flex align-items-center text-center">
                <asp:Label ID="Label3" runat="server" Text="Data di Nascita" CssClass="form-label w-50"></asp:Label>
                <asp:TextBox ID="txtDataNascita" runat="server" TextMode="Date" CssClass="form-control w-75 mx-auto"></asp:TextBox>
            </div>
        </div>--%>

        <!-- Data di Nascita non centrata -->
        <div class="row g-3 mb-5" style="box-shadow: 0px 4px 12px #21212115">
            <div class="col-md-6 d-flex">
                <asp:Label ID="Label13" runat="server" Text="Data di Nascita" CssClass="form-label w-50"></asp:Label>
                <asp:TextBox ID="txtDataDiNascita" runat="server" TextMode="Date" CssClass="form-control w-75 mx-auto"></asp:TextBox>
            </div>
        </div>

        <!-- Indirizzo, Città, CAP, Provincia -->
        <div class="row g-3 mb-5" style="box-shadow: 0px 4px 12px #21212115">
            <!-- Indirizzo, Città -->
            <div class="col-md-6 d-flex align-items-center">
                <asp:Label ID="Label6" runat="server" Text="Indirizzo" CssClass="form-label w-25"></asp:Label>
                <asp:TextBox ID="txtIndirizzo" runat="server" CssClass="form-control w-75"></asp:TextBox>
            </div>
            <div class="col-md-6 d-flex align-items-center">
                <asp:Label ID="Label8" runat="server" Text="Città" CssClass="form-label w-25"></asp:Label>
                <asp:TextBox ID="txtCitta" runat="server" CssClass="form-control w-75"></asp:TextBox>
            </div>

            <!-- CAP, Provincia -->
            <div class="col-md-6 d-flex align-items-center">
                <asp:Label ID="Label7" runat="server" Text="CAP" CssClass="form-label w-25"></asp:Label>
                <asp:TextBox ID="txtCAP" runat="server" CssClass="form-control w-75"></asp:TextBox>
            </div>
            <div class="col-md-6 d-flex align-items-center">
                <asp:Label ID="Label9" runat="server" Text="Provincia" CssClass="form-label w-25"></asp:Label>
                <asp:TextBox ID="txtProvincia" runat="server" CssClass="form-control w-75"></asp:TextBox>
            </div>
        </div>

        <!-- Immagine Profilo e Tipo -->
        <div class="row g-3 mb-5" style="box-shadow: 0px 4px 12px #21212115">
            <div class="col-md-6 d-flex align-items-center">
                <asp:Label ID="Label10" runat="server" Text="Immagine Profilo:" CssClass="form-label w-25"></asp:Label>
                <asp:FileUpload ID="fuFotoProfilo" runat="server" CssClass="form-control w-75" />
            </div>
            <div class="col-md-6 d-flex align-items-center">
                <asp:Label ID="Label11" runat="server" Text="Tipo:" CssClass="form-label w-25"></asp:Label>
                <asp:TextBox ID="txtTipo" runat="server" CssClass="form-control w-75"></asp:TextBox>
            </div>
        </div>

        <!-- Abilitato e Salva -->
        <div class="row g-3 mb-5" style="box-shadow: 0px 4px 12px #21212115">
            <div class="col-md-6 d-flex align-items-center">
                <asp:Label ID="Label12" runat="server" Text="Abilitato:" CssClass="form-label w-25"></asp:Label>
                <asp:CheckBox ID="CheckBoxAbilitato" runat="server" CssClass="form-check-input" />
            </div>
            <div class="col-md-6 text-center">
                <asp:Button ID="btnSalva" runat="server" Text="Salva" CssClass="btn btn-primary" OnClick="btnSalva_Click" />
            </div>
        </div>

    </div>
</asp:Content>

