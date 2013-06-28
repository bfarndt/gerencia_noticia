<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmEditarNoticia.aspx.cs" Inherits="Noticia.Apresentacao.frmEditarNoticia" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
            <asp:ValidationSummary runat="server" ID="vsPalavras" ValidationGroup="validacaoPalavras" />

            <div>
                <table>
                    <tr>
                        <td class="labelForm">Título:</td>
                        <td>
                            <asp:TextBox runat="server" MaxLength="49" Width="300px" ID="txtTitulo"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvTitulo" runat="server" ControlToValidate="txtTitulo" Display="Dynamic" ErrorMessage="Campo título é requerido." ValidationGroup="validacao">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="labelForm">Conteúdo:</td>
                        <td>
                            <asp:TextBox runat="server" MaxLength="1000" Width="500px" Height="300px" TextMode="MultiLine" ID="txtConteudo"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvConteudo" runat="server" ControlToValidate="txtConteudo" Display="Dynamic" ErrorMessage="Campo conteúdo é requerido." ValidationGroup="validacao">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                </table>

                <table>
                    <tr>
                        <td class="labelForm">Palavra chave:</td>
                        <td>
                            <asp:TextBox runat="server" MaxLength="49" Width="300px" ID="txtPalavra"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvPalavra" runat="server" ControlToValidate="txtPalavra" Display="Dynamic" ErrorMessage="Campo palavra chave é requerido." ValidationGroup="validacaoPalavras">*</asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <asp:ImageButton ID="imgOK_palavras" CausesValidation="true"
                                ValidationGroup="validacaoPalavras" runat="server" ImageUrl="~/Imagem/add.png" OnClick="imgOK_palavras_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView runat="server" ID="grvPalavras" Width="140%"
                                AutoGenerateColumns="False" DataKeyNames="PalavraChaveTexto"
                                AllowSorting="True"
                                OnRowCommand="grvPalavras_RowCommand"
                                OnRowDataBound="grvPalavras_RowDataBound"
                                EmptyDataText="Nenhum registro encontrado.">
                                <Columns>

                                    <asp:TemplateField HeaderText="Palavra chave" ConvertEmptyStringToNull="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPalavra" runat="server" Text='<%# Bind("PalavraChaveTexto") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Excluir">
                                        <ItemTemplate>
                                            <asp:ImageButton runat="server" OnClientClick="if(confirm('Deseja realmente remover esta palavra?')){return true;}else{return false;}" ID="ibtExcluir" ImageUrl="~/Imagem/ico_delete_grid.gif"
                                                CommandArgument='<%# Eval("PalavraChaveTexto") %>' CommandName="EXCLUIR" CausesValidation="true" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" Width="30px" />
                                    </asp:TemplateField>
                                </Columns>

                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div class="contentFinal">

            <div class="AcaoFormulario">
                <asp:Panel runat="server" ID="pnlAcao">
                    <asp:ImageButton ID="btn_salvar" CausesValidation="true"
                        ValidationGroup="validacao" runat="server" ImageUrl="../imagem/btnSalvar.png"
                        OnClick="btn_salvar_Click" />
                </asp:Panel>
            </div>

        </div>


    </form>
</body>
</html>
