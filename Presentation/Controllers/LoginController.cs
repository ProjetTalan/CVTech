using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Application.Interface;
using Application.Models;
using Newtonsoft.Json;
using Presentation.Models;

namespace Presentation.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        private readonly IProfileService _profileService;

        public LoginController(IProfileService profileService)
        {
	        _profileService = profileService;
        }
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]

        public async Task<ActionResult> Identification(LoginModel loginModel)
        {
	        if (ModelState.IsValid)
	        {
		        ProfileModel userModelTemp = new ProfileModel
				{
			        Email = loginModel.LoginEmail,
			        Password = loginModel.LoginPassword
		        };

				if (await _profileService.UserExist(userModelTemp))
				{
					var objectJson =
						JsonConvert.SerializeObject(await _profileService.GetProfileByModelAsync(userModelTemp));

					FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, Constantes.Authent, DateTime.Now,
														DateTime.Now.AddMinutes(30),
														true, objectJson, FormsAuthentication.FormsCookiePath);

					Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket)));

					return new JsonResult { Data = true, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
		        }

		        return new JsonResult { Data = false, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

	        }
	        return new JsonResult { Data = false, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}