using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class CTL_USERSController : Controller
    {
        //
        // GET: /CTL_USERS/
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Result result = BL.CTL_USERS.GetAll();

            ML.CTL_USERS usuario = new ML.CTL_USERS();
            usuario.Usuarios = result.Objects;
            return View(usuario);
        }
        [HttpPost]
        public ActionResult GetAll(ML.CTL_USERS usuario)
        {
            usuario.Name = usuario.Name == null ? "" : usuario.Name;
            usuario.LastName = usuario.LastName == null ? "" : usuario.LastName;
            usuario.SurName = usuario.SurName == null ? "" : usuario.SurName;
            usuario.Email = usuario.Email == null ? "" : usuario.Email;
            ML.Result result = BL.CTL_USERS.GetAllBusqueda(usuario);
            usuario.Usuarios = result.Objects;
            return View(usuario);
        }

        [HttpGet]
        public ActionResult Form(int? Id) //Add & Update
        {
            ML.Result result = new ML.Result();
            ML.CTL_USERS usuario = new ML.CTL_USERS();
            ML.Result resultRol = BL.CTL_ROLES.GetAll();
            usuario.Role = new ML.CTL_ROLES();
            usuario.Role.Roles = resultRol.Objects;


            if (Id == null)
            {
                return View(usuario);
            }
            else
            {
                result = BL.CTL_USERS.GetById(Id.Value);
                if (result.Correct)
                {
                    usuario = (ML.CTL_USERS)result.Object;
                    usuario.Role.Roles = resultRol.Objects;
                    return View(usuario);
                }
                else
                {
                    ViewBag.Message = result.ErrorMessage;
                    return PartialView("Modal");
                }

            }


        }

        [HttpPost]
        public ActionResult Form(ML.CTL_USERS usuario)
        {
            ML.Result result = new ML.Result();
            if (usuario.Id == 0)//Add
            {
                result = BL.CTL_USERS.Add(usuario);
                ViewBag.Message = "El Usuario se agrego correctamente";


            }
            else //Update
            {
                result = BL.CTL_USERS.Update(usuario);
                ViewBag.Message = "El Usuario se Actualizo";
            }

            if (!result.Correct)
            {
                ViewBag.Message = "No se pudo Agregar correctamente el Usuario " + result.ErrorMessage;
            }
            return PartialView("Modal");
        }
        [HttpGet]
        public ActionResult Delete(int Id)
        {
            
            ML.CTL_USERS usuario = new ML.CTL_USERS();
            usuario.Id = Id;
            ML.Result result = BL.CTL_USERS.Delete(usuario);
            return RedirectToAction("GetAll");
        }
        [HttpGet]
        public ActionResult GetAllJquery()
        {
            ML.Result result = BL.CTL_USERS.GetAll();

            ML.CTL_USERS usuario = new ML.CTL_USERS();
            usuario.Usuarios = result.Objects;
            return View(usuario);
        }
        [HttpPost]
        public ActionResult GetAllJquery(ML.CTL_USERS usuario)
        {
            usuario.Name = usuario.Name == null ? "" : usuario.Name;
            usuario.LastName = usuario.LastName == null ? "" : usuario.LastName;
            usuario.SurName = usuario.SurName == null ? "" : usuario.SurName;
            usuario.Email = usuario.Email == null ? "" : usuario.Email;
            ML.Result result = BL.CTL_USERS.GetAllBusqueda(usuario);
            usuario.Usuarios = result.Objects;
            return View(usuario);
        }
        [HttpGet]
        public ActionResult FormPassword(int Id) 
        {
            ML.Result result = new ML.Result();
            ML.CTL_USERS usuario = new ML.CTL_USERS();
            result = BL.CTL_USERS.GetById(Id);
            ML.Result resultRol = BL.CTL_ROLES.GetAll();
            usuario.Role = new ML.CTL_ROLES();
            usuario.Role.Roles = resultRol.Objects;
           
                if (result.Correct)
                {

                    usuario= (ML.CTL_USERS)result.Object;
                    usuario.Password = "";
                    usuario.Role.Roles = resultRol.Objects;
                    return View(usuario);
                }
                else
                {
                    ViewBag.Message = result.ErrorMessage;
                    return PartialView("Modal");
                }

       }


    

        [HttpPost]
        public ActionResult FormPassword(ML.CTL_USERS usuario)
        {
            
                ML.Result result = BL.CTL_USERS.Update(usuario);
                ViewBag.Message = "EL Password se Actualizo";
            

            if (!result.Correct)
            {
                ViewBag.Message = "No se pudo Agregar correctamente el Usuario " + result.ErrorMessage;
            }
            return PartialView("Modal");
        }
	}
}