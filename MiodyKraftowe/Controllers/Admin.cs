using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MiodyKraftowe.Controllers
{

    [Authorize(Roles = "Admin")]
    public class Admin : Controller
    {
        private readonly Microsoft.AspNetCore.Identity.UserManager<Microsoft.AspNetCore.Identity.IdentityUser> _userManager;

        public Admin(Microsoft.AspNetCore.Identity.UserManager<Microsoft.AspNetCore.Identity.IdentityUser> userManager)
        {
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }
        public IActionResult AddUserToRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddUserToRole(string Email, string UserId, string RoleId)
        {
            var user = await _userManager.FindByEmailAsync(Email);
            if (user == null)
            {
                return NotFound("Nie znaleziono użytkownika o podanym adresie email.");
            }

            var result = await _userManager.AddToRoleAsync(user, RoleId);
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }
    }
}
