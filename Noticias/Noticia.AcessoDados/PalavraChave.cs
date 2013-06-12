using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Noticia.AcessoDados
{
    public class PalavraChave
    {
        /*
         	@intIdPalavraChave INT = NULL,
	@intIdNoticia INT = NULL,
	@vchPalavraChave VARCHAR(50) = NULL
         * 
         * 	tblPal.IdPalavraChave,
			tblPal.IdNoticia,
			tblPal.PalavraChave
         */
        AcessoDadosSqlServer objDados = new AcessoDadosSqlServer();

        public List<Entidades.PalavraChave> Consultar(Entidades.PalavraChave entidade)
        {
            try
            {
                DataTable objDataTable = null;

                objDados.LimparParametros();
                objDados.AdicionarParametros("@vchAcao", "SELECIONAR");
                objDados.AdicionarParametros("@intIdPalavraChave", entidade.IdPalavraChave);
                objDados.AdicionarParametros("@intIdNoticia", entidade.Noticia.IdNoticia);
                objDados.AdicionarParametros("@vchPalavraChave", entidade.PalavraChaveTexto);

                objDataTable = objDados.ExecutaConsultar(System.Data.CommandType.StoredProcedure, "spPalavraChave");

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
                objDados.LimparParametros();
                object objRetorno = null;
                if (entidade != null)
                {
                    objDados.AdicionarParametros("@vchAcao", "INSERIR");
                    objDados.AdicionarParametros("@intIdPalavraChave", entidade.IdPalavraChave);
                    objDados.AdicionarParametros("@intIdNoticia", entidade.Noticia.IdNoticia);
                    objDados.AdicionarParametros("@vchPalavraChave", entidade.PalavraChaveTexto);

                    objRetorno = objDados.ExecutarManipulacao(CommandType.StoredProcedure, "spPalavraChave");
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
                objDados.LimparParametros();
                object objRetorno = null;
                if (entidade != null && entidade.IdPalavraChave > 0)
                {
                    objDados.AdicionarParametros("@vchAcao", "ALTERAR");
                    objDados.AdicionarParametros("@intIdPalavraChave", entidade.IdPalavraChave);
                    objDados.AdicionarParametros("@intIdNoticia", entidade.Noticia.IdNoticia);
                    objDados.AdicionarParametros("@vchPalavraChave", entidade.PalavraChaveTexto);

                    objRetorno = objDados.ExecutarManipulacao(CommandType.StoredProcedure, "spPalavraChave");
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
                objDados.LimparParametros();
                object objRetorno = null;
                if (entidade != null && entidade.IdPalavraChave > 0)
                {
                    objDados.AdicionarParametros("@vchAcao", "DELETAR");
                    objDados.AdicionarParametros("@intIdPalavraChave", entidade.IdPalavraChave);

                    objRetorno = objDados.ExecutarManipulacao(CommandType.StoredProcedure, "spPalavraChave");
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
