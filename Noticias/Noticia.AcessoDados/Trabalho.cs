using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Noticia.AcessoDados
{
    public class Trabalho : ICrud<Entidades.Trabalho>
    {
        AcessoDadosSqlServer objDados = new AcessoDadosSqlServer();

        public List<Entidades.Trabalho> Consultar(Entidades.Trabalho entidade)
        {
            try
            {
                DataTable objDataTable = null;

                objDados.LimparParametros();
                objDados.AdicionarParametros("@vchAcao", "SELECIONAR");
                objDados.AdicionarParametros("@intIdTrabalho", entidade.IdTrabalho);
                objDados.AdicionarParametros("@intIdTipoUsuario", entidade.TipoUsuario.IdTipoUsuario);

                objDataTable = objDados.ExecutaConsultar(System.Data.CommandType.StoredProcedure, "spTrabalho");

                List<Entidades.Trabalho> objRetorno = new List<Entidades.Trabalho>();

                if (objDataTable.Rows.Count <= 0)
                {
                    return objRetorno;
                }

                foreach (DataRow objLinha in objDataTable.Rows)
                {
                    Entidades.Trabalho objNovoTrabalho = new Entidades.Trabalho();

                    objNovoTrabalho.TipoUsuario = new Entidades.TipoUsuario();
                    objNovoTrabalho.IdTrabalho = objLinha["IdTrabalho"] != DBNull.Value ? Convert.ToInt32(objLinha["IdTrabalho"]) : 0;
                    objNovoTrabalho.TipoUsuario.IdTipoUsuario = objLinha["IdTipoUsuario"] != DBNull.Value ? Convert.ToInt32(objLinha["IdTipoUsuario"]) : 0;
                    objNovoTrabalho.ValorHoraTrabalhada = objLinha["ValorHoraTrabalhada"] != DBNull.Value ? Convert.ToDecimal(objLinha["ValorHoraTrabalhada"]) : 0;

                    objRetorno.Add(objNovoTrabalho);
                }

                return objRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string Inserir(Entidades.Trabalho entidade)
        {
            try
            {
                objDados.LimparParametros();
                object objRetorno = null;
                if (entidade != null)
                {
                    objDados.AdicionarParametros("@vchAcao", "INSERIR");
                    objDados.AdicionarParametros("@intIdTipoUsuario", entidade.TipoUsuario.IdTipoUsuario);
                    objDados.AdicionarParametros("@decValorHoraTrabalhada", entidade.ValorHoraTrabalhada);

                    objRetorno = objDados.ExecutarManipulacao(CommandType.StoredProcedure, "spTrabalho");
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

        public string Alterar(Entidades.Trabalho entidade)
        {
            try
            {
                objDados.LimparParametros();
                object objRetorno = null;
                if (entidade != null && entidade.IdTrabalho > 0)
                {
                    objDados.AdicionarParametros("@vchAcao", "ALTERAR");
                    objDados.AdicionarParametros("@intIdTrabalho", entidade.IdTrabalho);
                    objDados.AdicionarParametros("@intIdTipoUsuario", entidade.TipoUsuario.IdTipoUsuario);
                    objDados.AdicionarParametros("@decValorHoraTrabalhada", entidade.ValorHoraTrabalhada);

                    objRetorno = objDados.ExecutarManipulacao(CommandType.StoredProcedure, "spTrabalho");
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

        public string Excluir(Entidades.Trabalho entidade)
        {
            try
            {
                objDados.LimparParametros();
                object objRetorno = null;
                if (entidade != null && entidade.IdTrabalho > 0)
                {
                    objDados.AdicionarParametros("@vchAcao", "DELETAR");
                    objDados.AdicionarParametros("@intIdTrabalho", entidade.IdTrabalho);

                    objRetorno = objDados.ExecutarManipulacao(CommandType.StoredProcedure, "spTrabalho");
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
