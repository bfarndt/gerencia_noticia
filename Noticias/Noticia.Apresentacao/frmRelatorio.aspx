<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmRelatorio.aspx.cs" Inherits="Noticia.Apresentacao.frmRelatorio" %>

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

    <h2>Relatório
    </h2>

    <fieldset>
        <legend>Filtrar Notícia</legend>
        <table border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td class="labelForm">Título:</td>
                <td>
                    <asp:TextBox runat="server" MaxLength="49" Width="320px" ID="txtTitulo"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="labelForm">Status:</td>
                <td>
                    <asp:DropDownList ID="ddlStatus" Width="320px"
                        runat="server" Style="margin-left: 0px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:ImageButton ID="btnVoltar" AlternateText="Voltar" ImageUrl="~/imagem/btnVoltar.png" runat="server" OnClick="btnVoltar_Click" />
                    <asp:ImageButton ID="btnFiltrar" AlternateText="Filtrar Notícias" ImageUrl="~/Imagem/btnFiltrar.png" runat="server" OnClick="btnFiltrar_Click" />
                </td>
                <td>
                    <asp:ImageButton ID="imgExportar" Height="30" Width="30" ImageUrl="~/Imagem/export_icon.jpg" runat="server" OnClick="imgExportar_Click" />
                </td>
            </tr>
        </table>
    </fieldset>



    <asp:GridView runat="server" ID="grvHistorico" Width="100%"
        AutoGenerateColumns="False" DataKeyNames="IdHistorico"
        AllowSorting="True"
        EmptyDataText="Nenhum registro encontrado."
        OnRowCommand="grvHistorico_RowCommand"
        OnRowDataBound="grvHistorico_RowDataBound"
        Style="margin-right: 0px">
        <Columns>

            <asp:TemplateField HeaderText="Notícia" ConvertEmptyStringToNull="False">
                <ItemTemplate>
                    <asp:Label ID="lblNoticia" runat="server" Text='<%# Bind("Noticia.Titulo") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Usuário" ConvertEmptyStringToNull="False">
                <ItemTemplate>
                    <asp:Label ID="lblUsuario" runat="server" Text='<%# Bind("Usuario.Nome") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Status" ConvertEmptyStringToNull="False">
                <ItemTemplate>
                    <asp:Label ID="lblStatus" runat="server" Text='<%# Bind("StatusNoticia.Descricao") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Data/Hora" ConvertEmptyStringToNull="False">
                <ItemTemplate>
                    <asp:Label ID="lblDataHora" runat="server" Text='<%# Bind("DataHora") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Descrição" ConvertEmptyStringToNull="False">
                <ItemTemplate>
                    <asp:Label ID="lblDescricao" runat="server" Text='<%# Bind("Descricao") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Ações">
                <ItemTemplate>
                    <asp:ImageButton runat="server" ID="ibtVisualizar" ImageUrl="~/Imagem/magnifier.png"
                        CommandArgument='<%# Eval("IdNoticia") %>' CommandName="VISUALIZAR" CausesValidation="False" ToolTip="Visualizar" />
                </ItemTemplate>
            </asp:TemplateField>

        </Columns>

    </asp:GridView>

    <asp:Button runat="server" ID="btnPost" OnClick="btnPost_Click" CssClass="display-none" />

</asp:Content>
