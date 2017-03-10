using MVC_Tutorial_2017.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_Tutorial_2017.Controllers
{
    public class ClientesController : Controller
    {
        ClientesBD bd = new ClientesBD();
        // GET: Clientes
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
        public ActionResult Create(ClientesModel dados)
        {
            if (ModelState.IsValid)
            {
                bd.adicionarCliente(dados);
                return RedirectToAction("index");
            }
            return View(dados);
        }
        public ActionResult Edit(int? id)
        {
            if (id == null) return RedirectToAction("index");
            return View(bd.lista((int)id)[0]);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ClientesModel dados)
        {
            if (ModelState.IsValid)
            {
                bd.atualizarCliente(dados);
                return RedirectToAction("index");
            }
            return View(dados);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null) return RedirectToAction("index");
            return View(bd.lista((int)id)[0]);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public ActionResult ConfirmarDelete(int? id)
        {
            bd.removerCliente((int)id);
            return View("index");
        }
    }
}