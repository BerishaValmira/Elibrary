using LibraryMngSys.Areas.Identity.Data;
using LibraryMngSys.Data;
using Microsoft.EntityFrameworkCore;
using LibraryMngSys.Wrappers;
using X.PagedList;

namespace LibraryMngSys.Models.User
{
    public class UserServices: IBaseServices<LibraryMngSysUser>
    {

        private readonly LibraryMngSysContext _db;

        private readonly UserUtility _userUtility;

        public UserServices(LibraryMngSysContext db, UserUtility userUtility)
        {
            _db = db;
            _userUtility = userUtility;
        }


        public async Task<PageResponse<LibraryMngSysUser>> List(PageRequest<LibraryMngSysUser> request)
        {
            if (request.SortColumn == null)
            {
                request.SortColumn = "Id";
            }
            if (!new UserUtility().userUtils.ContainsKey(request.SortColumn))
            {
                request.SortColumn = "Id";
            }

            IEnumerable<LibraryMngSysUser> objUserList = await _db.Users.ToListAsync();


            if (!String.IsNullOrEmpty(request.FilterString))
            {
                objUserList = objUserList.Where(
                u => u.Name.Contains(request.FilterString) ||
                u.Surname.Contains(request.FilterString) ||
                u.Email.Contains(request.FilterString));
            }
            int totalCount = objUserList.Count();
            
            if (request.SortDirection == "DESC")
            {
                objUserList = objUserList.OrderByDescending(_userUtility.userUtils[request.SortColumn]);
            }
            else
            {
                objUserList = objUserList.OrderBy(_userUtility.userUtils[request.SortColumn]);
            }

            if (request.Page <= 0) request.Page = 1;

            if (request.Size <= 0) request.Size = 5;

            int totalPages = (int)Math.Ceiling((decimal)(totalCount / request.Size)) + 1;
            if (totalCount == (totalPages - 1) * request.Size)
            {
                totalPages--;
            }

            return new PageResponse<LibraryMngSysUser>
            {
                data = objUserList.ToPagedListAsync(request.Page, request.Size),
                utilities = _userUtility,
                header = _userUtility.header,
                SortDirection = request.SortDirection,
                SortColumn = request.SortColumn,
                TotalCount = totalCount,
                Size = request.Size,
                Page = request.Page,
                TotalPages = totalPages,
                FilterString = request.FilterString,
            };

        }

        public async Task<LibraryMngSysUser?> Details(string Id)
        {
            return await _db.Users
                .FirstOrDefaultAsync(u => u.Id == Id);
        }

        public async Task<LibraryMngSysUser> GetByEmail(string email)
        {
            var LibraryMngSysUser = await _db.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Email == email);
            return LibraryMngSysUser;
        }

        public async Task<LibraryMngSysUser> Add(LibraryMngSysUser LibraryMngSysUser)
        {
            _db.Users.Add(LibraryMngSysUser);
            await _db.SaveChangesAsync();
            return LibraryMngSysUser;
        }

        public async Task<LibraryMngSysUser?> GetById(string id)
        {
            return await _db.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<LibraryMngSysUser?> Update(LibraryMngSysUser libraryMngSysUser)
        {
            _db.Users.Update(libraryMngSysUser);
            await _db.SaveChangesAsync();
            return libraryMngSysUser;
        }

        public async Task<LibraryMngSysUser> Delete(LibraryMngSysUser LibraryMngSysUser)
        {
            _db.Users.Remove(LibraryMngSysUser);
            await _db.SaveChangesAsync();
            return LibraryMngSysUser;
        }

        public Task<LibraryMngSysUser> Details(Guid Id)
        {
            return null;
        }

        public Task<LibraryMngSysUser> GetById(Guid Id)
        {
            return null;
        }
    }
}
