using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplicationfloristeria.Data;
using WebApplicationfloristeria.Models;

namespace WebApplicationfloristeria.Controllers
{
    public class CategoriaController : Controller
    {

        private CategoriaDAO dao = new CategoriaDAO();

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
        public ActionResult Create(Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                if (dao.Registrar(categoria) > 0)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(categoria);
        }
        public ActionResult Edit(int id)
        {
            var categoria = dao.BuscarPorId(id);
            if (categoria == null)
            {
                return HttpNotFound();
            }
            return View(categoria);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                if (dao.Actualizar(categoria))
                {
                    return RedirectToAction("Index");
                }
            }
            return View(categoria);
        }

        public ActionResult Delete(int id)
        {
            var categoria = dao.BuscarPorId(id);
            if (categoria == null)
            {
                return HttpNotFound();
            }
            return View(categoria);
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
