
using Data_Transfer_API.DATA.Service_Db;
using MongoDB.Driver;

namespace Data_Transfer_API.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    { 
        private readonly IMongoService _mongoService; //creating a private instance of IMongoService
        private  IMongoCollection<T>? _collection; //creating a global instance of IMongoCollection


        public   Repository(IMongoService mongoService)
        {
            _mongoService = mongoService;

            InitializeRepository().GetAwaiter().GetResult();
        }

        private async Task InitializeRepository()
        {
            _collection = await _mongoService.GetCollections<T>(typeof(T).Name);

            if (_collection == null)
            {
                await _mongoService.CreateCollection<T>(typeof(T).Name);
            }
        }


        //this method is used to insert data when we create a new object
        public async Task CreateAsync(T entity)
        {
          try
            {
                if (_collection == null)
                {
                    throw new Exception("Collection is null");
                }
                 await _collection.InsertOneAsync(entity);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }
        }

        //this method is used to delete data 
        public async Task DeleteAsync( Guid id)
        {
           try
            {
                if (_collection == null)
                {
                    throw new Exception("Collection is null");
                }
                var filter = Builders<T>.Filter.Eq("Id", id);

                await _collection.DeleteOneAsync(filter);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                
            }
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            try
            {
                if (_collection == null)
                {
                    throw new Exception("Collection is null");
                }

                return await _collection.Find(_ => true).ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
           
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
           try
            {
                if (_collection == null)
                {
                    throw new Exception("Collection is null");
                }

                var filter = Builders<T>.Filter.Eq("Id", id);
                return await _collection.Find(filter).FirstOrDefaultAsync();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }
    }
}
