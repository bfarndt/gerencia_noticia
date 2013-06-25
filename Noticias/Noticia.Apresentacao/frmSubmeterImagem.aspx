<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmSubmeterImagem.aspx.cs" Inherits="Noticia.Apresentacao.frmSubmeterImagem" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">


    <div class="legendFormulario">
        Submissão de imagens
    </div>


    <table>
        <tr>
            <td>
                <asp:AsyncFileUpload runat="server"
                    ID="AsyncFileUpload2" Width="400px" UploaderStyle="Modern"
                    UploadingBackColor="#CCFFFF" ThrobberID="myThrobber"
                    OnUploadedComplete="AsyncFileUpload2_UploadedComplete"
                    OnUploadedFileError="AsyncFileUpload2_UploadedFileError" />
            </td>
            <td>
                <asp:ImageButton ID="imgAtualizar" ImageUrl="~/Imagem/update.png" OnClick="imgAtualizar_Click"  runat="server" Height="33px" Width="41px" />
            </td>
        </tr>
    </table>
    

    <div class="legendFormulario">
        Imagens
    </div>

    <table>
        <tr>
            <td>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:GridView runat="server" ID="grvImagens" Width="140%"
                            AutoGenerateColumns="False" DataKeyNames="IdImagemArquivo"
                            AllowSorting="True"
                            OnRowCommand="grvImagens_RowCommand"
                            OnRowDataBound="grvImagens_RowDataBound"
                            EmptyDataText="Nenhum registro encontrado.">

                            <Columns>

                                <asp:TemplateField HeaderText="Imagem" ConvertEmptyStringToNull="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNomeArquivo" runat="server" Text='<%# Bind("NomeArquivo") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Excluir">
                                    <ItemTemplate>
                                        <asp:ImageButton runat="server" OnClientClick="if(confirm('Deseja realmente remover esta imagem?')){return true;}else{return false;}" ID="ibtExcluir" ImageUrl="~/Imagem/ico_delete_grid.gif"
                                            CommandArgument='<%# Eval("IdImagemArquivo") %>' CommandName="EXCLUIR" CausesValidation="true" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" Width="30px" />
                                </asp:TemplateField>
                            </Columns>

                        </asp:GridView>

                        </td>
                </tr>
            </table>
            
                    </ContentTemplate>
                </asp:UpdatePanel>
</asp:Content>
