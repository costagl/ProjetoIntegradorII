using Microsoft.AspNetCore.Mvc;

namespace BuscaEmprego.Controllers
{
    public class CandidaturaController : DefaultController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
