<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmCriarNoticia.aspx.cs" Inherits="Noticia.Apresentacao.frmCriarNoticia" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    
    <h2>
        Manter Usuário
    </h2>

    <table>
        <tr>
            <td class="labelForm">Título:</td>
            <td>
                <asp:TextBox runat="server" MaxLength="49" Width="208px" ID="txtTitulo"></asp:TextBox>
            </td>
        </tr>
    </table>

        <div class="contentFinal">
            <div class="AcaoFormulario">
                <asp:Panel runat="server" ID="pnlAcao">
                    <asp:ImageButton ID="btn_salvar" CausesValidation="true" 
                        ValidationGroup="validacao" runat="server" ImageUrl="../imagem/btnSalvar.png" 
                        onclick="btn_salvar_Click" />
                    <asp:ImageButton ID="btnNovo" Visible="false" runat="server" ImageUrl="../imagem/btnNovo.png" 
                        onclick="btnNovo_Click" />
                    <asp:ImageButton ID="btnVoltar" AlternateText="Voltar para listagem" 
                        ImageUrl="~/imagem/btnVoltar.png" runat="server" onclick="btnVoltar_Click" />
                </asp:Panel>
            </div>
        </div> 

</asp:Content>
