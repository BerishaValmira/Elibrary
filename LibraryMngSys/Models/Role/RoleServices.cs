using LibraryMngSys.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LibraryMngSys.Models.Role
{
    public class RoleServices
    {
        private readonly LibraryMngSysContext _db;

        public RoleServices(LibraryMngSysContext db)
        {
            _db = db;
        }

        public async Task<ICollection<IdentityRole>> List()
        {
            return await _db.Roles.ToListAsync();
        }
    }
}
