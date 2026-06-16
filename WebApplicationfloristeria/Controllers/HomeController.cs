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
    public class HomeController : Controller
    {
        private const string RutaImgProductos = "~/Contenido/Multimedia/Productos";
        private IProducto productoDAO;
        private ICategoria categoriaDAO;
        public HomeController()
        {
            productoDAO = new ProductoDAO();
            categoriaDAO = new CategoriaDAO();
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Princip()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Floristería()
        {
            ViewBag.Message = "mi pagina web.";

            return View();
        }
        // GET: FloristeríaClient
        public ActionResult FloristeríaClient()
        {

            List<Producto> products = productoDAO.ListarTodo();
            ViewBag.Categorias = categoriaDAO.ListarTodo();
            if (products == null)
            {
                throw new Exception("La lista de productos es NULL");
            }
            return View(products);
        }
    }
}