using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Noticia.AcessoDados
{
    public class DiasTrabalhados
    {
        public List<Entidades.DiasTrabalhados> Consultar(Entidades.DiasTrabalhados entidade)
        {
            try
            {
                DataTable objDataTable = null;

                Dados.LimparParametros();
                Dados.AdicionarParametros("@vchAcao", "SELECIONAR");
                Dados.AdicionarParametros("@intIdUsuario", entidade.Usuario.IdUsuario);
                Dados.AdicionarParametros("@intIdDia", entidade.DiaSemana.IdDia);

                objDataTable = Dados.ExecutaConsultar(System.Data.CommandType.StoredProcedure, "spDiasTrabalhados");

                List<Entidades.DiasTrabalhados> objRetorno = new List<Entidades.DiasTrabalhados>();

                if (objDataTable.Rows.Count <= 0)
                {
                    return objRetorno;
                }

                foreach (DataRow objLinha in objDataTable.Rows)
                {
                    Entidades.DiasTrabalhados objNovoDiasTrabalhados = new Entidades.DiasTrabalhados();

                    objNovoDiasTrabalhados.Usuario = new Entidades.Usuario();
                    objNovoDiasTrabalhados.Usuario.IdUsuario = objLinha["IdUsuario"] != DBNull.Value ? Convert.ToInt32(objLinha["IdUsuario"]) : 0;
                    objNovoDiasTrabalhados.Usuario = new AcessoDados.Usuario().Consultar(objNovoDiasTrabalhados.Usuario).First();

                    objNovoDiasTrabalhados.DiaSemana = new Entidades.DiaSemana();
                    objNovoDiasTrabalhados.DiaSemana.IdDia = objLinha["IdDia"] != DBNull.Value ? Convert.ToInt32(objLinha["IdDia"]) : 0;
                    objNovoDiasTrabalhados.DiaSemana = new AcessoDados.DiaSemana().Consultar(objNovoDiasTrabalhados.DiaSemana).First();

                    objRetorno.Add(objNovoDiasTrabalhados);
                }

                return objRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string Inserir(Entidades.DiasTrabalhados entidade)
        {
            try
            {
                Dados.LimparParametros();
                object objRetorno = null;
                if (entidade != null)
                {
                    Dados.AdicionarParametros("@vchAcao", "INSERIR");
                    Dados.AdicionarParametros("@intIdUsuario", entidade.Usuario.IdUsuario);
                    Dados.AdicionarParametros("@intIdDia", entidade.DiaSemana.IdDia);

                    objRetorno = Dados.ExecutarManipulacao(CommandType.StoredProcedure, "spDiasTrabalhados");
                }

                int intResultado = 0;
                if (objRetorno != null)
                {
                    if (int.TryParse(objRetorno.ToString(), out intResultado))
                        return intResultado.ToString();
                    else
                        throw new Exception(objRetorno.ToString());
                }
                else
                {
                    return "Não foi possível executar";
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string Alterar(Entidades.DiasTrabalhados entidade)
        {
            try
            {
                return "Utilize o excluir depois inserir";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string Excluir(Entidades.DiasTrabalhados entidade)
        {
            try
            {
                Dados.LimparParametros();
                object objRetorno = null;
                if (entidade != null && entidade.Usuario != null && entidade.Usuario.IdUsuario > 0 &&
                    entidade.DiaSemana != null && entidade.DiaSemana.IdDia > 0)
                {
                    Dados.AdicionarParametros("@vchAcao", "DELETAR");
                    Dados.AdicionarParametros("@intIdUsuario", entidade.Usuario.IdUsuario);
                    Dados.AdicionarParametros("@intIdDia", entidade.DiaSemana.IdDia);

                    objRetorno = Dados.ExecutarManipulacao(CommandType.StoredProcedure, "spDiasTrabalhados");
                }

                int intResultado = 0;
                if (objRetorno != null)
                {
                    if (int.TryParse(objRetorno.ToString(), out intResultado))
                        return intResultado.ToString();
                    else
                        throw new Exception(objRetorno.ToString());
                }
                else
                {
                    return "Não foi possível executar";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
