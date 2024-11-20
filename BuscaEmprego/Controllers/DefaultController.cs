using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.Models;

namespace BuscaEmprego.Controllers
{

    [Authorize(Roles = "admin")]
    public class DefaultController : Controller
    {
    }
}
