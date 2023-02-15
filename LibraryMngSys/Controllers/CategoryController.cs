using Microsoft.AspNetCore.Mvc;
using LibraryMngSys.Models.Category;
using LibraryMngSys.Wrappers;
using LibraryMngSys.Models.Book;

namespace LibraryMngSys.Controllers
{
    public class CategoryController : Controller
    {
        private readonly CategoryServices categoryServices;

        public CategoryController(CategoryServices _cs)
        {
            categoryServices = _cs;
        }

        //disa te dhena qe nuk shfaqen n front mund ta perdorim viewbag per ti dergu
        public async Task<IActionResult> Index(
            [FromQuery]
            PageRequest<Category> request
            )
        {
            PageResponse<Category> response = await categoryServices.List(request);
            ViewBag.response = response;
            ViewBag.request = request;
            return View("_Table", response);
        }
        public async Task<IActionResult> Details(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await categoryServices.Details(id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        //GET
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        //POST
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            Category cat)
        {
            if (await categoryServices.GetByName(cat.Name) != null)
            {
                ModelState.AddModelError("Name", "Name taken");
            }
          
            if (!ModelState.IsValid)
            {
                return View(cat);
            }

            Category category = new Category()
            {
                Id = cat.Id,
                Name = cat.Name,
                Description = cat.Description,
            };

            await categoryServices.Add(category);
            TempData["success"] = "Category created successfully";
            return RedirectToAction("Index");
        }

        //GET
        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var category = await categoryServices.GetById(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        //PUT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id,
            [Bind("Id,Name,Description")]
            Category category)
        {
            var cat = await categoryServices.GetByName(category.Name);
            if (cat != null && cat.Id != cat.Id)
            {
                ModelState.AddModelError("Name", "Name taken");
            }
           
            if (!ModelState.IsValid)
            {
                return View(cat);
            }
            await categoryServices.Update(category);
            TempData["success"] = "Category updated successfully";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var cat = await categoryServices.GetById(id);
            if (cat == null)
            {
                return NotFound();
            }
            return View(cat);
        }

        //PUT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Category cat)
        {
            var catFromDb =await categoryServices.GetById(cat.Id);

            if (cat == null)
            {
                return NotFound();
            }
            await categoryServices.Delete(catFromDb);
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index");
        }

    }
}
