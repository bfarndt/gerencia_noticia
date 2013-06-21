<%@ Page Title="Log In" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Login.aspx.cs" Inherits="Noticia.Apresentacao.Account.Login" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
   <h2>
        Autenticação
    </h2>
    <p>
        Por favor insíra seu usuário e senha.
    </p>

    <asp:ValidationSummary ID="LoginUserValidationSummary" runat="server" CssClass="failureNotification" 
            ValidationGroup="LoginUserValidationGroup"/>

    <div style="background-position:center" class="accountInfo" >
        <fieldset class="login">
            <legend>Informações do usuário</legend>
            <p>
                <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">Login:</asp:Label>
                <asp:TextBox ID="UserName" MaxLength="28" runat="server" CssClass="textEntry"></asp:TextBox>
                <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName" 
                        CssClass="failureNotification" ErrorMessage="Login é requirido." ToolTip="Login é requirido." 
                        ValidationGroup="LoginUserValidationGroup">*</asp:RequiredFieldValidator>
            </p>
            <p>
                <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Senha:</asp:Label>
                <asp:TextBox ID="Password" MaxLength="28" runat="server" CssClass="passwordEntry" TextMode="Password"></asp:TextBox>
                <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password" 
                        CssClass="failureNotification" ErrorMessage="Senha é requirida." ToolTip="Senha é requirida." 
                        ValidationGroup="LoginUserValidationGroup">*</asp:RequiredFieldValidator>
            </p>
        </fieldset>
        <p class="submitButton">
            <asp:ImageButton ID="LoginButton" AlternateText="Acessar sistema" ImageUrl="~/Imagem/btnAcessar.png" runat="server" onclick="LoginButton_Click" />
        </p>
    </div></asp:Content>
