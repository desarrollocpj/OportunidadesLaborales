﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaReclutamiento.Entidades
{
    public class OfimaticaEntidad
    {
        public int ofi_id { get; set; }
        public string ofi_tipo { get; set; }
        public string ofi_centro_estudio { get; set; }
        public int fk_ofimatica { get; set; }
        public string ofi_nivel { get; set; }
        public DateTime ofi_periodo_ini { get; set; }
        public DateTime ofi_periodo_fin { get; set; }
        public DateTime ofi_fecha_reg { get; set; }
        public DateTime ofi_fecha_act { get; set; }
        public int fk_postulante { get; set; }
        public string eof_nombre { get; set; }
    }
}