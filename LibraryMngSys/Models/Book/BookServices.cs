using LibraryMngSys.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LibraryMngSys.Wrappers;
using X.PagedList;
using LibraryMngSys.Models.User;
using LibraryMngSys.Models.Category;

namespace LibraryMngSys.Models.Book
{
    public class BookServices: IBaseServices<Book>
    {
        private readonly LibraryMngSysContext _db;

        private readonly  BookUtility _BookUtility;

        public BookServices(LibraryMngSysContext db, BookUtility BookUtility)
        {
            _db = db;
            _BookUtility = BookUtility;
        }
        public async Task<PageResponse<Book>> List(PageRequest<Book> request)
        {
            if (request.SortColumn == null)
            {
                request.SortColumn = "Id";
            }
            if (!new BookUtility().BookUtils.ContainsKey(request.SortColumn))
            {
                request.SortColumn = "Id";
            }
            IEnumerable<Book> objBookList = await _db.Book.ToListAsync();

            if (!String.IsNullOrEmpty(request.FilterString))
            {
                objBookList = objBookList.Where(
                    u => u.Title.Contains(request.FilterString) ||
                    u.Author.Contains(request.FilterString));
                   
            }
           int totalCount = objBookList.Count();
           if (request.SortDirection == "DESC")
            {
                objBookList = objBookList.OrderByDescending(_BookUtility.BookUtils[request.SortColumn]);
            }
            else
            {
                objBookList = objBookList.OrderBy(_BookUtility.BookUtils[request.SortColumn]);
            }
            if (request.Page <= 0) request.Page = 1;
            if (request.Size <= 0) request.Size = 5;

            int totalPages = (int)Math.Ceiling((decimal)(totalCount / request.Size)) + 1;
            if (totalCount == (totalPages - 1) * request.Size)
            {
                totalPages--;
            }
            return new PageResponse<Book>
            {
                data = objBookList.ToPagedListAsync(request.Page, request.Size),
                utilities = _BookUtility,
                header = _BookUtility.header,
                SortDirection = request.SortDirection,
                SortColumn = request.SortColumn,
                TotalCount = totalCount,
                Size = request.Size,
                Page = request.Page,
                TotalPages = totalPages,
                FilterString = request.FilterString,
            };

        }

        public async Task<Book> Details(Guid Id)
        {
            var book = await _db.Book
                .FirstOrDefaultAsync(u => u.Id == Id);
            return book;
        }
        public async Task<Book?> GetByTitle(string title)
        {
            var book = await _db.Book.AsNoTracking().FirstOrDefaultAsync(x => x.Title == title);
            return book;
        }
        public async Task<Book> Add(Book book)
        {
            book.createdAt = DateTime.Now.ToUniversalTime();
            book.Id = Guid.NewGuid();
            book.updatedAt = DateTime.Now.ToUniversalTime();
            _db.Book.Add(book);
            await _db.SaveChangesAsync();
            return book;
        }
        public async Task<Book?> GetById(Guid guid)
        {
            return await _db.Book.FirstOrDefaultAsync(u => u.Id == guid);
        }

        public async Task<Book?> Update(Book book)
        {
            book.updatedAt = DateTime.Now.ToUniversalTime();
            _db.Book.Update(book);
            await _db.SaveChangesAsync();
            return book;
        }
        public async Task<Book> Delete(Book book)
        {
            _db.Book.Remove(book);
            await _db.SaveChangesAsync();
            return book;
        }

        public async Task<List<SelectListItem>> GetAllCategories()
        {
            var ListCategories = new List<SelectListItem>();
            List<Category.Category> Categories = await _db.Category.ToListAsync();
            ListCategories = Categories.Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.Name,
            }).ToList();
            return ListCategories;
        }

        public Task<Book> Details(string Id)
        {
            return null;
        }
    }
}
