using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryMngSys.Controllers
{
    public class RoleController : Controller
    {
        [Authorize(Policy = "RequireUser")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
