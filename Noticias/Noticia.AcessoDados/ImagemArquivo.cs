using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Noticia.AcessoDados
{
    public class ImagemArquivo
    {
        public List<Entidades.ImagemArquivo> Consultar(Entidades.ImagemArquivo entidade)
        {
            try
            {
                DataTable objDataTable = null;

                Dados.LimparParametros();
                Dados.AdicionarParametros("@vchAcao", "SELECIONAR");
                Dados.AdicionarParametros("@intIdImagem", entidade.Imagem.IdImagem);
                Dados.AdicionarParametros("@binImagem", entidade.ImagemBytes);
                Dados.AdicionarParametros("@vchExtensao", entidade.Extensao);
                Dados.AdicionarParametros("@vchTamanho", entidade.Tamanho);
                Dados.AdicionarParametros("@vchFormato", entidade.Formato);

                objDataTable = Dados.ExecutaConsultar(System.Data.CommandType.StoredProcedure, "spImagemArquivo");

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
                    objNovoImagemArquivo.Imagem = new AcessoDados.Imagem().Consultar(objNovoImagemArquivo.Imagem).First();

                    objNovoImagemArquivo.ImagemBytes = objLinha["Imagem"] != DBNull.Value ? objLinha["Imagem"] as byte[] : null;
                    objNovoImagemArquivo.Extensao = objLinha["Extensao"] != DBNull.Value ? Convert.ToString(objLinha["Extensao"]) : "";
                    objNovoImagemArquivo.Tamanho = objLinha["Tamanho"] != DBNull.Value ? Convert.ToString(objLinha["Tamanho"]) : "";
                    objNovoImagemArquivo.Formato = objLinha["Formato"] != DBNull.Value ? Convert.ToString(objLinha["Formato"]) : "";

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
                Dados.LimparParametros();
                object objRetorno = null;
                if (entidade != null)
                {
                    Dados.AdicionarParametros("@vchAcao", "INSERIR");
                    Dados.AdicionarParametros("@intIdImagem", entidade.Imagem.IdImagem);
                    Dados.AdicionarParametros("@binImagem", entidade.ImagemBytes);
                    Dados.AdicionarParametros("@vchExtensao", entidade.Extensao);
                    Dados.AdicionarParametros("@vchTamanho", entidade.Tamanho);
                    Dados.AdicionarParametros("@vchFormato", entidade.Formato);

                    objRetorno = Dados.ExecutarManipulacao(CommandType.StoredProcedure, "spImagemArquivo");
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
                Dados.LimparParametros();
                object objRetorno = null;
                if (entidade != null && entidade.Imagem != null && entidade.Imagem.IdImagem > 0)
                {
                    Dados.AdicionarParametros("@vchAcao", "ALTERAR");
                    Dados.AdicionarParametros("@intIdImagem", entidade.Imagem.IdImagem);
                    Dados.AdicionarParametros("@vchFormato", entidade.Formato);

                    objRetorno = Dados.ExecutarManipulacao(CommandType.StoredProcedure, "spImagemArquivo");
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
                Dados.LimparParametros();
                object objRetorno = null;
                if (entidade != null && entidade.Imagem != null && entidade.Imagem.IdImagem > 0)
                {
                    Dados.AdicionarParametros("@vchAcao", "DELETAR");
                    Dados.AdicionarParametros("@intIdImagem", entidade.Imagem.IdImagem);

                    objRetorno = Dados.ExecutarManipulacao(CommandType.StoredProcedure, "spImagemArquivo");
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
