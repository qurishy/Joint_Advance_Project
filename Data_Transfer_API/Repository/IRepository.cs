namespace Data_Transfer_API.Repository
{
    public interface IRepository<T> where T : class
    {
            Task<IEnumerable<T>> GetAllAsync();

            Task<T> GetByIdAsync( string id);

            Task CreateAsync( T entity);

           // Task UpdateAsync(string collectionName, Guid id, T entity);

            Task DeleteAsync( string id);

        

    }
}
