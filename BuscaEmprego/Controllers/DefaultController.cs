using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BuscaEmprego.Controllers
{

    [Authorize(Roles = "admin")]
    public class DefaultController : Controller
    {

    }
}
