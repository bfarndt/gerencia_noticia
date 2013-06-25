﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmListarNoticia.aspx.cs" Inherits="Noticia.Apresentacao.frmListarNoticia" %>
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

    <h2>Listar Notícia
    </h2>

    <fieldset>
        <legend>Filtrar Notícia</legend>
        <table border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td style="padding-left: 10px; padding-right: 10px; height: 26px;">
                    <asp:Label ID="lblTipo" runat="server" Text="Tipo: "></asp:Label></td>
                <td>
                    <asp:DropDownList ID="ddlTipo" Width="320px" runat="server">
                    </asp:DropDownList>
                </td>
                <td style="padding-left: 10px; padding-right: 10px; height: 26px;">
                    <asp:Label ID="Label1" runat="server" Text="Título: "></asp:Label></td>
                <td>
                    <asp:TextBox runat="server" MaxLength="49" Width="320px" ID="txtTitulo"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:ImageButton ID="btnVoltar" AlternateText="Voltar" ImageUrl="~/imagem/btnVoltar.png" runat="server" OnClick="btnVoltar_Click" />
                    <asp:ImageButton ID="btnFiltrar" AlternateText="Filtrar funcionarios" ImageUrl="~/Imagem/btnFiltrar.png" runat="server" OnClick="btnFiltrar_Click" />
                    <asp:ImageButton ID="btnNovo" AlternateText="Inserir Notícias" ImageUrl="~/Imagem/btnNovo.png" runat="server" OnClick="btnNovo_Click" />
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

            <asp:TemplateField HeaderText="Título" ConvertEmptyStringToNull="False" ControlStyle-Width="300">
                <ItemTemplate>
                    <asp:Label ID="lblTitulo" runat="server" Text='<%# Bind("Titulo") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Ações">
                <ItemTemplate>
                    <asp:ImageButton runat="server" ID="ibtVisualizar" ImageUrl="~/Imagem/magnifier.png"
                        CommandArgument='<%# Eval("IdNoticia") %>' CommandName="VISUALIZAR" CausesValidation="False" ToolTip="Visualizar" />
                    <asp:ImageButton runat="server" ID="ibtEditar" ImageUrl="~/Imagem/ico_edit_grid.gif"
                        CommandArgument='<%# Eval("IdNoticia") %>' CommandName="EDITAR" CausesValidation="False" ToolTip="Editar" />
                    <asp:ImageButton runat="server" OnClientClick="if(confirm('Deseja realmente remover este Notícia?')){return true;}else{return false;}" ID="ibtExcluir" ImageUrl="~/Imagem/ico_delete_grid.gif"
                        CommandArgument='<%# Eval("IdNoticia") %>' CommandName="EXCLUIR" CausesValidation="False" ToolTip="Excluir" />
                </ItemTemplate>
            </asp:TemplateField>

        </Columns>

    </asp:GridView>

    <asp:Button runat="server" ID="btnPost" OnClick="btnPost_Click" CssClass="display-none" />

</asp:Content>
