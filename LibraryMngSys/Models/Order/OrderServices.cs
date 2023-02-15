using LibraryMngSys.Data;
using LibraryMngSys.Models.Book;
using LibraryMngSys.Models.Supplier;
using LibraryMngSys.Wrappers;
using X.PagedList;

namespace LibraryMngSys.Models.Order
{
    public class OrderServices : IBaseServices<Order>
    {
        private readonly LibraryMngSysContext _db;

        private readonly OrderUtility _orderUtility;

        private readonly SupplierServices _supplierServices;

        private readonly BookServices _bookServices;

        public OrderServices(LibraryMngSysContext db, OrderUtility orderUtility, SupplierServices supplierServices, BookServices bookServices)
        {
            _db = db;
            _orderUtility = orderUtility;
            _supplierServices = supplierServices;
            _bookServices = bookServices;
        }

        public async Task<PageResponse<Order>> List(PageRequest<Order> request)
        {
            if (request.SortColumn == null)
            {
                request.SortColumn = "Id";
            }
            if (!new OrderUtility().OrderUtils.ContainsKey(request.SortColumn))
            {
                request.SortColumn = "Id";
            }
            IEnumerable<Order> objOrderList = await _db.Order.ToListAsync();

            if (!String.IsNullOrEmpty(request.FilterString))
            {
                objOrderList = objOrderList.Where(
                    u => u.book.Contains(request.FilterString) ||
                    u.Supplier.Author.Contains(request.FilterString));

            }
            int totalCount = objOrderList.Count();
            if (request.SortDirection == "DESC")
            {
                objOrderList = objOrderList.OrderByDescending(_orderUtility.OrderUtils[request.SortColumn]);
            }
            else
            {
                objOrderList = objOrderList.OrderBy(_orderUtility.OrderUtils[request.SortColumn]);
            }
            if (request.Page <= 0) request.Page = 1;
            if (request.Size <= 0) request.Size = 5;

            int totalPages = (int)Math.Ceiling((decimal)(totalCount / request.Size)) + 1;
            if (totalCount == (totalPages - 1) * request.Size)
            {
                totalPages--;
            }
            return new PageResponse<Order>
            {
                data = objOrderList.ToPagedListAsync(request.Page, request.Size),
                utilities = _orderUtility,
                header = _orderUtility.header,
                SortDirection = request.SortDirection,
                SortColumn = request.SortColumn,
                TotalCount = totalCount,
                Size = request.Size,
                Page = request.Page,
                TotalPages = totalPages,
                FilterString = request.FilterString,
            };

        }

        public Task<Order> Add(Order entity)
        {
            throw new NotImplementedException();
        }

        public Task<Order> Delete(Order entity)
        {
            throw new NotImplementedException();
        }

        public Task<Order> Details(string Id)
        {
            throw new NotImplementedException();
        }

        public Task<Order> Details(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Task<Order> GetById(Guid Id)
        {
            throw new NotImplementedException();
        }

        

        public Task<Order> Update(Order entity)
        {
            throw new NotImplementedException();
        }
    }
}
