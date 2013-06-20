using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Noticia.AcessoDados
{
    public class Usuario : ICrud<Entidades.Usuario>
    {

        AcessoDados.TipoUsuario dadosTipoUsuario = new AcessoDados.TipoUsuario();

        public List<Entidades.Usuario> Consultar(Entidades.Usuario entidade)
        {
            try
            {
                DataTable objDataTable = null;

                Dados.LimparParametros();
                Dados.AdicionarParametros("@vchAcao", "SELECIONAR");
                Dados.AdicionarParametros("@intIdUsuario", entidade.IdUsuario);
                Dados.AdicionarParametros("@vchLogin", entidade.Login);
                Dados.AdicionarParametros("@vchSenha", entidade.Senha);
                Dados.AdicionarParametros("@vchNome", entidade.Nome);
                if (entidade.TipoUsuario != null && entidade.TipoUsuario.IdTipoUsuario > 0)
                    Dados.AdicionarParametros("@intIdTipoUsuario", entidade.TipoUsuario.IdTipoUsuario);

                objDataTable = Dados.ExecutaConsultar(System.Data.CommandType.StoredProcedure, "spUsuario");

                List<Entidades.Usuario> objRetorno = new List<Entidades.Usuario>();

                if (objDataTable.Rows.Count <= 0)
                {
                    return objRetorno;
                }

                foreach (DataRow objLinha in objDataTable.Rows)
                {
                    Entidades.Usuario objNovoUsuario = new Entidades.Usuario();

                    objNovoUsuario.IdUsuario = objLinha["IdUsuario"] != DBNull.Value ? Convert.ToInt32(objLinha["IdUsuario"]) : 0;
                    objNovoUsuario.Login = objLinha["Login"] != DBNull.Value ? Convert.ToString(objLinha["Login"]) : null;
                    objNovoUsuario.Senha = objLinha["Senha"] != DBNull.Value ? Convert.ToString(objLinha["Senha"]) : null;
                    objNovoUsuario.Nome = objLinha["Nome"] != DBNull.Value ? Convert.ToString(objLinha["Nome"]) : null;
                    objNovoUsuario.TipoUsuario = new Entidades.TipoUsuario()
                    {
                        IdTipoUsuario = objLinha["IdTipoUsuario"] != DBNull.Value ? Convert.ToInt32(objLinha["IdTipoUsuario"]) : 0
                    };
                    objNovoUsuario.TipoUsuario = dadosTipoUsuario.Consultar(objNovoUsuario.TipoUsuario).First();

                    List<Entidades.UsuarioEndereco> enderecos = new AcessoDados.UsuarioEndereco().Consultar(new Entidades.UsuarioEndereco() { Usuario = objNovoUsuario, Email = null, Telefone = null });

                    if (enderecos.Count > 0)
                        objNovoUsuario.UsuarioEndereco = enderecos.First();

                    List<Entidades.Contratacao> contratacoes = new AcessoDados.Contratacao().Consultar(new Entidades.Contratacao() { Usuario = objNovoUsuario, DataHora = null });

                    if (contratacoes.Count > 0)
                        objNovoUsuario.Contratacao = contratacoes.First();

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
                Dados.LimparParametros();
                object objRetorno = null;
                if (entidade != null)
                {
                    Dados.AdicionarParametros("@vchAcao", "INSERIR");
                    Dados.AdicionarParametros("@vchLogin", entidade.Login);
                    Dados.AdicionarParametros("@vchSenha", entidade.Senha);
                    Dados.AdicionarParametros("@vchNome", entidade.Nome);
                    Dados.AdicionarParametros("@intIdTipoUsuario", entidade.TipoUsuario.IdTipoUsuario);

                    objRetorno = Dados.ExecutarManipulacao(CommandType.StoredProcedure, "spUsuario");
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
                Dados.LimparParametros();
                object objRetorno = null;
                if (entidade != null && entidade.IdUsuario > 0)
                {
                    Dados.AdicionarParametros("@vchAcao", "ALTERAR");
                    Dados.AdicionarParametros("@intIdUsuario", entidade.IdUsuario);
                    Dados.AdicionarParametros("@vchLogin", entidade.Login);
                    Dados.AdicionarParametros("@vchSenha", entidade.Senha);
                    Dados.AdicionarParametros("@vchNome", entidade.Nome);
                    Dados.AdicionarParametros("@intIdTipoUsuario", entidade.TipoUsuario.IdTipoUsuario);

                    objRetorno = Dados.ExecutarManipulacao(CommandType.StoredProcedure, "spUsuario");
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
                Dados.LimparParametros();
                object objRetorno = null;
                if (entidade != null && entidade.IdUsuario > 0)
                {
                    Dados.AdicionarParametros("@vchAcao", "DELETAR");
                    Dados.AdicionarParametros("@intIdUsuario", entidade.IdUsuario);

                    objRetorno = Dados.ExecutarManipulacao(CommandType.StoredProcedure, "spUsuario");
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
