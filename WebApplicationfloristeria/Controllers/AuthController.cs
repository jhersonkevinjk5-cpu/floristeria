using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplicationfloristeria.Data;
using WebApplicationfloristeria.Data.Interfaces;
using WebApplicationfloristeria.Models;

namespace WebApplicationfloristeria.Controllers
{
    public class AuthController : Controller
    {
        private IUsuario usuarioDAO;
        private ICliente clienteDAO;
        public AuthController() 
        {
            usuarioDAO = new UsuarioDAO();
            clienteDAO = new ClienteDAO();
        }
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }
        // POST: Login
        [HttpPost]
        public ActionResult Login(FormCollection form)
        {
            string correo = form["usuarioNombre"];
            string clave = form["contraseña"];


            Usuario usuario = usuarioDAO.Validar(correo, clave);

            if (usuario != null)
            {
                Session["usuario"] = usuario;

                return RedirectToAction("Index", "Producto");
            }

            Cliente cliente = clienteDAO.Validar(correo, clave);

            if (cliente != null)
            {
                Session["cliente"] = cliente;

                return RedirectToAction("FloristeríaClient", "Home");
            }

            ViewBag.Mensaje = "Usuario o contraseña incorrectos";

            return View();
        }
        // GET: Logout
        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Login");
        }
    }
}