using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Noticia.Negocios
{
    public class GrupoTrabalho
    {
        AcessoDados.GrupoTrabalho dalGrupoTrabalho = new AcessoDados.GrupoTrabalho();

        public bool TemGrupoTrabalhoEmBranco(Entidades.GrupoTrabalho grupoTrabalho)
        {
            return string.IsNullOrWhiteSpace(grupoTrabalho.Descricao);
        }

        public bool TemGrupoTrabalhoExistente(Entidades.GrupoTrabalho grupoTrabalho)
        {
            if (!TemGrupoTrabalhoEmBranco(grupoTrabalho))
            {
                var gruposAproximados = dalGrupoTrabalho.Consultar(new Entidades.GrupoTrabalho() { IdGrupoTrabalho = null, Descricao = grupoTrabalho.Descricao });
                if (gruposAproximados.Count > 0)
                {
                    int found = (from f in gruposAproximados
                                 where f.Descricao == grupoTrabalho.Descricao
                                 select f).Count();
                    return (found > 0);
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return true;
            }
        }



        public List<Entidades.GrupoTrabalho> Listar(Entidades.GrupoTrabalho grupoTrabalho)
        {
            throw new NotImplementedException();
        }
    }
}
