using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using TheWorld.Models;
using Microsoft.AspNet.Identity;
using TheWorld.ViewModel;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace TheWorld.Controllers.Web
{
    public class AuthController : Controller
    {
        private SignInManager<WorldUser> _signInManager;

        public AuthController(SignInManager<WorldUser> signInManager)
        {
            _signInManager = signInManager;

        }
        // GET: /<controller>/
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
                RedirectToAction("trips", "app");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel, string ReturnUrl)
        {
            if (ModelState.IsValid)
            {
                var signInResult = await _signInManager.PasswordSignInAsync(loginViewModel.Username, loginViewModel.Password, true, false);

                if(signInResult.Succeeded)
                {
                    if (string.IsNullOrWhiteSpace(ReturnUrl))
                        RedirectToAction("trips", "app");
                    else
                        RedirectToAction(ReturnUrl);
                }
                else
                {
                    ModelState.AddModelError("", "Username or Password incorrect");
                }
            }
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            if (User.Identity.IsAuthenticated)
            {
                await _signInManager.SignOutAsync();
            }            
            return RedirectToAction("Index", "App");
        }
    }
}
