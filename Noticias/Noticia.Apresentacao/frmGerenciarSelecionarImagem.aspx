<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmGerenciarSelecionarImagem.aspx.cs" Inherits="Noticia.Apresentacao.frmGerenciarSelecionarImagem" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

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
<body style="height: auto">
    <form id="form1" runat="server">
        <div class="contentPage" style="height: 353px">
            <asp:ValidationSummary runat="server" ID="vs" ValidationGroup="validacao" />

            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>

            <div class="legendFormulario">
                Imagem
            </div>

            <table>
                <tr>
                    <td>

                        <table>
                            <tr>
                                <td>
                                    <table>
                                        <tr>
                                            <td class="labelForm">Legenda:</td>
                                            <td>
                                                <asp:TextBox runat="server" MaxLength="40" Width="300px" ID="txtLegenda"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvLegenda" runat="server" ControlToValidate="txtLegenda" Display="Dynamic" ErrorMessage="Campo legenda é requerido." ValidationGroup="validacao">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="labelForm">Local:</td>
                                            <td>
                                                <asp:TextBox runat="server" MaxLength="40" Width="200px" ID="txtLocal"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvLocal" runat="server" ControlToValidate="txtLocal" Display="Dynamic" ErrorMessage="Campo local é requerido." ValidationGroup="validacao">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="labelForm">Data e Hora:</td>
                                            <td>
                                                <asp:TextBox runat="server" MaxLength="40" Width="160px" ID="txtDataHora"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvDataHora" runat="server" ControlToValidate="txtDataHora" Display="Dynamic" ErrorMessage="Campo Data Hora é requerido." ValidationGroup="validacao">*</asp:RequiredFieldValidator>
                                                <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server"
                                                    TargetControlID="txtDataHora"
                                                    Mask="99/99/9999 99:99"
                                                    MessageValidatorTip="true"
                                                    OnFocusCssClass="MaskedEditFocus"
                                                    OnInvalidCssClass="MaskedEditError"
                                                    MaskType="DateTime"
                                                    InputDirection="RightToLeft"
                                                    AcceptNegative="Left"
                                                    DisplayMoney="Left"
                                                    ErrorTooltipEnabled="True">
                                                </asp:MaskedEditExtender>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td>
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:Image ID="imgImagem" runat="server" Height="200" Width="230" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
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
                    </asp:Panel>
                </div>
            </div>
        </div>

    </form>
</body>
</html>
