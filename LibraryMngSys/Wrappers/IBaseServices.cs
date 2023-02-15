namespace LibraryMngSys.Wrappers
{
    public interface IBaseServices<T>
    {
        public Task<PageResponse<T>> List(PageRequest<T> request);

        public Task<T> Details(string Id);

        public Task<T> Details(Guid Id);

        public Task<T> GetById(Guid Id);

        public Task<T> Delete(T entity);

        public Task<T> Add(T entity);

        public Task<T> Update(T entity);
    } 
}
