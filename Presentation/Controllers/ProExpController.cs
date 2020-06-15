using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Application.Interface;
using Application.Models;
using Newtonsoft.Json;
using Presentation.ViewModels.ProExpViewModels;

namespace Presentation.Controllers
{
	[AuthorizeCustom]
    public class ProExpController : Controller
    {
	    // GET: ProExp
	    private IProExpService _proExpService;
	    private ProfileModel _userProExp;

	    public ProExpController(IProExpService proExpService)
	    {
		    _proExpService = proExpService;

		    if (System.Web.HttpContext.Current.Request.Cookies[".ASPXAUTH"] != null)
		    {
			    FormsAuthenticationTicket ticket = null;
			    var toDecrypt = System.Web.HttpContext.Current.Request.Cookies[".ASPXAUTH"].Value;
			    if (toDecrypt != null)
			    {
				    try
				    {
					    ticket = FormsAuthentication.Decrypt(toDecrypt);
				    }
				    catch (Exception e)
				    {
					    Console.Write(e.Message);
				    }

				    if (ticket != null)
				    {
					    _userProExp = JsonConvert.DeserializeObject<ProfileModel>(ticket.UserData);
				    }
			    }
		    }
        }
        public async Task<ActionResult> Index()
        {
            var indexVm = new IndexProExpViewModel
            {
                ProExpModels = new List<ProExpModel>(await _proExpService.GetAllProExpFrom(_userProExp)) 
            };
            return View(indexVm);
        }

        // GET: ProExp/Details/5
        public async Task<ActionResult> Details(int? id)
        {
	        if (id == null)
	        {
		        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
	        }

	        ProExpModel proExpModel = await _proExpService.GetProExpByIdAsync(id.Value);

	        if (proExpModel == null)
	        {
		        return HttpNotFound();
	        }

	        var detailVm = new DetailsProExpViewModel
	        {
                CityName = proExpModel.CityName,
                CompanyName = proExpModel.CompanyName,
                FromDate = proExpModel.FromDate,
                ToDate = proExpModel.ToDate,
                ProfileTechModels = new List<ProfileTechModel>(await _proExpService.GetAllTechnoFrom(proExpModel))
	        };

	        return View(detailVm);
        }

        // GET: ProExp/Create
        public ActionResult Create()
        {
            var createVm = new CreateProExpViewModel();
            return View(createVm);
        }

        // POST: ProExp/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "CompanyName, CityName, FromDate, ToDate")] CreateProExpViewModel proExpViewModel)
        {
	        if (ModelState.IsValid)
	        {
                var proExpToAdd = new ProExpModel
                {
                    CompanyName = proExpViewModel.CompanyName,
                    CityName = proExpViewModel.CityName,
                    FromDate = proExpViewModel.FromDate,
                    ToDate = proExpViewModel.ToDate,
                    ProfileId = _userProExp.Id
                };

		        await _proExpService.CreateAsync(proExpToAdd);
		        return RedirectToAction("Index");
	        }

	        return View(proExpViewModel);
        }

        // GET: ProExp/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProExp/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ProExp/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProExp/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
