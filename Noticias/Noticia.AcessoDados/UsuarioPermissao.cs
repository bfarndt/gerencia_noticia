using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Noticia.AcessoDados
{
    public class UsuarioPermissao : ICrud<Entidades.UsuarioPermissao>
    {
        AcessoDadosSqlServer objDados = new AcessoDadosSqlServer();

        public List<Entidades.UsuarioPermissao> Consultar(Entidades.UsuarioPermissao entidade)
        {
            try
            {
                DataTable objDataTable = null;

                objDados.LimparParametros();
                objDados.AdicionarParametros("@vchAcao", "SELECIONAR");
                objDados.AdicionarParametros("@intIdUsuario", entidade.Usuario.IdUsuario);
                objDados.AdicionarParametros("@intIdPermissao", entidade.Permissao.IdPermissao);

                objDataTable = objDados.ExecutaConsultar(System.Data.CommandType.StoredProcedure, "spUsuarioPermissao");

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
                    objNovoUsuarioPermissao.Permissao = new Entidades.Permissao();
                    objNovoUsuarioPermissao.Permissao.IdPermissao = objLinha["IdPermissao"] != DBNull.Value ? Convert.ToInt32(objLinha["IdPermissao"]) : 0;

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
                objDados.LimparParametros();
                object objRetorno = null;
                if (entidade != null)
                {
                    objDados.AdicionarParametros("@vchAcao", "INSERIR");
                    objDados.AdicionarParametros("@intIdUsuario", entidade.Usuario.IdUsuario);
                    objDados.AdicionarParametros("@intIdPermissao", entidade.Permissao.IdPermissao);

                    objRetorno = objDados.ExecutarManipulacao(CommandType.StoredProcedure, "spUsuarioPermissao");
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
                objDados.LimparParametros();
                object objRetorno = null;
                if (entidade != null && entidade.Usuario != null && entidade.Usuario.IdUsuario > 0 &&
                    entidade.Permissao != null && entidade.Permissao.IdPermissao > 0)
                {
                    objDados.AdicionarParametros("@vchAcao", "DELETAR");
                    objDados.AdicionarParametros("@intIdUsuario", entidade.Usuario.IdUsuario);
                    objDados.AdicionarParametros("@intIdPermissao", entidade.Permissao.IdPermissao);

                    objRetorno = objDados.ExecutarManipulacao(CommandType.StoredProcedure, "spUsuarioPermissao");
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
