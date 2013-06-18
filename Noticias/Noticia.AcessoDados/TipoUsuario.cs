using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Noticia.AcessoDados
{
    public class TipoUsuario : ICrud<Entidades.TipoUsuario>
    {
        public List<Entidades.TipoUsuario> Consultar(Entidades.TipoUsuario entidade)
        {
            try
            {
                DataTable objDataTable = null;

                Dados.LimparParametros();
                Dados.AdicionarParametros("@vchAcao", "SELECIONAR");
                Dados.AdicionarParametros("@intIdTipoUsuario", entidade.IdTipoUsuario);
                Dados.AdicionarParametros("@vchDescricao", entidade.Descricao);

                objDataTable = Dados.ExecutaConsultar(System.Data.CommandType.StoredProcedure, "spTipoUsuario");

                List<Entidades.TipoUsuario> objRetorno = new List<Entidades.TipoUsuario>();

                if (objDataTable.Rows.Count <= 0)
                {
                    return objRetorno;
                }

                foreach (DataRow objLinha in objDataTable.Rows)
                {
                    Entidades.TipoUsuario objNovoTipoUsuario = new Entidades.TipoUsuario();

                    objNovoTipoUsuario.IdTipoUsuario = objLinha["IdTipoUsuario"] != DBNull.Value ? Convert.ToInt32(objLinha["IdTipoUsuario"]) : 0;
                    objNovoTipoUsuario.Descricao = objLinha["Descricao"] != DBNull.Value ? Convert.ToString(objLinha["Descricao"]) : null;

                    objRetorno.Add(objNovoTipoUsuario);
                }

                return objRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string Inserir(Entidades.TipoUsuario entidade)
        {
            try
            {
                Dados.LimparParametros();
                object objRetorno = null;
                if (entidade != null)
                {
                    Dados.AdicionarParametros("@vchAcao", "INSERIR");
                    Dados.AdicionarParametros("@vchDescricao", entidade.Descricao);

                    objRetorno = Dados.ExecutarManipulacao(CommandType.StoredProcedure, "spTipoUsuario");
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

        public string Alterar(Entidades.TipoUsuario entidade)
        {
            try
            {
                Dados.LimparParametros();
                object objRetorno = null;
                if (entidade != null && entidade.IdTipoUsuario > 0)
                {
                    Dados.AdicionarParametros("@vchAcao", "ALTERAR");
                    Dados.AdicionarParametros("@intIdTipoUsuario", entidade.IdTipoUsuario);
                    Dados.AdicionarParametros("@vchDescricao", entidade.Descricao);

                    objRetorno = Dados.ExecutarManipulacao(CommandType.StoredProcedure, "spTipoUsuario");
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

        public string Excluir(Entidades.TipoUsuario entidade)
        {
            try
            {
                Dados.LimparParametros();
                object objRetorno = null;
                if (entidade != null && entidade.IdTipoUsuario > 0)
                {
                    Dados.AdicionarParametros("@vchAcao", "DELETAR");
                    Dados.AdicionarParametros("@intIdTipoUsuario", entidade.IdTipoUsuario);

                    objRetorno = Dados.ExecutarManipulacao(CommandType.StoredProcedure, "spTipoUsuario");
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
