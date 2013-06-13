using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Noticia.AcessoDados
{
    public class Noticia : ICrud<Entidades.Noticia>
    {
        AcessoDadosSqlServer objDados = new AcessoDadosSqlServer();

        public List<Entidades.Noticia> Consultar(Entidades.Noticia entidade)
        {
            try
            {
                DataTable objDataTable = null;

                objDados.LimparParametros();
                objDados.AdicionarParametros("@vchAcao", "SELECIONAR");
                objDados.AdicionarParametros("@intIdNoticia", entidade.IdNoticia);
                objDados.AdicionarParametros("@vchTitulo", entidade.Titulo);
                objDados.AdicionarParametros("@vchConteudo", entidade.Conteudo);

                objDataTable = objDados.ExecutaConsultar(System.Data.CommandType.StoredProcedure, "spNoticia");

                List<Entidades.Noticia> objRetorno = new List<Entidades.Noticia>();

                if (objDataTable.Rows.Count <= 0)
                {
                    return objRetorno;
                }

                foreach (DataRow objLinha in objDataTable.Rows)
                {
                    Entidades.Noticia objNovaNoticia = new Entidades.Noticia();

                    objNovaNoticia.IdNoticia = objLinha["IdNoticia"] != DBNull.Value ? Convert.ToInt32(objLinha["IdNoticia"]) : 0;
                    objNovaNoticia.Titulo = objLinha["Titulo"] != DBNull.Value ? Convert.ToString(objLinha["Titulo"]) : null;
                    objNovaNoticia.Conteudo = objLinha["Conteudo"] != DBNull.Value ? Convert.ToString(objLinha["Conteudo"]) : null;

                    objRetorno.Add(objNovaNoticia);
                }

                return objRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string Inserir(Entidades.Noticia entidade)
        {
            try
            {
                objDados.LimparParametros();
                object objRetorno = null;
                if (entidade != null)
                {
                    objDados.AdicionarParametros("@vchAcao", "INSERIR");
                    objDados.AdicionarParametros("@vchTitulo", entidade.Titulo);
                    objDados.AdicionarParametros("@vchConteudo", entidade.Conteudo);

                    objRetorno = objDados.ExecutarManipulacao(CommandType.StoredProcedure, "spNoticia");
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

        public string Alterar(Entidades.Noticia entidade)
        {
            try
            {
                objDados.LimparParametros();
                object objRetorno = null;
                if (entidade != null && entidade.IdNoticia > 0)
                {
                    objDados.AdicionarParametros("@vchAcao", "ALTERAR");
                    objDados.AdicionarParametros("@intIdNoticia", entidade.IdNoticia);
                    objDados.AdicionarParametros("@vchTitulo", entidade.Titulo);
                    objDados.AdicionarParametros("@vchConteudo", entidade.Conteudo);

                    objRetorno = objDados.ExecutarManipulacao(CommandType.StoredProcedure, "spNoticia");
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

        public string Excluir(Entidades.Noticia entidade)
        {
            try
            {
                objDados.LimparParametros();
                object objRetorno = null;
                if (entidade != null && entidade.IdNoticia > 0)
                {
                    objDados.AdicionarParametros("@vchAcao", "DELETAR");
                    objDados.AdicionarParametros("@intIdNoticia", entidade.IdNoticia);

                    objRetorno = objDados.ExecutarManipulacao(CommandType.StoredProcedure, "spNoticia");
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
