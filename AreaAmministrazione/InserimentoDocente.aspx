<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="InserimentoDocente.aspx.cs" Inherits="Default2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="container mt-3">
        <h1>Inserimento Docente</h1>

        <div id="form1" method="post" enctype="multipart/form-data">
            <div class="container mt-5">

                <div class="row">
                    <div class="col-12 mb-5 mb-xl-0 col-xl-8 bg-white border border-1 shadow p-4">

                        <h4 class="heading-small text-muted">Dati Anagrafici</h4>
                        <div class="row mb-3">
                            <div class="col-12 col-lg-6 mb-2">
                                <label class="form-label">Nome</label>
                                <asp:TextBox ID="txtNome" class="form-control form-control-alternative" runat="server" MaxLength="50"></asp:TextBox>
                                <%--<input name="Nome" class="form-control form-control-alternative" maxlength="50" required />--%>
                            </div>
                            <div class="col-12 col-lg-6 mb-2">
                                <label class="form-label">Cognome</label>
                                <asp:TextBox ID="txtCognome" class="form-control form-control-alternative" MaxLength="50" runat="server"></asp:TextBox>
                                <%--<input name="Cognome" class="form-control form-control-alternative" maxlength="50" required />--%>
                            </div>
                            <div class="col-12 col-lg-6 mb-2">
                                <label class="form-label">Data di Nascita</label>
                                <asp:TextBox ID="txtDataDiNascita" class="form-control form-control-alternative" runat="server" TextMode="Date"></asp:TextBox>
                                <%--<input name="DataNascita" type="date" class="form-control form-control-alternative" required />--%>
                            </div>
                        </div>

                        <hr class="my-4" />
                        <h4 class="heading-small text-muted">Informazioni di Contatto</h4>
                        <div class="row mb-3">
                            <div class="col-12 col-lg-9 mb-2">
                                <label class="form-label">Indirizzo</label>
                                <asp:TextBox ID="txtIndirizzo" class="form-control form-control-alternative" MaxLength="50" runat="server"></asp:TextBox>
                                <%--<input name="Indirizzo" class="form-control form-control-alternative" maxlength="50" required />--%>
                            </div>
                            <div class="col-12 col-lg-3 mb-2">
                                <label class="form-label">CAP</label>
                                <asp:TextBox ID="txtCAP" class="form-control form-control-alternative" MaxLength="5" runat="server"></asp:TextBox>
                                <%--<input name="CAP" class="form-control form-control-alternative" maxlength="5" pattern="\d{5}" />--%>
                            </div>
                            <div class="col-12 col-lg-9 mb-2">
                                <label class="form-label">Città</label>
                                <asp:TextBox ID="txtCitta" class="form-control form-control-alternative" MaxLength="50" runat="server"></asp:TextBox>
                                <%--<input name="Citta" class="form-control form-control-alternative" maxlength="50" required />--%>
                            </div>
                            <div class="col-12 col-lg-3 mb-2">
                                <label class="form-label">Provincia</label>
                                <asp:TextBox ID="txtProvincia" class="form-control form-control-alternative text-uppercase" MaxLength="2" runat="server"></asp:TextBox>
                                <%--<input name="Provincia" class="form-control form-control-alternative text-uppercase" maxlength="2" oninput="this.value = this.value.toUpperCase()" pattern="[A-Z]{2}" />--%>
                            </div>
                        </div>

                        <hr class="my-4" />
                        <h4 class="heading-small text-muted">Credenziali</h4>
                        <div class="row mb-3">
                            <div class="col-12 mb-2">
                                <label class="form-label">Email</label>
                                <asp:TextBox ID="txtEmail" class="form-control form-control-alternative" runat="server"></asp:TextBox>
                                <%--<input name="Email" type="email" class="form-control form-control-alternative" required />--%>
                            </div>
                            <div class="col-12 mb-2">
                                <label class="form-label">Password</label>
                                <asp:TextBox ID="txtPWD" class="form-control form-control-alternative" runat="server"></asp:TextBox>
                                <%--<input name="Password" type="password" class="form-control form-control-alternative" required />--%>
                            </div>
                        </div>

                        <div class="text-center">
                            <div class="row g-3 mb-5">

                                <div class="col-md-4 d-flex align-items-center form-check">
                                    <asp:Label ID="Label12" runat="server" Text="Abilitato" CssClass="form-label w-25 mb-3"></asp:Label>
                                    <asp:CheckBox ID="CheckBoxAbilitato" runat="server" CssClass="form-check form-switch mb-3" />
                                </div>

                                <div class="col-md-4 text-center">
                                    <asp:Button ID="btnSalva" class="btn btn-primary" runat="server" Text="Salva" OnClick="btnSalva_Click" />
                                    <%--<button type="submit" class="btn btn-primary" style="width: 179px;">Salva</button>--%>
                                </div>
                                
                            </div>
                        </div>
                    </div>

                    <div class="col-12 col-xl-4 px-0 px-lg-2 mt-5 mt-xl-0">
                        <div class="bg-white border border-1 shadow p-4 position-relative">

                            <h5 class="text-center">Immagine Profilo</h5>
                            <p class="text-center">Carica un'immagine per il tuo profilo (JPEG, JPG, PNG)</p>

                            <div class="row g-3 d-flex flex-column align-items-center">
                                <div class="col-auto">
                                    <asp:FileUpload ID="fuFotoProfilo" runat="server" CssClass="form-control mb-3" />
                                </div>
                            </div>

                            <%--<div class="input-group mt-4">
                                <input type="file" name="ImmagineProfilo" class="form-control" />
                            </div>--%>
                            <div id="lblMessaggioUpload" class="text-danger d-block mt-2 text-center"></div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
