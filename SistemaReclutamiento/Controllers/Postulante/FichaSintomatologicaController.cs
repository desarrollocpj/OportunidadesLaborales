﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaReclutamiento.Controllers.Postulante
{
    public class FichaSintomatologicaController : Controller
    {
        // GET: FichaSintomatologica
        public ActionResult FormularioFichaVista()
        {
            return View();
        }
    }
}