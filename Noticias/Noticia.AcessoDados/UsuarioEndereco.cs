using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Noticia.AcessoDados
{
    public class UsuarioEndereco : ICrud<Entidades.UsuarioEndereco>
    {
        AcessoDadosSqlServer objDados = new AcessoDadosSqlServer();

        public List<Entidades.UsuarioEndereco> Consultar(Entidades.UsuarioEndereco entidade)
        {
            try
            {
                DataTable objDataTable = null;

                objDados.LimparParametros();
                objDados.AdicionarParametros("@vchAcao", "SELECIONAR");
                objDados.AdicionarParametros("@intIdUsuario", entidade.Usuario.IdUsuario);
                objDados.AdicionarParametros("@vchEmail", entidade.Email);
                objDados.AdicionarParametros("@vchTelefone", entidade.Telefone);

                objDataTable = objDados.ExecutaConsultar(System.Data.CommandType.StoredProcedure, "spUsuarioEndereco");

                List<Entidades.UsuarioEndereco> objRetorno = new List<Entidades.UsuarioEndereco>();

                if (objDataTable.Rows.Count <= 0)
                {
                    return objRetorno;
                }

                foreach (DataRow objLinha in objDataTable.Rows)
                {
                    Entidades.UsuarioEndereco objNovoUsuarioEndereco = new Entidades.UsuarioEndereco();

                    objNovoUsuarioEndereco.Usuario = new Entidades.Usuario();
                    objNovoUsuarioEndereco.Usuario.IdUsuario = objLinha["IdUsuario"] != DBNull.Value ? Convert.ToInt32(objLinha["IdUsuario"]) : 0;
                    objNovoUsuarioEndereco.Usuario = new AcessoDados.Usuario().Consultar(objNovoUsuarioEndereco.Usuario).First();

                    objNovoUsuarioEndereco.Email = objLinha["Email"] != DBNull.Value ? Convert.ToString(objLinha["Email"]) : null;
                    objNovoUsuarioEndereco.Telefone = objLinha["Telefone"] != DBNull.Value ? Convert.ToString(objLinha["Telefone"]) : null;

                    objRetorno.Add(objNovoUsuarioEndereco);
                }

                return objRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string Inserir(Entidades.UsuarioEndereco entidade)
        {
            try
            {
                objDados.LimparParametros();
                object objRetorno = null;
                if (entidade != null)
                {
                    objDados.AdicionarParametros("@vchAcao", "INSERIR");
                    objDados.AdicionarParametros("@intIdUsuario", entidade.Usuario.IdUsuario);
                    objDados.AdicionarParametros("@vchEmail", entidade.Email);
                    objDados.AdicionarParametros("@vchTelefone", entidade.Telefone);

                    objRetorno = objDados.ExecutarManipulacao(CommandType.StoredProcedure, "spUsuarioEndereco");
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

        public string Alterar(Entidades.UsuarioEndereco entidade)
        {
            try
            {
                objDados.LimparParametros();
                object objRetorno = null;
                if (entidade != null && entidade.Usuario.IdUsuario > 0)
                {
                    objDados.AdicionarParametros("@vchAcao", "ALTERAR");
                    objDados.AdicionarParametros("@intIdUsuario", entidade.Usuario.IdUsuario);
                    objDados.AdicionarParametros("@vchEmail", entidade.Email);
                    objDados.AdicionarParametros("@vchTelefone", entidade.Telefone);

                    objRetorno = objDados.ExecutarManipulacao(CommandType.StoredProcedure, "spUsuarioEndereco");
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

        public string Excluir(Entidades.UsuarioEndereco entidade)
        {
            try
            {
                objDados.LimparParametros();
                object objRetorno = null;
                if (entidade != null && entidade.Usuario.IdUsuario > 0)
                {
                    objDados.AdicionarParametros("@vchAcao", "DELETAR");
                    objDados.AdicionarParametros("@intIdUsuario", entidade.Usuario.IdUsuario);

                    objRetorno = objDados.ExecutarManipulacao(CommandType.StoredProcedure, "spUsuarioEndereco");
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