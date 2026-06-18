using Rotativa;
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

        // =========================
        // Index Principal Del USU
        // =========================
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



        // =========================
        // Exportar PDF
        // =========================
        public ActionResult ExportarPDF()
        {
            return new ActionAsPdf("ReporteVentas")
            {
                FileName = "ReporteVentas.pdf"
            };
        }

        public ActionResult ReporteVentas()
        {
            var pedidos = pedidoDAO.ListarTodo();
            return View(pedidos);
        }

        // =========================
        // Extra opciones
        // =========================
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

        // ============================
        // Index Principal del Cliente
        // ============================
        public ActionResult FloristeríaClient()
        {
            if (Session["cliente"] == null)
            {
                return RedirectToAction("Login", "Acceso"); // tu controller login
            }
            List<Producto> products = productoDAO.ListarTodo();
            ViewBag.Categorias = categoriaDAO.ListarTodo();

            if (products == null)
            {
                throw new Exception("La lista de productos es NULL");
            }
            return View(products);
        }

        // ============================
        // Carrito
        // ============================

        [HttpPost]
        public JsonResult EliminarCarrito(int id)
        {
            if (Session["Carrito"] == null)
            {
                return Json(new
                {
                    cantidad = 0,
                    total = 0
                });
            }

            var carrito = (List<CarritoItem>)Session["Carrito"];

            var producto = carrito.FirstOrDefault(x => x.IdProducto == id);

            if (producto != null)
            {
                carrito.Remove(producto);
            }

            Session["Carrito"] = carrito;

            return Json(new
            {
                cantidad = carrito.Sum(x => x.Cantidad),
                total = carrito.Sum(x => x.SubTotal)
            });
        }


        [HttpPost]
        public JsonResult AgregarCarrito(int id, string nombre, decimal precio, string imagen)
        {
            List<CarritoItem> carrito;

            if (Session["Carrito"] == null)
            {
                carrito = new List<CarritoItem>();
            }
            else
            {
                carrito = (List<CarritoItem>)Session["Carrito"];
            }

            var producto = carrito.FirstOrDefault(x => x.IdProducto == id);

            if (producto != null)
            {
                producto.Cantidad++;
            }
            else
            {
                carrito.Add(new CarritoItem()
                {
                    IdProducto = id,
                    NombreProducto = nombre,
                    Precio = precio,
                    Cantidad = 1,
                    Imagen = imagen
                });
            }

            Session["Carrito"] = carrito;

            return Json(new
            {
                cantidad = carrito.Sum(x => x.Cantidad),
                total = carrito.Sum(x => x.SubTotal)
            });
        }
        [HttpPost]
        public JsonResult FinalizarCompra()
        {
            if (Session["cliente"] == null)
            {
                return Json(new
                {
                    ok = false,
                    mensaje = "Debe iniciar sesión"
                });
            }

            var carrito = Session["Carrito"] as List<CarritoItem>;

            if (carrito == null || !carrito.Any())
            {
                return Json(new
                {
                    ok = false,
                    mensaje = "Carrito vacío"
                });
            }

            Cliente cliente = (Cliente)Session["cliente"];

            decimal subtotal = carrito.Sum(x => x.SubTotal);
            decimal igv = subtotal * 0.18m;
            decimal total = subtotal + igv;

            Pedido pedido = new Pedido();

            pedido.FechaPedido = DateTime.Now;
            pedido.IdCliente = cliente.IdCliente;

            // por ahora temporal
            pedido.IdUsuario = 1;

            pedido.IdMetodoPago = 1;

            pedido.FechaEntrega = DateTime.Now.AddDays(1);

            pedido.DireccionEntrega = cliente.Direccion;

            pedido.Dedicatoria = "";

            pedido.SubTotal = subtotal;
            pedido.IGV = igv;
            pedido.Total = total;
            pedido.IdEstado = 1;

            // IMPORTANTE
            pedido.DetallePedidos = new List<DetallePedido>();

            foreach (var item in carrito)
            {
                pedido.DetallePedidos.Add(new DetallePedido()
                {
                    IdProducto = item.IdProducto,
                    Cantidad = item.Cantidad,
                    PrecioUnitario = item.Precio,
                    Importe = item.SubTotal
                });
            }

            // UNA sola llamada
            int resultado = pedidoDAO.Registrar(pedido);

            if (resultado > 0)
            {
                Session["Carrito"] = null;

                return Json(new
                {
                    ok = true,
                    mensaje = "Compra realizada"
                });
            }

            return Json(new
            {
                ok = false,
                mensaje = "Error al guardar"
            });
        }




    }
}