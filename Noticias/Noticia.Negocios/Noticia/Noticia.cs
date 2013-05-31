using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Noticia.Negocios.Noticia
{
    public class Noticia
    {
        public Entidades.Noticia CriarNoticia(Entidades.Noticia objNoticia)
        {
            try
            {
                AcessoDados.Noticia dal = new AcessoDados.Noticia();


                dal.Consultar(objNoticia);
                //Validar regras de negócios



                //Executar insert
                string strRetorno = string.Empty;
                strRetorno = dal.Inserir(objNoticia);

                 Entidades.Noticia obj = new Entidades.Noticia();
                obj.IdNoticia = 6;

                string strErros = "";

                foreach (var item in dal.Consultar(obj))
                {
                    Entidades.Noticia objLuis = new Entidades.Noticia();

                    objLuis = item;
                }

                throw new Exception(strErros);
                

                Entidades.Noticia  obj2 = dal.Consultar(obj).First();

                var found = (from f in dal.Consultar(obj)
                            where f.IdNoticia == 1
                            select f).OrderBy(p => p.IdNoticia);


                objNoticia = new Entidades.Noticia();
                int intResult = 0;
                if (!int.TryParse(strRetorno, out intResult))
                {
                    throw new Exception("ETECETERÁ");
                }

                objNoticia.IdNoticia = intResult;

                dal.Consultar(objNoticia);

                return objNoticia;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
