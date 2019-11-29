﻿using Npgsql;
using SistemaReclutamiento.Entidades.IntranetPJ;
using SistemaReclutamiento.Utilitarios;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace SistemaReclutamiento.Models.IntranetPJ
{
    public class IntranetElementoModel
    {
        string _conexion;
        public IntranetElementoModel()
        {
            _conexion = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
        }
        public (List<IntranetElementoEntidad> intranetElementoLista, claseError error) IntranetElementoListarJson()
        {
            List<IntranetElementoEntidad> lista = new List<IntranetElementoEntidad>();
            claseError error = new claseError();
            string consulta = @"SELECT elem_id, elem_titulo, elem_descripcion, elem_contenido, elem_orden
                                , elem_estado, fk_seccion, fk_tipo_elemento
	                            FROM intranet.int_elemento
                                order by elem_orden;";
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
                                var Elemento = new IntranetElementoEntidad
                                {

                                    elem_id = ManejoNulos.ManageNullInteger(dr["elem_id"]),
                                    elem_titulo = ManejoNulos.ManageNullStr(dr["elem_titulo"]),
                                    elem_descripcion = ManejoNulos.ManageNullStr(dr["elem_descripcion"]),
                                    elem_contenido = ManejoNulos.ManageNullStr(dr["elem_contenido"]),
                                    elem_orden = ManejoNulos.ManageNullInteger(dr["elem_orden"]),
                                    elem_estado = ManejoNulos.ManageNullStr(dr["elem_estado"]),
                                    fk_seccion = ManejoNulos.ManageNullInteger(dr["fk_seccion"]),
                                    fk_tipo_elemento = ManejoNulos.ManageNullInteger(dr["fk_tipo_elemento"]),
                                };

                                lista.Add(Elemento);
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                error.Key = ex.Data.Count.ToString();
                error.Value = ex.Message;
            }
            return (intranetElementoLista: lista, error: error);
        }

        public (List<IntranetElementoEntidad> intranetElementoListaxSeccionID, claseError error) IntranetElementoListarxSeccionIDJson(int seccion_id)
        {
            List<IntranetElementoEntidad> lista = new List<IntranetElementoEntidad>();
            claseError error = new claseError();
            string consulta = @"SELECT elem_id, elem_titulo, elem_descripcion, elem_contenido, elem_orden
                                , elem_estado, fk_seccion, fk_tipo_elemento
	                            FROM intranet.int_elemento
                                where fk_seccion = @p0 
                                order by elem_orden;";
            try
            {
                using (var con = new NpgsqlConnection(_conexion))
                {
                    con.Open();
                    var query = new NpgsqlCommand(consulta, con);
                    query.Parameters.AddWithValue("@p0", seccion_id);
                    using (var dr = query.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                var Elemento = new IntranetElementoEntidad
                                {

                                    elem_id = ManejoNulos.ManageNullInteger(dr["elem_id"]),
                                    elem_titulo = ManejoNulos.ManageNullStr(dr["elem_titulo"]),
                                    elem_descripcion = ManejoNulos.ManageNullStr(dr["elem_descripcion"]),
                                    elem_contenido = ManejoNulos.ManageNullStr(dr["elem_contenido"]),
                                    elem_orden = ManejoNulos.ManageNullInteger(dr["elem_orden"]),
                                    elem_estado = ManejoNulos.ManageNullStr(dr["elem_estado"]),
                                    fk_seccion = ManejoNulos.ManageNullInteger(dr["fk_seccion"]),
                                    fk_tipo_elemento = ManejoNulos.ManageNullInteger(dr["fk_tipo_elemento"]),
                                };

                                lista.Add(Elemento);
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                error.Key = ex.Data.Count.ToString();
                error.Value = ex.Message;
            }
            return (intranetElementoListaxSeccionID: lista, error: error);
        }


        public (IntranetElementoEntidad intranetElemento, claseError error) IntranetElementoIdObtenerJson(int Elemento_id)
        {
            IntranetElementoEntidad intranetElemento = new IntranetElementoEntidad();
            claseError error = new claseError();
            string consulta = @"SELECT elem_id, elem_titulo, elem_descripcion, elem_contenido, elem_orden 
                                , elem_estado, fk_seccion, fk_tipo_elemento
	                            FROM intranet.int_elemento
                                where elem_id=@p0;";
            try
            {
                using (var con = new NpgsqlConnection(_conexion))
                {
                    con.Open();
                    var query = new NpgsqlCommand(consulta, con);
                    query.Parameters.AddWithValue("@p0", Elemento_id);
                    using (var dr = query.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {

                                intranetElemento.elem_id = ManejoNulos.ManageNullInteger(dr["elem_id"]);
                                intranetElemento.elem_titulo = ManejoNulos.ManageNullStr(dr["elem_titulo"]);
                                intranetElemento.elem_descripcion = ManejoNulos.ManageNullStr(dr["elem_descripcion"]);
                                intranetElemento.elem_contenido = ManejoNulos.ManageNullStr(dr["elem_contenido"]);
                                intranetElemento.elem_orden = ManejoNulos.ManageNullInteger(dr["elem_orden"]);
                                intranetElemento.elem_estado = ManejoNulos.ManageNullStr(dr["elem_estado"]);
                                intranetElemento.fk_seccion = ManejoNulos.ManageNullInteger(dr["fk_seccion"]);
                                intranetElemento.fk_tipo_elemento = ManejoNulos.ManageNullInteger(dr["fk_tipo_elemento"]);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                error.Key = ex.Data.Count.ToString();
                error.Value = ex.Message;
            }
            return (intranetElemento: intranetElemento, error: error);
        }
        public (int idIntranetElementoInsertado, claseError error) IntranetElementoInsertarJson(IntranetElementoEntidad intranetElemento)
        {
            //bool response = false;
            int idIntranetElementoInsertado = 0;
            string consulta = @"INSERT INTO intranet.int_elemento(
	 elem_titulo, elem_descripcion, elem_contenido, elem_orden, elem_estado, fk_seccion, fk_tipo_elemento)
	VALUES (@p0, @p1, @p2, @p3, @p5, @p6, @p7)
                returning elem_id;";
            claseError error = new claseError();
            try
            {
                using (var con = new NpgsqlConnection(_conexion))
                {
                    con.Open();
                    var query = new NpgsqlCommand(consulta, con);
                    query.Parameters.AddWithValue("@p0", ManejoNulos.ManageNullStr(intranetElemento.elem_titulo));
                    query.Parameters.AddWithValue("@p1", ManejoNulos.ManageNullStr(intranetElemento.elem_descripcion));
                    query.Parameters.AddWithValue("@p2", ManejoNulos.ManageNullStr(intranetElemento.elem_contenido));
                    query.Parameters.AddWithValue("@p3", ManejoNulos.ManageNullInteger(intranetElemento.elem_orden));
                    query.Parameters.AddWithValue("@p5", ManejoNulos.ManageNullStr(intranetElemento.elem_estado));
                    query.Parameters.AddWithValue("@p6", ManejoNulos.ManageNullInteger(intranetElemento.fk_seccion));
                    query.Parameters.AddWithValue("@p7", ManejoNulos.ManageNullInteger(intranetElemento.fk_tipo_elemento));
                    idIntranetElementoInsertado = Int32.Parse(query.ExecuteScalar().ToString());
                    //query.ExecuteNonQuery();
                    //response = true;
                }
            }
            catch (Exception ex)
            {
                error.Key = ex.Data.Count.ToString();
                error.Value = ex.Message;
            }
            return (idIntranetElementoInsertado: idIntranetElementoInsertado, error: error);
        }
        public (bool intranetElementoEditado, claseError error) IntranetElementoEditarJson(IntranetElementoEntidad intranetElemento)
        {
            claseError error = new claseError();
            bool response = false;
            string consulta = @"UPDATE intranet.int_elemento
	                            SET elem_titulo=@p0, elem_descripcion=@p1, elem_contenido=@p2, elem_orden=@p3,
                                elem_estado=@p5, fk_seccion=@p6, fk_tipo_elemento=@p7
	                            WHERE elem_id=@p8;";
            try
            {
                using (var con = new NpgsqlConnection(_conexion))
                {
                    con.Open();
                    var query = new NpgsqlCommand(consulta, con);
                    query.Parameters.AddWithValue("@p0", ManejoNulos.ManageNullStr(intranetElemento.elem_titulo));
                    query.Parameters.AddWithValue("@p1", ManejoNulos.ManageNullStr(intranetElemento.elem_descripcion));
                    query.Parameters.AddWithValue("@p2", ManejoNulos.ManageNullStr(intranetElemento.elem_contenido));
                    query.Parameters.AddWithValue("@p3", ManejoNulos.ManageNullInteger(intranetElemento.elem_orden));
                    query.Parameters.AddWithValue("@p5", ManejoNulos.ManageNullStr(intranetElemento.elem_estado));
                    query.Parameters.AddWithValue("@p6", ManejoNulos.ManageNullInteger(intranetElemento.fk_seccion));
                    query.Parameters.AddWithValue("@p7", ManejoNulos.ManageNullInteger(intranetElemento.fk_tipo_elemento));
                    query.Parameters.AddWithValue("@p8", ManejoNulos.ManageNullInteger(intranetElemento.elem_id));
                    query.ExecuteNonQuery();
                    response = true;
                }
            }
            catch (Exception ex)
            {
                error.Key = ex.Data.Count.ToString();
                error.Value = ex.Message;
            }
            return (intranetElementoEditado: response, error: error);
        }
        public (bool intranetElementoEliminado, claseError error) IntranetElementoEliminarJson(int Elemento_id)
        {
            bool response = false;
            string consulta = @"DELETE FROM intranet.int_elemento
	                                WHERE elem_id=@p0;";
            claseError error = new claseError();
            try
            {
                using (var con = new NpgsqlConnection(_conexion))
                {
                    con.Open();

                    var query = new NpgsqlCommand(consulta, con);
                    query.Parameters.AddWithValue("@p0", ManejoNulos.ManageNullInteger(Elemento_id));
                    query.ExecuteNonQuery();
                    response = true;
                }
            }
            catch (Exception ex)
            {
                error.Key = ex.Data.Count.ToString();
                error.Value = ex.Message;
            }

            return (intranetElementoEliminado: response, error: error);
        }
    }
}