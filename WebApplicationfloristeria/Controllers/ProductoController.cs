using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplicationfloristeria.Data;
using WebApplicationfloristeria.Data.Interfaces;
using WebApplicationfloristeria.Filtros;
using WebApplicationfloristeria.Models;

namespace WebApplicationfloristeria.Controllers
{
    [AuthFilter]
    public class ProductoController : Controller
    {
        private const string RutaImgProductos = "~/Contenido/Multimedia/Productos";
        private IProducto productoDAO;
        private ICategoria categoriaDAO;
        public ProductoController() {
            productoDAO = new ProductoDAO();
            categoriaDAO = new CategoriaDAO();
        }
        //  ----------------------------------------------------------------------------
        // |                               Index                                        |
        //  ----------------------------------------------------------------------------
        // GET: Producto
        public ActionResult Index()
        {
            List<Producto> products = productoDAO.ListarTodo();
            ViewBag.Categoria = new SelectList(
                categoriaDAO.ListarTodo(),
                "IdCategoria",
                "NombreCategoria"
            );
            if (products == null)
            {
                throw new Exception("La lista de productos es NULL");
            }
            return View(products);
        }

        //  ----------------------------------------------------------------------------
        // |                           Registrar                                        |
        //  ----------------------------------------------------------------------------
        // GET: Producto
        public ActionResult Create()
        {
            ViewBag.Categoria = new SelectList(
                categoriaDAO.ListarTodo(),
                "IdCategoria",
                "NombreCategoria"
            );

            return View(new Producto());
        }
        // POST: Producto
        [HttpPost]public ActionResult Create( Producto producto, HttpPostedFileBase archivoImg)
        {
            ViewBag.Categoria = new SelectList(
                    categoriaDAO.ListarTodo(),
                    "IdCategoria",
                    "NombreCategoria"
                );
            if(archivoImg !=null && archivoImg.ContentLength > 0)
            {
                string nombreArchivo = Path.GetFileName( archivoImg.FileName );
                string rutaServer = Path.Combine(Server.MapPath(RutaImgProductos), nombreArchivo);
                archivoImg.SaveAs(rutaServer);
                producto.Imagen = $"{RutaImgProductos}/{nombreArchivo}";
            }
            int idG = productoDAO.Registrar(producto);
            ViewBag.Ok = idG > 0;
            ViewBag.Titulo = idG > 0 ? "Producto Registrado" : "Error al Registrar !";
            ViewBag.Mensaje = idG > 0 ? $"El Producto se Registro Correctamente con el ID :{idG}" : "Codigo de error : 101";

            return View();
        }

        //  ----------------------------------------------------------------------------
        // |                              Buscar                                        |
        //  ----------------------------------------------------------------------------
        // GET: Producto
        public ActionResult Buscar()
        {
            return View();
        }
        // POST: Producto
        [HttpPost]
        public ActionResult Buscar(FormCollection reg)
        {
            return View(reg);
        }

        //  ----------------------------------------------------------------------------
        // |                             Detalle                                        |
        //  ----------------------------------------------------------------------------
        // GET: Producto
        public ActionResult Details(int id)
        {
            return View(productoDAO.BuscarPorId(id));
        }

        //  ----------------------------------------------------------------------------
        // |                             Editar                                         |
        //  ----------------------------------------------------------------------------
        // GET: Producto
        public ActionResult Edit(int id)
        {
            Producto producto = productoDAO.BuscarPorId(id);

            // Depuración
            var categoria = producto.Categoria;
            var estado = producto.Estado;

            ViewBag.Categoria = new SelectList(
                categoriaDAO.ListarTodo(),
                "IdCategoria",
                "NombreCategoria",
                producto.Categoria?.IdCategoria
            );
            return View(producto);
        }
        // POST: Producto
        [HttpPost]
        public ActionResult Edit(Producto producto, HttpPostedFileBase archivoImg)
        {
            ViewBag.Categoria = new SelectList(
                categoriaDAO.ListarTodo(),
                "IdCategoria",
                "NombreCategoria"
            );

            if (archivoImg != null && archivoImg.ContentLength > 0)
            {
                string nombreArchivo = Path.GetFileName(archivoImg.FileName);
                string rutaServer = Path.Combine(Server.MapPath(RutaImgProductos), nombreArchivo);

                archivoImg.SaveAs(rutaServer);
                producto.Imagen = $"{RutaImgProductos}/{nombreArchivo}";
            }
            else
            {
                producto.Imagen = producto.Imagen;
            }
            bool Ok = productoDAO.Actualizar(producto);

            ViewBag.Ok = Ok;
            ViewBag.Titulo = Ok ? "Producto Actualizado" : "Error al Actualizar !";
            ViewBag.Mensaje = Ok ? "El Producto  se Actualizo Correctamente. " : "Codigo de Error : 103";

            ModelState.Clear();
            return View(producto);
        }
        //  ----------------------------------------------------------------------------
        // |                             Eliminar                                       |
        //  ----------------------------------------------------------------------------
        // GET: Producto
        public ActionResult Delete( int id)
        {
            bool Ok = productoDAO.Eliminar(id);

            TempData["Ok"] = Ok;
            TempData["Titulo"] = Ok ? "Producto eliminado" : "Error al eliminar";
            TempData["Mensaje"] = Ok ? "El producto se elimino correctamente" : "Codigo de error : 104";
            return RedirectToAction("Index");
        }
    }
}