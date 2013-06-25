using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Noticia.Negocios
{
    public class GrupoTrabalho
    {
        AcessoDados.GrupoTrabalho dalGrupoTrabalho = new AcessoDados.GrupoTrabalho();
        AcessoDados.GrupoTrabalhoUsuario dalGrupoTrabalhoUsuario = new AcessoDados.GrupoTrabalhoUsuario();

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
            try
            {
                return new AcessoDados.GrupoTrabalho().Consultar(grupoTrabalho);
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }

        public List<Entidades.GrupoTrabalho> GruposPorUsuario(Entidades.Usuario usuario)
        {
            try
            {
                List<Entidades.GrupoTrabalho> retorno = new List<Entidades.GrupoTrabalho>();

                foreach (var item in dalGrupoTrabalhoUsuario.Consultar(new Entidades.GrupoTrabalhoUsuario() { Usuario = usuario }))
                {
                    retorno.Add(item.GrupoTrabalho);
                }

                return retorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
