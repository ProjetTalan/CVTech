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
	    private ProfileModel _userModel;

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
					    _userModel = JsonConvert.DeserializeObject<ProfileModel>(ticket.UserData);
				    }
			    }
		    }
        }
        public async Task<ActionResult> Index()
        {
            var indexVm = new IndexProExpViewModel
            {
                ProExpModels = new List<ProExpModel>(await _proExpService.GetAllProExpFrom(_userModel)) 
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
                ProfileTechModels = new List<ProfileTechModel>(await _proExpService.GetAllTechnoFrom(proExpModel)),
	        };

	        if (proExpModel.ExperienceDescriptionModel != null &&
	            proExpModel.ExperienceDescriptionModel.Count > 0)
	        {
		        detailVm.ExperienceDescriptionModel = proExpModel.ExperienceDescriptionModel[0];
	        }

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
        public async Task<ActionResult> Create(
	        [Bind(Include = "CompanyName, CityName, FromDate, ToDate, ProExpDescription, ProExpName, ProExpPosition")] CreateProExpViewModel proExpViewModel)
        {
	        if (ModelState.IsValid)
	        {
                var proExpToAdd = new ProExpModel
                {
                    CompanyName = proExpViewModel.CompanyName,
                    CityName = proExpViewModel.CityName,
                    FromDate = proExpViewModel.FromDate,
                    ToDate = proExpViewModel.ToDate,
                    ProfileId = _userModel.Id,
					ExperienceDescriptionModel = new List<ExperienceDescriptionModel>()
                };

                var expProDesc = new ExperienceDescriptionModel
                {
	                Description = proExpViewModel.ProExpDescription,
	                PositionDesc = proExpViewModel.ProExpPosition,
	                Name = proExpViewModel.ProExpName
                };

                proExpToAdd.ExperienceDescriptionModel.Add(expProDesc);


				await _proExpService.CreateAsync(proExpToAdd);
		        return RedirectToAction("Index");
	        }

	        return View(proExpViewModel);
        }

        // GET: ProExp/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
	        if (id == null)
	        {
		        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
	        }


	        var proExpModel = await _proExpService.GetProExpByIdAsync(id.Value);

	        if (proExpModel == null)
	        {
		        return HttpNotFound();
	        }

	        var proExptoEdit = new EditProExpViewModel
	        {
                CityName = proExpModel.CityName,
                CompanyName = proExpModel.CompanyName,
                FromDate = proExpModel.FromDate,
                ToDate = proExpModel.ToDate
	        };
            return View(proExptoEdit);
        }

		// POST: ProExp/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Edit([Bind(Include = "CompanyName, CityName, FromDate, ToDate")] EditProExpViewModel proExpViewModel)
		{
			if (ModelState.IsValid)
			{
				var proExpEdited = new ProExpModel
				{
					Id = proExpViewModel.Id,
					CityName = proExpViewModel.CityName,
					CompanyName = proExpViewModel.CompanyName,
					FromDate = proExpViewModel.FromDate,
					ToDate = proExpViewModel.ToDate
				};

				await _proExpService.UpdateAsync(proExpEdited.Id, proExpEdited);
				return RedirectToAction("Index");
			}
			return View(proExpViewModel);
		}

		// GET: ProExp/Delete/5
		public async Task<ActionResult> Delete(int? id)
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

			var deleteVm = new DeleteProExpViewModel
			{
				FromDate = proExpModel.FromDate,
				ToDate = proExpModel.ToDate,
				CityName = proExpModel.CityName,
				CompanyName = proExpModel.CompanyName,
				ProfileTechModels = new List<ProfileTechModel>(await _proExpService.GetAllTechnoFrom(proExpModel)) 
			};

			return View(deleteVm);
		}

		// POST: Admin/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> DeleteConfirmed(int id)
		{
			await _proExpService.RemoveAsync(id);
			return RedirectToAction("Index");
		}
	}
}
