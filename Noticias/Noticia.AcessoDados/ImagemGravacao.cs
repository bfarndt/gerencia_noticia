using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Noticia.AcessoDados
{
    public class ImagemGravacao
    {
        AcessoDadosSqlServer objDados = new AcessoDadosSqlServer();

        public List<Entidades.ImagemGravacao> Consultar(Entidades.ImagemGravacao entidade)
        {
            try
            {
                DataTable objDataTable = null;

                objDados.LimparParametros();
                objDados.AdicionarParametros("@vchAcao", "SELECIONAR");
                objDados.AdicionarParametros("@intIdImagem", entidade.Imagem.IdImagem);
                objDados.AdicionarParametros("@vchLocalGravacao", entidade.LocalGravacao);

                objDataTable = objDados.ExecutaConsultar(System.Data.CommandType.StoredProcedure, "spImagemGravacao");

                List<Entidades.ImagemGravacao> objRetorno = new List<Entidades.ImagemGravacao>();

                if (objDataTable.Rows.Count <= 0)
                {
                    return objRetorno;
                }

                foreach (DataRow objLinha in objDataTable.Rows)
                {
                    Entidades.ImagemGravacao objNovoImagemGravacao = new Entidades.ImagemGravacao();

                    objNovoImagemGravacao.Imagem = new Entidades.Imagem();
                    objNovoImagemGravacao.Imagem.IdImagem = objLinha["IdImagem"] != DBNull.Value ? Convert.ToInt32(objLinha["IdImagem"]) : 0;
                    objNovoImagemGravacao.Imagem = new AcessoDados.Imagem().Consultar(objNovoImagemGravacao.Imagem).First();
                    
                    objNovoImagemGravacao.DataHoraGravacao = objLinha["DataHoraGravacao"] != DBNull.Value ? Convert.ToDateTime(objLinha["DataHoraGravacao"]) : (DateTime?)null;
                    objNovoImagemGravacao.LocalGravacao = objLinha["LocalGravacao"] != DBNull.Value ? Convert.ToString(objLinha["LocalGravacao"]) : "";

                    objRetorno.Add(objNovoImagemGravacao);
                }

                return objRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string Inserir(Entidades.ImagemGravacao entidade)
        {
            try
            {
                objDados.LimparParametros();
                object objRetorno = null;
                if (entidade != null)
                {
                    objDados.AdicionarParametros("@vchAcao", "INSERIR");
                    objDados.AdicionarParametros("@intIdImagem", entidade.Imagem.IdImagem);
                    objDados.AdicionarParametros("@datDataHoraGravacao", entidade.DataHoraGravacao);
                    objDados.AdicionarParametros("@vchLocalGravacao", entidade.LocalGravacao);

                    objRetorno = objDados.ExecutarManipulacao(CommandType.StoredProcedure, "spImagemGravacao");
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

        public string Alterar(Entidades.ImagemGravacao entidade)
        {
            try
            {
                objDados.LimparParametros();
                object objRetorno = null;
                if (entidade != null && entidade.Imagem != null && entidade.Imagem.IdImagem > 0)
                {
                    objDados.AdicionarParametros("@vchAcao", "ALTERAR");
                    objDados.AdicionarParametros("@intIdImagem", entidade.Imagem.IdImagem);
                    objDados.AdicionarParametros("@datDataHoraGravacao", entidade.DataHoraGravacao);
                    objDados.AdicionarParametros("@vchLocalGravacao", entidade.LocalGravacao);

                    objRetorno = objDados.ExecutarManipulacao(CommandType.StoredProcedure, "spImagemGravacao");
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

        public string Excluir(Entidades.ImagemGravacao entidade)
        {
            try
            {
                objDados.LimparParametros();
                object objRetorno = null;
                if (entidade != null && entidade.Imagem != null && entidade.Imagem.IdImagem > 0)
                {
                    objDados.AdicionarParametros("@vchAcao", "DELETAR");
                    objDados.AdicionarParametros("@intIdImagem", entidade.Imagem.IdImagem);

                    objRetorno = objDados.ExecutarManipulacao(CommandType.StoredProcedure, "spImagemGravacao");
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
