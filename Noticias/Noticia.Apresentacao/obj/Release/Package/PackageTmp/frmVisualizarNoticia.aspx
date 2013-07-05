<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmVisualizarNoticia.aspx.cs" Inherits="Noticia.Apresentacao.frmVisualizarNoticia" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="~/Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="~/Styles/StyleSheet.css" rel="stylesheet" type="text/css" />
    <link href="~/Styles/highslide.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function fecha() {
            var oMe = window.self;
            oMe.opener = window.self;
            oMe.close();
        }
    </script>
</head>
<body style="background-color: white; height: auto">
    <form id="form1" runat="server">
        <div class="contentPage" style="height: 353px">
            <asp:ValidationSummary runat="server" ID="vs" ValidationGroup="validacao" />

            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>



            <div style="border-top">

                <div class="legendFormulario">
                    Notícia
                </div>
                <table>
                    <tr>
                        <td class="labelForm">Título:</td>
                        <td>
                            <asp:TextBox runat="server" ReadOnly="true" MaxLength="40" Width="300px" ID="txtTitulo"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="labelForm">Conteúdo:</td>
                        <td>
                            <asp:TextBox runat="server" ReadOnly="true" MaxLength="1000" Width="500px" Height="300px" TextMode="MultiLine" ID="txtConteudo"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </div>
            <div style="border-top">
                <div class="legendFormulario">
                    Imagens
                </div>
                <asp:GridView runat="server" ID="grvImagem" Width="400"
                    AutoGenerateColumns="False" DataKeyNames="IdImagem"
                    AllowSorting="True"
                    OnRowDataBound="grvImagem_RowDataBound"
                    EmptyDataText="Nenhum registro encontrado.">
                    <Columns>

                        <asp:TemplateField HeaderText="Legenda" ConvertEmptyStringToNull="False">
                            <ItemTemplate>
                                <asp:Label ID="lblLegenda" runat="server" Text='<%# Bind("Legenda") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Imagem" ConvertEmptyStringToNull="False">
                            <ItemTemplate>
                                <asp:Image ID="imgFoto" runat="server" Height="200" Width="230" />
                            </ItemTemplate>
                        </asp:TemplateField>

                    </Columns>

                </asp:GridView>
            </div>


            <br />

            <div class="contentFinal">
            </div>
        </div>

    </form>
</body>
</html>
