using HDProjectWeb.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HDProjectWeb.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly UserManager<_Login> userManager;
        private readonly SignInManager<_Login> signInManager;

        public UsuarioController(UserManager<_Login> userManager, SignInManager<_Login> signInManager)    
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }
               /*public async Task<IActionResult> Login(LoginViewModel modelo)
        {
            if(!ModelState.IsValid)
            {
                return View(modelo);
            }
            var resultado = await signInManager.PasswordSignInAsync(modelo.CodUser,modelo.Password,
                modelo.Recuerdame, lockoutOnFailure: false);
            if(resultado.Succeeded) 
            {
                return RedirectToAction("Index","RQCompra");
            }
            else
            {
                ModelState.AddModelError(String.Empty, "Nombre de usuario o token incorrecto");
                return View(modelo);
            }       
        } */
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);
                return RedirectToAction("Index", "Home");
        }
    }
}
