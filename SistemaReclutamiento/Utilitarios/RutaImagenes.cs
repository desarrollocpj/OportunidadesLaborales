﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;


namespace SistemaReclutamiento.Utilitarios
{
    public class RutaImagenes
    {
        public void imagenPostulante_CV(string ruta,string name)
        {
            string postulante_image = @""+ruta+"/"+name;
            if (name != null && name != ""&& ruta!=null && ruta!="")
            {
                if (System.IO.File.Exists(postulante_image))
                {
                    byte[] imagebytes = System.IO.File.ReadAllBytes(postulante_image);
                    string base64String = Convert.ToBase64String(imagebytes);
                    HttpContext.Current.Session["rutaPerfil"] = base64String;
                }
                else {
                    postulante_image = HttpContext.Current.Server.MapPath("~/images/user.png");
                    byte[] imagebytes = System.IO.File.ReadAllBytes(postulante_image);
                    string base64String = Convert.ToBase64String(imagebytes);
                    HttpContext.Current.Session["rutaPerfil"] = base64String;
                }
            }
            else
            {
                postulante_image = HttpContext.Current.Server.MapPath("~/images/user.png");
                byte[] imagebytes = System.IO.File.ReadAllBytes(postulante_image);
                string base64String = Convert.ToBase64String(imagebytes);
                HttpContext.Current.Session["rutaPerfil"] = base64String;
            }
        }

        public void Postulante_CV(string ruta,string name)
        {
            string postulante_cv = @"" + ruta + "/" + name;
            if (postulante_cv != null)
            {
                if (System.IO.File.Exists(postulante_cv))
                {
                    byte[] imagebytes = System.IO.File.ReadAllBytes(postulante_cv);
                    string base64String = Convert.ToBase64String(imagebytes);
                    HttpContext.Current.Session["rutaCv"] = base64String;
                }
            }
        }
    }
}