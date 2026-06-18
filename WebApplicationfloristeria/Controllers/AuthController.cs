using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
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
            string tipoLogin = form["tipoLogin"];
            // =========================
            // LOGIN USUARIO
            // =========================
            if (tipoLogin == "usuario")
            {
                string correo = form["correoUsuario"];
                string clave = form["clave"];

                if (!string.IsNullOrEmpty(correo) && !string.IsNullOrEmpty(clave))
                {
                    Usuario usuario = usuarioDAO.Validar(correo, clave);

                    if (usuario != null)
                    {
                        Session["usuario"] = usuario;
                        return RedirectToAction("Index", "Home");
                    }
                }
            }

            // =========================
            // LOGIN CLIENTE
            // =========================
            else if (tipoLogin == "cliente")
            {
                string nombre = form["nombreCliente"];
                string correo = form["correoCliente"];

                if (!string.IsNullOrEmpty(nombre) && !string.IsNullOrEmpty(correo))
                {
                    Cliente cliente = clienteDAO.Validar(correo, nombre);

                    if (cliente != null)
                    {
                        Session["cliente"] = cliente;
                        return RedirectToAction("FloristeríaClient", "Home");
                    }
                }
            }

            // =========================
            // ERROR GENERAL
            // =========================
            ViewBag.Mensaje = "Datos incorrectos";
            System.Diagnostics.Debug.WriteLine("TIPO: " + tipoLogin);
            System.Diagnostics.Debug.WriteLine("NOMBRE: " + form["nombreCliente"]);
            System.Diagnostics.Debug.WriteLine("CORREO: " + form["correoCliente"]);
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