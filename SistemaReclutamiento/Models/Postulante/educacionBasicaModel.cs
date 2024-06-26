﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using SistemaReclutamiento.Entidades;
using Npgsql;
using SistemaReclutamiento.Utilitarios;
using System.Diagnostics;

namespace SistemaReclutamiento.Models

{
    public class EducacionBasicaModel
    {
        string _conexion;
        public EducacionBasicaModel()
        {
            _conexion = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
        }
        public List<EducacionBasicaEntidad> EducacionBasicaListaporPostulanteJson(int fk_postulante)
        {
            List<EducacionBasicaEntidad> lista = new List<EducacionBasicaEntidad>();
            string consulta = @"SELECT 
                                eba_id, 
                                eba_tipo, 
                                eba_nombre,
                                eba_condicion,
                                eba_fecha_reg, 
                                eba_fecha_act, 
                                fk_postulante
	                            FROM gestion_talento.gdt_per_educacion_bas
                                where fk_postulante=@p0
                                order by eba_id desc;";
            try
            {
                using (var con = new NpgsqlConnection(_conexion))
                {
                    con.Open();
                    var query = new NpgsqlCommand(consulta, con);
                    query.Parameters.AddWithValue("@p0", fk_postulante);

                    using (var dr = query.ExecuteReader())
                    {
                        if (dr.HasRows) { 
                            while (dr.Read())
                            {
                                var educacionbasica = new EducacionBasicaEntidad
                                {
                                                             
                                    eba_id = ManejoNulos.ManageNullInteger(dr["eba_id"]),
                                    eba_tipo = ManejoNulos.ManageNullStr(dr["eba_tipo"]),
                                    eba_nombre = ManejoNulos.ManageNullStr(dr["eba_nombre"]),
                                    eba_condicion = ManejoNulos.ManageNullStr(dr["eba_condicion"]),
                                    eba_fecha_reg = ManejoNulos.ManageNullDate(dr["eba_fecha_reg"]),
                                    eba_fecha_act = ManejoNulos.ManageNullDate(dr["eba_fecha_act"]),
                                    fk_postulante = ManejoNulos.ManageNullInteger(dr["fk_postulante"]),                            

                                };

                                lista.Add(educacionbasica);
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine("" + ex.Message + this.GetType().FullName + " " + DateTime.Now.ToLongDateString());
            }

            return lista;
        }
        public EducacionBasicaEntidad EducacionBasicaIdObtenerJson(int eba_id)
        {
            EducacionBasicaEntidad educacionBasica = new EducacionBasicaEntidad();
            string consulta = @"SELECT eba_id, 
                                    eba_tipo, 
                                    eba_nombre, 
                                    eba_condicion, 
                                    eba_fecha_reg, 
                                    eba_fecha_act, 
                                    fk_postulante
	                                FROM gestion_talento.gdt_per_educacion_bas	                                  
                                            where eba_id=@p0;";
            try
            {
                using (var con = new NpgsqlConnection(_conexion))
                {
                    con.Open();
                    var query = new NpgsqlCommand(consulta, con);
                    query.Parameters.AddWithValue("@p0", eba_id);
                    using (var dr = query.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                educacionBasica.eba_id = ManejoNulos.ManageNullInteger(dr["eba_id"]);
                                educacionBasica.eba_tipo = ManejoNulos.ManageNullStr(dr["eba_tipo"]);
                                educacionBasica.eba_nombre = ManejoNulos.ManageNullStr(dr["eba_nombre"]);
                                educacionBasica.eba_condicion = ManejoNulos.ManageNullStr(dr["eba_condicion"]);
                                educacionBasica.eba_fecha_reg = ManejoNulos.ManageNullDate(dr["eba_fecha_reg"]);
                                educacionBasica.eba_fecha_act = ManejoNulos.ManageNullDate(dr["eba_fecha_act"]);
                                educacionBasica.fk_postulante = ManejoNulos.ManageNullInteger(dr["fk_postulante"]);
                                
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine("" + ex.Message + this.GetType().FullName + " " + DateTime.Now.ToLongDateString());
            }
            return educacionBasica;
        }
        public bool EducacionBasicaInsertarJson(EducacionBasicaEntidad educacionBasica)
        {
            bool response = false;
            string consulta = @"INSERT INTO 
                                        gestion_talento.gdt_per_educacion_bas
                                           (                                           
                                            eba_tipo, 
                                            eba_nombre, 
                                            eba_condicion, 
                                            eba_fecha_reg,                                         
                                            fk_postulante
                                            )
	                                            VALUES (@p0, @p1, @p2, @p3, @p4); ";
            try
            {
                using (var con = new NpgsqlConnection(_conexion))
                {
                    con.Open();
                    var query = new NpgsqlCommand(consulta, con);
                    query.Parameters.AddWithValue("@p0", educacionBasica.eba_tipo);
                    query.Parameters.AddWithValue("@p1", educacionBasica.eba_nombre);
                    query.Parameters.AddWithValue("@p2", educacionBasica.eba_condicion);
                    query.Parameters.AddWithValue("@p3", educacionBasica.eba_fecha_reg);
                    query.Parameters.AddWithValue("@p4", educacionBasica.fk_postulante);
                    query.ExecuteNonQuery();
                    response = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return response;
        }
        public bool EducacionBasicaEditarJson(EducacionBasicaEntidad educacionBasica)
        {
            bool response = false;
            string consulta = @"
                UPDATE gestion_talento.gdt_per_educacion_bas
	                    SET     
                        eba_tipo=@p0, 
                        eba_nombre=@p1, 
                        eba_condicion=@p2,                  
                        eba_fecha_act=@p3                    
                        WHERE eba_id=@p4;";
            try
            {
                using (var con = new NpgsqlConnection(_conexion))
                {
                    con.Open();
                    var query = new NpgsqlCommand(consulta, con);
                    query.Parameters.AddWithValue("@p0", ManejoNulos.ManageNullStr(educacionBasica.eba_tipo));
                    query.Parameters.AddWithValue("@p1", ManejoNulos.ManageNullStr(educacionBasica.eba_nombre));
                    query.Parameters.AddWithValue("@p2", ManejoNulos.ManageNullStr(educacionBasica.eba_condicion));
                    query.Parameters.AddWithValue("@p3", ManejoNulos.ManageNullStr(educacionBasica.eba_fecha_act));
                    query.Parameters.AddWithValue("@p4", ManejoNulos.ManageNullStr(educacionBasica.eba_id));                    
                    query.ExecuteNonQuery();
                    response = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return response;
        }
        public bool EducacionBasicaEliminarJson(int id)
        {
            bool response = false;
            string consulta = @"
                                DELETE FROM 
                                gestion_talento.gdt_per_educacion_bas
                                WHERE  eba_id=@p0;";
            try
            {
                using (var con = new NpgsqlConnection(_conexion))
                {
                    con.Open();

                    var query = new NpgsqlCommand(consulta, con);
                    query.Parameters.AddWithValue("@p0", ManejoNulos.ManageNullInteger(id));
                    query.ExecuteNonQuery();
                    response = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message); 
            }

            return response;
        }
    }
}