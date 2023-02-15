using Microsoft.AspNetCore.Mvc;
using LibraryMngSys.Models.Shop;
using LibraryMngSys.Wrappers;

namespace LibraryMngSys.Controllers
{
    public class ShopController : Controller
    {
        private readonly ShopServices shopServices;

        public ShopController(ShopServices _shs)
        {
            shopServices = _shs;
        }

        public async Task<IActionResult> Index(
            [FromQuery]
            PageRequest<Shop> request
            )
        {
            PageResponse<Shop> response = await shopServices.List(request);
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

            var shop = await shopServices.Details(id);
            if (shop == null)
            {
                return NotFound();
            }

            return View(shop);
        }
        //GET
        public IActionResult Create()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Shop shop)
        {
            if (!ModelState.IsValid)
            {
                return View(shop);
            }

            await shopServices.Add(shop);
            TempData["success"] = "User created successfully";
            return RedirectToAction("Index");
        }

        //GET
        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var shop = await shopServices.GetById(id);
            if (shop == null)
            {
                return NotFound();
            }
            return View(shop);
        }

        //PUT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id,
            [Bind("Id,Name,Address,Location,Name")]
            Shop shop)
        {
            if (!ModelState.IsValid)
            {
                return View(shop);
            }
            await shopServices.Update(shop);
            TempData["success"] = "User updated successfully";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var user = await shopServices.GetById(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }


        //PUT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Shop shop)
        {
            if (shop == null)
            {
                return NotFound();
            }
            await shopServices.Delete(shop);
            TempData["success"] = "User deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
