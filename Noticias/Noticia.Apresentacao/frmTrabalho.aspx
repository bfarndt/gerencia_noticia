<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmTrabalho.aspx.cs" Inherits="Noticia.Apresentacao.frmTrabalho" %>
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

    <h2>
        Definição de trabalho
    </h2>
    <p>
    
    <asp:GridView Width="100%" ID="grvTrabalho" runat="server" 
        DataKeyNames="IdTrabalho"
        AutoGenerateColumns="False" ShowFooter="True" 
        onrowcancelingedit="grvTrabalho_RowCancelingEdit" 
        onrowcommand="grvTrabalho_RowCommand" onrowdatabound="grvTrabalho_RowDataBound" 
        onrowdeleting="grvTrabalho_RowDeleting" onrowediting="grvTrabalho_RowEditing" 
        EmptyDataText="Nenhum registro encontrado."
        onrowupdating="grvTrabalho_RowUpdating">
        <Columns>   

            <asp:TemplateField HeaderText="Tipo usuário" ConvertEmptyStringToNull="False">
                <ItemTemplate>
                    <asp:Label ID="lblTipoUsuario" runat="server" Text='<%# Bind("TipoUsuario.Descricao") %>' ></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Valor Hora" ConvertEmptyStringToNull="False">
                <ItemTemplate>
                    <asp:Label ID="lblValorHora" runat="server" Text='<%# Bind("ValorHoraTrabalhada") %>' ></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:Textbox ID="txtValorHoraEdit" runat="server" MaxLength="9" Width="420" Text='<%# Bind("ValorHoraTrabalhada") %>' ></asp:Textbox>
                    <asp:RequiredFieldValidator ID="rfvValorHoraEdit" runat="server"
                        ControlToValidate="txtValorHoraEdit"
                        ErrorMessage="Informe o valor."
                        ValidationGroup="validacao">*</asp:RequiredFieldValidator>
                </EditItemTemplate>
            </asp:TemplateField>
           
            <asp:TemplateField  HeaderText="Editar">
                <EditItemTemplate>
                    <asp:ImageButton ID="ibtn_update" runat="server" CommandName="Update" AlternateText="Salvar" ToolTip="Salvar" 
                        ImageUrl="~/Imagem/ico_salvar_grid.gif" ValidationGroup="validacao" CausesValidation="true" />

                </EditItemTemplate>
                <ItemTemplate>
                        <asp:ImageButton ID="ibtn_edit" runat="server" ImageUrl="~/Imagem/ico_edit_grid.gif" CausesValidation="False" CommandName="Edit"
                         AlternateText="Editar" ToolTip="Editar" />
                </ItemTemplate>
                <ItemStyle Width="40px" />
                <FooterStyle Width="40px" />
            </asp:TemplateField>
                        
        </Columns>
    </asp:GridView>

    </p>


</asp:Content>
