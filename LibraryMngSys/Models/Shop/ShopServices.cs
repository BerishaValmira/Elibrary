using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LibraryMngSys.Data;
using LibraryMngSys.Wrappers;
using X.PagedList;
using LibraryMngSys.Models.Category;

namespace LibraryMngSys.Models.Shop
{
    public class ShopServices: IBaseServices<Shop>
    {
        private readonly LibraryMngSysContext _db;

        private readonly ShopUtility _shu;

        public ShopServices(LibraryMngSysContext db, ShopUtility shopUtility)
        {
            _db = db;
            _shu = shopUtility;
        }


        public async Task<PageResponse<Shop>> List(PageRequest<Shop> request)
        {
            if (request.SortColumn == null)
            {
                request.SortColumn = "Id";
            }
            if (!_shu.ShopUtils.ContainsKey(request.SortColumn))
            {
                request.SortColumn = "Id";
            }

            IEnumerable<Shop> objList = await _db.Shop.ToListAsync();


            if (!String.IsNullOrEmpty(request.FilterString))
            {
                objList = objList.Where(
                u => u.Name.Contains(request.FilterString) ||
                u.Address.Contains(request.FilterString) ||
                u.Location.Contains(request.FilterString));
            }
            int totalCount = objList.Count();

            if (request.SortDirection == "DESC")
            {
                objList = objList.OrderByDescending(_shu.ShopUtils[request.SortColumn]);
            }
            else
            {
                objList = objList.OrderBy(_shu.ShopUtils[request.SortColumn]);
            }

            if (request.Page <= 0) request.Page = 1;

            if (request.Size <= 0) request.Size = 5;

            int totalPages = (int)Math.Ceiling((decimal)(totalCount / request.Size)) + 1;
            if (totalCount == (totalPages - 1) * request.Size)
            {
                totalPages--;
            }

            return new PageResponse<Shop>
            {
                data = objList.ToPagedListAsync(request.Page, request.Size),
                utilities = _shu,
                header = _shu.header,
                SortDirection = request.SortDirection,
                SortColumn = request.SortColumn,
                TotalCount = totalCount,
                Size = request.Size,
                Page = request.Page,
                TotalPages = totalPages,
                FilterString = request.FilterString,
            };

        }

        public async Task<Shop> Details(Guid Id)
        {
            var shop = await _db.Shop
                .FirstOrDefaultAsync(u => u.Id == Id);
            return shop;
        }

        public async Task<Shop> Add(Shop shop)
        {
            shop.OpeningDate = DateTime.Now.ToUniversalTime();
            shop.Id = Guid.NewGuid();
            _db.Shop.Add(shop);
            await _db.SaveChangesAsync();
            return shop;
        }

        public async Task<Shop> GetById(Guid guid)
        {
            return await _db.Shop.FirstOrDefaultAsync(u => u.Id == guid);
        }

        public async Task<Shop?> Update(Shop shop)
        {
            _db.Shop.Update(shop);
            await _db.SaveChangesAsync();
            return shop;
        }

        public async Task<Shop> Delete(Shop shop)
        {
            _db.Shop.Remove(shop);
            await _db.SaveChangesAsync();
            return shop;
        }

        public Task<Shop> Details(string Id)
        {
            return null;
        }
    }
}
