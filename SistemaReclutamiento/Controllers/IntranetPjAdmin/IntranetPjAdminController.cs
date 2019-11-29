﻿using SistemaReclutamiento.Entidades.IntranetPJ;
using SistemaReclutamiento.Models;
using SistemaReclutamiento.Models.IntranetPJ;
using SistemaReclutamiento.Utilitarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaReclutamiento.Controllers.IntranetPJAdmin
{
    public class IntranetPjAdminController : Controller
    {
        IntranetUsuarioModel usuariobl = new IntranetUsuarioModel();
        IntranetMenuModel intranetMenubl = new IntranetMenuModel();
        // GET: IntranetPjAdmin
        public ActionResult Index()
        {
            return View("~/Views/IntranetPJAdmin/IntranetPJAdminIndex.cshtml");
        }

        public ActionResult PanelMenus()
        {
            return View("~/Views/IntranetPJAdmin/IntranetPJMenus.cshtml");
        }
        public ActionResult PanelActividades()
        {
            return View("~/Views/IntranetPJAdmin/IntranetPJActividades.cshtml");
        }
        public ActionResult PanelComentarios()
        {
            return View("~/Views/IntranetPJAdmin/IntranetPJComentarios.cshtml");
        }
        public ActionResult PanelSecciones(int menu_id=1)
        {
            List<IntranetMenuEntidad> intranetMenu = new List<IntranetMenuEntidad>();
            claseError error = new claseError();
            string mensajeerrorBD = "";
            string mensaje = "";
            try
            {
                var menuTupla = intranetMenubl.IntranetMenuListarJson();
                error = menuTupla.error;
                if (error.Key.Equals(string.Empty))
                {
                    intranetMenu = menuTupla.intranetMenuLista;
                    ViewBag.Menu = intranetMenu;
                }
                else
                {
                    mensajeerrorBD += "Error en Menus: " + error.Value + "\n";
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }
            
            return View("~/Views/IntranetPJAdmin/IntranetPJSecciones.cshtml");
        }
        #region Region Acceso a Mantenimiento Intranet PJ
        [HttpPost]
        public ActionResult IntranetPJValidarLoginJson(string usu_login, string usu_password)
        {
            bool respuesta = false;
            string errormensaje = "";
            IntranetUsuarioEntidad usuario = new IntranetUsuarioEntidad();
            claseError error = new claseError();
            try
            {
                var usuarioTupla = usuariobl.IntranetUsuarioValidarCredenciales(usu_login.ToLower());
                error = usuarioTupla.error;
                if (error.Key.Equals(string.Empty))
                {
                    usuario = usuarioTupla.usuario;
                    if (usuario.usu_id > 0)
                    {
                        if (usuario.usu_password.Equals(Seguridad.EncriptarSHA512(usu_password.Trim())))
                        {
                            if (usuario.usu_estado.Equals('A'))
                            {
                                IntranetUsuarioEntidad usuarioSession = new IntranetUsuarioEntidad();
                                usuarioSession.usu_id = usuario.usu_id;
                                usuarioSession.usu_nombre = usuario.usu_nombre;
                                usuarioSession.usu_tipo = usuario.usu_tipo;
                                usuarioSession.usu_estado = usuario.usu_estado;
                                Session["usu_full"] = usuarioSession;
                                respuesta = true;
                                errormensaje = "Bienvenido, " + usuario.usu_nombre;
                            }
                            else
                            {
                                errormensaje = "Su cuenta se encuentra desactivada";
                            }
                        }
                        else
                        {
                            errormensaje = "La contraseña ingresada con coincide con con el usuario ingresado";
                        }
                    }
                    else
                    {
                        errormensaje = "No se encuentra el usuario ingresado";
                    }
                }
                else
                {
                    respuesta = false;
                    errormensaje = error.Value;
                }
            }
            catch (Exception exp)
            {
                errormensaje = exp.Message + "";
            }

            return Json(new { respuesta = respuesta, mensaje = errormensaje, /*, usuario=usuario*/ });
        }
    #endregion
    }
}