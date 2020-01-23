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
    public class IntranetFooterModel
    {
        string _conexion;
        public IntranetFooterModel()
        {
            _conexion = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
        }
        public(int idIntranetFooterInsertado, claseError error) IntranetFooterInsertarJson(IntranetFooterEntidad intranetFooter)
        {
            //bool response = false;
            int idIntranetFooterInsertado = 0;
            string consulta = @"INSERT INTO intranet.int_footer(
	                        foot_descripcion, foot_estado, foot_imagen)
	                        VALUES (@p0, @p1, @p2,@p3)
                                foot_id ;";
            claseError error = new claseError();
            try
            {
                using (var con = new NpgsqlConnection(_conexion))
                {
                    con.Open();
                    var query = new NpgsqlCommand(consulta, con);
                    query.Parameters.AddWithValue("@p0", ManejoNulos.ManageNullStr(intranetFooter.foot_descripcion));
                    query.Parameters.AddWithValue("@p1", ManejoNulos.ManageNullStr(intranetFooter.foot_estado));
                    query.Parameters.AddWithValue("@p2", ManejoNulos.ManageNullDate(intranetFooter.foot_imagen));
                    query.Parameters.AddWithValue("@p3", ManejoNulos.ManageNullDate(intranetFooter.foot_posicion));
                    idIntranetFooterInsertado = Int32.Parse(query.ExecuteScalar().ToString());
                    //query.ExecuteNonQuery();
                    //response = true;
                }
            }
            catch (Exception ex)
            {
                error.Key = ex.Data.Count.ToString();
                error.Value = ex.Message;
            }
            return (idIntranetFooterInsertado: idIntranetFooterInsertado, error: error);
        }
        public (bool intranetFooterEliminado, claseError error) IntranetFooterEliminarJson(int foot_id)
        {
            bool response = false;
            string consulta = @"DELETE FROM intranet.int_footer
	                                WHERE foot_id=@p0;";
            claseError error = new claseError();
            try
            {
                using (var con = new NpgsqlConnection(_conexion))
                {
                    con.Open();

                    var query = new NpgsqlCommand(consulta, con);
                    query.Parameters.AddWithValue("@p0", ManejoNulos.ManageNullInteger(foot_id));
                    query.ExecuteNonQuery();
                    response = true;
                }
            }
            catch (Exception ex)
            {
                error.Key = ex.Data.Count.ToString();
                error.Value = ex.Message;
            }

            return (intranetFooterEliminado: response, error: error);
        }
    }
}