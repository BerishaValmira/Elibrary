using Microsoft.EntityFrameworkCore;
using LibraryMngSys.Data;
using LibraryMngSys.Wrappers;
using X.PagedList;
using LibraryMngSys.Models.Book;

namespace LibraryMngSys.Models.Category
{
    public class CategoryServices: IBaseServices<Category>
    {
        private readonly LibraryMngSysContext _db;

        private readonly CategoryUtility _CategoryUtility;


        public CategoryServices(LibraryMngSysContext db, CategoryUtility CategoryUtility)
        {
            _db = db;
            _CategoryUtility = CategoryUtility;
        }

        public async Task<PageResponse<Category>> List(PageRequest<Category> request)
        {
            if (request.SortColumn == null)
            {
                request.SortColumn = "Id";
            }
            if (!new CategoryUtility().CategoryUtils.ContainsKey(request.SortColumn))
            {
                request.SortColumn = "Id";
            }

            IEnumerable<Category> objCategoryList = await _db.Category.ToListAsync();


            if (!String.IsNullOrEmpty(request.FilterString))
            {
                objCategoryList = objCategoryList.Where(
                u => u.Name.Contains(request.FilterString) ||
                u.Description.Contains(request.FilterString));


            }
            int totalCount = objCategoryList.Count();


            if (request.SortDirection == "DESC")
            {
                objCategoryList = objCategoryList.OrderByDescending(_CategoryUtility.CategoryUtils[request.SortColumn]);
            }
            else
            {
                objCategoryList = objCategoryList.OrderBy(_CategoryUtility.CategoryUtils[request.SortColumn]);
            }

            if (request.Page <= 0) request.Page = 1;

            if (request.Size <= 0) request.Size = 5;

            int totalPages = (int)Math.Ceiling((decimal)(totalCount / request.Size)) + 1;
            if (totalCount == (totalPages - 1) * request.Size)
            {
                totalPages--;
            }

            return new PageResponse<Category>
            {
                data = objCategoryList.ToPagedListAsync(request.Page, request.Size),
                utilities = _CategoryUtility,
                header = _CategoryUtility.header,
                SortDirection = request.SortDirection,
                SortColumn = request.SortColumn,
                TotalCount = totalCount,
                Size = request.Size,
                Page = request.Page,
                TotalPages = totalPages,
                FilterString = request.FilterString,
            };

        }
        public async Task<Category> Details(Guid Id)
        {
            return await _db.Category
                .FirstOrDefaultAsync(u => u.Id == Id);
            // books
        }
        public async Task<Category> Add(Category category)
        {
            category.Id = Guid.NewGuid();
            _db.Category.Add(category);
            await _db.SaveChangesAsync();
            return category;
        }
         
        public async Task<Category> GetById(Guid guid)
        {
            return await _db.Category.FirstOrDefaultAsync(u => u.Id == guid);
        }
        public async Task<Category> Update(Category category)
        {
            var books = await _db.Book.Where(b => b.Category == category.Name)
                        .ToListAsync();

            books.ForEach(x =>
            {
                x.TypeBookCategory = null;
                x.Category = "No Data";
            });
            _db.Book.UpdateRange(books);
            _db.Category.Update(category);
            await _db.SaveChangesAsync();
            return category;
        }
        public async Task<Category> Delete(Category category)
        {
            var books = await _db.Book.Where(b => b.Category == category.Name)
                        .ToListAsync();

            books.ForEach(x =>
            {
                x.TypeBookCategory = null;
                x.Category = "No Data";
            });
            var sdfsdf = books;

            _db.Book.UpdateRange(books);
            _db.Category.Remove(category);
            await _db.SaveChangesAsync();
            return category;
        }

        //
        public async Task<Category> GetByName(string name)
        {
            var category =await _db.Category.AsNoTracking().FirstOrDefaultAsync(x => x.Name == name);
            return category;
        }

        public Task<Category>? Details(string Id)
        {
            return null;
        }
    }
}



