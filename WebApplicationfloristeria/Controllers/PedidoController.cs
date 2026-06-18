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
    public class PedidoController : Controller
    {
        private PedidoDAO pedidoDao = new PedidoDAO();
        private ClienteDAO clienteDao = new ClienteDAO();
        private ProductoDAO productoDao = new ProductoDAO();

        public ActionResult Index()
        {
            var lista = pedidoDao.ListarTodo();
            return View(lista);
        }

        public ActionResult Create()
        {
            if (Session["Carrito"] == null)
            {
                Session["Carrito"] = new List<DetallePedido>();
            }

            ViewBag.Clientes = new SelectList(clienteDao.ListarTodo(), "IdCliente", "Nombres");
            ViewBag.Productos = new SelectList(productoDao.ListarTodo(), "IdProducto", "NombreProducto");

            var carrito = (List<DetallePedido>)Session["Carrito"];

            decimal subTotal = carrito.Sum(d => d.Importe);
            decimal igv = subTotal * 0.18m;
            decimal total = subTotal + igv;

            ViewBag.SubTotal = subTotal;
            ViewBag.IGV = igv;
            ViewBag.Total = total;

            return View(new Pedido());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Pedido pedido)
        {
            var carrito = (List<DetallePedido>)Session["Carrito"];

            if (carrito == null || carrito.Count == 0)
            {
                ModelState.AddModelError("", "Debe agregar al menos un producto al detalle del pedido.");
            }

            if (ModelState.IsValid)
            {
                pedido.FechaPedido = DateTime.Now;
                pedido.IdUsuario = 1;
                pedido.IdMetodoPago = 1;
                pedido.IdEstado = 1;
                pedido.SubTotal = carrito.Sum(d => d.Importe);
                pedido.IGV = pedido.SubTotal * 0.18m;
                pedido.Total = pedido.SubTotal + pedido.IGV;
                pedido.DetallePedidos = carrito;

                if (pedidoDao.RegistrarPedidoCompleto(pedido))
                {
                    Session["Carrito"] = null;
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Ocurrió un error en la transacción al guardar el pedido.");
                }
            }

            ViewBag.Clientes = new SelectList(clienteDao.ListarTodo(), "IdCliente", "Nombres");
            ViewBag.Productos = new SelectList(productoDao.ListarTodo(), "IdProducto", "NombreProducto");
            ViewBag.SubTotal = carrito.Sum(d => d.Importe);
            ViewBag.IGV = Convert.ToDecimal(ViewBag.SubTotal) * 0.18m;
            ViewBag.Total = Convert.ToDecimal(ViewBag.SubTotal) + Convert.ToDecimal(ViewBag.IGV);

            return View(pedido);
        }

        [HttpPost]
        public ActionResult AgregarProducto(int idProducto, int cantidad, decimal precioUnitario)
        {
            var carrito = (List<DetallePedido>)Session["Carrito"] ?? new List<DetallePedido>();
            var itemExistente = carrito.FirstOrDefault(d => d.IdProducto == idProducto);

            if (itemExistente != null)
            {
                itemExistente.Cantidad += cantidad;
                itemExistente.Importe = itemExistente.Cantidad * itemExistente.PrecioUnitario;
            }
            else
            {
                DetallePedido nuevoDetalle = new DetallePedido
                {
                    IdProducto = idProducto,
                    Cantidad = cantidad,
                    PrecioUnitario = precioUnitario,
                    Importe = cantidad * precioUnitario
                };
                carrito.Add(nuevoDetalle);
            }

            Session["Carrito"] = carrito;
            return RedirectToAction("Create");
        }

        public ActionResult EliminarProducto(int idProducto)
        {
            var carrito = (List<DetallePedido>)Session["Carrito"];
            if (carrito != null)
            {
                var item = carrito.FirstOrDefault(d => d.IdProducto == idProducto);
                if (item != null)
                {
                    carrito.Remove(item);
                }
                Session["Carrito"] = carrito;
            }
            return RedirectToAction("Create");
        }
    }
}