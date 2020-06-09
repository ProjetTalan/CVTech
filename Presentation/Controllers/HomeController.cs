using System;
using System.Web.Mvc;
using System.Web.Security;
using Application.Models;
using Newtonsoft.Json;

namespace Presentation.Controllers
{
	public class HomeController : Controller
	{
		[AuthorizeCustom]
		public ActionResult Index()
		{

			return View();
		}

		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}
	}
}