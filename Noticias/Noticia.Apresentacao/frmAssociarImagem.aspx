<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmAssociarImagem.aspx.cs" Inherits="Noticia.Apresentacao.frmAssociarImagem" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <fieldset>
        <table border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <asp:ImageButton ID="btnVoltar" AlternateText="Voltar" ImageUrl="~/imagem/btnVoltar.png" runat="server" OnClick="btnVoltar_Click" />
                </td>
            </tr>
        </table>
    </fieldset>

    <table>
        <tr>
            <td class="auto-style1">Notícia:</td>
            <td>
                <asp:DropDownList ID="ddlNoticia" Width="320px"
                    runat="server" Style="margin-left: 0px">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvNoticia" runat="server"
                    ControlToValidate="ddlNoticia"
                    ErrorMessage="Informe a notícia" InitialValue="0"
                    ValidationGroup="validacao">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>Imagem:</td>
            <td>
                <asp:DropDownList ID="ddlImagem" Width="320px"
                    runat="server">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvImagem" runat="server"
                    ControlToValidate="ddlImagem"
                    ErrorMessage="Informe a imagem" InitialValue="0"
                    ValidationGroup="validacao">*</asp:RequiredFieldValidator>
            </td>
            <td>
                <asp:ImageButton ID="imgOK_Imagem" CausesValidation="true"
                    ValidationGroup="validacao" runat="server" ImageUrl="~/Imagem/add.png" OnClick="imgOK_Imagem_Click" />
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td>
                <asp:GridView runat="server" ID="grvNoticiaImagem" Width="400"
                    AutoGenerateColumns="False" DataKeyNames="Chave"
                    AllowSorting="True"
                    OnRowCommand="grvNoticiaImagem_RowCommand"
                    OnRowDataBound="grvNoticiaImagem_RowDataBound"
                    EmptyDataText="Nenhum registro encontrado.">
                    <Columns>

                        <asp:TemplateField HeaderText="Notícia" ConvertEmptyStringToNull="False">
                            <ItemTemplate>
                                <asp:Label ID="lblNoticia" runat="server" Text='<%# Bind("Noticia.Titulo") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Imagem" ConvertEmptyStringToNull="False">
                            <ItemTemplate>
                                <asp:Label ID="lblImagem" runat="server" Text='<%# Bind("Imagem.Legenda") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Excluir">
                            <ItemTemplate>
                                <asp:ImageButton runat="server" OnClientClick="if(confirm('Deseja realmente remover esta associação?')){return true;}else{return false;}" ID="ibtExcluir" ImageUrl="~/Imagem/ico_delete_grid.gif"
                                    CommandArgument='<%# Eval("Chave") %>' CommandName="EXCLUIR" CausesValidation="true" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" Width="30px" />
                        </asp:TemplateField>
                    </Columns>

                </asp:GridView>
            </td>
        </tr>
    </table>

</asp:Content>
