using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Noticia.Negocios
{
    public class Trabalho
    {
        AcessoDados.DiasTrabalhados dalUsuarioPermissao = new AcessoDados.DiasTrabalhados();

        public List<Entidades.Trabalho> Listar()
        {
            try
            {
                return new AcessoDados.Trabalho().Consultar(new Entidades.Trabalho() { IdTrabalho = null, TipoUsuario = null });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Entidades.DiaSemana> DiasTrabalhoPorUsuario(Entidades.Usuario usuario)
        {
            try
            {
                List<Entidades.DiaSemana> retorno = new List<Entidades.DiaSemana>();

                foreach (var item in dalUsuarioPermissao.Consultar(new Entidades.DiasTrabalhados() { Usuario = usuario, DiaSemana = null }))
                {
                    retorno.Add(item.DiaSemana);
                }

                return retorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Entidades.DiaSemana> ListarDiasSemana()
        {
            try
            {
                return new AcessoDados.DiaSemana().Consultar(new Entidades.DiaSemana() { IdDia = null });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
