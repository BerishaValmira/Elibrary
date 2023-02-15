using Microsoft.AspNetCore.Mvc;
using LibraryMngSys.Models.Book;
using LibraryMngSys.Wrappers;
using Microsoft.AspNetCore.Mvc.Rendering;
using LibraryMngSys.ViewModels;
using LibraryMngSys.Models.Category;

namespace LibraryMngSys.Controllers
{
    public class BookController : Controller
    {

        private readonly BookServices bookServices;

        private readonly CategoryServices categoryServices;

        public BookController(BookServices _bs, CategoryServices _categoryServices)
        {
            bookServices = _bs;
            categoryServices = _categoryServices;
        }
        public async Task<IActionResult> Index(
            [FromQuery]
            PageRequest<Book> request
            )
        {
            PageResponse<Book> response = await bookServices.List(request);
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
            var book = await bookServices.Details(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        //GET
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var categories = await bookServices.GetAllCategories();
                

            return View(new AddBookCategory
            {
                Categories = categories
            });
        }

        //POST
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            AddBookCategory addBookCategory)
        {
            if (await bookServices.GetByTitle(addBookCategory.Book.Title) != null)
            {
                ModelState.AddModelError("Title", "Title taken");
            }
            if (!ModelState.IsValid && addBookCategory.Categories!=null)
            {
                return View(addBookCategory.Book);
            }

            var assfdsf = addBookCategory.SelectedCategory;

            var guid = Guid.Parse(addBookCategory.SelectedCategory);

            var cat = await categoryServices.GetById(guid);


            Book bookk = new Book
            {
                Id = Guid.NewGuid(),
                Title = addBookCategory.Book.Title,
                Category = cat.Name,
                createdAt = DateTime.Now,
                Author = addBookCategory.Book.Author,
                updatedAt = DateTime.Now,
                TypeBookCategory = cat
            };

            await bookServices.Add(bookk);
            TempData["success"] = "Book created successfully";
            return RedirectToAction("Index");
        }
        //GET
        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var book = await bookServices.GetById(id);
            if (book == null)
            {
                return NotFound();
            }
            var categories = await bookServices.GetAllCategories();

            var selectedCat = categories.FirstOrDefault(x => x.Value == book.Category);

            if (selectedCat == null)
            {
                selectedCat = new SelectListItem
                {
                    Value= "No data",
                };
            }

            return View(new AddBookCategory
            {
                Book = book,
                Categories = categories,
                SelectedCategory = selectedCat.Value
            });
        }
        //PUT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AddBookCategory addBookCategory)
        {
            var book = addBookCategory.Book;
            var bs =await bookServices.GetByTitle(book.Title);
            if (bs != null && bs.Id != book.Id)
            {
                ModelState.AddModelError("Title", "Title taken");
            }
            if (!ModelState.IsValid && addBookCategory.Categories != null)
            {
                return View(addBookCategory);
            }
            var category = await categoryServices.GetById(Guid.Parse(addBookCategory.SelectedCategory));
            book.TypeBookCategory = category;
            book.Category = category.Name;
            await bookServices.Update(book);
            TempData["success"] = "Book updated successfully";
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(Guid id)
        {
            var book = await bookServices.GetById(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        //PUT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Book book)
        {
            if (book == null)
            {
                return NotFound();
            }
            await bookServices.Delete(book);
            TempData["success"] = "Book deleted successfully";
            return RedirectToAction("Index");
        }

    }
}
