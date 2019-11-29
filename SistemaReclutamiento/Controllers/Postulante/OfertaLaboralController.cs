﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SistemaReclutamiento.Models;
using SistemaReclutamiento.Entidades;
using SistemaReclutamiento.Utilitarios;

namespace SistemaReclutamiento.Controllers
{

    public class OfertaLaboralController : Controller
    {
        OfertaLaboralModel ofertaLaboralbl = new OfertaLaboralModel();
        DetPreguntaOLAModel detpreguntabl = new DetPreguntaOLAModel();
        DetRespuestaOLAModel detrespuestabl = new DetRespuestaOLAModel();
        // GET: OfertaLaboral
        public ActionResult OfertaLaboralListarVista()
        {
            return View();
        }
        public ActionResult OfertaLaboralListarMisPostulacionesVista()
        {
            return View();
        }

        [HttpPost]
        public ActionResult OfertaLaboralIndexListarJson()
        {
            claseError error = new claseError();
            // string ola_cod_cargo = Convert.ToString(Request.Form["ola_cod_cargo"]); 
            PostulanteEntidad postulante = (PostulanteEntidad)Session["postulante"];
            bool respuestaConsulta = false;
            string errormensaje = "";
            var lista = new List<OfertaLaboralEntidad>();
            try
            {
                var tuplalista = ofertaLaboralbl.OfertaLaboralListarVistaIndexJson(postulante.pos_id);
                lista = tuplalista.lista;
                error = tuplalista.error;
                if (!error.Key.Equals(string.Empty)) {
                    return Json(new { mensaje = error.Value, respuesta = false });
                }
                errormensaje = "Listando Ofertas";
                respuestaConsulta = true;
            }
            catch (Exception exp)
            {
                errormensaje = exp.Message + ",Llame Administrador";
            }
            return Json(new { data = lista.ToList(), mensaje = errormensaje, respuesta = respuestaConsulta });
        }

        [HttpPost]
        public ActionResult OfertaLaboralListarJson(ReporteOfertaLaboral reporte)
        {
            claseError error = new claseError();
            // string ola_cod_cargo = Convert.ToString(Request.Form["ola_cod_cargo"]); 
            PostulanteEntidad postulante = (PostulanteEntidad)Session["postulante"];
            UbigeoModel ubigeobl = new UbigeoModel();
            UbigeoEntidad ubigeo = new UbigeoEntidad();
            DateTime fecha_fin = DateTime.Now;
            DateTime fecha_ayuda;
            bool respuestaConsulta = false;
            string errormensaje = "";
            reporte.busqueda = string.Empty;
            var lista = new List<OfertaLaboralEntidad>();
            if (reporte.ubi_pais_id != string.Empty && reporte.ubi_pais_id != null)
            {
                if (reporte.ubi_departamento_id != string.Empty && reporte.ubi_departamento_id != null)
                {
                    if (reporte.ubi_provincia_id != string.Empty && reporte.ubi_provincia_id != null)
                    {
                        if (reporte.ubi_distrito_id != string.Empty && reporte.ubi_distrito_id != null)
                        {
                            reporte.busqueda = "DISTRITO";
                        }
                        else
                        {
                            reporte.busqueda = "PROVINCIA";
                        }
                    }
                    else
                    {
                        reporte.busqueda = "DEPARTAMENTO";
                    }
                }
                else
                {
                    reporte.busqueda = "PAIS";
                }
            }
            if (reporte.ola_rango_fecha == "hoy")
            {
                reporte.ola_fecha_ini = DateTime.Parse(fecha_fin.ToShortDateString());
                //reporte.ola_fecha_ini = fecha_inicio;
            }
            if (reporte.ola_rango_fecha == "semana")
            {
                fecha_ayuda = fecha_fin.AddDays(-7);
                reporte.ola_fecha_ini = DateTime.Parse(fecha_ayuda.ToShortDateString());
            }
            if (reporte.ola_rango_fecha == "mes")
            {
                int dias = DateTime.DaysInMonth(fecha_fin.Year, fecha_fin.Month);
                fecha_ayuda = fecha_fin.AddDays(-dias);
                reporte.ola_fecha_ini = DateTime.Parse(fecha_ayuda.ToShortDateString());
            }
            reporte.pos_id = postulante.pos_id;
            try
            {
                var tuplalista = ofertaLaboralbl.OfertaLaboralListarJson(reporte);
                lista = tuplalista.lista;
                error = tuplalista.error;
                if (!error.Key.Equals(string.Empty))
                {
                    return Json(new { mensaje = error.Value, respuesta = false });
                }
                errormensaje = "Listando Ofertas";
                respuestaConsulta = true;
            }
            catch (Exception exp)
            {
                errormensaje = exp.Message + ",Llame Administrador";
            }
            return Json(new { data = lista.ToList(), mensaje = errormensaje, respuesta=respuestaConsulta });
        }

        [HttpPost]
        public ActionResult OfertaLaboralListarMisPostulacionesJson(ReporteOfertaLaboral reporte)
        {
            claseError error = new claseError();
            PostulanteEntidad postulante = (PostulanteEntidad)Session["postulante"];
            UbigeoModel ubigeobl = new UbigeoModel();
            UbigeoEntidad ubigeo = new UbigeoEntidad();
            DateTime fecha_fin = DateTime.Now;
            DateTime fecha_ayuda;
            bool respuestaConsulta = false;
            string errormensaje = "";
            reporte.busqueda = string.Empty;
            var lista = new List<OfertaLaboralEntidad>();
            if (reporte.ubi_pais_id != string.Empty && reporte.ubi_pais_id != null)
            {
                if (reporte.ubi_departamento_id != string.Empty && reporte.ubi_departamento_id != null)
                {
                    if (reporte.ubi_provincia_id != string.Empty && reporte.ubi_provincia_id != null)
                    {
                        if (reporte.ubi_distrito_id != string.Empty && reporte.ubi_distrito_id != null)
                        {
                            reporte.busqueda = "DISTRITO";
                        }
                        else
                        {
                            reporte.busqueda = "PROVINCIA";
                        }
                    }
                    else
                    {
                        reporte.busqueda = "DEPARTAMENTO";
                    }
                }
                else
                {
                    reporte.busqueda = "PAIS";
                }
            }
            if (reporte.ola_rango_fecha == "hoy")
            {
                reporte.ola_fecha_ini = DateTime.Parse(fecha_fin.ToShortDateString());
                //reporte.ola_fecha_ini = fecha_inicio;
            }
            if (reporte.ola_rango_fecha == "semana")
            {
                fecha_ayuda = fecha_fin.AddDays(-7);
                reporte.ola_fecha_ini = DateTime.Parse(fecha_ayuda.ToShortDateString());
            }
            if (reporte.ola_rango_fecha == "mes")
            {
                int dias = DateTime.DaysInMonth(fecha_fin.Year, fecha_fin.Month);
                fecha_ayuda = fecha_fin.AddDays(-dias);
                reporte.ola_fecha_ini = DateTime.Parse(fecha_ayuda.ToShortDateString());
            }
            reporte.pos_id = postulante.pos_id;
            try
            {
                var tuplalista = ofertaLaboralbl.PostulanteListarMisOfertasPostuladasJson(reporte);
                lista = tuplalista.lista;
                error = tuplalista.error;
                if (!error.Key.Equals(string.Empty))
                {
                    return Json(new { mensaje = error.Value, respuesta = false });
                }
                errormensaje = "Listando Postulaciones";
                respuestaConsulta = true;
            }
            catch (Exception exp)
            {
                errormensaje = exp.Message + ",Llame Administrador";
            }
            return Json(new { data = lista.ToList(), mensaje = errormensaje, respuesta = respuestaConsulta });

            //var postulante = (PostulanteEntidad)Session["postulante"];           
            //bool respuestaConsulta = false;
            //string errormensaje = "";
            //var lista = new List<OfertaLaboralEntidad>();
            //try
            //{
            //    lista = ofertaLaboralbl.PostulanteListarPostulacionesJson(postulante.pos_id);
            //    errormensaje = "Listando Mis Postulaciones";
            //    respuestaConsulta = true;
            //}
            //catch (Exception exp)
            //{
            //    errormensaje = exp.Message + ",Llame Administrador";
            //}
            //return Json(new { data = lista.ToList(), mensaje = errormensaje, respuesta = respuestaConsulta });
        }
        [HttpPost]
        public ActionResult OfertaLaboralIdObtenerJson(int ola_id)
        {
            claseError error = new claseError();
            var errormensaje = "";
            var ofertaLaboral = new OfertaLaboralEntidad();
            bool response = false;
            try
            {
                var tuplaofertaLaboral = ofertaLaboralbl.OfertaLaboralIdObtenerJson(ola_id);
                ofertaLaboral = tuplaofertaLaboral.ofertalaboral;
                error = tuplaofertaLaboral.error;
                if (!error.Key.Equals(string.Empty))
                {
                    return Json(new { mensaje = error.Value, respuesta = false });
                }

                response = true;
            }
            catch (Exception exp)
            {
                errormensaje = exp.Message + ",Llame Administrador";
            }
            return Json(new { data = ofertaLaboral, mensaje = errormensaje, respuesta = response });
        }
        public ActionResult DetPreguntaOLAListarJson(int ola_id)
        {
            claseError error = new claseError();
            var errormensaje = "";
            var response=false;
            var detallepregunta = new List<DetPreguntaOLAEntidad>();
            var detallerespuesta = new List<DetRespuestaOLAEntidad>();
            var ofertaLaboral = new OfertaLaboralEntidad();
            try
            {
                var tuplaofertaLaboral = ofertaLaboralbl.OfertaLaboralIdObtenerJson(ola_id);
                ofertaLaboral = tuplaofertaLaboral.ofertalaboral;
                error = tuplaofertaLaboral.error;
                if (!error.Key.Equals(string.Empty))
                {
                    return Json(new { mensaje = error.Value, respuesta = false });
                }
                detallepregunta = detpreguntabl.DetPreguntaListarporPreguntaJson(ola_id);
                if (detallepregunta.Count > 0) {
                    foreach (var m in detallepregunta) {
                        detallerespuesta = detrespuestabl.DetRespuestaListarporPreguntaJson(m.dop_id);
                        m.DetalleRespuesta = detallerespuesta;
                    }
                }
                response = true;
            }
            catch (Exception exp)
            {
                errormensaje = exp.Message + ",Llame Administrador";
            }
            return Json(new { data = detallepregunta,oferta=ofertaLaboral, mensaje = errormensaje, respuesta = response });
        }
    }
}