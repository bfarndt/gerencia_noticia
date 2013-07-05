<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmListarNoticiaParaSubmissao.aspx.cs" Inherits="Noticia.Apresentacao.frmListarNoticiaParaSubmissao" %>

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

    <h2>Listar Notícias para Submissão
    </h2>

    <fieldset>
        <legend>Filtrar Notícia</legend>
        <table border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td style="padding-left: 10px; padding-right: 10px; height: 26px;">
                    <asp:Label ID="lblTitulo" runat="server" Text="Título: "></asp:Label></td>
                <td>
                    <asp:TextBox runat="server" MaxLength="49" Width="320px" ID="txtTitulo"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:ImageButton ID="btnVoltar" AlternateText="Voltar" ImageUrl="~/imagem/btnVoltar.png" runat="server" OnClick="btnVoltar_Click" />
                    <asp:ImageButton ID="btnFiltrar" AlternateText="Filtrar Notícias" ImageUrl="~/Imagem/btnFiltrar.png" runat="server" OnClick="btnFiltrar_Click" />
                </td>
            </tr>
        </table>
    </fieldset>


    <asp:GridView runat="server" ID="grvNoticia" Width="100%"
        AutoGenerateColumns="False" DataKeyNames="IdNoticia"
        AllowSorting="True"
        EmptyDataText="Nenhum registro encontrado."
        OnRowCommand="grvNoticia_RowCommand"
        OnRowDataBound="grvNoticia_RowDataBound"
        Style="margin-right: 0px">
        <Columns>

            <asp:TemplateField HeaderText="Título" ConvertEmptyStringToNull="False" ControlStyle-Width="600">
                <ItemTemplate>
                    <asp:Label ID="lblTitulo" runat="server" Text='<%# Bind("Titulo") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Ações">
                <ItemTemplate>
                    <asp:ImageButton runat="server" ID="ibtEditar" ImageUrl="~/Imagem/ico_habilitar.gif"
                        CommandArgument='<%# Eval("IdNoticia") %>' CommandName="SUBMETER" CausesValidation="False" ToolTip="Submeter" />

                                        <asp:ImageButton runat="server" ID="ibtCancel" ImageUrl="~/Imagem/ico_cancel_grid.gif"
                        CommandArgument='<%# Eval("IdNoticia") %>' CommandName="CANCELAR" CausesValidation="False" ToolTip="Cancelar" />
                    
                    <asp:ImageButton runat="server" ID="ibtVisualizar" ImageUrl="~/Imagem/magnifier.png"
                        CommandArgument='<%# Eval("IdNoticia") %>' CommandName="VISUALIZAR" CausesValidation="False" ToolTip="Visualizar" />
                </ItemTemplate>
            </asp:TemplateField>

        </Columns>

    </asp:GridView>

    <asp:Button runat="server" ID="btnPost" OnClick="btnPost_Click" CssClass="display-none" />


</asp:Content>
