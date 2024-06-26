﻿using Npgsql;
using SistemaReclutamiento.Entidades;
using SistemaReclutamiento.Utilitarios;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace SistemaReclutamiento.Models
{
    public class ConfiguracionModel
    {
        string _conexion;
        public ConfiguracionModel()
        {
            _conexion = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
        }
        public ConfiguracionEntidad ConfiguracionObtenerporNemonicJson(string nemotecnico)
        {
            ConfiguracionEntidad configuracion = new ConfiguracionEntidad();
            string consulta = @"SELECT 
                                config_nombre,                           
                                config_estado, 
                                config_nemonic, 
                                config_id
	                            FROM marketing.cpj_configuracion
                                where config_nemonic=@p0;";
            try
            {
                using (var con = new NpgsqlConnection(_conexion))
                {
                    con.Open();
                    var query = new NpgsqlCommand(consulta, con);
                    query.Parameters.AddWithValue("@p0", nemotecnico);
                    using (var dr = query.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                configuracion.config_id = ManejoNulos.ManageNullInteger(dr["config_id"]);
                                configuracion.config_nombre = ManejoNulos.ManageNullStr(dr["config_nombre"]);
                                configuracion.config_nemonic = ManejoNulos.ManageNullStr(dr["config_nemonic"]);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine("" + ex.Message + this.GetType().FullName + " " + DateTime.Now.ToLongDateString());
            }
            return configuracion;
        }
    }
}