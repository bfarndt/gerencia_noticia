<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmCriarNoticia.aspx.cs" Inherits="Noticia.Apresentacao.frmCriarNoticia" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">


    <h2>Criar Notícia
    </h2>

    <table>
        <tr>
            <td class="labelForm">Título:</td>
            <td>
                <asp:TextBox runat="server" MaxLength="49" Width="208px" ID="txtTitulo"></asp:TextBox>
            </td>
        </tr>
    </table>

    <div id="tabelaGrupo" visible="false" runat="server">
        <div class="legendFormulario">
            Grupo trabalho
        </div>

        <table>
            <tr>
                <td class="labelForm" style="width: 100px">Grupo trabalho:</td>
                <td>
                    <asp:DropDownList ID="ddlGrupo" Width="320px"
                        runat="server">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvGrupo" runat="server"
                        ControlToValidate="ddlGrupo"
                        ErrorMessage="Informe o grupo" InitialValue="0"
                        ValidationGroup="validacaoGrupo">*</asp:RequiredFieldValidator>
                </td>
                <td>
                    <asp:ImageButton ID="imgOK_Grupo" CausesValidation="true"
                        ValidationGroup="validacaoGrupo" runat="server" ImageUrl="~/Imagem/add.png" OnClick="imgOK_Grupo_Click" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView runat="server" ID="grvGrupo" Width="140%"
                        AutoGenerateColumns="False" DataKeyNames="IdGrupoTrabalho"
                        AllowSorting="True"
                        OnRowCommand="grvGrupo_RowCommand"
                        OnRowDataBound="grvGrupo_RowDataBound"
                        EmptyDataText="Nenhum registro encontrado.">

                        <Columns>

                            <asp:TemplateField HeaderText="Grupo" ConvertEmptyStringToNull="False">
                                <ItemTemplate>
                                    <asp:Label ID="lblGrupo" runat="server" Text='<%# Bind("Descricao") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Excluir">
                                <ItemTemplate>
                                    <asp:ImageButton runat="server" OnClientClick="if(confirm('Deseja realmente remover este grupo?')){return true;}else{return false;}" ID="ibtExcluir" ImageUrl="~/Imagem/ico_delete_grid.gif"
                                        CommandArgument='<%# Eval("IdGrupoTrabalho") %>' CommandName="EXCLUIR" CausesValidation="true" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Width="30px" />
                            </asp:TemplateField>
                        </Columns>

                    </asp:GridView>
                </td>
            </tr>
        </table>
    </div>


    <div class="contentFinal">
        <div class="AcaoFormulario">
            <asp:Panel runat="server" ID="pnlAcao">
                <asp:ImageButton ID="btn_salvar" CausesValidation="true"
                    ValidationGroup="validacao" runat="server" ImageUrl="../imagem/btnSalvar.png"
                    OnClick="btn_salvar_Click" />
                <asp:ImageButton ID="btnNovo" Visible="false" runat="server" ImageUrl="../imagem/btnNovo.png"
                    OnClick="btnNovo_Click" />
                <asp:ImageButton ID="btnVoltar" AlternateText="Voltar para listagem"
                    ImageUrl="~/imagem/btnVoltar.png" runat="server" OnClick="btnVoltar_Click" />
            </asp:Panel>
        </div>
    </div>

</asp:Content>
