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
        public async Task<ActionResult> Create([Bind(Include = "FirstName,LastName,Email,Password,RoleNumber,DateOfBirth")] CreateProfileViewModel profileViewModel)
        {
	        if (ModelState.IsValid)
	        {
		        var profilToAdd = new ProfileModel()
		        {
			        Email = profileViewModel.Email,
			        Role = (Role)profileViewModel.RoleNumber,
			        FirstName = profileViewModel.FirstName,
			        LastName = profileViewModel.LastName,
                    Password = profileViewModel.Password,
					DateOfBirth = profileViewModel.DateOfBirth
		        };

		        await _profileService.CreateAsync(profilToAdd);
		        return RedirectToAction("Index");
	        }

	        return View(profileViewModel);
        }

        // GET: Admin/Edit/5
        public async Task<ActionResult> Edit(int? id)
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

			var profileToEdit = new EditProfileViewModel()
			{
				Id = profileModel.Id,
				Email = profileModel.Email,
				Password = profileModel.Password,
				RoleNumber = (int) profileModel.Role,
				FirstName = profileModel.FirstName,
				LastName = profileModel.LastName,
				DateOfBirth = profileModel.DateOfBirth
			};

			return View(profileToEdit);
		}

        // POST: Admin/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
		public async Task<ActionResult> Edit([Bind(Include = "Id,FirstName,LastName,Email,Password,RoleNumber,DateOfBirth")] EditProfileViewModel profileModel)
        {
			if (ModelState.IsValid)
			{
				var profileEdited = new ProfileModel
				{
					Id = profileModel.Id,
					Email = profileModel.Email,
					Password = profileModel.Password,
					Role = (Role)profileModel.RoleNumber,
					FirstName = profileModel.FirstName,
					LastName = profileModel.LastName,
					DateOfBirth = profileModel.DateOfBirth
				};

				await _profileService.UpdateAsync(profileEdited.Id, profileEdited);
				return RedirectToAction("Index");
			}
			return View(profileModel);
		}

        // GET: Admin/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
	        if (id == null)
	        {
		        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
	        }

	        ProfileModel tempModel = await _profileService.GetProfileByIdAsync(id);

	        if (tempModel == null)
	        {
		        return HttpNotFound();
	        }

	        var deleteVm = new DeleteProfileViewModel
	        {
		        Email = tempModel.Email,
		        RoleName = tempModel.RoleName,
		        FirstName = tempModel.FirstName,
		        LastName = tempModel.LastName
            };

	        return View(deleteVm);
        }

        // POST: Admin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
		public async Task<ActionResult> DeleteConfirmed(int id)
        {
			await _profileService.RemoveProfileAsync(id);
			return RedirectToAction("Index");
		}
    }
}
