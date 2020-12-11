
using Microsoft.AspNetCore.Mvc;

namespace WebApp_Cnpm.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
