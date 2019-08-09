﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaReclutamiento.Models
{
    public class ClaseLibreModel
    {
    }
    public class ReporteOfertaLaboral
    {
        public String ola_nombre { get; set; }
        public String ola_cod_empresa { get; set; }
        public String ola_cod_cargo { get; set; }
        public int ubi_distrito_id { get; set; }
        public string ola_rango_fecha { get; set; }
        public DateTime ola_fecha_ini { get; set; }
        public int pos_id { get; set; }
    }
    public class postulacionEntidad
    {
        public int ppo_id { get; set; }
        public string ppo_tipo_direcion { get; set; }
        public string ppo_direccion { get; set; }
        public string ppo_tipo_calle { get; set; }
        public string ppo_numero_casa { get; set; }
        public string ppo_tipo_casa { get; set; }
        public string ppo_celular { get; set; }
        public string ppo_estado_civil { get; set; }
        public bool ppo_brevete { get; set; }
        public string ppo_num_brevete { get; set; }
        public bool ppo_referido { get; set; }
        public string ppo_nombre_referido { get; set; }
        public string ppo_cv { get; set; }
        public string ppo_foto { get; set; }
        public string ppo_situacion { get; set; }
        public DateTime ppo_fecha_reg { get; set; }
        public DateTime ppo_fecha_act { get; set; }
        public string ppo_estado { get; set; }
        public int fk_postulante { get; set; }
        public int fk_oferta_laboral { get; set; }
    }

}