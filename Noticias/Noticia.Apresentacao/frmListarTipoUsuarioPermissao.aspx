<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmListarTipoUsuarioPermissao.aspx.cs" Inherits="Noticia.Apresentacao.frmListarTipoUsuarioPermissao" %>
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

    <h2>
        Perfil/Permissão usuário
    </h2>

    
    <asp:GridView runat="server" ID="grvTipoUsuario" Width="100%"
        AutoGenerateColumns="False" DataKeyNames="IdTipoUsuario"
        AllowSorting="True"
        EmptyDataText="Nenhum registro encontrado."
        OnRowCommand="grvTipoUsuario_RowCommand"
        OnRowDataBound="grvTipoUsuario_RowDataBound"
        Style="margin-right: 0px">
        <Columns>

            <asp:TemplateField HeaderText="Tipo usuário" ConvertEmptyStringToNull="False">
                <ItemTemplate>
                    <asp:Label ID="lblNome" runat="server" Text='<%# Bind("Descricao") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Ações" HeaderStyle-Width="50px">
                <ItemTemplate>
                    <asp:ImageButton runat="server" ID="ibtPermissao" ImageUrl="~/Imagem/group.gif" 
                        CommandArgument='<%# Eval("IdTipoUsuario") %>' CommandName="PERMISSAO" CausesValidation="False" ToolTip="Permissão" />
                </ItemTemplate>
            </asp:TemplateField>

        </Columns>

    </asp:GridView>

    <asp:Button runat="server" ID="btnPost" OnClick="btnPost_Click" CssClass="display-none" />

</asp:Content>
