﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaReclutamiento.Entidades.WebCorporativa
{
    public class WebTipoElementoEntidad
    {
        public int tipo_id { get; set; }
        public string tipo_nombre { get; set; }
        public string tipo_estado { get; set; }
        public int tipo_orden { get; set; }
    }
}