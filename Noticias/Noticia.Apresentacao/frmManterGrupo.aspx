<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmManterGrupo.aspx.cs" Inherits="Noticia.Apresentacao.frmManterGrupo" %>

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
                Dados Grupo
            </div>

            <table>
                <tr>
                    <td>
                        <table>
                      
                            <tr>
                                <td class="labelForm">Descrição:</td>
                                <td>
                                    <asp:TextBox runat="server" MaxLength="40" Width="98px" ID="txtDescricao"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvDescricao" runat="server" ControlToValidate="txtDescricao" Display="Dynamic" ErrorMessage="Campo descrição é requerido." ValidationGroup="validacao">*</asp:RequiredFieldValidator>
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
