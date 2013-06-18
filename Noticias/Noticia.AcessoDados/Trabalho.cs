using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Noticia.AcessoDados
{
    public class Trabalho : ICrud<Entidades.Trabalho>
    {
        public List<Entidades.Trabalho> Consultar(Entidades.Trabalho entidade)
        {
            try
            {
                DataTable objDataTable = null;

                Dados.LimparParametros();
                Dados.AdicionarParametros("@vchAcao", "SELECIONAR");
                Dados.AdicionarParametros("@intIdTrabalho", entidade.IdTrabalho);
                Dados.AdicionarParametros("@intIdTipoUsuario", entidade.TipoUsuario.IdTipoUsuario);

                objDataTable = Dados.ExecutaConsultar(System.Data.CommandType.StoredProcedure, "spTrabalho");

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
                    objNovoTrabalho.TipoUsuario = new AcessoDados.TipoUsuario().Consultar(objNovoTrabalho.TipoUsuario).First();

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
                Dados.LimparParametros();
                object objRetorno = null;
                if (entidade != null)
                {
                    Dados.AdicionarParametros("@vchAcao", "INSERIR");
                    Dados.AdicionarParametros("@intIdTipoUsuario", entidade.TipoUsuario.IdTipoUsuario);
                    Dados.AdicionarParametros("@decValorHoraTrabalhada", entidade.ValorHoraTrabalhada);

                    objRetorno = Dados.ExecutarManipulacao(CommandType.StoredProcedure, "spTrabalho");
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
                Dados.LimparParametros();
                object objRetorno = null;
                if (entidade != null && entidade.IdTrabalho > 0)
                {
                    Dados.AdicionarParametros("@vchAcao", "ALTERAR");
                    Dados.AdicionarParametros("@intIdTrabalho", entidade.IdTrabalho);
                    Dados.AdicionarParametros("@intIdTipoUsuario", entidade.TipoUsuario.IdTipoUsuario);
                    Dados.AdicionarParametros("@decValorHoraTrabalhada", entidade.ValorHoraTrabalhada);

                    objRetorno = Dados.ExecutarManipulacao(CommandType.StoredProcedure, "spTrabalho");
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
                Dados.LimparParametros();
                object objRetorno = null;
                if (entidade != null && entidade.IdTrabalho > 0)
                {
                    Dados.AdicionarParametros("@vchAcao", "DELETAR");
                    Dados.AdicionarParametros("@intIdTrabalho", entidade.IdTrabalho);

                    objRetorno = Dados.ExecutarManipulacao(CommandType.StoredProcedure, "spTrabalho");
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
