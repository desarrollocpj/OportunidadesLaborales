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
    public class IntranetDetalleElementoModel
    {
        string _conexion;
        public IntranetDetalleElementoModel()
        {
            _conexion = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
        }
        public (List<IntranetDetalleElementoEntidad> intranetDetalleElementoLista, claseError error) IntranetDetalleElementoListarJson()
        {
            List<IntranetDetalleElementoEntidad> lista = new List<IntranetDetalleElementoEntidad>();
            claseError error = new claseError();
            string consulta = @"SELECT detel_id, detel_descripcion, detel_nombre, 
                                detel_extension, detel_ubicacion, detel_estado, fk_elemento, fk_seccion_elemento,detel_orden,detel_posicion
	                                FROM intranet.int_detalle_elemento;";
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
                                var Imagen = new IntranetDetalleElementoEntidad
                                {

                                    detel_id = ManejoNulos.ManageNullInteger(dr["detel_id"]),
                                    detel_descripcion = ManejoNulos.ManageNullStr(dr["detel_descripcion"]),
                                    detel_nombre = ManejoNulos.ManageNullStr(dr["detel_nombre"]),
                                    detel_extension = ManejoNulos.ManageNullStr(dr["detel_extension"]),
                                    detel_ubicacion = ManejoNulos.ManageNullStr(dr["detel_ubicacion"]),
                                    detel_estado = ManejoNulos.ManageNullStr(dr["detel_estado"]),
                                    fk_elemento = ManejoNulos.ManageNullInteger(dr["fk_elemento"]),
                                    fk_seccion_elemento = ManejoNulos.ManageNullInteger(dr["fk_seccion_elemento"]),
                                    detel_orden = ManejoNulos.ManageNullInteger(dr["detel_orden"]),
                                    detel_posicion = ManejoNulos.ManageNullStr(dr["detel_posicion"]),
                                };

                                lista.Add(Imagen);
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
            return (intranetDetalleElementoLista: lista, error: error);
        }

        public (List<IntranetDetalleElementoEntidad> intranetDetalleElementoListaxElementoID, claseError error) IntranetDetalleElementoListarxElementoIDJson(int elemento_id)
        {
            List<IntranetDetalleElementoEntidad> lista = new List<IntranetDetalleElementoEntidad>();
            claseError error = new claseError();
            string consulta = @"SELECT detel_id, detel_descripcion,detel_nombre, 
                                detel_extension, detel_ubicacion, detel_estado, fk_elemento, fk_seccion_elemento,detel_orden,detel_posicion
	                                FROM intranet.int_detalle_elemento 
                                    where fk_elemento=@p0;";
            try
            {
                using (var con = new NpgsqlConnection(_conexion))
                {
                    con.Open();
                    var query = new NpgsqlCommand(consulta, con);
                    query.Parameters.AddWithValue("@p0", elemento_id);
                    using (var dr = query.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                var Imagen = new IntranetDetalleElementoEntidad
                                {
                                    detel_id = ManejoNulos.ManageNullInteger(dr["detel_id"]),
                                    detel_descripcion = ManejoNulos.ManageNullStr(dr["detel_descripcion"]),
                                    detel_nombre = ManejoNulos.ManageNullStr(dr["detel_nombre"]),
                                    detel_extension = ManejoNulos.ManageNullStr(dr["detel_extension"]),
                                    detel_ubicacion = ManejoNulos.ManageNullStr(dr["detel_ubicacion"]),
                                    detel_estado = ManejoNulos.ManageNullStr(dr["detel_estado"]),
                                    fk_elemento = ManejoNulos.ManageNullInteger(dr["fk_elemento"]),
                                    fk_seccion_elemento = ManejoNulos.ManageNullInteger(dr["fk_seccion_elemento"]),
                                    detel_orden = ManejoNulos.ManageNullInteger(dr["detel_orden"]),
                                    detel_posicion = ManejoNulos.ManageNullStr(dr["detel_posicion"]),

                                };

                                lista.Add(Imagen);
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
            return (intranetDetalleElementoListaxElementoID: lista, error: error);
        }

        public (IntranetDetalleElementoEntidad intranetDetalleElemento, claseError error) IntranetDetalleElementoIdObtenerJson(int detel_id)
        {
            IntranetDetalleElementoEntidad intranetImagen = new IntranetDetalleElementoEntidad();
            claseError error = new claseError();
            string consulta = @"SELECT detel_id, detel_descripcion, detel_nombre, 
                                detel_extension, detel_ubicacion, detel_estado, fk_elemento, fk_seccion_elemento
	                                FROM intranet.int_detalle_elemento where detel_id=@p0;";
            try
            {
                using (var con = new NpgsqlConnection(_conexion))
                {
                    con.Open();
                    var query = new NpgsqlCommand(consulta, con);
                    query.Parameters.AddWithValue("@p0", detel_id);
                    using (var dr = query.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {

                                intranetImagen.detel_id = ManejoNulos.ManageNullInteger(dr["detel_id"]);
                                intranetImagen.detel_descripcion = ManejoNulos.ManageNullStr(dr["detel_descripcion"]);
                                intranetImagen.detel_nombre = ManejoNulos.ManageNullStr(dr["detel_nombre"]);
                                intranetImagen.detel_extension = ManejoNulos.ManageNullStr(dr["detel_extension"]);
                                intranetImagen.detel_ubicacion = ManejoNulos.ManageNullStr(dr["detel_ubicacion"]);
                                intranetImagen.detel_estado = ManejoNulos.ManageNullStr(dr["detel_estado"]);
                                intranetImagen.fk_elemento = ManejoNulos.ManageNullInteger(dr["fk_elemento"]);
                                intranetImagen.fk_seccion_elemento = ManejoNulos.ManageNullInteger(dr["fk_seccion_elemento"]);
                                intranetImagen.detel_orden = ManejoNulos.ManageNullInteger(dr["detel_orden"]);
                                intranetImagen.detel_posicion = ManejoNulos.ManageNullStr(dr["detel_posicion"]);
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
            return (intranetDetalleElemento: intranetImagen, error: error);
        }
        public (int idIntranetDetalleElementoInsertado, claseError error) IntranetDetalleElementoInsertarJson(IntranetDetalleElementoEntidad intranetDetalleElemento)
        {
            //bool response = false;
            int idIntranetImagenInsertado = 0;
            string consulta = @"
            INSERT INTO intranet.int_detalle_elemento(
	            detel_descripcion, detel_nombre, detel_extension, detel_ubicacion, detel_estado,fk_elemento,fk_seccion_elemento)
	            VALUES ( @p0, @p1, @p2,@p3, @p5,@p6,@p7);
                returning detel_id;";
            claseError error = new claseError();
            try
            {
                using (var con = new NpgsqlConnection(_conexion))
                {
                    con.Open();
                    var query = new NpgsqlCommand(consulta, con);
                    query.Parameters.AddWithValue("@p0", ManejoNulos.ManageNullStr(intranetDetalleElemento.detel_descripcion));
                    query.Parameters.AddWithValue("@p1", ManejoNulos.ManageNullStr(intranetDetalleElemento.detel_nombre));
                    query.Parameters.AddWithValue("@p2", ManejoNulos.ManageNullStr(intranetDetalleElemento.detel_extension));
                    query.Parameters.AddWithValue("@p3", ManejoNulos.ManageNullStr(intranetDetalleElemento.detel_ubicacion));
                    query.Parameters.AddWithValue("@p5", ManejoNulos.ManageNullStr(intranetDetalleElemento.detel_estado));
                    query.Parameters.AddWithValue("@p6", ManejoNulos.ManageNullInteger(intranetDetalleElemento.fk_elemento));
                    query.Parameters.AddWithValue("@p7", ManejoNulos.ManageNullInteger(intranetDetalleElemento.fk_seccion_elemento));
                    idIntranetImagenInsertado = Int32.Parse(query.ExecuteScalar().ToString());
                    //query.ExecuteNonQuery();
                    //response = true;
                }
            }
            catch (Exception ex)
            {
                error.Key = ex.Data.Count.ToString();
                error.Value = ex.Message;
            }
            return (idIntranetDetalleElementoInsertado: idIntranetImagenInsertado, error: error);
        }

        public (bool intranetDetalleElementoEditado, claseError error) IntranetDetalleElementoEditarJson(IntranetDetalleElementoEntidad intranetDetalleElemento)
        {
            claseError error = new claseError();
            bool response = false;
            string consulta = @"UPDATE intranet.int_detalle_elemento
	                    SET  detel_descripcion=@p0, detel_nombre=@p1, detel_extension=@p2, detel_ubicacion=@p3,  detel_estado=@p5,fk_elemento=@p7,@fk_seccion_elemento=@p8
	                    WHERE detel_id=@p6;";
            try
            {
                using (var con = new NpgsqlConnection(_conexion))
                {
                    con.Open();
                    var query = new NpgsqlCommand(consulta, con);
                    query.Parameters.AddWithValue("@p0", ManejoNulos.ManageNullStr(intranetDetalleElemento.detel_descripcion));
                    query.Parameters.AddWithValue("@p1", ManejoNulos.ManageNullStr(intranetDetalleElemento.detel_nombre));
                    query.Parameters.AddWithValue("@p2", ManejoNulos.ManageNullStr(intranetDetalleElemento.detel_extension));
                    query.Parameters.AddWithValue("@p3", ManejoNulos.ManageNullStr(intranetDetalleElemento.detel_ubicacion));
                    query.Parameters.AddWithValue("@p5", ManejoNulos.ManageNullStr(intranetDetalleElemento.detel_estado));
                    query.Parameters.AddWithValue("@p6", ManejoNulos.ManageNullInteger(intranetDetalleElemento.detel_id));
                    query.Parameters.AddWithValue("@p7", ManejoNulos.ManageNullInteger(intranetDetalleElemento.fk_elemento));
                    query.Parameters.AddWithValue("@p8", ManejoNulos.ManageNullInteger(intranetDetalleElemento.fk_seccion_elemento));
                    query.ExecuteNonQuery();
                    response = true;
                }
            }
            catch (Exception ex)
            {
                error.Key = ex.Data.Count.ToString();
                error.Value = ex.Message;
            }
            return (intranetDetalleElementoEditado: response, error: error);
        }
        public (bool intranetDetalleElementoEliminado, claseError error) IntranetDetalleElementoEliminarJson(int detel_id)
        {
            bool response = false;
            string consulta = @"DELETE FROM intranet.int_detalle_elemento
	                            WHERE detel_id=@p0;";
            claseError error = new claseError();
            try
            {
                using (var con = new NpgsqlConnection(_conexion))
                {
                    con.Open();

                    var query = new NpgsqlCommand(consulta, con);
                    query.Parameters.AddWithValue("@p0", ManejoNulos.ManageNullInteger(detel_id));
                    query.ExecuteNonQuery();
                    response = true;
                }
            }
            catch (Exception ex)
            {
                error.Key = ex.Data.Count.ToString();
                error.Value = ex.Message;
            }

            return (intranetDetalleElementoEliminado: response, error: error);
        }
    }
}