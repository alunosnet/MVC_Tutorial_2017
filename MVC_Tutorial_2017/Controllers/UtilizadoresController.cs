using MVC_Tutorial_2017.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_Tutorial_2017.Controllers
{
    [Authorize]
    public class UtilizadoresController : Controller
    {
        UtilizadoresBD bd = new UtilizadoresBD();
        // GET: Utilizadores
        public ActionResult Index()
        {
            if (Session["perfil"].Equals(1))
                return new HttpStatusCodeResult(401);
            return View(bd.lista());
        }
        
        //adicionar utilizador
        public ActionResult Create()
        {
            if (Session["perfil"].Equals(1))
                return new HttpStatusCodeResult(401);
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UtilizadoresModel novo)
        {
            if (ModelState.IsValid)
            {

                bd.adicionarUtilizadores(novo);
                return RedirectToAction("Index");
            }
            return View(novo);
        }
        public ActionResult Delete(string id)
        {
            if (Session["perfil"].Equals(1))
                return new HttpStatusCodeResult(401);
            return View(bd.lista(id)[0]);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public ActionResult ConfirmarDelete(string id)
        {
            bd.removerUtilizador(id);
            return RedirectToAction("index");
        }

        public ActionResult Edit(string id)
        {
            if (Session["perfil"].Equals(1))
                return new HttpStatusCodeResult(401);
            return View(bd.lista(id)[0]);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UtilizadoresModel dados)
        {
            if (ModelState.IsValid)
            {
                bd.editarUtilizadores(dados);
                return RedirectToAction("index");
            }
            return View(dados);
        }
    }
}