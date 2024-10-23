using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BuscaEmprego.Controllers
{
    public class InicioController : DefaultController
    {
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }
    }
}
