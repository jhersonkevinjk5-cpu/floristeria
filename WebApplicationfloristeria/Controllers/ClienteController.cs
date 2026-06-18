using System;
using System.Web.Mvc;
using WebApplicationfloristeria.Data.Interfaces; // Namespace donde está tu ClienteDAO
using WebApplicationfloristeria.Models;

namespace WebApplicationfloristeria.Controllers
{
    public class ClienteController : Controller
    {
        private ClienteDAO dao = new ClienteDAO();

        public ActionResult Index()
        {
            var lista = dao.ListarTodo();
            return View(lista);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                if (cliente.FechaRegistro == DateTime.MinValue)
                {
                    cliente.FechaRegistro = DateTime.Now;
                }

                int resultado = dao.Registrar(cliente);
                if (resultado > 0)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(cliente);
        }

        public ActionResult Edit(int id)
        {
            var cliente = dao.BuscarPorId(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                if (dao.Actualizar(cliente))
                {
                    return RedirectToAction("Index");
                }
            }
            return View(cliente);
        }

        public ActionResult Delete(int id)
        {
            var cliente = dao.BuscarPorId(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            dao.Eliminar(id);
            return RedirectToAction("Index");
        }
    }
}