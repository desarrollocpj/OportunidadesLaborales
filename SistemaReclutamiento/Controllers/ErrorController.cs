﻿using SistemaReclutamiento.Utilitarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaReclutamiento.Controllers
{
    [autorizacion(false)]
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult NotFound()
        {
            Response.StatusCode = 404;
            return View("NotFound");
        }
        public ActionResult NoAutorizadoIntranetSGC()
        {
            Response.StatusCode = 401;
            return View("NoAutorizadoIntranetSGC");
        }
    }
}