using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Noticia.AcessoDados
{
    public class Permissao : ICrud<Entidades.Permissao>
    {
        public List<Entidades.Permissao> Consultar(Entidades.Permissao entidade)
        {
            try
            {
                DataTable objDataTable = null;

                Dados.LimparParametros();
                Dados.AdicionarParametros("@vchAcao", "SELECIONAR");
                Dados.AdicionarParametros("@intIdPermissao", entidade.IdPermissao);
                Dados.AdicionarParametros("@vchDescricao", entidade.Descricao);

                objDataTable = Dados.ExecutaConsultar(System.Data.CommandType.StoredProcedure, "spPermissao");

                List<Entidades.Permissao> objRetorno = new List<Entidades.Permissao>();

                if (objDataTable.Rows.Count <= 0)
                {
                    return objRetorno;
                }

                foreach (DataRow objLinha in objDataTable.Rows)
                {
                    Entidades.Permissao objNovoPermissao = new Entidades.Permissao();

                    objNovoPermissao.IdPermissao = objLinha["IdPermissao"] != DBNull.Value ? Convert.ToInt32(objLinha["IdPermissao"]) : 0;
                    objNovoPermissao.Descricao = objLinha["Descricao"] != DBNull.Value ? Convert.ToString(objLinha["Descricao"]) : null;

                    objRetorno.Add(objNovoPermissao);
                }

                return objRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string Inserir(Entidades.Permissao entidade)
        {
            try
            {
                Dados.LimparParametros();
                object objRetorno = null;
                if (entidade != null)
                {
                    Dados.AdicionarParametros("@vchAcao", "INSERIR");
                    Dados.AdicionarParametros("@vchDescricao", entidade.Descricao);

                    objRetorno = Dados.ExecutarManipulacao(CommandType.StoredProcedure, "spPermissao");
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

        public string Alterar(Entidades.Permissao entidade)
        {
            try
            {
                Dados.LimparParametros();
                object objRetorno = null;
                if (entidade != null && entidade.IdPermissao > 0)
                {
                    Dados.AdicionarParametros("@vchAcao", "ALTERAR");
                    Dados.AdicionarParametros("@intIdPermissao", entidade.IdPermissao);
                    Dados.AdicionarParametros("@vchDescricao", entidade.Descricao);

                    objRetorno = Dados.ExecutarManipulacao(CommandType.StoredProcedure, "spPermissao");
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

        public string Excluir(Entidades.Permissao entidade)
        {
            try
            {
                Dados.LimparParametros();
                object objRetorno = null;
                if (entidade != null && entidade.IdPermissao > 0)
                {
                    Dados.AdicionarParametros("@vchAcao", "DELETAR");
                    Dados.AdicionarParametros("@intIdPermissao", entidade.IdPermissao);

                    objRetorno = Dados.ExecutarManipulacao(CommandType.StoredProcedure, "spPermissao");
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
