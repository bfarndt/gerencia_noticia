using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Noticia.AcessoDados
{
    /// <summary>
    /// Interface genérico para cadastros
    /// </summary>
    /// <typeparam name="T">Tipo Generico de parametro</typeparam>
    interface ICrud<T>
    {
        List<T> Consultar(T entidade);
        string Inserir(T entidade);
        string Alterar(T entidade);
        string Excluir(T entidade);
    }
}
