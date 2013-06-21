<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmManterUsuario.aspx.cs" Inherits="Noticia.Apresentacao.frmManterUsuario" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="~/Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="~/Styles/StyleSheet.css" rel="stylesheet" type="text/css" />
    <link href="~/Styles/highslide.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function fecha() {
            var oMe = window.self;
            oMe.opener = window.self;
            oMe.close();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="contentPage">
            <asp:ValidationSummary runat="server" ID="vs" ValidationGroup="validacao" />

            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>

            <div class="legendFormulario">
                Dados Usuário
            </div>

            <table>
                <tr>
                    <td>
                        <table>
                            <tr>
                                <td class="labelForm" style="width: 100px">Tipo:</td>
                                <td>
                                    <asp:DropDownList ID="ddlTipo" Width="320px"
                                        runat="server">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvTipo" runat="server"
                                        ControlToValidate="ddlTipo"
                                        ErrorMessage="Informe o tipo" InitialValue="0"
                                        ValidationGroup="validacao">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="labelForm">Login:</td>
                                <td>
                                    <asp:TextBox runat="server" MaxLength="40" Width="98px" ID="txtLogin"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvLogin" runat="server" ControlToValidate="txtLogin" Display="Dynamic" ErrorMessage="Campo login é requerido." ValidationGroup="validacao">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="labelForm">Senha:</td>
                                <td>
                                    <asp:TextBox runat="server" MaxLength="40" Width="98px" TextMode="Password" ID="txtSenha"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvSenha" runat="server" ControlToValidate="txtSenha" Display="Dynamic" ErrorMessage="Campo senha é requerido." ValidationGroup="validacao">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="labelForm">Nome:</td>
                                <td>
                                    <asp:TextBox runat="server" MaxLength="40" Width="303px" ID="txtNome"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvNome" runat="server" ControlToValidate="txtNome" Display="Dynamic" ErrorMessage="Campo nome é requerido." ValidationGroup="validacao">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="labelForm">E-mail:</td>
                                <td>
                                    <asp:TextBox runat="server" MaxLength="40" Width="302px" ID="txtEmail"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfv" runat="server" ControlToValidate="txtEmail" Display="Dynamic" ErrorMessage="Campo e-mail é requerido." ValidationGroup="validacao">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>

                            <tr>
                                <td class="labelForm">Telefone:</td>
                                <td>
                                    <asp:TextBox runat="server" MaxLength="40" Width="302px" ID="txtTelefone"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvTelefone" runat="server" ControlToValidate="txtTelefone" Display="Dynamic" ErrorMessage="Campo telefone é requerido." ValidationGroup="validacao">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                        </table>

                    </td>
                </tr>
            </table>

            <br />

            <div class="contentFinal">
                <div class="AcaoFormulario">
                    <asp:Panel runat="server" ID="pnlAcao">
                        <asp:ImageButton ID="btn_salvar" CausesValidation="true"
                            ValidationGroup="validacao" runat="server" ImageUrl="../imagem/btnSalvar.png"
                            OnClick="btn_salvar_Click" />
                        <asp:ImageButton ID="btnNovo" Visible="false" runat="server" ImageUrl="../imagem/btnNovo.png"
                            OnClick="btnNovo_Click" />
                    </asp:Panel>
                </div>
            </div>
        </div>

    </form>
</body>
</html>
