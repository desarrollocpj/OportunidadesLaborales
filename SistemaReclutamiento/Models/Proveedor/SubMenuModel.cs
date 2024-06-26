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
    public class SubMenuModel
    {
        string _conexion;
        public SubMenuModel()
        {
            _conexion = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
        }
        public List<SubMenuEntidad> SubMenuListarJson()
        {
            List<SubMenuEntidad> lista = new List<SubMenuEntidad>();
                string consulta = @"SELECT submenu.snu_descripcion, 
                                            submenu.snu_url, 
                                            submenu.snu_orden, 
                                            submenu.snu_icono, 
                                            submenu.snu_estado, 
                                            submenu.fk_menu, 
                                            submenu.snu_id,                                 
                                            submenu.snu_descripcion_eng, 
                                            submenu.snu_template,
                                            menu.men_descripcion,
                                            modulo.mod_descripcion
	                                FROM seguridad.seg_submenu as submenu 
	                                inner join seguridad.seg_menu as menu
	                                on menu.men_id=submenu.fk_menu
	                                inner join seguridad.seg_modulo as modulo
	                                on menu.fk_modulo=modulo.mod_id
	                                where modulo.mod_tipo='Proveedor'
	                                ;";
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
                                var menu = new SubMenuEntidad
                                {

                                    snu_descripcion = ManejoNulos.ManageNullStr(dr["snu_descripcion"]),
                                    snu_url = ManejoNulos.ManageNullStr(dr["snu_url"]),
                                    snu_orden = ManejoNulos.ManageNullInteger(dr["snu_orden"]),
                                    snu_icono = ManejoNulos.ManageNullStr(dr["snu_icono"]),
                                    snu_estado = ManejoNulos.ManageNullStr(dr["snu_estado"]),
                                    fk_menu = ManejoNulos.ManageNullInteger(dr["fk_menu"]),
                                    snu_id = ManejoNulos.ManageNullInteger(dr["snu_id"]),
                                    snu_descripcion_eng = ManejoNulos.ManageNullStr(dr["snu_descripcion_eng"]),
                                    snu_template = ManejoNulos.ManageNullStr(dr["snu_template"]),
                                    men_descripcion=ManejoNulos.ManageNullStr(dr["men_descripcion"]),
                                    mod_descripcion=ManejoNulos.ManageNullStr(dr["mod_descripcion"])
                                };
                                lista.Add(menu);
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
        public List<SubMenuEntidad> SubMenuListarPorMenuJson(int fk_menu,int fk_usuario)
        {
            List<SubMenuEntidad> lista = new List<SubMenuEntidad>();
            string consulta = @"SELECT  submenu.snu_descripcion, 
		                    submenu.snu_url, 
		                    submenu.snu_orden,
		                    submenu.snu_icono, 
		                    submenu.snu_estado,
		                    submenu.fk_menu, 
		                    submenu.snu_id,
		                    submenu.snu_descripcion_eng,
		                    submenu.snu_template
		                    FROM seguridad.seg_submenu as submenu
		                    join seguridad.seg_permiso as permiso
		                    on submenu.snu_id=permiso.fk_submenu
		                    where permiso.fk_usuario=@p0
                               and submenu.fk_menu=@p1;";
            try
            {
                using (var con = new NpgsqlConnection(_conexion))
                {
                    con.Open();
                    var query = new NpgsqlCommand(consulta, con);
                    query.Parameters.AddWithValue("@p0", fk_usuario);
                    query.Parameters.AddWithValue("@p1", fk_menu);
                    using (var dr = query.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                var menu = new SubMenuEntidad
                                {

                                    snu_descripcion= ManejoNulos.ManageNullStr(dr["snu_descripcion"]),
                                    snu_url = ManejoNulos.ManageNullStr(dr["snu_url"]),
                                    snu_orden = ManejoNulos.ManageNullInteger(dr["snu_orden"]),
                                    snu_icono = ManejoNulos.ManageNullStr(dr["snu_icono"]),
                                    snu_estado = ManejoNulos.ManageNullStr(dr["snu_estado"]),
                                    fk_menu = ManejoNulos.ManageNullInteger(dr["fk_menu"]),
                                    snu_id = ManejoNulos.ManageNullInteger(dr["snu_id"]),
                                    snu_descripcion_eng = ManejoNulos.ManageNullStr(dr["snu_descripcion_eng"]),
                                    snu_template = ManejoNulos.ManageNullStr(dr["snu_template"]),
                                };

                                lista.Add(menu);
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
        public bool SubMenuInsertarJson(SubMenuEntidad submenu)
        {
            bool response = false;
            string consulta = @"INSERT INTO seguridad.seg_submenu(
	                        snu_descripcion, snu_url, snu_icono, snu_estado, fk_menu,snu_id)
	                        VALUES (@p0,@p1,@p2,@p3,@p4,@p5); ";
            try
            {
                using (var con = new NpgsqlConnection(_conexion))
                {
                    con.Open();
                    var query = new NpgsqlCommand(consulta, con);
                    query.Parameters.AddWithValue("@p0", submenu.snu_descripcion);
                    query.Parameters.AddWithValue("@p1", submenu.snu_url);
                    query.Parameters.AddWithValue("@p2", submenu.snu_icono);
                    query.Parameters.AddWithValue("@p3", submenu.snu_estado);
                    query.Parameters.AddWithValue("@p4", submenu.fk_menu);
                    query.Parameters.AddWithValue("@p5", submenu.snu_id);
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
        public int SubMenuObtenerUltimo()
        {
            int snu_id = 0;
            string consulta = @"select snu_id
                                  from seguridad.seg_submenu
                                     order by snu_id desc
                                     limit 1;";
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
                                snu_id = ManejoNulos.ManageNullInteger(dr["snu_id"]);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine("" + ex.Message + this.GetType().FullName + " " + DateTime.Now.ToLongDateString());
            }
            return snu_id;
        }
        public List<SubMenuEntidad> SubMenuListarPorMenu(int fk_menu)
        {
            List<SubMenuEntidad> lista = new List<SubMenuEntidad>();
            string consulta = @"select submenu.snu_descripcion, 
				                    submenu.snu_url, 
				                    submenu.snu_orden,
				                    submenu.snu_icono, 
				                    submenu.snu_estado,
				                    submenu.fk_menu, 
				                    submenu.snu_id,
				                    submenu.snu_descripcion_eng,
				                    submenu.snu_template
				                    FROM seguridad.seg_submenu as submenu
				                    join seguridad.seg_menu as menu
				                    on submenu.fk_menu=menu.men_id
				                    where submenu.fk_menu=@p0;";
            try
            {
                using (var con = new NpgsqlConnection(_conexion))
                {
                    con.Open();
                    var query = new NpgsqlCommand(consulta, con);
                    query.Parameters.AddWithValue("@p0", fk_menu);
                    using (var dr = query.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                var menu = new SubMenuEntidad
                                {

                                    snu_descripcion = ManejoNulos.ManageNullStr(dr["snu_descripcion"]),
                                    snu_url = ManejoNulos.ManageNullStr(dr["snu_url"]),
                                    snu_orden = ManejoNulos.ManageNullInteger(dr["snu_orden"]),
                                    snu_icono = ManejoNulos.ManageNullStr(dr["snu_icono"]),
                                    snu_estado = ManejoNulos.ManageNullStr(dr["snu_estado"]),
                                    fk_menu = ManejoNulos.ManageNullInteger(dr["fk_menu"]),
                                    snu_id = ManejoNulos.ManageNullInteger(dr["snu_id"]),
                                    snu_descripcion_eng = ManejoNulos.ManageNullStr(dr["snu_descripcion_eng"]),
                                    snu_template = ManejoNulos.ManageNullStr(dr["snu_template"]),
                                };

                                lista.Add(menu);
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
    }
}