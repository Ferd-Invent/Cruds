using Cruds.Domain;
using Cruds.Infra;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using System.Web.Helpers;

namespace Crud.WebView.Pages
{
   
    public class IndexModel : PageModel
    {
        public static RepoLinqTodb repoLinqToDB;
        private readonly ILogger<IndexModel> _logger;
       
        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            var _client = repoLinqToDB.GetAllClientes();
            var _clientList = _client.AsEnumerable();
           // var grid = new WebGrid(source: repoLinqToDB.GetAllClientes());
        }
         
      
    } 
}