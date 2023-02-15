using LibraryMngSys.Data;
using LibraryMngSys.Wrappers;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace LibraryMngSys.Models.Supplier
{
    public class SupplierServices: IBaseServices<Supplier>
    {
        private readonly LibraryMngSysContext _db;
        private readonly SupplierUtility _spu;

        public SupplierServices(LibraryMngSysContext db, SupplierUtility supplierUtility)
        {
            _db = db;
            _spu = supplierUtility;
        }



        public async Task<PageResponse<Supplier>> List(PageRequest<Supplier> request)
        {
            if (request.SortColumn == null)
            {
                request.SortColumn = "Id";
            }
            if (!_spu.SupplierUtils.ContainsKey(request.SortColumn))
            {
                request.SortColumn = "Id";
            }

            IEnumerable<Supplier> objList = await _db.Supplier.ToListAsync();


            if (!String.IsNullOrEmpty(request.FilterString))
            {
                objList = objList.Where(
                u => u.Name.Contains(request.FilterString) ||
                u.Address.Contains(request.FilterString) ||
                u.Email.Contains(request.FilterString) ||
                u.ContactNumber.Contains(request.FilterString));
            }
            int totalCount = objList.Count();

            if (request.SortDirection == "DESC")
            {
                objList = objList.OrderByDescending(_spu.SupplierUtils[request.SortColumn]);
            }
            else
            {
                objList = objList.OrderBy(_spu.SupplierUtils[request.SortColumn]);
            }

            if (request.Page <= 0) request.Page = 1;

            if (request.Size <= 0) request.Size = 5;

            int totalPages = (int)Math.Ceiling((decimal)(totalCount / request.Size)) + 1;
            if (totalCount == (totalPages - 1) * request.Size)
            {
                totalPages--;
            }

            return new PageResponse<Supplier>
            {
                data = objList.ToPagedListAsync(request.Page, request.Size),
                header = _spu.header,
                utilities = _spu,
                SortDirection = request.SortDirection,
                SortColumn = request.SortColumn,
                TotalCount = totalCount,
                Size = request.Size,
                Page = request.Page,
                TotalPages = totalPages,
                FilterString = request.FilterString,
            };

        }

        public async Task<Supplier?> Details(Guid Id)
        {
            return await _db.Supplier.FirstOrDefaultAsync(x => x.Id == Id);
        }

        public async Task<Supplier> Add(Supplier supplier)
        {
            supplier.createdAt = DateTime.Now.ToUniversalTime();
            supplier.Id = Guid.NewGuid();
            supplier.updatedAt = DateTime.Now.ToUniversalTime();
            _db.Supplier.Add(supplier);
            await _db.SaveChangesAsync();
            return supplier;
        }

        public async Task<Supplier?> GetById(Guid guid)
        {
            return await _db.Supplier.FirstOrDefaultAsync(u => u.Id == guid);
        }

        public async Task<Supplier?> Update(Supplier supplier)
        {
            _db.Supplier.Update(supplier);
            await _db.SaveChangesAsync();
            return supplier;
        }
        // duhet me ja shtu edhe me hek orders 
        public async Task<Supplier> Delete(Supplier supplier)
        {
            _db.Supplier.Remove(supplier);
            await _db.SaveChangesAsync();
            return supplier;
        }

        public Supplier? GetByName(string name)
        {
            var supplier = _db.Supplier.AsNoTracking().FirstOrDefault(x => x.Name == name);
            return supplier;
        }

        public Task<Supplier> Details(string Id)
        {
            return null;
        }
    }
}

