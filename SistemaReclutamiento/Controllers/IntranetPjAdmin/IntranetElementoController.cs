﻿using SistemaReclutamiento.Entidades.IntranetPJ;
using SistemaReclutamiento.Models;
using SistemaReclutamiento.Models.IntranetPJ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaReclutamiento.Controllers.IntranetPjAdmin
{
    public class IntranetElementoController : Controller
    {
        // GET: IntranetElemento
        IntranetElementoModel intranetElementobl = new IntranetElementoModel();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult IntranetElementoListarxMenuIDJson(int sec_id)
        {
            string mensaje = "";
            string mensajeConsola = "";
            bool respuesta = false;
            List<IntranetElementoEntidad> listaElementos = new List<IntranetElementoEntidad>();
            claseError error = new claseError();
            try
            {
                var ElementoTupla = intranetElementobl.IntranetElementoListarxSeccionIDJson(sec_id);
                error = ElementoTupla.error;
                listaElementos = ElementoTupla.intranetElementoListaxSeccionID;
                if (error.Key.Equals(string.Empty))
                {
                    mensaje = "Listando Elementoes";
                    respuesta = true;
                }
                else
                {
                    mensajeConsola = error.Value;
                    mensaje = "No se Pudieron Listar las Elementoes";
                }

            }
            catch (Exception exp)
            {
                mensaje = exp.Message + ",Llame Administrador";
            }
            return Json(new { data = listaElementos.ToList(), respuesta = respuesta, mensaje = mensaje, mensajeconsola = mensajeConsola });
        }
        [HttpPost]
        public ActionResult IntranetElementoInsertarJson(IntranetElementoEntidad intranetElemento)
        {
            string mensaje = "";
            string mensajeConsola = "";
            bool respuesta = false;
            int idIntranetElementoInsertado = 0;
            claseError error = new claseError();
            try
            {
                var totalElementosTupla = intranetElementobl.IntranetElementoObtenerTotalRegistrosxSeccionJson(intranetElemento.fk_seccion);
                error = totalElementosTupla.error;
                if (error.Key.Equals(string.Empty))
                {
                    intranetElemento.elem_descripcion = intranetElemento.elem_titulo;
                    intranetElemento.elem_contenido = intranetElemento.elem_titulo;
                    intranetElemento.elem_orden = totalElementosTupla.intranetElementosTotal + 1;
                    var seccionTupla = intranetElementobl.IntranetElementoInsertarJson(intranetElemento);
                    error = seccionTupla.error;

                    if (error.Key.Equals(string.Empty))
                    {
                        mensaje = "Se Registró Correctamente";
                        respuesta = true;
                        idIntranetElementoInsertado = seccionTupla.idIntranetElementoInsertado;
                    }
                    else
                    {
                        mensaje = "No se Pudo insertar el Elemento";
                        mensajeConsola = error.Value;
                    }
                }
                else
                {
                    mensaje = "Error al Insertar el Nuevo Elemento";
                    mensajeConsola = error.Value;
                }


            }
            catch (Exception exp)
            {
                mensaje = exp.Message + " ,Llame Administrador";
            }

            return Json(new { respuesta = respuesta, mensaje = mensaje, idIntranetElementoInsertado = idIntranetElementoInsertado, mensajeconsola = mensajeConsola });
        }
    }
}