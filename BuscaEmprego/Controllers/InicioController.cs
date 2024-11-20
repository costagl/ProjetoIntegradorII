using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BuscaEmprego.Controllers
{
    [AllowAnonymous]
    public class InicioController : DefaultController
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
