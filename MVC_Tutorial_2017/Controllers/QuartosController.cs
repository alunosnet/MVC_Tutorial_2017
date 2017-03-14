using MVC_Tutorial_2017.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_Tutorial_2017.Controllers
{
    public class QuartosController : Controller
    {
        QuartosBD bd = new QuartosBD();
        // GET: Quartos
        public ActionResult Index()
        {
            return View(bd.lista());
        }

        public ActionResult Create()
        {
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
            if (id == null) return new HttpStatusCodeResult(404);

        }
    }
}