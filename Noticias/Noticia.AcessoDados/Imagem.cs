using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Noticia.AcessoDados
{
    public class Imagem : ICrud<Entidades.Imagem>
    {
        public List<Entidades.Imagem> Consultar(Entidades.Imagem entidade)
        {
            try
            {
                DataTable objDataTable = null;

                Dados.LimparParametros();
                Dados.AdicionarParametros("@vchAcao", "SELECIONAR");
                Dados.AdicionarParametros("@intIdImagem", entidade.IdImagem);
                Dados.AdicionarParametros("@vchLegenda", entidade.Legenda);
                Dados.AdicionarParametros("@bitSelecionada", entidade.Selecionada);

                objDataTable = Dados.ExecutaConsultar(System.Data.CommandType.StoredProcedure, "spImagem");

                List<Entidades.Imagem> objRetorno = new List<Entidades.Imagem>();

                if (objDataTable.Rows.Count <= 0)
                {
                    return objRetorno;
                }

                foreach (DataRow objLinha in objDataTable.Rows)
                {
                    Entidades.Imagem objNovoImagem = new Entidades.Imagem();

                    objNovoImagem.IdImagem = objLinha["IdImagem"] != DBNull.Value ? Convert.ToInt32(objLinha["IdImagem"]) : 0;
                    objNovoImagem.Legenda = objLinha["Legenda"] != DBNull.Value ? Convert.ToString(objLinha["Legenda"]) : null;
                    objNovoImagem.Selecionada = objLinha["Selecionada"] != DBNull.Value ? Convert.ToBoolean(objLinha["Selecionada"]) : false;
                    objNovoImagem.ImagemGravacao = new AcessoDados.ImagemGravacao().Consultar(objNovoImagem).First();
                    objRetorno.Add(objNovoImagem);
                }

                return objRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string Inserir(Entidades.Imagem entidade)
        {
            try
            {
                Dados.LimparParametros();
                object objRetorno = null;
                if (entidade != null)
                {
                    Dados.AdicionarParametros("@vchAcao", "INSERIR");
                    Dados.AdicionarParametros("@vchLegenda", entidade.Legenda);
                    Dados.AdicionarParametros("@bitSelecionada", entidade.Selecionada);

                    objRetorno = Dados.ExecutarManipulacao(CommandType.StoredProcedure, "spImagem");
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

        public string Alterar(Entidades.Imagem entidade)
        {
            try
            {
                Dados.LimparParametros();
                object objRetorno = null;
                if (entidade != null && entidade.IdImagem > 0)
                {
                    Dados.AdicionarParametros("@vchAcao", "ALTERAR");
                    Dados.AdicionarParametros("@intIdImagem", entidade.IdImagem);
                    Dados.AdicionarParametros("@vchLegenda", entidade.Legenda);
                    Dados.AdicionarParametros("@bitSelecionada", entidade.Selecionada);

                    objRetorno = Dados.ExecutarManipulacao(CommandType.StoredProcedure, "spImagem");
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

        public string Excluir(Entidades.Imagem entidade)
        {
            try
            {
                Dados.LimparParametros();
                object objRetorno = null;
                if (entidade != null && entidade.IdImagem > 0)
                {
                    Dados.AdicionarParametros("@vchAcao", "DELETAR");
                    Dados.AdicionarParametros("@intIdImagem", entidade.IdImagem);

                    objRetorno = Dados.ExecutarManipulacao(CommandType.StoredProcedure, "spImagem");
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
