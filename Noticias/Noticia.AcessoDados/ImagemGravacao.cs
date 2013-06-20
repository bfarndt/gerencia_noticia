using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Noticia.AcessoDados
{
    public class ImagemGravacao
    {
        public List<Entidades.ImagemGravacao> Consultar(Entidades.Imagem imagem)
        {
            try
            {
                DataTable objDataTable = null;

                Dados.LimparParametros();
                Dados.AdicionarParametros("@vchAcao", "SELECIONAR");
                Dados.AdicionarParametros("@intIdImagem", imagem.IdImagem);

                objDataTable = Dados.ExecutaConsultar(System.Data.CommandType.StoredProcedure, "spImagemGravacao");

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
                Dados.LimparParametros();
                object objRetorno = null;
                if (entidade != null)
                {
                    Dados.AdicionarParametros("@vchAcao", "INSERIR");
                    Dados.AdicionarParametros("@intIdImagem", entidade.Imagem.IdImagem);
                    Dados.AdicionarParametros("@datDataHoraGravacao", entidade.DataHoraGravacao);
                    Dados.AdicionarParametros("@vchLocalGravacao", entidade.LocalGravacao);

                    objRetorno = Dados.ExecutarManipulacao(CommandType.StoredProcedure, "spImagemGravacao");
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
                Dados.LimparParametros();
                object objRetorno = null;
                if (entidade != null && entidade.Imagem != null && entidade.Imagem.IdImagem > 0)
                {
                    Dados.AdicionarParametros("@vchAcao", "ALTERAR");
                    Dados.AdicionarParametros("@intIdImagem", entidade.Imagem.IdImagem);
                    Dados.AdicionarParametros("@datDataHoraGravacao", entidade.DataHoraGravacao);
                    Dados.AdicionarParametros("@vchLocalGravacao", entidade.LocalGravacao);

                    objRetorno = Dados.ExecutarManipulacao(CommandType.StoredProcedure, "spImagemGravacao");
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
                Dados.LimparParametros();
                object objRetorno = null;
                if (entidade != null && entidade.Imagem != null && entidade.Imagem.IdImagem > 0)
                {
                    Dados.AdicionarParametros("@vchAcao", "DELETAR");
                    Dados.AdicionarParametros("@intIdImagem", entidade.Imagem.IdImagem);

                    objRetorno = Dados.ExecutarManipulacao(CommandType.StoredProcedure, "spImagemGravacao");
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
