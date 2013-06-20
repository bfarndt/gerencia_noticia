using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Noticia.AcessoDados
{
    public class UsuarioPermissao : ICrud<Entidades.UsuarioPermissao>
    {
        public List<Entidades.UsuarioPermissao> Consultar(Entidades.UsuarioPermissao entidade)
        {
            try
            {
                DataTable objDataTable = null;

                Dados.LimparParametros();
                Dados.AdicionarParametros("@vchAcao", "SELECIONAR");
                if (entidade.Usuario != null)
                    Dados.AdicionarParametros("@intIdUsuario", entidade.Usuario.IdUsuario);
                if (entidade.Permissao != null)
                    Dados.AdicionarParametros("@intIdPermissao", entidade.Permissao.IdPermissao);

                objDataTable = Dados.ExecutaConsultar(System.Data.CommandType.StoredProcedure, "spUsuarioPermissao");

                List<Entidades.UsuarioPermissao> objRetorno = new List<Entidades.UsuarioPermissao>();

                if (objDataTable.Rows.Count <= 0)
                {
                    return objRetorno;
                }

                foreach (DataRow objLinha in objDataTable.Rows)
                {
                    Entidades.UsuarioPermissao objNovoUsuarioPermissao = new Entidades.UsuarioPermissao();

                    objNovoUsuarioPermissao.Usuario = new Entidades.Usuario();
                    objNovoUsuarioPermissao.Usuario.IdUsuario = objLinha["IdUsuario"] != DBNull.Value ? Convert.ToInt32(objLinha["IdUsuario"]) : 0;
                    objNovoUsuarioPermissao.Usuario = new AcessoDados.Usuario().Consultar(objNovoUsuarioPermissao.Usuario).First();

                    objNovoUsuarioPermissao.Permissao = new Entidades.Permissao();
                    objNovoUsuarioPermissao.Permissao.IdPermissao = objLinha["IdPermissao"] != DBNull.Value ? Convert.ToInt32(objLinha["IdPermissao"]) : 0;
                    objNovoUsuarioPermissao.Permissao = new AcessoDados.Permissao().Consultar(objNovoUsuarioPermissao.Permissao).First();

                    objRetorno.Add(objNovoUsuarioPermissao);
                }

                return objRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string Inserir(Entidades.UsuarioPermissao entidade)
        {
            try
            {
                Dados.LimparParametros();
                object objRetorno = null;
                if (entidade != null)
                {
                    Dados.AdicionarParametros("@vchAcao", "INSERIR");
                    Dados.AdicionarParametros("@intIdUsuario", entidade.Usuario.IdUsuario);
                    Dados.AdicionarParametros("@intIdPermissao", entidade.Permissao.IdPermissao);

                    objRetorno = Dados.ExecutarManipulacao(CommandType.StoredProcedure, "spUsuarioPermissao");
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

        public string Alterar(Entidades.UsuarioPermissao entidade)
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

        public string Excluir(Entidades.UsuarioPermissao entidade)
        {
            try
            {
                Dados.LimparParametros();
                object objRetorno = null;
                if (entidade != null && entidade.Usuario != null && entidade.Usuario.IdUsuario > 0 &&
                    entidade.Permissao != null && entidade.Permissao.IdPermissao > 0)
                {
                    Dados.AdicionarParametros("@vchAcao", "DELETAR");
                    Dados.AdicionarParametros("@intIdUsuario", entidade.Usuario.IdUsuario);
                    Dados.AdicionarParametros("@intIdPermissao", entidade.Permissao.IdPermissao);

                    objRetorno = Dados.ExecutarManipulacao(CommandType.StoredProcedure, "spUsuarioPermissao");
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

        public string ExcluirPorUsuario(Entidades.Usuario entidade)
        {
            try
            {
                Dados.LimparParametros();
                object objRetorno = null;
                if (entidade != null && entidade.IdUsuario > 0)
                {
                    Dados.AdicionarParametros("@vchAcao", "DELETAR_POR_USUARIO");
                    Dados.AdicionarParametros("@intIdUsuario", entidade.IdUsuario);

                    objRetorno = Dados.ExecutarManipulacao(CommandType.StoredProcedure, "spUsuarioPermissao");
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
