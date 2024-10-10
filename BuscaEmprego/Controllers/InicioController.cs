using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BuscaEmprego.Controllers
{
    public class InicioController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
