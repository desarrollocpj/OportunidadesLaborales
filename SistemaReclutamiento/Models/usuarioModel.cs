﻿using System;
using System.Collections.Generic;
using System.Configuration;
//using System.Data.SqlClient;
using Npgsql;
using System.Linq;
using System.Web;
using SistemaReclutamiento.Entidades;
using SistemaReclutamiento.Utilitarios;

namespace SistemaReclutamiento.Models
{
    public class UsuarioModel
    {
        string _conexion = string.Empty;
        string busquedaProveedor = "PROVEEDOR";
        public UsuarioModel() {
            _conexion = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
        }
        #region Usuario Postulante
               
        public UsuarioEntidad UsuarioObtenerxID(int id)
        {
            UsuarioEntidad usuario = new UsuarioEntidad();
            string consulta = @"SELECT usu_id,usu_nombre,usu_estado,usu_clave_temp,fk_persona, usu_token,usu_exp_token 
	                            FROM seguridad.seg_usuario where usu_id=@p0;";
            try
            {
                using (var con = new NpgsqlConnection(_conexion))
                {
                    con.Open();
                    var query = new NpgsqlCommand(consulta, con);
                    query.Parameters.AddWithValue("@p0", id);
                    using (var dr = query.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                usuario.usu_id = ManejoNulos.ManageNullInteger(dr["usu_id"]);
                                usuario.usu_nombre = ManejoNulos.ManageNullStr(dr["usu_nombre"]);
                                //usuario.usu_contrasenia = ManejoNulos.ManageNullStr(dr["usu_contraseña"]);
                                usuario.usu_estado = ManejoNulos.ManageNullStr(dr["usu_estado"]);
                                usuario.usu_clave_temp = ManejoNulos.ManageNullStr(dr["usu_clave_temp"]);
                                usuario.fk_persona = ManejoNulos.ManageNullInteger(dr["fk_persona"]);
                                usuario.usu_token = ManejoNulos.ManageNullStr(dr["usu_token"]);
                                usuario.usu_exp_token = ManejoNulos.ManageNullStr(dr["usu_exp_token"]);

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return usuario;
        }
       
        public UsuarioEntidad PostulanteValidarCredenciales(string usu_login)
        {
            UsuarioEntidad usuario = new UsuarioEntidad();
            string consulta = @"SELECT usu_id,lower(usu_nombre) as usu_nombre,usu_contraseña,usu_estado,fk_persona,usu_clave_temp,usu_tipo
	                            FROM seguridad.seg_usuario where lower(usu_nombre)=@p0";
            try
            {
                using (var con = new NpgsqlConnection(_conexion))
                {
                    con.Open();
                    var query = new NpgsqlCommand(consulta, con);
                    query.Parameters.AddWithValue("@p0", usu_login);
                    using (var dr = query.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                usuario.usu_id = ManejoNulos.ManageNullInteger(dr["usu_id"]);
                                usuario.usu_nombre = ManejoNulos.ManageNullStr(dr["usu_nombre"]);
                                usuario.usu_contrasenia = ManejoNulos.ManageNullStr(dr["usu_contraseña"]);
                                usuario.usu_estado = ManejoNulos.ManageNullStr(dr["usu_estado"]);
                                usuario.fk_persona = ManejoNulos.ManageNullInteger(dr["fk_persona"]);
                                usuario.usu_clave_temp = ManejoNulos.ManageNullStr(dr["usu_clave_temp"]);
                                usuario.usu_tipo = ManejoNulos.ManageNullStr(dr["usu_tipo"]);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return usuario;
        }
        /// <summary>
        /// Metodo para Validar Una cuenta de usuario creada y que no ha sido activada
        /// </summary>
        /// <param name="usuario">(usuarioEntidad) datos del usuario</param>
        /// <returns>true o false</returns>
        public bool PostulanteUsuarioValidarEmailJson(UsuarioEntidad usuario)
        {
            bool response = false;
            string consulta = @"UPDATE seguridad.seg_usuario
            SET usu_estado = 'A'
            WHERE usuarioID = @p1";
            try
            {
                using (var con = new NpgsqlConnection(_conexion))
                {
                    con.Open();
                    var query = new NpgsqlCommand(consulta, con);               
                    query.Parameters.AddWithValue("@p1", usuario.usu_id);
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
      
        public int PostulanteUsuarioInsertarJson(UsuarioEntidad usuario)
        {
            int idUsuarioInsertado = 0;
            string consulta = @"INSERT INTO seguridad.seg_usuario(
	                            fk_persona, usu_nombre, usu_contraseña, usu_estado, usu_clave_temp, usu_cambio_pass,usu_fecha_reg,usu_tipo)
                                VALUES(@p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7)
                                returning usu_id; ";
            try
            {
                using (var con = new NpgsqlConnection(_conexion))
                {
                    con.Open();
                    var query = new NpgsqlCommand(consulta, con);
                    query.Parameters.AddWithValue("@p0", usuario.fk_persona);
                    query.Parameters.AddWithValue("@p1", usuario.usu_nombre);
                    query.Parameters.AddWithValue("@p2", usuario.usu_contrasenia);
                    query.Parameters.AddWithValue("@p3", usuario.usu_estado);
                    query.Parameters.AddWithValue("@p4", usuario.usu_clave_temp);
                    query.Parameters.AddWithValue("@p5", usuario.usu_cambio_pass);
                    query.Parameters.AddWithValue("@p6", usuario.usu_fecha_reg);
                    query.Parameters.AddWithValue("@p7", usuario.usu_tipo);
                    //query.ExecuteNonQuery();
                    //response = true;
                    idUsuarioInsertado = Int32.Parse(query.ExecuteScalar().ToString());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return idUsuarioInsertado;
        }
        /// <summary>
        /// Metodo para Obtener un token de acceso que se crea para activaciones o cambios de contraseña, encriptado con SHA512
        /// </summary>
        /// <param name="token">token obtenido desde URL</param>
        /// <returns>(usuarioEntidad) usuario al que le corresponde el token enviado</returns>
        public UsuarioEntidad PostulanteUsuarioObtenerTokenJson(string token)
        {
            UsuarioEntidad usuario = new UsuarioEntidad();
            string consulta = @"SELECT usu_id,usu_nombre,usu_contraseña,usu_estado,usu_clave_temp
	                            FROM seguridad.seg_usuario where usu_clave_temp=@p0;";
            try
            {
                using (var con = new NpgsqlConnection(_conexion))
                {
                    con.Open();
                    var query = new NpgsqlCommand(consulta, con);
                    query.Parameters.AddWithValue("@p0", token);
                    using (var dr = query.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                usuario.usu_id = ManejoNulos.ManageNullInteger(dr["usu_id"]);
                                usuario.usu_nombre = ManejoNulos.ManageNullStr(dr["usu_nombre"]);
                                usuario.usu_contrasenia = ManejoNulos.ManageNullStr(dr["usu_contraseña"]);
                                usuario.usu_estado = ManejoNulos.ManageNullStr(dr["usu_estado"]);
                                usuario.usu_clave_temp = ManejoNulos.ManageNullStr(dr["usu_clave_temp"]);
                           
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return usuario;
        }
        public bool PostulanteUsuarioEditarEstadoJson(int id, string password)
        {
            bool response = false;
            string consulta = @"
                UPDATE seguridad.seg_usuario
                SET 
                usu_estado=@p0,
                usu_contraseña=@p1,
                usu_clave_temp=@p2
	            WHERE usu_id=@p3;";
            try
            {
                using (var con = new NpgsqlConnection(_conexion))
                {
                    con.Open();
                    var query = new NpgsqlCommand(consulta, con);
                    query.Parameters.AddWithValue("@p0", "A");
                    query.Parameters.AddWithValue("@p1", password);
                    query.Parameters.AddWithValue("@p2", "");
                    query.Parameters.AddWithValue("@p3", id);
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

        public bool PostulanteUsuarioEditarContraseniaJson(int id, string password)
        {
            bool response = false;
            string consulta = @"
                UPDATE seguridad.seg_usuario
                SET 
                usu_contraseña=@p0
	            WHERE usu_id=@p1;";
            try
            {
                using (var con = new NpgsqlConnection(_conexion))
                {
                    con.Open();
                    var query = new NpgsqlCommand(consulta, con);
                    query.Parameters.AddWithValue("@p0", password);
                    query.Parameters.AddWithValue("@p1", id);
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
        /// <summary>
        /// Metodo que sirve para verificar que nombres de usuario no se repitan
        /// </summary>
        /// <param name="direccion_correo">(string) direccion de correo obtenida de formulario de Registro</param>
        /// <returns>retorna la informacion del usuario buscado</returns>
        public UsuarioEntidad PostulanteUsuarioObtenerxCorreo(string direccion_correo)
        {
            UsuarioEntidad usuario = new UsuarioEntidad();
            string consulta = @"SELECT usu_id,usu_nombre,usu_estado
	                            FROM seguridad.seg_usuario where usu_nombre=@p0;";
            try
            {
                using (var con = new NpgsqlConnection(_conexion))
                {
                    con.Open();
                    var query = new NpgsqlCommand(consulta, con);
                    query.Parameters.AddWithValue("@p0", direccion_correo);
                    using (var dr = query.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                usuario.usu_id = ManejoNulos.ManageNullInteger(dr["usu_id"]);
                                usuario.usu_nombre = ManejoNulos.ManageNullStr(dr["usu_nombre"]);                      
                                usuario.usu_estado = ManejoNulos.ManageNullStr(dr["usu_estado"]);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return usuario;
        }

        public (List<UsuarioEntidadPostulante> lista, claseError error) PostulantesListarJson()
        {
            claseError error = new claseError();
            List<UsuarioEntidadPostulante> lista = new List<UsuarioEntidadPostulante>();

            string consulta = @"SELECT usu.usu_id,usu.usu_estado,per.per_numdoc,per.per_nombre,per.per_apellido_pat,per.per_correoelectronico
	                            FROM seguridad.seg_usuario usu
                                join marketing.cpj_persona per on per.per_id=usu.fk_persona
                                where usu.usu_tipo='POSTULANTE' and usu.usu_estado='A' ";

            try
            {
                using (var con = new NpgsqlConnection(_conexion))
                {
                    con.Open();
                    var query = new NpgsqlCommand(consulta, con);
                    using (var dr = query.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            var postulantes = new UsuarioEntidadPostulante
                            {
                                usu_id = ManejoNulos.ManageNullInteger(dr["usu_id"]),
                                per_nombre = ManejoNulos.ManageNullStr(dr["per_nombre"]),
                                per_apellido_pat = ManejoNulos.ManageNullStr(dr["per_apellido_pat"]),
                                per_num_doc = ManejoNulos.ManageNullStr(dr["per_numdoc"]),
                                per_correoelectronico = ManejoNulos.ManageNullStr(dr["per_correoelectronico"]),
                                usu_estado = ManejoNulos.ManageNullStr(dr["usu_estado"]),
                                
                            };
                            lista.Add(postulantes);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                error.Respuesta = false;
                error.Mensaje = ex.Message;
                //Console.Write(ex.Message);
            }
            return (lista: lista, error: error);
        }


        #endregion

        #region Usuario Proveedor
        public UsuarioEntidad ProveedorValidarCredenciales(string usu_login)
        {
            UsuarioEntidad usuario = new UsuarioEntidad();
            //    string consulta = @"SELECT u.usu_id as usu_id,
            //                        lower(u.usu_nombre) as usu_nombre,
            //                        u.usu_contraseña as usu_contraseña,
            //                        u.usu_estado as usu_estado,
            //                        u.fk_persona as fk_persona,
            //                        u.usu_clave_temp as usu_clave_temp,
            //                        per.per_tipo as per_tipo
            //                     FROM seguridad.seg_usuario as u
            //join marketing.cpj_persona as per
            //on u.fk_persona=per.per_id
            //where
            //lower(u.usu_nombre)=@p0;";
            string consulta = @"SELECT lower(usu_nombre) as usu_nombre, 
                                usu_contraseña, 
                                usu_estado,
                                fk_persona,
                                usu_id, 
                                usu_fecha_reg,
                                usu_fecha_act, 
                                usu_clave_temp, 
                                usu_tipo
                                FROM seguridad.seg_usuario
                                where usu_nombre = @p0
                               ; ";
            try
            {
                using (var con = new NpgsqlConnection(_conexion))
                {
                    con.Open();
                    var query = new NpgsqlCommand(consulta, con);
                    query.Parameters.AddWithValue("@p0", usu_login);
                  
                    using (var dr = query.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                usuario.usu_id = ManejoNulos.ManageNullInteger(dr["usu_id"]);
                                usuario.usu_nombre = ManejoNulos.ManageNullStr(dr["usu_nombre"]);
                                usuario.usu_contrasenia = ManejoNulos.ManageNullStr(dr["usu_contraseña"]);
                                usuario.usu_estado = ManejoNulos.ManageNullStr(dr["usu_estado"]);
                                usuario.fk_persona = ManejoNulos.ManageNullInteger(dr["fk_persona"]);
                                usuario.usu_clave_temp = ManejoNulos.ManageNullStr(dr["usu_clave_temp"]);
                                usuario.usu_tipo = ManejoNulos.ManageNullStr(dr["usu_tipo"]);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return usuario;
        }
        public int ProveedorUsuarioInsertarJson(UsuarioEntidad usuario)
        {
            int idUsuarioInsertado = 0;
            string consulta = @"INSERT INTO seguridad.seg_usuario(
	                            fk_persona, usu_nombre, usu_contraseña, usu_estado, usu_clave_temp, usu_cambio_pass,usu_fecha_reg,usu_tipo)
                                VALUES(@p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7)
                                returning usu_id; ";
            try
            {
                using (var con = new NpgsqlConnection(_conexion))
                {
                    con.Open();
                    var query = new NpgsqlCommand(consulta, con);
                    query.Parameters.AddWithValue("@p0", usuario.fk_persona);
                    query.Parameters.AddWithValue("@p1", usuario.usu_nombre);
                    query.Parameters.AddWithValue("@p2", usuario.usu_contrasenia);
                    query.Parameters.AddWithValue("@p3", usuario.usu_estado);
                    query.Parameters.AddWithValue("@p4", usuario.usu_clave_temp);
                    query.Parameters.AddWithValue("@p5", usuario.usu_cambio_pass);
                    query.Parameters.AddWithValue("@p6", usuario.usu_fecha_reg);
                    query.Parameters.AddWithValue("@p7", usuario.usu_tipo);
                    //query.ExecuteNonQuery();
                    //response = true;
                    idUsuarioInsertado = Int32.Parse(query.ExecuteScalar().ToString());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return idUsuarioInsertado;
        }
        public bool ProveedorUsuarioEditarContraseniaJson(int id, string password)
        {
            bool response = false;
            string consulta = @"
                UPDATE seguridad.seg_usuario
                SET 
                usu_contraseña=@p0
	            WHERE usu_id=@p1;";
            try
            {
                using (var con = new NpgsqlConnection(_conexion))
                {
                    con.Open();
                    var query = new NpgsqlCommand(consulta, con);
                    query.Parameters.AddWithValue("@p0", password);
                    query.Parameters.AddWithValue("@p1", id);
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
        public List<UsuarioEntidad> ProveedorListarUsuariosPorPersonaJson(int fk_persona)
        {
            List<UsuarioEntidad> lista = new List<UsuarioEntidad>();
            string consulta = @"SELECT lower(usu_nombre) as usu_nombre, 
                                usu_contraseña, 
                                usu_estado,
                                fk_persona,
                                usu_id, 
                                usu_fecha_reg,
                                usu_fecha_act, 
                                usu_clave_temp, 
                                usu_tipo
                                FROM seguridad.seg_usuario
                                where fk_persona = @p0
                                and usu_estado='A';";
            try
            {
                using (var con = new NpgsqlConnection(_conexion))
                {
                    con.Open();
                    var query = new NpgsqlCommand(consulta, con);
                    query.Parameters.AddWithValue("@p0", fk_persona);

                    using (var dr = query.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                var usuario = new UsuarioEntidad
                                {
                                    usu_contrasenia = ManejoNulos.ManageNullStr(dr["usu_contraseña"]),
                                    usu_estado = ManejoNulos.ManageNullStr(dr["usu_estado"]),
                                    fk_persona = ManejoNulos.ManageNullInteger(dr["fk_persona"]),
                                    usu_id = ManejoNulos.ManageNullInteger(dr["usu_id"]),
                                    usu_fecha_reg = ManejoNulos.ManageNullDate(dr["usu_fecha_reg"]),
                                    usu_fecha_act = ManejoNulos.ManageNullDate(dr["usu_fecha_act"]),
                                    usu_clave_temp = ManejoNulos.ManageNullStr(dr["usu_clave_temp"]),
                                    usu_tipo = ManejoNulos.ManageNullStr(dr["usu_tipo"]),
                                };
                                lista.Add(usuario);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return lista;
        }

        public List<UsuarioEntidad> ProveedorListarUsuariosPorTipoJson()
        {
            List<UsuarioEntidad> lista = new List<UsuarioEntidad>();
            string consulta = @"SELECT usu_nombre, 
                                usu_contraseña, 
                                usu_estado,
                                fk_persona,
                                usu_id, 
                                usu_fecha_reg,
                                usu_fecha_act, 
                                usu_clave_temp, 
                                usu_tipo
                                FROM seguridad.seg_usuario
                                where usu_tipo='PROVEEDOR'
                                and usu_estado='A';";
            try
            {
                using (var con = new NpgsqlConnection(_conexion))
                {
                    con.Open();
                    var query = new NpgsqlCommand(consulta, con);
                    //query.Parameters.AddWithValue("@p0", fk_persona);

                    using (var dr = query.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                var usuario = new UsuarioEntidad
                                {
                                    usu_contrasenia = ManejoNulos.ManageNullStr(dr["usu_contraseña"]),
                                    usu_estado = ManejoNulos.ManageNullStr(dr["usu_estado"]),
                                    fk_persona = ManejoNulos.ManageNullInteger(dr["fk_persona"]),
                                    usu_id = ManejoNulos.ManageNullInteger(dr["usu_id"]),
                                    usu_fecha_reg = ManejoNulos.ManageNullDate(dr["usu_fecha_reg"]),
                                    usu_fecha_act = ManejoNulos.ManageNullDate(dr["usu_fecha_act"]),
                                    usu_clave_temp = ManejoNulos.ManageNullStr(dr["usu_clave_temp"]),
                                    usu_tipo = ManejoNulos.ManageNullStr(dr["usu_tipo"]),
                                    usu_nombre=ManejoNulos.ManageNullStr(dr["usu_nombre"])
                                };
                                lista.Add(usuario);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return lista;
        }
        /// <summary>
        /// Metodo para Obtener un token de acceso que se crea para activaciones o cambios de contraseña, encriptado con SHA512
        /// </summary>
        /// <param name="token">token obtenido desde URL</param>
        /// <returns>(usuarioEntidad) usuario al que le corresponde el token enviado</returns>
        public UsuarioEntidad ProveedorUsuarioObtenerTokenJson(string token)
        {
            UsuarioEntidad usuario = new UsuarioEntidad();
            string consulta = @"SELECT usu_id,usu_nombre,usu_contraseña,usu_estado,usu_clave_temp
	                            FROM seguridad.seg_usuario where usu_clave_temp=@p0;";
            try
            {
                using (var con = new NpgsqlConnection(_conexion))
                {
                    con.Open();
                    var query = new NpgsqlCommand(consulta, con);
                    query.Parameters.AddWithValue("@p0", token);
                    using (var dr = query.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                usuario.usu_id = ManejoNulos.ManageNullInteger(dr["usu_id"]);
                                usuario.usu_nombre = ManejoNulos.ManageNullStr(dr["usu_nombre"]);
                                usuario.usu_contrasenia = ManejoNulos.ManageNullStr(dr["usu_contraseña"]);
                                usuario.usu_estado = ManejoNulos.ManageNullStr(dr["usu_estado"]);
                                usuario.usu_clave_temp = ManejoNulos.ManageNullStr(dr["usu_clave_temp"]);

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return usuario;
        }
        /// <summary>
        /// Metodo que sirve para verificar que nombres de usuario no se repitan
        /// </summary>
        /// <param name="direccion_correo">(string) direccion de correo obtenida de formulario de Registro</param>
        /// <returns>retorna la informacion del usuario buscado</returns>
        public UsuarioEntidad ProveedorUsuarioObtenerxRUC(string ruc)
        {
            UsuarioEntidad usuario = new UsuarioEntidad();
            string consulta = @"SELECT usu_id,usu_nombre,usu_estado
	                            FROM seguridad.seg_usuario where usu_nombre=@p0;";
            try
            {
                using (var con = new NpgsqlConnection(_conexion))
                {
                    con.Open();
                    var query = new NpgsqlCommand(consulta, con);
                    query.Parameters.AddWithValue("@p0", ruc);
                    using (var dr = query.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                usuario.usu_id = ManejoNulos.ManageNullInteger(dr["usu_id"]);
                                usuario.usu_nombre = ManejoNulos.ManageNullStr(dr["usu_nombre"]);
                                usuario.usu_estado = ManejoNulos.ManageNullStr(dr["usu_estado"]);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return usuario;
        }
        #endregion

        #region UsuarioItranet
        public (bool tokenInsertado,claseError error) IntranetInsertarTokenJson(string token, int usu_id)
        {
            claseError error = new claseError();
            bool response = false;
            string consulta = @"UPDATE seguridad.seg_usuario
	            SET usu_token=@p0, usu_fecha_exp_token=@p1
                WHERE usu_id=@p3; ";
            DateTime fechaExpToken = DateTime.Now.AddHours(12);
            try
            {
                using(var con=new NpgsqlConnection(_conexion))
                {
                    con.Open();
                    var query = new NpgsqlCommand(consulta,con);
                    query.Parameters.AddWithValue("@p0", ManejoNulos.ManageNullStr(token));
                    query.Parameters.AddWithValue("@p1", ManejoNulos.ManageNullStr(fechaExpToken));
                    query.Parameters.AddWithValue("@p2", ManejoNulos.ManageNullStr(usu_id));
                }
            }
            catch (Exception ex) {
                error.Respuesta = false;
                error.Mensaje = ex.Message;
            }
            return (tokenInsertado: response, error: error);
        }
        public (List<UsuarioPersonaEntidad> listaUsuarios, claseError error) IntranetListarUsuariosTokenJson()
        {
            List<UsuarioPersonaEntidad> listaUsuarios = new List<UsuarioPersonaEntidad>();
            claseError error = new claseError();
            string consulta = @"SELECT usu_nombre,
                                per_id, usu_id, usu_estado,
                                usu_token, usu_exp_token,
                                per_nombre,
                                per_apellido_pat, per_apellido_mat, per_numdoc
	                                FROM marketing.cpj_persona
	                                join seguridad.seg_usuario
	                                on marketing.cpj_persona.per_id=seguridad.seg_usuario.fk_persona
	                                where per_tipo='EMPLEADO' order by per_apellido_pat;";
            try
            {
                using (var con = new NpgsqlConnection(_conexion)) {
                    con.Open();
                    var query = new NpgsqlCommand(consulta, con);
                    using (var dr = query.ExecuteReader()) {
                        if (dr.HasRows) {
                            while (dr.Read()) {
                                var usuario = new UsuarioPersonaEntidad()
                                {
                                    usu_nombre = ManejoNulos.ManageNullStr(dr["usu_nombre"]),
                                    usu_estado = ManejoNulos.ManageNullStr(dr["usu_estado"]),
                                    per_apellido_mat = ManejoNulos.ManageNullStr(dr["per_apellido_mat"]),
                                    per_nombre = ManejoNulos.ManageNullStr(dr["per_nombre"]),
                                    per_numdoc = ManejoNulos.ManageNullStr(dr["per_numdoc"]),
                                    per_apellido_pat = ManejoNulos.ManageNullStr(dr["per_apellido_pat"]),
                                    usu_token = ManejoNulos.ManageNullStr(dr["usu_token"]),
                                    usu_exp_token = ManejoNulos.ManageNullDate(dr["usu_exp_token"]),
                                    per_id = ManejoNulos.ManageNullInteger(dr["per_id"]),
                                    usu_id = ManejoNulos.ManageNullInteger(dr["usu_id"]),
                                };
                                listaUsuarios.Add(usuario);
                            }
                        }
                    }
                }
            }
            catch (Exception Ex) {
                error.Respuesta = false;
                error.Mensaje = Ex.Message;
            }
            return (listaUsuarios: listaUsuarios, error: error);
        }
        public (List<UsuarioPersonaEntidad> listaUsuarios, claseError error) IntranetListarUsuariosJson()
        {
            List<UsuarioPersonaEntidad> listaUsuarios = new List<UsuarioPersonaEntidad>();
            claseError error = new claseError();
            string consulta = @"SELECT usu_nombre,
		                        per_id, usu_id,
		                        per_nombre,
		                        per_apellido_pat, per_apellido_mat,usu_estado
			                        FROM marketing.cpj_persona
			                        join seguridad.seg_usuario
			                        on marketing.cpj_persona.per_id=seguridad.seg_usuario.fk_persona
			                        where usu_tipo='EMPLEADO';";
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
                                var usuario = new UsuarioPersonaEntidad()
                                {
                                    usu_nombre = ManejoNulos.ManageNullStr(dr["usu_nombre"]),
                                    per_apellido_mat = ManejoNulos.ManageNullStr(dr["per_apellido_mat"]),
                                    per_nombre = ManejoNulos.ManageNullStr(dr["per_nombre"]),
                                    per_apellido_pat = ManejoNulos.ManageNullStr(dr["per_apellido_pat"]),
                                    per_id = ManejoNulos.ManageNullInteger(dr["per_id"]),
                                    usu_id = ManejoNulos.ManageNullInteger(dr["usu_id"]),
                                    usu_estado = ManejoNulos.ManageNullStr(dr["usu_estado"]),
                                };
                                listaUsuarios.Add(usuario);
                            }
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                error.Respuesta = false;
                error.Mensaje = Ex.Message;
            }
            return (listaUsuarios: listaUsuarios, error: error);
        }
        #endregion
    }
}