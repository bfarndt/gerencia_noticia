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
    <form id="form1" runat="server" >

        <div class="contentPage" >
            <asp:ValidationSummary runat="server" ID="vs" ValidationGroup="validacao" />
            <asp:ValidationSummary runat="server" ID="vsPermissao" ValidationGroup="validacaoPermissao" />
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

            <div class="legendFormulario">
                Grupo trabalho
            </div>

            <table>
                <tr>
                    <td class="labelForm" style="width: 100px">Grupo trabalho:</td>
                    <td>
                        <asp:DropDownList ID="ddlGrupoTrabalho" Width="320px"
                            runat="server">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvGrupo" runat="server"
                            ControlToValidate="ddlPermissao"
                            ErrorMessage="Informe a permissão" InitialValue="0"
                            ValidationGroup="validacaoPermissao">*</asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:ImageButton ID="ImageButton1" CausesValidation="true"
                            ValidationGroup="validacaoPermissao" runat="server" ImageUrl="~/Imagem/add.png" OnClick="imgOK_permissao_Click" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:GridView runat="server" ID="GridView1" Width="140%"
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

            <br />



        </div>

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

    </form>
</body>
</html>
