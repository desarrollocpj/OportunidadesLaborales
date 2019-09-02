﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaReclutamiento.Entidades
{
    public class usuarioEntidad
    {
        public string usu_nombre { get; set; }
        public string usu_contrasenia { get; set; }
        public string usu_estado { get; set; }
        public int fk_persona { get; set; }
        public int usu_id {get;set;}
        public DateTime usu_fecha_reg { get; set; }
        public DateTime usu_fecha_act { get; set; }
        public bool usu_cambio_pass { get; set; }
        public int fk_template { get; set; }
        public bool usu_enviar_mail { get; set; }
        public bool usu_enviado { get; set; }
        public string usu_clave_temp { get; set; }  
        public string per_tipo { get; set; }
    }
}