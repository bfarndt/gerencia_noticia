using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Noticia.AcessoDados
{
    public class NoticiaImagem : ICrud<Entidades.NoticiaImagem>
    {
        AcessoDadosSqlServer objDados = new AcessoDadosSqlServer();

        public List<Entidades.NoticiaImagem> Consultar(Entidades.NoticiaImagem entidade)
        {
            try
            {
                DataTable objDataTable = null;

                objDados.LimparParametros();
                objDados.AdicionarParametros("@vchAcao", "SELECIONAR");
                objDados.AdicionarParametros("@intIdNoticia", entidade.Noticia.IdNoticia);
                objDados.AdicionarParametros("@intIdImagem", entidade.Imagem.IdImagem);

                objDataTable = objDados.ExecutaConsultar(System.Data.CommandType.StoredProcedure, "spNoticiaImagem");

                List<Entidades.NoticiaImagem> objRetorno = new List<Entidades.NoticiaImagem>();

                if (objDataTable.Rows.Count <= 0)
                {
                    return objRetorno;
                }

                foreach (DataRow objLinha in objDataTable.Rows)
                {
                    Entidades.NoticiaImagem objNovoNoticiaImagem = new Entidades.NoticiaImagem();

                    objNovoNoticiaImagem.Noticia = new Entidades.Noticia();
                    objNovoNoticiaImagem.Noticia.IdNoticia = objLinha["IdNoticia"] != DBNull.Value ? Convert.ToInt32(objLinha["IdNoticia"]) : 0;
                    objNovoNoticiaImagem.Noticia = new AcessoDados.Noticia().Consultar(objNovoNoticiaImagem.Noticia).First();

                    objNovoNoticiaImagem.Imagem = new Entidades.Imagem();
                    objNovoNoticiaImagem.Imagem.IdImagem = objLinha["IdImagem"] != DBNull.Value ? Convert.ToInt32(objLinha["IdImagem"]) : 0;

                    objRetorno.Add(objNovoNoticiaImagem);
                }

                return objRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string Inserir(Entidades.NoticiaImagem entidade)
        {
            try
            {
                objDados.LimparParametros();
                object objRetorno = null;
                if (entidade != null)
                {
                    objDados.AdicionarParametros("@vchAcao", "INSERIR");
                    objDados.AdicionarParametros("@intIdNoticia", entidade.Noticia.IdNoticia);
                    objDados.AdicionarParametros("@intIdImagem", entidade.Imagem.IdImagem);

                    objRetorno = objDados.ExecutarManipulacao(CommandType.StoredProcedure, "spNoticiaImagem");
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

        public string Alterar(Entidades.NoticiaImagem entidade)
        {
            try
            {
                return "Utilize o excluir depois inserir";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string Excluir(Entidades.NoticiaImagem entidade)
        {
            try
            {
                objDados.LimparParametros();
                object objRetorno = null;
                if (entidade != null && entidade.Noticia != null && entidade.Noticia.IdNoticia > 0 &&
                    entidade.Imagem != null && entidade.Imagem.IdImagem > 0)
                {
                    objDados.AdicionarParametros("@vchAcao", "DELETAR");
                    objDados.AdicionarParametros("@intIdNoticia", entidade.Noticia.IdNoticia);
                    objDados.AdicionarParametros("@intIdImagem", entidade.Imagem.IdImagem);

                    objRetorno = objDados.ExecutarManipulacao(CommandType.StoredProcedure, "spNoticiaImagem");
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
