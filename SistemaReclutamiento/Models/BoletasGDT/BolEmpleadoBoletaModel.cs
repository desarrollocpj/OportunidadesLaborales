﻿using Npgsql;
using SistemaReclutamiento.Entidades.BoletasGDT;
using SistemaReclutamiento.Utilitarios;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace SistemaReclutamiento.Models.BoletasGDT
{
    public class BolEmpleadoBoletaModel
    {
        string _conexion;
        public BolEmpleadoBoletaModel()
        {
            _conexion = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
        }
        public (int totalInsertados, claseError error) BoolEmpleadoBoletaInsertarMasivoJson(string values)
        {
            //bool response = false;
            int totalInsertados = 0;
            string consulta = @"INSERT INTO boletas_gdt.bol_empleado_boleta(
	emp_co_trab, emp_co_empr, emp_anio, emp_periodo, emp_ruta_pdf, emp_enviado, emp_descargado, emp_fecha_reg, emp_no_trab, emp_apel_pat, emp_apel_mat, emp_direc_mail, emp_nro_cel, emp_tipo_doc)
	VALUES " +values;
            claseError error = new claseError();
            try
            {
                using (var con = new NpgsqlConnection(_conexion))
                {
                    con.Open();
                    var query = new NpgsqlCommand(consulta, con);

                    totalInsertados = query.ExecuteNonQuery();
                    //query.ExecuteNonQuery();
                    //response = true;
                }
            }
            catch (Exception ex)
            {
                error.Key = ex.Data.Count.ToString();
                error.Value = ex.Message;
            }
            return (totalInsertados: totalInsertados, error: error);
        }
        public (List<BolEmpleadoBoletaEntidad> lista, claseError error) BoolEmpleadoBoletaListarJson(string emp_co_empr, string anio, string periodo)
        {
            claseError error = new claseError();
            List<BolEmpleadoBoletaEntidad> listaBoletas = new List<BolEmpleadoBoletaEntidad>();
            string consulta = @"SELECT emp_co_trab, emp_co_empr, emp_anio, emp_periodo, 
                            emp_ruta_pdf, emp_enviado, emp_descargado, emp_fecha_act, emp_fecha_reg, emp_no_trab, 
                            emp_apel_pat, emp_apel_mat, emp_direc_mail, emp_nro_cel, emp_tipo_doc
	                            FROM boletas_gdt.bol_empleado_boleta
                                where emp_co_empr=@p0
	                                and emp_anio=@p1
	                                and emp_periodo=@p2;";
            try
            {
                using (var con = new NpgsqlConnection(_conexion))
                {
                    con.Open();
                    var query = new NpgsqlCommand(consulta, con);
                    query.Parameters.AddWithValue("@p0", emp_co_empr);
                    query.Parameters.AddWithValue("@p1", anio);
                    query.Parameters.AddWithValue("@p2", periodo);

                    using (var dr = query.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                var boleta = new BolEmpleadoBoletaEntidad
                                {
                                    emp_co_trab = ManejoNulos.ManageNullStr(dr["emp_co_trab"]),
                                    emp_co_empr = ManejoNulos.ManageNullStr(dr["emp_co_empr"]),
                                    emp_anio = ManejoNulos.ManageNullStr(dr["emp_anio"]),
                                    emp_periodo = ManejoNulos.ManageNullStr(dr["emp_periodo"]),
                                    emp_ruta_pdf = ManejoNulos.ManageNullStr(dr["emp_ruta_pdf"]),
                                    emp_enviado = ManejoNulos.ManageNullInteger(dr["emp_enviado"]),
                                    emp_descargado = ManejoNulos.ManageNullInteger(dr["emp_descargado"]),
                                    emp_fecha_act = ManejoNulos.ManageNullDate(dr["emp_fecha_act"]),
                                    emp_fecha_reg = ManejoNulos.ManageNullDate(dr["emp_fecha_reg"]),
                                    emp_no_trab = ManejoNulos.ManageNullStr(dr["emp_no_trab"]),
                                    emp_apel_pat = ManejoNulos.ManageNullStr(dr["emp_apel_pat"]),
                                    emp_apel_mat = ManejoNulos.ManageNullStr(dr["emp_apel_mat"]),
                                    emp_direc_mail = ManejoNulos.ManageNullStr(dr["emp_direc_mail"]),
                                    emp_nro_cel = ManejoNulos.ManageNullStr(dr["emp_nro_cel"]),
                                    emp_tipo_doc = ManejoNulos.ManageNullStr(dr["emp_tipo_doc"]),
                                };

                                listaBoletas.Add(boleta);
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
            return (lista: listaBoletas, error: error);
        }
    }
}