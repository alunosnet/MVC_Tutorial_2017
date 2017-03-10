using MVC_Tutorial_2017.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_Tutorial_2017.Controllers
{
    public class UtilizadoresController : Controller
    {
        UtilizadoresBD bd = new UtilizadoresBD();
        // GET: Utilizadores
        public ActionResult Index()
        {
            return View(bd.lista());
        }
        
        //adicionar utilizador
        public ActionResult Create()
        {
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
            return View(bd.lista(id)[0]);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string id)
        {

        }
    }
}