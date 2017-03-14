using MVC_Tutorial_2017.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_Tutorial_2017.Controllers
{
    [Authorize]
    public class ClientesController : Controller
    {
        ClientesBD bd = new ClientesBD();
        int registosPorPagina = 5;
        // GET: Clientes
        public ActionResult Index()
        {
            return View(bd.lista());
        }
        public ActionResult Index2(int? id)
        {
            if (id == null) id = 1;
            ViewBag.paginaAtual = id;
            return View(bd.listaPagina((int)id, registosPorPagina));
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ClientesModel dados,HttpPostedFileBase fotografia)
        {
            if (ModelState.IsValid)
            {
                if(fotografia==null)
                {
                    ModelState.AddModelError("", "Indique uma fotografia para o cliente");
                    return View(dados);
                }
                int id=bd.adicionarCliente(dados);

                string caminho = Server.MapPath("~/Content/Imagens/") + id + ".jpg";
                fotografia.SaveAs(caminho);
              
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
        public ActionResult Pesquisar()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Pesquisar(string tbNome)
        {
            ViewBag.listaClientes = bd.pesquisa(tbNome);
            return View();
        }
        public ActionResult Pesquisar2()
        {
            return View();
        }
        public JsonResult PesquisarJson(string id)
        {
            if (String.IsNullOrEmpty(id))
                return Json(null, JsonRequestBehavior.AllowGet);
            return Json(bd.pesquisa(id), JsonRequestBehavior.AllowGet);
        }
    }
}