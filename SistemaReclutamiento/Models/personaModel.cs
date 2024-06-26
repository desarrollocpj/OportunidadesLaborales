﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using SistemaReclutamiento.Entidades;
using SistemaReclutamiento.Utilitarios;
//using System.Data.SqlClient;
using Npgsql;
using System.Diagnostics;
using System.Collections;

namespace SistemaReclutamiento.Models
{
    public class PersonaModel
    {
        string _conexion;
        public PersonaModel() {
            _conexion = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
        }
     
        public PersonaEntidad PersonaIdObtenerJson(int per_id)
        {
            PersonaEntidad persona = new PersonaEntidad();
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
                Trace.WriteLine("" + ex.Message + this.GetType().FullName + " " + DateTime.Now.ToLongDateString());
            }
            return persona;
        }
        public (PersonaEntidad persona,claseError error)PersonaDniObtenerJson(string num_doc)
        {
            PersonaEntidad persona = new PersonaEntidad();
            claseError error = new claseError();
            //List<claseError> listaerror = new List<claseError>();
           
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
                                            where per_numdoc=@p0;";
            try
            {
                using (var con = new NpgsqlConnection(_conexion))
                {
                    con.Open();
                    var query = new NpgsqlCommand(consulta, con);
                    query.Parameters.AddWithValue("@p0", num_doc);
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
                //error.code = ex.HResult.ToString();
                //error.code = (string)ex.Data.Keys.ToString();
                //if (ex.Data.Count > 0)
                //{

                //    foreach (DictionaryEntry de in ex.Data)
                //    {
                //        //a= @"    Key: {0,-20}      Value: {1}" + de.Key.ToString() + "'"+ de.Value;
                //        //Console.WriteLine();
                //        var error = new claseError
                //        {
                //            Key = de.Key.ToString(),
                //            Value = de.Value.ToString()
                //        };
                //        listaerror.Add(error);
                //    }
                //}

                error.Respuesta = false;
                error.Mensaje = ex.Message;
                Console.Write(ex.Message);
                //ELog.save(this, ex);
            }
            //return persona;
            return (persona:persona,error:error);
        }
        public int PersonaInsertarJson(PersonaEntidad persona)
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
                                per_fecha_reg,
                                fk_ubigeo
                                )
	                            VALUES (@p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8)                                    
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
                    query.Parameters.AddWithValue("@p7", persona.per_fecha_reg);
                    query.Parameters.AddWithValue("@p8", persona.fk_ubigeo);

                    idPersonaInsertada = Int32.Parse(query.ExecuteScalar().ToString());
               
                    //response = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return idPersonaInsertada;
        }
        public bool PersonaEditarJson(PersonaEntidad persona)
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
                per_sexo=@p10,
                per_fecha_act=@p11
	                WHERE per_id=@p12;";
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
                    query.Parameters.AddWithValue("@p11", ManejoNulos.ManageNullDate(persona.per_fecha_act));
                    query.Parameters.AddWithValue("@p12", ManejoNulos.ManageNullInteger(persona.per_id));
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
        internal PersonaEntidad PersonaEmailDniObtenerJson(string per_correoelectronico,string per_numdoc)
        {
            PersonaEntidad persona = new PersonaEntidad();
            string consulta = @"SELECT                                                                  
                                    per_id,                          
                                    per_numdoc,
                                    per_correoelectronico
	                                FROM marketing.cpj_persona
                                    where per_numdoc=@p0 and  per_correoelectronico=@p1;";
            try
            {
                using (var con = new NpgsqlConnection(_conexion))
                {
                    con.Open();
                    var query = new NpgsqlCommand(consulta, con);
                    query.Parameters.AddWithValue("@p0", per_numdoc);
                    query.Parameters.AddWithValue("@p1", per_correoelectronico);
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
                Trace.WriteLine("" + ex.Message + this.GetType().FullName + " " + DateTime.Now.ToLongDateString());
            }
            return persona;
        }
        internal (PersonaEntidad persona, claseError error) PersonaEmailObtenerJson(string per_correoelectronico)
        {
            claseError error = new claseError();
            PersonaEntidad persona = new PersonaEntidad();
            string consulta = @"SELECT                                                                  
                                    per_id,                          
                                    lower(per_correoelectronico) as per_correoelectronico
	                                FROM marketing.cpj_persona
                                    where per_correoelectronico=@p0;";
            try
            {
                using (var con = new NpgsqlConnection(_conexion))
                {
                    con.Open();
                    var query = new NpgsqlCommand(consulta, con);
                    query.Parameters.AddWithValue("@p0", per_correoelectronico);
                    using (var dr = query.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                persona.per_id = ManejoNulos.ManageNullInteger(dr["per_id"]);
                                persona.per_correoelectronico = ManejoNulos.ManageNullStr(dr["per_correoelectronico"]);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                error.Respuesta = false;
                error.Mensaje = ex.Message;
               // Trace.WriteLine("" + ex.Message + this.GetType().FullName + " " + DateTime.Now.ToLongDateString());
            }
            return (persona:persona, error:error);
        }
        public (List<PersonaEntidad> listaPersonas, claseError error) PersonaListarEmpleadosJson() {
            List<PersonaEntidad> listaPersonas = new List<PersonaEntidad>();
            claseError error = new claseError();
            string consulta = @"SELECT per_nombre, per_apellido_pat, per_direccion, per_fechanacimiento,
                                per_correoelectronico, per_tipo, per_estado, per_id, per_apellido_mat, 
                                per_telefono, per_celular, per_tipodoc, per_numdoc, 
                                fk_ubigeo, per_sexo, per_fecha_reg, per_fecha_act, fk_cargo, per_foto
	                            FROM marketing.cpj_persona
                                where per_tipo='EMPLEADO';";
            try
            {
                using (var con = new NpgsqlConnection(_conexion)) {
                    con.Open();
                    var query = new NpgsqlCommand(consulta, con);
                    using (var dr = query.ExecuteReader()) {
                        if (dr.HasRows) {
                            while (dr.Read()) {
                                var Persona = new PersonaEntidad
                                {
                                    per_nombre = ManejoNulos.ManageNullStr(dr["per_nombre"]),
                                    per_apellido_pat = ManejoNulos.ManageNullStr(dr["per_apellido_pat"]),
                                    per_direccion = ManejoNulos.ManageNullStr(dr["per_direccion"]),
                                    per_fechanacimiento = ManejoNulos.ManageNullDate(dr["per_fechanacimiento"]),
                                    per_correoelectronico = ManejoNulos.ManageNullStr(dr["per_correoelectronico"]),
                                    per_tipo = ManejoNulos.ManageNullStr(dr["per_tipo"]),
                                    per_estado = ManejoNulos.ManageNullStr(dr["per_estado"]),
                                    per_id = ManejoNulos.ManageNullInteger(dr["per_id"]),
                                    per_apellido_mat = ManejoNulos.ManageNullStr(dr["per_apellido_mat"]),
                                    per_telefono = ManejoNulos.ManageNullStr(dr["per_telefono"]),
                                    per_celular = ManejoNulos.ManageNullStr(dr["per_celular"]),
                                    per_tipodoc = ManejoNulos.ManageNullStr(dr["per_tipodoc"]),
                                    per_numdoc = ManejoNulos.ManageNullStr(dr["per_numdoc"]),
                                    fk_ubigeo = ManejoNulos.ManageNullInteger(dr["fk_ubigeo"]),
                                    per_sexo = ManejoNulos.ManageNullStr(dr["per_sexo"]),
                                    per_fecha_reg = ManejoNulos.ManageNullDate(dr["per_fecha_reg"]),
                                    per_fecha_act = ManejoNulos.ManageNullDate(dr["per_fecha_act"]),
                                    fk_cargo = ManejoNulos.ManageNullInteger(dr["fk_cargo"]),
                                    per_foto = ManejoNulos.ManageNullStr(dr["per_foto"]),
                                };
                                listaPersonas.Add(Persona);
                            }
                        }
                    }
                }
            }
            catch (Exception ex) {
                error.Respuesta = false;
                error.Mensaje = ex.Message;
            }
            return (listaPersonas: listaPersonas, error: error);
        }
        #region Region Proveedor
        public int PersonaProveedorInsertarJson(PersonaEntidad persona)
        {
            int idPersonaInsertada = 0;
            //bool response = false;
            string consulta = @"
                            INSERT INTO marketing.cpj_persona(
                                per_numdoc, 
                                per_nombre, 
                                per_apellido_pat, 
                                per_apellido_mat,  
                                per_correoelectronico,  
                                per_estado,   
                                per_fecha_reg                              
                                )
	                            VALUES (@p0,@p1,@p2,@p3,@p4,@p5,@p6)                                    
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
                    query.Parameters.AddWithValue("@p6", persona.per_fecha_reg);
                    idPersonaInsertada = Int32.Parse(query.ExecuteScalar().ToString());

                    //response = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return idPersonaInsertada;
        }
        #endregion
        #region Region IntranetPJ
        public (List<PersonaEntidad> personaLista, claseError error) PersonaObtenerCumpleaniosporDia()
        {
            claseError error = new claseError();
            List<PersonaEntidad> personaLista = new List<PersonaEntidad>();
            string consulta = @"select 
                                    per_id,
                                    per_nombre,
                                    per_apellido_pat,
                                    per_correoelectronico,
                                    per_apellido_mat,
                                    per_fechanacimiento ,
									per_tipo,
                                    per_numdoc
                                    from marketing.cpj_persona join 
									seguridad.seg_usuario
									on seguridad.seg_usuario.fk_persona=marketing.cpj_persona.per_id
                                    where 
									extract(day from per_fechanacimiento)>=extract(day from(select current_date))
									and
	                            	extract(month from per_fechanacimiento)=extract(month from (select current_date))
	                                and per_tipo='EMPLEADO'
	                                and per_estado='A'
									and usu_estado='A'
									order by extract(day from per_fechanacimiento) asc
									limit 8";
            try
            {
                using (var con = new NpgsqlConnection(_conexion))
                {
                    con.Open();
                    var query = new NpgsqlCommand(consulta, con);
                    using (var dr = query.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {

                            while (dr.Read())
                            {
                                var persona = new PersonaEntidad
                                {
                                    per_id = ManejoNulos.ManageNullInteger(dr["per_id"]),
                                    per_nombre = ManejoNulos.ManageNullStr(dr["per_nombre"]),
                                    per_apellido_pat = ManejoNulos.ManageNullStr(dr["per_apellido_pat"]),
                                    per_fechanacimiento = ManejoNulos.ManageNullDate(dr["per_fechanacimiento"]),
                                    per_correoelectronico = ManejoNulos.ManageNullStr(dr["per_correoelectronico"]),
                                    per_apellido_mat = ManejoNulos.ManageNullStr(dr["per_apellido_mat"]),
                                    per_numdoc=ManejoNulos.ManageNullStr(dr["per_numdoc"])
                                };
                                personaLista.Add(persona);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                error.Respuesta = false;
                error.Mensaje = ex.Message;
                // Trace.WriteLine("" + ex.Message + this.GetType().FullName + " " + DateTime.Now.ToLongDateString());
            }
            return (personaLista: personaLista, error: error);
        }
        #endregion
    }
}