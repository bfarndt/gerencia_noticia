using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Noticia.AcessoDados
{
    public class Historico : ICrud<Entidades.Historico>
    {
        public List<Entidades.Historico> Consultar(Entidades.Historico entidade, List<Entidades.StatusNoticia> VariosStatusNoticia)
        {
            try
            {
                DataTable objDataTable = null;

                AcessoDados.Noticia dadosNoticias = new AcessoDados.Noticia();
                AcessoDados.Usuario dadosUsuarios = new AcessoDados.Usuario();
                AcessoDados.StatusNoticia dadosStatus = new AcessoDados.StatusNoticia();

                Dados.LimparParametros();
                Dados.AdicionarParametros("@vchAcao", "SELECIONAR");

                if (entidade != null)
                {
                    Dados.AdicionarParametros("@intIdHistorico", entidade.IdHistorico);
                    Dados.AdicionarParametros("@intIdNoticia", entidade.Noticia.IdNoticia);
                    Dados.AdicionarParametros("@intIdUsuario", entidade.Usuario.IdUsuario);
                }

                string strVariosStatus = string.Empty;
                if (VariosStatusNoticia != null)
                {
                    foreach (var item in VariosStatusNoticia)
                    {
                        strVariosStatus = strVariosStatus + item.IdStatus.ToString() + ",";
                    }

                    if (!string.IsNullOrWhiteSpace(strVariosStatus))
                    {
                        strVariosStatus = strVariosStatus.Remove(strVariosStatus.Length - 1, 1);
                        Dados.AdicionarParametros("@vchVariosIdStatus", strVariosStatus);
                    }
                }

                objDataTable = Dados.ExecutaConsultar(System.Data.CommandType.StoredProcedure, "spHistorico");

                List<Entidades.Historico> objRetorno = new List<Entidades.Historico>();

                if (objDataTable.Rows.Count <= 0)
                {
                    return objRetorno;
                }

                foreach (DataRow objLinha in objDataTable.Rows)
                {
                    Entidades.Historico objNovoHistorico = new Entidades.Historico();

                    objNovoHistorico.IdHistorico = objLinha["IdHistorico"] != DBNull.Value ? Convert.ToInt32(objLinha["IdHistorico"]) : 0;
                    objNovoHistorico.Noticia = new Entidades.Noticia();
                    objNovoHistorico.Noticia.IdNoticia = objLinha["IdNoticia"] != DBNull.Value ? Convert.ToInt32(objLinha["IdNoticia"]) : 0;

                    objNovoHistorico.Usuario = new Entidades.Usuario();
                    objNovoHistorico.Usuario.IdUsuario = objLinha["IdUsuario"] != DBNull.Value ? Convert.ToInt32(objLinha["IdUsuario"]) : 0;
                    objNovoHistorico.Usuario = dadosUsuarios.Consultar(objNovoHistorico.Usuario).First();

                    objNovoHistorico.StatusNoticia = new Entidades.StatusNoticia();
                    objNovoHistorico.StatusNoticia.IdStatus = objLinha["IdStatus"] != DBNull.Value ? Convert.ToInt32(objLinha["IdStatus"]) : 0;
                    objNovoHistorico.StatusNoticia = dadosStatus.Consultar(objNovoHistorico.StatusNoticia).First();

                    objNovoHistorico.DataHora = objLinha["DataHora"] != DBNull.Value ? (DateTime?)objLinha["DataHora"] : (DateTime?)null;
                    objNovoHistorico.Descricao = objLinha["Descricao"] != DBNull.Value ? (string)objLinha["Descricao"] : (string)null;

                    objRetorno.Add(objNovoHistorico);
                }

                return objRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string Inserir(Entidades.Historico entidade)
        {
            try
            {
                Dados.LimparParametros();
                object objRetorno = null;
                if (entidade != null)
                {
                    Dados.AdicionarParametros("@vchAcao", "INSERIR");
                    Dados.AdicionarParametros("@intIdNoticia", entidade.Noticia.IdNoticia);
                    Dados.AdicionarParametros("@intIdUsuario", entidade.Usuario.IdUsuario);
                    Dados.AdicionarParametros("@intIdStatus", entidade.StatusNoticia.IdStatus);
                    Dados.AdicionarParametros("@datDataHora", entidade.DataHora);
                    Dados.AdicionarParametros("@vchDescricao", entidade.Descricao);

                    objRetorno = Dados.ExecutarManipulacao(CommandType.StoredProcedure, "spHistorico");
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

        public string Alterar(Entidades.Historico entidade)
        {
            try
            {
                return "Não implementado";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string Excluir(Entidades.Historico entidade)
        {
            try
            {
                return "Não implementado";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Entidades.Historico> Consultar(Entidades.Historico entidade)
        {
            throw new NotImplementedException();
        }
    }
}
