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
        private ICliente clienteDAO;
        private IPedido pedidoDAO;

        public HomeController()
        {
            productoDAO = new ProductoDAO();
            categoriaDAO = new CategoriaDAO();
            clienteDAO = new ClienteDAO();
            pedidoDAO = new PedidoDAO();
        }
        public ActionResult Index()
        {
            // obtener listas una sola vez
            var clientes = clienteDAO.ListarTodo();
            var pedidos = pedidoDAO.ListarTodo();

            // cards dashboard
            ViewBag.TotalClientes = clientes.Count();
            ViewBag.TotalPedidos = pedidos.Count();

            int mesActual = DateTime.Now.Month;
            int anioActual = DateTime.Now.Year;

            ViewBag.VentasMes = pedidos
                .Where(p => p.FechaPedido.Month == mesActual
                         && p.FechaPedido.Year == anioActual)
                .Sum(p => (decimal?)p.Total) ?? 0;
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