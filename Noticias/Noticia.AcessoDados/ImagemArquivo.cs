using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Noticia.AcessoDados
{
    public class ImagemArquivoArquivo
    {
        AcessoDadosSqlServer objDados = new AcessoDadosSqlServer();
        
        public List<Entidades.ImagemArquivo> Consultar(Entidades.ImagemArquivo entidade)
        {
            try
            {
                DataTable objDataTable = null;

                objDados.LimparParametros();
                objDados.AdicionarParametros("@vchAcao", "SELECIONAR");
                objDados.AdicionarParametros("@intIdImagem", entidade.Imagem.IdImagem);
                objDados.AdicionarParametros("@binImagem", entidade.ImagemBytes);
                objDados.AdicionarParametros("@vchExtensao", entidade.Extensao);
                objDados.AdicionarParametros("@vchTamanho", entidade.Tamanho);
                objDados.AdicionarParametros("@vchFormato", entidade.Formato);

                objDataTable = objDados.ExecutaConsultar(System.Data.CommandType.StoredProcedure, "spImagemArquivo");

                List<Entidades.ImagemArquivo> objRetorno = new List<Entidades.ImagemArquivo>();

                if (objDataTable.Rows.Count <= 0)
                {
                    return objRetorno;
                }

                foreach (DataRow objLinha in objDataTable.Rows)
                {
                    Entidades.ImagemArquivo objNovoImagemArquivo = new Entidades.ImagemArquivo();

                    objNovoImagemArquivo.Imagem = new Entidades.Imagem();
                    objNovoImagemArquivo.Imagem.IdImagem = objLinha["IdImagem"] != DBNull.Value ? Convert.ToInt32(objLinha["IdImagem"]) : 0;
                    objNovoImagemArquivo.ImagemBytes = objLinha["Imagem"] != DBNull.Value ? objLinha["Imagem"] as byte[] : null;
                    objNovoImagemArquivo.Extensao = objLinha["Extensao"] != DBNull.Value ? Convert.ToString(objLinha["Extensao"]) : "";
                    objNovoImagemArquivo.Extensao = objLinha["Tamanho"] != DBNull.Value ? Convert.ToString(objLinha["Tamanho"]) : "";
                    objNovoImagemArquivo.Extensao = objLinha["Formato"] != DBNull.Value ? Convert.ToString(objLinha["Formato"]) : "";

                    objRetorno.Add(objNovoImagemArquivo);
                }

                return objRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string Inserir(Entidades.ImagemArquivo entidade)
        {
            try
            {
                objDados.LimparParametros();
                object objRetorno = null;
                if (entidade != null)
                {
                    objDados.AdicionarParametros("@vchAcao", "INSERIR");
                    objDados.AdicionarParametros("@intIdImagem", entidade.Imagem.IdImagem);
                    objDados.AdicionarParametros("@binImagem", entidade.ImagemBytes);
                    objDados.AdicionarParametros("@vchExtensao", entidade.Extensao);
                    objDados.AdicionarParametros("@vchTamanho", entidade.Tamanho);
                    objDados.AdicionarParametros("@vchFormato", entidade.Formato);

                    objRetorno = objDados.ExecutarManipulacao(CommandType.StoredProcedure, "spImagemArquivo");
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

        public string Alterar(Entidades.ImagemArquivo entidade)
        {
            try
            {
                objDados.LimparParametros();
                object objRetorno = null;
                if (entidade != null && entidade.Imagem != null && entidade.Imagem.IdImagem > 0)
                {
                    objDados.AdicionarParametros("@vchAcao", "ALTERAR");
                    objDados.AdicionarParametros("@intIdImagem", entidade.Imagem.IdImagem);
                    objDados.AdicionarParametros("@vchFormato", entidade.Formato);

                    objRetorno = objDados.ExecutarManipulacao(CommandType.StoredProcedure, "spImagemArquivo");
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

        public string Excluir(Entidades.ImagemArquivo entidade)
        {
            try
            {
                objDados.LimparParametros();
                object objRetorno = null;
                if (entidade != null && entidade.Imagem != null && entidade.Imagem.IdImagem > 0)
                {
                    objDados.AdicionarParametros("@vchAcao", "DELETAR");
                    objDados.AdicionarParametros("@intIdImagem", entidade.Imagem.IdImagem);

                    objRetorno = objDados.ExecutarManipulacao(CommandType.StoredProcedure, "spImagemArquivo");
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
