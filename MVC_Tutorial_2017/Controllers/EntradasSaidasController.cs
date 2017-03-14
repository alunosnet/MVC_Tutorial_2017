using MVC_Tutorial_2017.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_Tutorial_2017.Controllers
{
    [Authorize]
    public class EntradasSaidasController : Controller
    {
        EntradaSaidaBD bd = new EntradaSaidaBD();
        // GET: EntradasSaidas
        public ActionResult Index()
        {
            if (Session["perfil"] == null) return RedirectToAction("Index", "Login");
            if( Session["perfil"].Equals(1))
                return View(bd.listaOcupados());

            return View(bd.listaTodos());
        }
        public ActionResult Entrada()
        {
            //lista clientes
            ClientesBD bdClientes = new ClientesBD();
            ViewBag.listaClientes= bdClientes.lista();
            //lista quartos
            QuartosBD bdQuartos = new QuartosBD();
            ViewBag.listaQuartos = bdQuartos.listaVazios();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Entrada(EntradaSaidaModel dados)
        {
            if (ModelState.IsValid)
            {
                bd.registarEntrada(dados);
                return RedirectToAction("Index");
            }
            return View(dados);
        }
        public ActionResult Saida(int? id)
        {
            if (id == null) return RedirectToAction("Index");
            return View(bd.listaOcupados((int)id)[0]);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Saida(EntradaSaidaModel dados)
        {
            if (ModelState.IsValid)
            {
                bd.registarSaida(dados);
                return RedirectToAction("Index");
            }
            return View(dados);
        }
    }
}