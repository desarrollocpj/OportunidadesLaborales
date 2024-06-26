﻿using SistemaReclutamiento.Entidades;
using SistemaReclutamiento.Entidades.Postulante;
using SistemaReclutamiento.Models;
using SistemaReclutamiento.Models.Postulante;
using SistemaReclutamiento.Utilitarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaReclutamiento.Controllers.Postulante
{
    [autorizacion(false)]
    public class PostulanteFavoritosController : Controller
    {
        // GET: PostulanteFavoritos
        PostulanteFavoritosModel postulanteFavoritosbl = new PostulanteFavoritosModel();
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult PostulanteFavoritosInsertaJson(int ola_id) {
            PostulanteEntidad postulante = (PostulanteEntidad)Session["postulante"];
            PostulanteFavoritosEntidad postulanteFavoritos = new PostulanteFavoritosEntidad();
            postulanteFavoritos.fk_oferta_laboral = ola_id;
            postulanteFavoritos.fk_postulante = postulante.pos_id;
            postulanteFavoritos.posfav_estado = "A";
            postulanteFavoritos.posfav_notificar = true;
            bool idPostulanteFavoritoInsertado = false;
            string errormensaje = "";
            bool response = false; 
            claseError error = new claseError();
            try {
                var favoritosTupla = postulanteFavoritosbl.IntranetPostulanteFavoritosInsertarJson(postulanteFavoritos);
                error = favoritosTupla.error;
                if (error.Respuesta)
                {
                    idPostulanteFavoritoInsertado = favoritosTupla.idIntranetPostulanteFavoritosInsertado;
                    if (idPostulanteFavoritoInsertado ==true)
                    {
                        errormensaje = "Agregado a Favoritos";
                        response = true;
                    }
                    else
                    {
                        errormensaje = "No se pudo Agregar a Favoritos";
                    }
                }
                else {
                    errormensaje = error.Mensaje;
                }
            }
            catch (Exception ex) {
                errormensaje = ex.Message;
            }
            return Json(new { respuesta = response, mensaje = errormensaje, data = idPostulanteFavoritoInsertado });
        }
        [HttpPost]
        public ActionResult PostulanteFavoritosEliminarJson(int ola_id) {
            PostulanteEntidad postulante = (PostulanteEntidad)Session["postulante"];
            int fk_oferta_laboral = ola_id;
            int fk_postulante = postulante.pos_id;
            bool postulanteFavoritoEliminado = false;
            string errormensaje = "";
            bool response = false;
            claseError error = new claseError();
            try
            {
                var postulanteFavoritosTupla = postulanteFavoritosbl.IntranetPostulanteFavoritosEliminarJson(fk_postulante, fk_oferta_laboral);
                error = postulanteFavoritosTupla.error;
                if (error.Respuesta)
                {
                    response = true;
                    errormensaje = "Quitado de Favoritos";
                    postulanteFavoritoEliminado = postulanteFavoritosTupla.idIntranetPostulanteFavoritosEliminado;
                }
                else {
                    errormensaje = "No se puede quitar de favoritos";
                }
            }
            catch (Exception ex) {
                errormensaje = ex.Message;
            }

            return Json(new { respuesta = response, mensaje = errormensaje, data = postulanteFavoritoEliminado });
        }
    }
}