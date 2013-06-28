<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmAvaliarNoticia.aspx.cs" Inherits="Noticia.Apresentacao.frmAvaliarNoticia" %>

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
<body style="background-color: white; height: auto">
    <form id="form1" runat="server">
        <div class="contentPage">
            <asp:ValidationSummary runat="server" ID="vs" ValidationGroup="validacao" />

            <div>
                <table>
                    <tr>
                        <td class="labelForm">Título:</td>
                        <td>
                            <asp:TextBox runat="server" ReadOnly="true" MaxLength="49" Width="300px" ID="txtTitulo"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="labelForm">Feedback:</td>
                        <td>
                            <asp:TextBox runat="server" MaxLength="1000" Width="500px" Height="300px" TextMode="MultiLine" ID="txtFeedback"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvFeedback" runat="server" ControlToValidate="txtFeedback" Display="Dynamic" ErrorMessage="Campo feedback é requerido." ValidationGroup="validacao">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div class="contentFinal" style="height:auto">

            <div class="AcaoFormulario">
                <asp:Panel runat="server" ID="pnlAcao" >
                    <table>
                        <tr>
                            <td>
                                <asp:ImageButton runat="server" ID="ibtEditar" Width="50" Height="50" ImageUrl="~/Imagem/approved.PNG" OnClick="ibtEditar_Click" CausesValidation="true" ValidationGroup="validacao" ToolTip="Aprovar" />
                            </td>
                            <td>

                            </td>
                            <td>
                                <asp:ImageButton runat="server" ID="ibtCancel" Width="50" Height="50" ImageUrl="~/Imagem/disaprove.PNG" OnClick="ibtCancel_Click" CausesValidation="true" ValidationGroup="validacao" ToolTip="Reprovar" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </div>

        </div>


    </form>
</body>
</html>
