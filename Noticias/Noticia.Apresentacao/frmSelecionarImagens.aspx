<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmSelecionarImagens.aspx.cs" Inherits="Noticia.Apresentacao.frmSelecionarImagens" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <script type="text/javascript">
        function fecha() {
            var oMe = window.self;
            oMe.opener = window.self;
            oMe.close();
        }
        function post() {
            <%=GetPostGrid() %>
            }
    </script>

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
                        <asp:TemplateField HeaderText="Local" ConvertEmptyStringToNull="False">
                            <ItemTemplate>
                                <asp:Label ID="lblLocal" runat="server" Text='<%# Bind("Imagem.ImagemGravacao.LocalGravacao") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Data Hora" ConvertEmptyStringToNull="False">
                            <ItemTemplate>
                                <asp:Label ID="lblDataHora" runat="server" Text='<%# Bind("Imagem.ImagemGravacao.DataHoraGravacao") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Selecionar">
                            <ItemTemplate>
                                <asp:ImageButton runat="server" ID="ibtVisualizar" ImageUrl="~/Imagem/ico_habilitar.gif"
                                    CommandArgument='<%# Eval("Chave") %>' CommandName="SELECIONAR" CausesValidation="False" ToolTip="Selecionar" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" Width="30px" />
                        </asp:TemplateField>
                    </Columns>

                </asp:GridView>
            </td>
        </tr>
    </table>

    <asp:Button runat="server" ID="btnPost" OnClick="btnPost_Click" CssClass="display-none" />
</asp:Content>
