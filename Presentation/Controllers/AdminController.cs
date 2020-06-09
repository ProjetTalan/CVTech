using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Application.Interface;
using Application.Models;
using Domaine.Entities;
using Presentation.ViewModels.AdminViewModels;

namespace Presentation.Controllers
{
	[AuthorizeCustom("Admin")]
    public class AdminController : Controller
    {
	    private IProfileService _profileService;

	    public AdminController(IProfileService profileService)
	    {
		    _profileService = profileService;
	    }
        // GET: Admin
        public async Task<ActionResult> Index()
        {
	        var indexVm = new IndexAdminViewModel()
	        {
		        ProfileModels = new List<ProfileModel>(await _profileService.GetAllProfilesAsync())
	        };
	        return View(indexVm);
        }

        // GET: Admin/Details/5
        public async Task<ActionResult> Details(int? id)
        {
	        if (id == null)
	        {
		        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
	        }

	        ProfileModel profileModel = await _profileService.GetProfileByIdAsync(id);

	        if (profileModel == null)
	        {
		        return HttpNotFound();
            }

	        var detailVm = new DetailsAdminViewModel()
	        {
		        Id = profileModel.Id,
		        Email = profileModel.Email,
		        RoleName = profileModel.RoleName,
		        FirstName = profileModel.FirstName,
		        LastName = profileModel.LastName
	        };
	        return View(detailVm);
        }

        // GET: Admin/Create
        public ActionResult Create()
        {
	        var createProfilViewModel = new CreateProfileViewModel();

            return View(createProfilViewModel);
        }

        // POST: Admin/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "FirstName,LastName,Email,Password,RoleNumber")] CreateProfileViewModel profileViewModel)
        {
	        if (ModelState.IsValid)
	        {
		        var profilToAdd = new ProfileModel()
		        {
			        Email = profileViewModel.Email,
			        Role = (Role)profileViewModel.RoleNumber,
			        FirstName = profileViewModel.FirstName,
			        LastName = profileViewModel.LastName,
                    Password = profileViewModel.Password
		        };

		        await _profileService.CreateAsync(profilToAdd);
		        return RedirectToAction("Index");
	        }

	        return View(profileViewModel);
        }

        // GET: Admin/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Admin/Edit/5
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

        // GET: Admin/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/Delete/5
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
