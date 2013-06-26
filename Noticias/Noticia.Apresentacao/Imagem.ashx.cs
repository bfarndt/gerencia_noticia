using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Noticia.Apresentacao
{
    /// <summary>
    /// Summary description for Imagem
    /// </summary>
    public class Imagem : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            if (context.Request.QueryString["idImg"] != null)
            {
                var consulta = new Negocios.Imagem().CarregarImagem(new Entidades.Imagem() { IdImagem = Convert.ToInt32(context.Request.QueryString["idImg"]) });
                if (consulta != null)
                {
                    byte[] myBytes = consulta;
                    if (myBytes != null)
                    {
                        System.IO.MemoryStream flsImagem = new System.IO.MemoryStream(myBytes);

                        context.Response.ContentType = "image/jpeg";
                        context.Response.Cache.SetCacheability(HttpCacheability.Public);

                        const int intBuffer = 1024 * 8;
                        byte[] btyBuffer = new byte[intBuffer];

                        //intCount recebe quantos bytes foram lidos
                        int intCount = flsImagem.Read(btyBuffer, 0, intBuffer);
                        while (intCount > 0)
                        {
                            context.Response.OutputStream.Write(btyBuffer, 0, intCount);
                            intCount = flsImagem.Read(btyBuffer, 0, intBuffer);
                        }

                        flsImagem.Dispose();
                    }
                }
            }

        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}