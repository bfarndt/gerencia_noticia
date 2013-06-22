using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Noticia.Entidades
{
    [Serializable]
    public class Permissao
    {
        public int? IdPermissao { get; set; }
        public string Descricao { get; set; }
    }

    public enum PermissaoEnum
    {
        Efetuar_Acesso = 1,
        Criar_Noticia = 2,
        Manter_grupo_de_trabalho = 3,
        Submeter_Imagens = 4,
        Associar_Imagens = 5,
        Editar_Noticia = 6,
        Selecionar_Imagens = 7,
        Submeter_Noticia = 8,
        Avaliar_Noticia = 9,
        Manter_Usuario = 10
    }
}