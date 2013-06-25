﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Noticia.Entidades
{
    [Serializable]
    public class ImagemArquivo
    {
        public int? IdImagemArquivo { get; set; }
        public Imagem Imagem { get; set; }
        public byte[] ImagemBytes { get; set; }
        public string Extensao { get; set; }
        public string Tamanho { get; set; }
        public string Formato { get; set; }
        public string NomeArquivo { get; set; }
    }
}
