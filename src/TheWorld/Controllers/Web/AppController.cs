using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;
using System.Linq;
using TheWorld.Models;
using TheWorld.Services;
using TheWorld.ViewModel;

namespace TheWorld.Controllers.Web
{
    public class AppController : Controller
    {
        private IMailService _mailService;
        private IWorldRepository _repository;

        public AppController(IMailService mailService, IWorldRepository worldRepository)
        {
            _mailService = mailService;
            _repository = worldRepository;
        }
        public IActionResult Index()
        {            
            return View();
        }

        [Authorize]
        public IActionResult Trips()
        {
            //var trips = _repository.GetAllTrips();
            //return View(trips);
            return View();
        }
        public IActionResult About()
        {            
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Contact(ContactViewModel contactInfo)
        {
            if (ModelState.IsValid)
            {
                var toAddress = Startup.Configuration["AppSettings:SiteEmailAddress"];

                if (string.IsNullOrWhiteSpace(toAddress))
                {
                    ModelState.AddModelError("", "Could not send email. Configuration error");
                }
                if (_mailService.SendMail(toAddress, toAddress, $"Contact Mail from {contactInfo.Name}", contactInfo.Message))
                {
                    ModelState.Clear();

                    ViewBag.Message = "Email sent successfully. Thanks!";
                }
            }            
            return View();
        }
    }
}
