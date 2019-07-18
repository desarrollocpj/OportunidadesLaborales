﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using SistemaReclutamiento.Entidades;
using SistemaReclutamiento.Utilitarios;
//using System.Data.SqlClient;
using Npgsql;

namespace SistemaReclutamiento.Models
{
    public class personaModel
    {
        string _conexion;
        public personaModel() {
            _conexion = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
        }
        public personaEntidad PersonaIdObtenerJson(int per_id)
        {
            personaEntidad persona = new personaEntidad();
            string consulta = @"SELECT 
                                    per_nombre, 
                                    per_apellido_pat, 
                                    per_direccion, 
                                    per_fechanacimiento, 
                                    per_correoelectronico, 
                                    per_tipo, 
                                    per_estado, 
                                    per_id, 
                                    per_apellido_mat, 
                                    per_telefono, 
                                    per_celular, 
                                    per_tipodoc, 
                                    per_numdoc, 
                                    fk_ubigeo, 
                                    per_sexo, 
                                    per_fecha_reg, 
                                    per_fecha_act, 
                                    fk_cargo, 
                                    per_foto
	                                    FROM marketing.cpj_persona
                                            where per_id=@p0;";         
            try
            {
                using (var con = new NpgsqlConnection(_conexion))
                {
                    con.Open();
                    var query = new NpgsqlCommand(consulta, con);
                    query.Parameters.AddWithValue("@p0", per_id);
                    using (var dr = query.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                persona.per_nombre = ManejoNulos.ManageNullStr(dr["per_nombre"]);
                                persona.per_apellido_pat = ManejoNulos.ManageNullStr(dr["per_apellido_pat"]);
                                persona.per_direccion = ManejoNulos.ManageNullStr(dr["per_direccion"]);
                                persona.per_fechanacimiento = ManejoNulos.ManageNullDate(dr["per_fechanacimiento"]);
                                persona.per_correoelectronico = ManejoNulos.ManageNullStr(dr["per_correoelectronico"]);
                                persona.per_tipo = ManejoNulos.ManageNullStr(dr["per_tipo"]);
                                persona.per_estado = ManejoNulos.ManageNullStr(dr["per_estado"]);
                                persona.per_id = ManejoNulos.ManageNullInteger(dr["per_id"]);
                                persona.per_apellido_mat = ManejoNulos.ManageNullStr(dr["per_apellido_mat"]);
                                persona.per_telefono = ManejoNulos.ManageNullStr(dr["per_telefono"]);
                                persona.per_celular = ManejoNulos.ManageNullStr(dr["per_celular"]);
                                persona.per_tipodoc = ManejoNulos.ManageNullStr(dr["per_tipodoc"]);
                                persona.per_numdoc = ManejoNulos.ManageNullStr(dr["per_numdoc"]);
                                persona.fk_ubigeo = ManejoNulos.ManageNullInteger(dr["fk_ubigeo"]);
                                persona.per_sexo = ManejoNulos.ManageNullStr(dr["per_sexo"]);
                                persona.per_fecha_reg = ManejoNulos.ManageNullDate(dr["per_fecha_reg"]);
                                persona.per_fecha_act = ManejoNulos.ManageNullDate(dr["per_fecha_act"]); 
                                persona.fk_cargo = ManejoNulos.ManageNullInteger(dr["fk_cargo"]);
                                persona.per_foto = ManejoNulos.ManageNullStr(dr["per_foto"]);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return persona;
        }
        
        public int PersonaInsertarJson(personaEntidad persona)
        {
            int idPersonaInsertada=0;
            //bool response = false;
            string consulta = @"
                            INSERT INTO marketing.cpj_persona(
                                per_numdoc, 
                                per_nombre, 
                                per_apellido_pat, 
                                per_apellido_mat,  
                                per_correoelectronico,  
                                per_estado,   
                                per_tipodoc,
                                per_fechanacimiento,
                                per_fecha_reg,
                                fk_ubigeo
                                )
	                            VALUES (@p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9)                                    
                                returning per_id;";
            try
            {
                using (var con = new NpgsqlConnection(_conexion))
                {
                    con.Open();
                    var query = new NpgsqlCommand(consulta, con);
                    query.Parameters.AddWithValue("@p0", persona.per_numdoc);
                    query.Parameters.AddWithValue("@p1", persona.per_nombre);
                    query.Parameters.AddWithValue("@p2", persona.per_apellido_pat);
                    query.Parameters.AddWithValue("@p3", persona.per_apellido_mat);
                    query.Parameters.AddWithValue("@p4", persona.per_correoelectronico);
                    query.Parameters.AddWithValue("@p5", persona.per_estado);
                    query.Parameters.AddWithValue("@p6", persona.per_tipodoc);
                    query.Parameters.AddWithValue("@p7", persona.per_fechanacimiento);
                    query.Parameters.AddWithValue("@p8", persona.per_fecha_reg);
                    query.Parameters.AddWithValue("@p9", persona.fk_ubigeo);

                    idPersonaInsertada = Int32.Parse(query.ExecuteScalar().ToString());
                  
                    //response = true;
                }
            }
            catch (Exception ex)
            {
            }
            return idPersonaInsertada;
        }
        public bool PersonaEditarJson(personaEntidad persona)
        {
            bool response = false;
            string consulta = @"
                UPDATE marketing.cpj_persona
                SET 
                per_nombre=@p0, 
                per_apellido_pat=@p1, 
                per_direccion=@p2, 
                per_fechanacimiento=@p3,      
                per_apellido_mat=@p4, 
                per_telefono=@p5, 
                per_celular=@p6, 
                per_tipodoc=@p7, 
                per_numdoc=@p8, 
                fk_ubigeo=@p9, 
                per_sexo=@p10
	                WHERE per_id=@p11;";
            try
            {
                using (var con = new NpgsqlConnection(_conexion))
                {
                    con.Open();
                    var query = new NpgsqlCommand(consulta, con);
                    query.Parameters.AddWithValue("@p0", ManejoNulos.ManageNullStr(persona.per_nombre));
                    query.Parameters.AddWithValue("@p1", ManejoNulos.ManageNullStr(persona.per_apellido_pat));
                    query.Parameters.AddWithValue("@p2", ManejoNulos.ManageNullStr(persona.per_direccion));
                    query.Parameters.AddWithValue("@p3", ManejoNulos.ManageNullDate(persona.per_fechanacimiento));
                    query.Parameters.AddWithValue("@p4", ManejoNulos.ManageNullStr(persona.per_apellido_mat));
                    query.Parameters.AddWithValue("@p5", ManejoNulos.ManageNullStr(persona.per_telefono));
                    query.Parameters.AddWithValue("@p6", ManejoNulos.ManageNullStr(persona.per_celular));
                    query.Parameters.AddWithValue("@p7", ManejoNulos.ManageNullStr(persona.per_tipodoc));
                    query.Parameters.AddWithValue("@p8", ManejoNulos.ManageNullStr(persona.per_numdoc));
                    query.Parameters.AddWithValue("@p9", ManejoNulos.ManageNullInteger(persona.fk_ubigeo));                  
                    query.Parameters.AddWithValue("@p10", ManejoNulos.ManageNullStr(persona.per_sexo));
                    query.Parameters.AddWithValue("@p11", ManejoNulos.ManageNullInteger(persona.per_id));
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
        internal personaEntidad PersonaDniEmailObtenerJson(string per_correoelectronico, string per_numdoc)
        {
            personaEntidad persona = new personaEntidad();
            string consulta = @"SELECT
                                    per_correoelectronico,                                   
                                    per_id,                          
                                    per_numdoc
	                                FROM marketing.cpj_persona
                                    where per_correoelectronico=@p0 or per_numdoc=@p1;";
            try
            {
                using (var con = new NpgsqlConnection(_conexion))
                {
                    con.Open();
                    var query = new NpgsqlCommand(consulta, con);
                    query.Parameters.AddWithValue("@p0", per_correoelectronico);
                    query.Parameters.AddWithValue("@p1", per_numdoc);
                    using (var dr = query.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                persona.per_id = ManejoNulos.ManageNullInteger(dr["per_id"]);
                                persona.per_numdoc = ManejoNulos.ManageNullStr(dr["per_numdoc"]);
                                persona.per_correoelectronico = ManejoNulos.ManageNullStr(dr["per_correoelectronico"]);

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return persona;
        }        
    }
}