﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaReclutamiento.Entidades.WebCorporativa
{
    public class WebMenuEntidad
    {
        public int menu_id { get; set; }
        public string menu_titulo { get; set; }
        public string menu_estado { get; set; }
        public int menu_orden { get; set; }
        public List<WebElementoEntidad> elemento { get; set; }
        //public WebMenuEntidad()
        //{
        //    this.elemento = new List<WebElementoEntidad>();
        //}
    }
}