<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmGerenciarTipoUsuarioPermissao.aspx.cs" Inherits="Noticia.Apresentacao.frmGerenciarTipoUsuarioPermissao" %>

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
<body style="background-color:white;height:auto">
    <form id="form1" runat="server" >
        <div class="contentPage">
            <asp:ValidationSummary runat="server" ID="vsPermissao" ValidationGroup="validacaoPermissao" />


            <div class="legendFormulario">
                Permissões
            </div>

            <table>
                <tr>
                    <td class="labelForm" style="width: 100px">Permissão:</td>
                    <td>
                        <asp:DropDownList ID="ddlPermissao" Width="320px"
                            runat="server">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvPermissao" runat="server"
                            ControlToValidate="ddlPermissao"
                            ErrorMessage="Informe a permissão" InitialValue="0"
                            ValidationGroup="validacaoPermissao">*</asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:ImageButton ID="imgOK_permissao" CausesValidation="true"
                            ValidationGroup="validacaoPermissao" runat="server" ImageUrl="~/Imagem/add.png" OnClick="imgOK_permissao_Click" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:GridView runat="server" ID="grvPermissoes" Width="140%"
                            AutoGenerateColumns="False" DataKeyNames="IdPermissao"
                            AllowSorting="True"
                            OnRowCommand="grvPermissoes_RowCommand"
                            OnRowDataBound="grvPermissoes_RowDataBound"
                            EmptyDataText="Nenhum registro encontrado.">

                            <Columns>

                                <asp:TemplateField HeaderText="Permissão" ConvertEmptyStringToNull="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPermissao" runat="server" Text='<%# Bind("Descricao") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Excluir">
                                    <ItemTemplate>
                                        <asp:ImageButton runat="server" OnClientClick="if(confirm('Deseja realmente remover esta permissão?')){return true;}else{return false;}" ID="ibtExcluir" ImageUrl="~/Imagem/ico_delete_grid.gif"
                                            CommandArgument='<%# Eval("IdPermissao") %>' CommandName="EXCLUIR" CausesValidation="true" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" Width="30px" />
                                </asp:TemplateField>
                            </Columns>

                        </asp:GridView>
                    </td>
                </tr>
            </table>


        </div>
    </form>
</body>
</html>
