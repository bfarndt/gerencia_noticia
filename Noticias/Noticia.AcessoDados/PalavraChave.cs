using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Noticia.AcessoDados
{
    public class PalavraChave
    {
        public List<Entidades.PalavraChave> Consultar(Entidades.PalavraChave entidade)
        {
            try
            {
                DataTable objDataTable = null;

                Dados.LimparParametros();
                Dados.AdicionarParametros("@vchAcao", "SELECIONAR");
                Dados.AdicionarParametros("@intIdPalavraChave", entidade.IdPalavraChave);
                Dados.AdicionarParametros("@intIdNoticia", entidade.Noticia.IdNoticia);
                Dados.AdicionarParametros("@vchPalavraChave", entidade.PalavraChaveTexto);

                objDataTable = Dados.ExecutaConsultar(System.Data.CommandType.StoredProcedure, "spPalavraChave");

                List<Entidades.PalavraChave> objRetorno = new List<Entidades.PalavraChave>();

                if (objDataTable.Rows.Count <= 0)
                {
                    return objRetorno;
                }

                foreach (DataRow objLinha in objDataTable.Rows)
                {
                    Entidades.PalavraChave objNovoPalavraChave = new Entidades.PalavraChave();

                    objNovoPalavraChave.IdPalavraChave = objLinha["IdPalavraChave"] != DBNull.Value ? Convert.ToInt32(objLinha["IdPalavraChave"]) : 0;
                    objNovoPalavraChave.Noticia = new Entidades.Noticia();
                    objNovoPalavraChave.Noticia.IdNoticia = objLinha["IdNoticia"] != DBNull.Value ? Convert.ToInt32(objLinha["IdNoticia"]) : 0;
                    objNovoPalavraChave.PalavraChaveTexto = objLinha["PalavraChave"] != DBNull.Value ? Convert.ToString(objLinha["PalavraChave"]) : "";

                    objRetorno.Add(objNovoPalavraChave);
                }

                return objRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string Inserir(Entidades.PalavraChave entidade)
        {
            try
            {
                Dados.LimparParametros();
                object objRetorno = null;
                if (entidade != null)
                {
                    Dados.AdicionarParametros("@vchAcao", "INSERIR");
                    Dados.AdicionarParametros("@intIdPalavraChave", entidade.IdPalavraChave);
                    Dados.AdicionarParametros("@intIdNoticia", entidade.Noticia.IdNoticia);
                    Dados.AdicionarParametros("@vchPalavraChave", entidade.PalavraChaveTexto);

                    objRetorno = Dados.ExecutarManipulacao(CommandType.StoredProcedure, "spPalavraChave");
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

        public string Alterar(Entidades.PalavraChave entidade)
        {
            try
            {
                Dados.LimparParametros();
                object objRetorno = null;
                if (entidade != null && entidade.IdPalavraChave > 0)
                {
                    Dados.AdicionarParametros("@vchAcao", "ALTERAR");
                    Dados.AdicionarParametros("@intIdPalavraChave", entidade.IdPalavraChave);
                    Dados.AdicionarParametros("@intIdNoticia", entidade.Noticia.IdNoticia);
                    Dados.AdicionarParametros("@vchPalavraChave", entidade.PalavraChaveTexto);

                    objRetorno = Dados.ExecutarManipulacao(CommandType.StoredProcedure, "spPalavraChave");
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

        public string Excluir(Entidades.PalavraChave entidade)
        {
            try
            {
                Dados.LimparParametros();
                object objRetorno = null;
                if (entidade != null && entidade.IdPalavraChave > 0)
                {
                    Dados.AdicionarParametros("@vchAcao", "DELETAR");
                    Dados.AdicionarParametros("@intIdNoticia", entidade.Noticia.IdNoticia);

                    objRetorno = Dados.ExecutarManipulacao(CommandType.StoredProcedure, "spPalavraChave");
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
