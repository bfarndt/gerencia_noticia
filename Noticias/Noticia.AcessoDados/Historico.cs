using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Noticia.AcessoDados
{
    public class Historico : ICrud<Entidades.Historico>
    {
        AcessoDadosSqlServer objDados = new AcessoDadosSqlServer();

        public List<Entidades.Historico> Consultar(Entidades.Historico entidade)
        {
            try
            {
                DataTable objDataTable = null;

                objDados.LimparParametros();
                objDados.AdicionarParametros("@vchAcao", "SELECIONAR");
                objDados.AdicionarParametros("@intIdHistorico", entidade.IdHistorico);
                objDados.AdicionarParametros("@intIdNoticia", entidade.Noticia.IdNoticia);
                objDados.AdicionarParametros("@intIdUsuario", entidade.Usuario.IdUsuario);
                objDados.AdicionarParametros("@intIdStatus", entidade.StatusNoticia.IdStatus);

                objDataTable = objDados.ExecutaConsultar(System.Data.CommandType.StoredProcedure, "spHistorico");

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
                    objNovoHistorico.StatusNoticia = new Entidades.StatusNoticia();
                    objNovoHistorico.StatusNoticia.IdStatus = objLinha["IdStatus"] != DBNull.Value ? Convert.ToInt32(objLinha["IdStatus"]) : 0;
                    objNovoHistorico.DataHora = objLinha["DataHora"] != DBNull.Value ? (DateTime?)objLinha["DataHora"] : (DateTime?)null;

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
                objDados.LimparParametros();
                object objRetorno = null;
                if (entidade != null)
                {
                    objDados.AdicionarParametros("@vchAcao", "INSERIR");
                    objDados.AdicionarParametros("@intIdNoticia", entidade.Noticia.IdNoticia);
                    objDados.AdicionarParametros("@intIdUsuario", entidade.Usuario.IdUsuario);
                    objDados.AdicionarParametros("@intIdStatus", entidade.StatusNoticia.IdStatus);
                    objDados.AdicionarParametros("@datDataHora", entidade.DataHora);

                    objRetorno = objDados.ExecutarManipulacao(CommandType.StoredProcedure, "spHistorico");
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
    }
}
