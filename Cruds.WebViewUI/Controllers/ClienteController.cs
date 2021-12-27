using Cruds.Domain;
using Cruds.Infra;
using DataModels;
using LinqToDB.Data;
using Microsoft.AspNetCore.Mvc;

namespace Cruds.WebViewUI.Controllers
{
    [Route("api/[controller]")]
    public class ClienteController : Controller
    {
        private readonly IRepositoryCliente _repoLinqTodb;
        public Cliente cliente;


        public ClienteController(IRepositoryCliente repoLinqTodb)
        {

            this._repoLinqTodb = repoLinqTodb;
            Cliente cliente = new();
            // cliente = new Cliente();

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

            if (id >= 0)
            {
                cliente = _repoLinqTodb.ExibirPorCodigo(id.Value);

                return View(cliente);

            }
            return View();
        }
        public IActionResult Edit(int id)
        {

            cliente = _repoLinqTodb.ExibirPorCodigo(id);

            return View(cliente);
        }
        public IActionResult Delete(int id)
        {
            if (id == null)
            {
                NotFound();
            }
            cliente = _repoLinqTodb.ExibirPorCodigo(id);

            if (cliente == null)
            {
                NotFound();
            }

            return View(cliente);
        }
        public IActionResult Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            cliente = _repoLinqTodb.ExibirPorCodigo(id);

            return View(cliente);
        }
        //[HttpPost]
        //[ValidateAntiForgeryToken]
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
       // [HttpPost]
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

        //[HttpPost, ActionName("Delete")]
        [HttpDelete]
        public IActionResult Delete(int id, Cliente cliente) {
            if (id == null)
            {
                NotFound();
            }
            else
            {
                _repoLinqTodb.DeletarCliente(id);
                return Ok(nameof(Clientes));

            }
            return View(cliente);
        }

        //Utilização de Rotas SAPUI5

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var till = _repoLinqTodb.RetornarClientes().ToList();
            return Ok(till);
        }

        [HttpGet("{Codigo}")]
        public IActionResult Detalhes(int codigo)
        {
            if (codigo == null)
            {
                return NotFound();
            }
            cliente = _repoLinqTodb.ExibirPorCodigo(codigo);

            return Ok(cliente);

        }
        [HttpPost]
        public async Task <IActionResult> CadastrarCliente([FromBody] Cliente cliente)
        {
           
           _repoLinqTodb.AdicionarClientes(cliente);
                return Ok(cliente);
        }

        [HttpPut]
        public async Task<IActionResult> Update ([FromBody] Cliente cliente)
        {

            _repoLinqTodb.AlterarCliente(cliente);
            return Ok(cliente);
        }

        [HttpDelete ("{id}")]
        public IActionResult Deletar(int id, Cliente cliente)
        {
            if (id == null)
            {
                NotFound();
            }
            else
            {
                _repoLinqTodb.DeletarCliente(id);
                return RedirectToAction(nameof(Clientes));

            }
            return Ok(cliente);
        }

        public bool ValoresNulos()
        {
            if (cliente != null)
            {
                return false;
            }
            return true;

        }


    }
}
