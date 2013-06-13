using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Noticia.AcessoDados
{
    public class Usuario : ICrud<Entidades.Usuario>
    {
        AcessoDadosSqlServer objDados = new AcessoDadosSqlServer();

        public List<Entidades.Usuario> Consultar(Entidades.Usuario entidade)
        {
            try
            {
                DataTable objDataTable = null;

                objDados.LimparParametros();
                objDados.AdicionarParametros("@vchAcao", "SELECIONAR");
                objDados.AdicionarParametros("@intIdUsuario", entidade.IdUsuario);
                objDados.AdicionarParametros("@vchLogin", entidade.Login);
                objDados.AdicionarParametros("@vchSenha", entidade.Senha);
                objDados.AdicionarParametros("@vchNome", entidade.Nome);
                objDados.AdicionarParametros("@intIdTipoUsuario", entidade.TipoUsuario.IdTipoUsuario);

                objDataTable = objDados.ExecutaConsultar(System.Data.CommandType.StoredProcedure, "spUsuario");

                List<Entidades.Usuario> objRetorno = new List<Entidades.Usuario>();

                if (objDataTable.Rows.Count <= 0)
                {
                    return objRetorno;
                }

                foreach (DataRow objLinha in objDataTable.Rows)
                {
                    Entidades.Usuario objNovoUsuario = new Entidades.Usuario();

                    objNovoUsuario.IdUsuario = objLinha["IdUsuario"] != DBNull.Value ? Convert.ToInt32(objLinha["IdNoticia"]) : 0;
                    objNovoUsuario.Login = objLinha["Login"] != DBNull.Value ? Convert.ToString(objLinha["Login"]) : null;
                    objNovoUsuario.Senha = objLinha["Senha"] != DBNull.Value ? Convert.ToString(objLinha["Senha"]) : null;
                    objNovoUsuario.Nome = objLinha["Nome"] != DBNull.Value ? Convert.ToString(objLinha["Nome"]) : null;
                    objNovoUsuario.TipoUsuario = new Entidades.TipoUsuario()
                    {
                        IdTipoUsuario = objLinha["IdTipoUsuario"] != DBNull.Value ? Convert.ToInt32(objLinha["IdTipoUsuario"]) : 0
                    };
                    objNovoUsuario.TipoUsuario = new AcessoDados.TipoUsuario().Consultar(objNovoUsuario.TipoUsuario).First();

                    objRetorno.Add(objNovoUsuario);
                }

                return objRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string Inserir(Entidades.Usuario entidade)
        {
            try
            {
                objDados.LimparParametros();
                object objRetorno = null;
                if (entidade != null)
                {
                    objDados.AdicionarParametros("@vchAcao", "INSERIR");
                    objDados.AdicionarParametros("@vchLogin", entidade.Login);
                    objDados.AdicionarParametros("@vchSenha", entidade.Senha);
                    objDados.AdicionarParametros("@vchNome", entidade.Nome);
                    objDados.AdicionarParametros("@intIdTipoUsuario", entidade.TipoUsuario.IdTipoUsuario);

                    objRetorno = objDados.ExecutarManipulacao(CommandType.StoredProcedure, "spUsuario");
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

        public string Alterar(Entidades.Usuario entidade)
        {
            try
            {
                objDados.LimparParametros();
                object objRetorno = null;
                if (entidade != null && entidade.IdUsuario > 0)
                {
                    objDados.AdicionarParametros("@vchAcao", "ALTERAR");
                    objDados.AdicionarParametros("@intIdUsuario", entidade.IdUsuario);
                    objDados.AdicionarParametros("@vchLogin", entidade.Login);
                    objDados.AdicionarParametros("@vchSenha", entidade.Senha);
                    objDados.AdicionarParametros("@vchNome", entidade.Nome);
                    objDados.AdicionarParametros("@intIdTipoUsuario", entidade.TipoUsuario.IdTipoUsuario);

                    objRetorno = objDados.ExecutarManipulacao(CommandType.StoredProcedure, "spUsuario");
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

        public string Excluir(Entidades.Usuario entidade)
        {
            try
            {
                objDados.LimparParametros();
                object objRetorno = null;
                if (entidade != null && entidade.IdUsuario > 0)
                {
                    objDados.AdicionarParametros("@vchAcao", "DELETAR");
                    objDados.AdicionarParametros("@intIdUsuario", entidade.IdUsuario);

                    objRetorno = objDados.ExecutarManipulacao(CommandType.StoredProcedure, "spUsuario");
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
