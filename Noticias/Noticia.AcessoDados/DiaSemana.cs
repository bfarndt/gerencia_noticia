using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Noticia.AcessoDados
{
    public class DiaSemana : ICrud<Entidades.DiaSemana>
    {
        public List<Entidades.DiaSemana> Consultar(Entidades.DiaSemana entidade)
        {
            try
            {
                DataTable objDataTable = null;

                Dados.LimparParametros();
                Dados.AdicionarParametros("@vchAcao", "SELECIONAR");
                Dados.AdicionarParametros("@intIdDia", entidade.IdDia);
                Dados.AdicionarParametros("@vchDescricao", entidade.Descricao);

                objDataTable = Dados.ExecutaConsultar(System.Data.CommandType.StoredProcedure, "spDiaSemana");

                List<Entidades.DiaSemana> objRetorno = new List<Entidades.DiaSemana>();

                if (objDataTable.Rows.Count <= 0)
                {
                    return objRetorno;
                }

                foreach (DataRow objLinha in objDataTable.Rows)
                {
                    Entidades.DiaSemana objNovoDiaSemana = new Entidades.DiaSemana();

                    objNovoDiaSemana.IdDia = objLinha["IdDia"] != DBNull.Value ? Convert.ToInt32(objLinha["IdDia"]) : 0;
                    objNovoDiaSemana.Descricao = objLinha["Descricao"] != DBNull.Value ? Convert.ToString(objLinha["Descricao"]) : null;

                    objRetorno.Add(objNovoDiaSemana);
                }

                return objRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string Inserir(Entidades.DiaSemana entidade)
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

        public string Alterar(Entidades.DiaSemana entidade)
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

        public string Excluir(Entidades.DiaSemana entidade)
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