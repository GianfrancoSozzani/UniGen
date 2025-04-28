<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="InserimentoDocente.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container mt-4 text-center">
        <h3>Inserimento Docente</h3>
        <%--<div class="row d-flex justify-content-center">
            <div class="col-md-8">

                <div class="mb-3">
                    <asp:Label ID="Label4" runat="server" Text="Email:" CssClass="form-label"></asp:Label>
                    <asp:TextBox ID="TextBox4" runat="server" CssClass="form-control w-50 mx-auto"></asp:TextBox>
                </div>

                <div class="mb-3">
                    <asp:Label ID="Label5" runat="server" Text="Password:" CssClass="form-label"></asp:Label>
                    <asp:TextBox ID="TextBox5" runat="server" CssClass="form-control w-50 mx-auto" TextMode="Password"></asp:TextBox>
                </div>

                <div class="mb-3">
                    <asp:Label ID="Label1" runat="server" Text="Cognome:" CssClass="form-label"></asp:Label>
                    <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control w-50 mx-auto"></asp:TextBox>
                </div>

                <div class="mb-3">
                    <asp:Label ID="Label2" runat="server" Text="Nome:" CssClass="form-label"></asp:Label>
                    <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control w-50 mx-auto"></asp:TextBox>
                </div>

                <div class="mb-3">
                    <asp:Label ID="Label3" runat="server" Text="Data di nascita:" CssClass="form-label"></asp:Label>
                    <asp:TextBox ID="txtDataNascita" runat="server" TextMode="Date" CssClass="form-control w-25 mx-auto"></asp:TextBox>
                </div>

                <div class="mb-3">
                    <asp:Label ID="Label6" runat="server" Text="Indirizzo:" CssClass="form-label"></asp:Label>
                    <asp:TextBox ID="TextBox6" runat="server" CssClass="form-control w-50 mx-auto"></asp:TextBox>
                </div>

                <div class="mb-3">
                    <asp:Label ID="Label8" runat="server" Text="Città:" CssClass="form-label"></asp:Label>
                    <asp:TextBox ID="TextBox8" runat="server" CssClass="form-control w-50 mx-auto"></asp:TextBox>
                </div>

                <div class="row justify-content-center g-3 mb-3">
                    <div class="col-md-5">
                        <asp:Label ID="Label7" runat="server" Text="CAP:" CssClass="form-label"></asp:Label>
                        <asp:TextBox ID="TextBox7" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-5">
                        <asp:Label ID="Label9" runat="server" Text="Provincia:" CssClass="form-label"></asp:Label>
                        <asp:TextBox ID="TextBox9" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>


                <div class="mb-3">
                    <asp:Label ID="Label10" runat="server" Text="Immagine Profilo:" CssClass="form-label"></asp:Label>
                    <asp:FileUpload ID="FileUpload1" runat="server" CssClass="form-control w-50 mx-auto" />
                </div>

                <div class="mb-3">
                    <asp:Label ID="Label11" runat="server" Text="Tipo:" CssClass="form-label"></asp:Label>
                    <asp:TextBox ID="TextBox10" runat="server" CssClass="form-control w-50 mx-auto"></asp:TextBox>
                </div>

                <div class="text-center">
                    <asp:Button ID="Button1" runat="server" Text="Inserisci" CssClass="btn btn-primary" />
                </div>

            </div>
        </div>--%>

        <div class="row justify-content-center">
            <div class="col-md-8">

                <!--Email e Password-->
                <div class="row g-3 mb-5">
                    <div class="col-md-6">
                        <asp:Label ID="Label4" runat="server" Text="Email:" CssClass="form-label"></asp:Label>
                        <asp:TextBox ID="TextBox4" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-6">
                        <asp:Label ID="Label5" runat="server" Text="Password:" CssClass="form-label"></asp:Label>
                        <asp:TextBox ID="TextBox5" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                    </div>
                </div>

                <!--Cognome e Nome-->
                <div class="row g-3 mb-5">
                    <div class="col-md-6">
                        <asp:Label ID="Label1" runat="server" Text="Cognome:" CssClass="form-label"></asp:Label>
                        <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-6">
                        <asp:Label ID="Label2" runat="server" Text="Nome:" CssClass="form-label"></asp:Label>
                        <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>

                <!--Data di Nascita-->
                <div class="row g-3 mb-5 justify-content-center">
                    <div class="col-md-6 text-center">
                        <asp:Label ID="Label3" runat="server" Text="Data di Nascita:" CssClass="form-label"></asp:Label>
                        <asp:TextBox ID="txtDataNascita" runat="server" TextMode="Date" CssClass="form-control mx-auto"></asp:TextBox>
                    </div>
                </div>

                <!--Indirizzo, Città, CAP, Provincia-->
                <div class="row g-3 mb-5">
                    <div class="col-md-3">
                        <asp:Label ID="Label6" runat="server" Text="Indirizzo:" CssClass="form-label"></asp:Label>
                        <asp:TextBox ID="TextBox6" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="Label8" runat="server" Text="Città:" CssClass="form-label"></asp:Label>
                        <asp:TextBox ID="TextBox8" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="Label7" runat="server" Text="CAP:" CssClass="form-label"></asp:Label>
                        <asp:TextBox ID="TextBox7" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="Label9" runat="server" Text="Provincia:" CssClass="form-label"></asp:Label>
                        <asp:TextBox ID="TextBox9" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>

                <!--Immagine Profilo e Tipo-->
                <div class="row g-3 mb-5">
                    <div class="col-md-6">
                        <asp:Label ID="Label10" runat="server" Text="Immagine Profilo:" CssClass="form-label"></asp:Label>
                        <asp:FileUpload ID="FileUpload1" runat="server" CssClass="form-control" />
                    </div>
                    <div class="col-md-6">
                        <asp:Label ID="Label11" runat="server" Text="Tipo:" CssClass="form-label"></asp:Label>
                        <asp:TextBox ID="TextBox10" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>

                <!--Abilitato e Salva-->
                <div class="row g-3 mb-5">
                    <div class="col-md-6">
                        <asp:Label ID="Label12" runat="server" Text="Abilitato:" CssClass="form-label"></asp:Label>
                        <asp:CheckBox ID="CheckBoxAbilitato" runat="server" CssClass="form-check-input" />
                    </div>
                    <div class="col-md-6 text-center">
                        <asp:Button ID="Button1" runat="server" Text="Salva" CssClass="btn btn-primary" />
                    </div>
                </div>

            </div>
</asp:Content>

