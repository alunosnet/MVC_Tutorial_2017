using MVC_Tutorial_2017.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_Tutorial_2017.Controllers
{
    [Authorize]
    public class QuartosController : Controller
    {
        QuartosBD bd = new QuartosBD();
        // GET: Quartos
        public ActionResult Index()
        {
            if (Session["perfil"].Equals(1))
                return new HttpStatusCodeResult(401);

            return View(bd.lista());
        }

        public ActionResult Create()
        {
            if (Session["perfil"].Equals(1))
                return new HttpStatusCodeResult(401);
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(QuartosModel dados)
        {

            if (ModelState.IsValid)
            {
                bd.adicionarQuarto(dados);
                return RedirectToAction("Index");
            }
            return View(dados);
        }

        public ActionResult Edit(int? id)
        {
            if (Session["perfil"].Equals(1))
                return new HttpStatusCodeResult(401);
            if (id == null) return new HttpStatusCodeResult(404);
            return View(bd.lista((int)id)[0]);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(QuartosModel dados)
        {
            if (ModelState.IsValid)
            {
                bd.atualizarQuarto(dados);
                return RedirectToAction("index");
            }
            return View(dados);
        }
        public ActionResult Delete(int? id)
        {
            if (Session["perfil"].Equals(1))
                return new HttpStatusCodeResult(401);
            if (id == null) return new HttpStatusCodeResult(404);
            return View(bd.lista((int)id)[0]);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public ActionResult ConfirmaDelete(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(404);
            bd.removerQuarto((int)id);
            return RedirectToAction("Index");
        }
    }
}