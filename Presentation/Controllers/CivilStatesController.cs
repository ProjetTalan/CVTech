using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Application.Interface;
using Application.Models;
using Presentation.ViewModels;

namespace Presentation.Controllers
{
	[AuthorizeCustom("Admin", "Consultant", "Recruiter")]
    public class CivilStatesController : Controller
    {
	    private readonly ICivilStateService _civilStateService;
	    public CivilStatesController(ICivilStateService civilStateService)
	    {
		    _civilStateService = civilStateService;
	    }
        // GET: CivilStates
        public async Task<ActionResult> Index()
        {
            var indexVm = new IndexCivilStateViewModel
            {
                CivilStates = new List<CivilStateModel>(await _civilStateService.GetAllCivilStatesAsync()) 
            };
            return View(indexVm);
        }

        // GET: CivilStates/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            CivilStateModel tempModel = await _civilStateService.GetCivilStateByIdAsync(id);

            if (tempModel == null)
            {
	            return HttpNotFound();
            }
            var detailVm = new DetailsCivilStateViewModel
            {
                Id = tempModel.Id,
	            Address = tempModel.Address,
	            DateOfBirth = tempModel.DateOfBirth,
	            FirstName = tempModel.FirstName,
	            LastName = tempModel.LastName
            };

            return View(detailVm);
        }

        // GET: CivilStates/Create
        public ActionResult Create()
        {
            var addCivilVm = new AddCivilStateViewModel();
            return View(addCivilVm);
        }

        // POST: CivilStates/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "FirstName,LastName,Address,DateOfBirth")] AddCivilStateViewModel civilState)
        {
            if (ModelState.IsValid)
            {
                var civilStateToAdd = new CivilStateModel
                {
                    Address = civilState.Address,
                    DateOfBirth = civilState.DateOfBirth,
                    FirstName = civilState.FirstName,
                    LastName = civilState.LastName
                };

                await _civilStateService.CreateAsync(civilStateToAdd);
                return RedirectToAction("Index");
            }

            return View(civilState);
        }

        // GET: CivilStates/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            CivilStateModel model = await _civilStateService.GetCivilStateByIdAsync(id);

            if (model == null)
            {
                return HttpNotFound();
            }

            var civilStateToEdit = new EditCivilStateViewModel
            {
                Id = model.Id,
                Address = model.Address,
                DateOfBirth = model.DateOfBirth,
                FirstName = model.FirstName,
                LastName = model.LastName
            };

            return View(civilStateToEdit);
        }

        // POST: CivilStates/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,FirstName,LastName,Address,DateOfBirth")] EditCivilStateViewModel civilState)
        {
            if (ModelState.IsValid)
            {
	            var civilStateEdited = new CivilStateModel
	            {
                    Id = civilState.Id,
		            Address = civilState.Address,
		            DateOfBirth = civilState.DateOfBirth,
		            FirstName = civilState.FirstName,
		            LastName = civilState.LastName
	            };

	            await _civilStateService.UpdateAsync(civilStateEdited.Id, civilStateEdited);
                return RedirectToAction("Index");
            }
            return View(civilState);
        }

        // GET: CivilStates/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            CivilStateModel tempModel = await _civilStateService.GetCivilStateByIdAsync(id);

            if (tempModel == null)
            {
                return HttpNotFound();
            }

            var deleteVm = new DeleteCivilStateViewModel
            {
	            Address = tempModel.Address,
	            DateOfBirth = tempModel.DateOfBirth,
	            FirstName = tempModel.FirstName,
	            LastName = tempModel.LastName
            };

            return View(deleteVm);
        }

        // POST: CivilStates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
	        await _civilStateService.RemoveCivilStateAsync(id);
            return RedirectToAction("Index");
        }
    }
}
