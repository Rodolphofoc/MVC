using CaelumEstoque.DAO;
using CaelumEstoque.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CaelumEstoque.Controllers
{
    public class ProdutoController : Controller
    {
        //
        // GET: /Produto/



        public IList<Produto> Produtos
        {

            get
            {
                if (Session["Produtos"] != null)
                    return (List<Produto>)Session["Produtos"];
                else
                    return null;

            }

            set { Session["Produtos"] = value; }
        }

        public ActionResult Index()
        {
            ProdutosDAO dao = new ProdutosDAO();
            this.Produtos = dao.Lista();
            CategoriasDAO daoCategoria = new CategoriasDAO();
            IList<CategoriaDoProduto> categorias = daoCategoria.Lista();
            ViewBag.Categorias = categorias;
            ViewBag.Produtos = Produtos;
            return View();
        }

        [HttpPost]
        public ActionResult Adiciona(Produto produto)
        {
            int idDaInformatica = 1;
            if (produto.CategoriaId.Equals(idDaInformatica) && produto.Preco < 100)
            {
                ModelState.AddModelError("produto.InformaticaComPrecoInvalido", "Produtos da categoria informática devem ter preço maior do que 100");
            }
            if (ModelState.IsValid)
            {
                ProdutosDAO dao = new ProdutosDAO();
                dao.Adiciona(produto);
                return RedirectToAction("Index");
            }
            else
            {
                CategoriasDAO categoriasDAO = new CategoriasDAO();
                ViewBag.Categorias = categoriasDAO.Lista();
                return View("Index");
            }
        }


        public ActionResult form()
        {
            return View();
        }


	}
}