using Cruds.Domain;
using Cruds.Infra;
using DataModels;
using LinqToDB.Data;
using Microsoft.AspNetCore.Mvc;

namespace Cruds.WebViewUI.Controllers
{
    public class ClienteController : Controller
    {
        private readonly IRepositoryCliente _repoLinqTodb;
        public Cliente cliente;
    

        public ClienteController(IRepositoryCliente repoLinqTodb )
        {

         this._repoLinqTodb = repoLinqTodb;
         cliente = new Cliente();
        
        }
        public IActionResult Index()
        {
            
            return View();
        }
        public IActionResult Clientes()
        {
            var till = _repoLinqTodb.RetornarClientes().ToList();
            return View(till);
        }
        public IActionResult Create(int? id)
        {

            if (id>=0)
            {
                 cliente = _repoLinqTodb.ExibirPorCodigo(id.Value);
               
                return View(cliente);

            }
            return View();
        }
        public IActionResult Edit(int id)
        {
         
            cliente  = _repoLinqTodb.ExibirPorCodigo(id);

            return View(cliente);
        }
        public IActionResult Delete(int id)
        {
            if(id == null)
            {
                NotFound();
            }
            cliente = _repoLinqTodb.ExibirPorCodigo(id);

            if(cliente == null)
            {
                NotFound();
            }

            return View(cliente);
        }
        public IActionResult Details(int id)
        {
            if (id == null)
            {
              return  NotFound();
            }
            cliente = _repoLinqTodb.ExibirPorCodigo(id);
           
            return View(cliente);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(int id, Cliente cliente)
        { 

            if (ModelState.IsValid && id == 0)
             {
               
                _repoLinqTodb.AdicionarClientes(cliente);
                return RedirectToAction(nameof(Clientes));
            }
            else
             {
                Edit(id, cliente);
                return RedirectToAction(nameof(Clientes));
             }
            return View(cliente);     
        }
        [HttpPost]
        public IActionResult Edit(int id, Cliente cliente)
        {
            cliente.Codigo = id;

            if (id != cliente.Codigo)
            {
                return NotFound();
            }
            else
            {
                _repoLinqTodb.AlterarCliente(cliente);
                return RedirectToAction(nameof(Clientes));


            }
            return View(cliente);
        }

        [HttpPost,ActionName("Delete")]
        public IActionResult Delete(int id,Cliente cliente) {
            if (id == null)
            {
                NotFound();
            }
            else
            {
                _repoLinqTodb.DeletarCliente(id);
                return RedirectToAction(nameof(Clientes));

            }
            return View(cliente);
        }





    }
}
