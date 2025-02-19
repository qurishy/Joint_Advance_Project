using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Data_Transfer_API.DATA.Service_Db
{
    public class MongoService : IMongoService 
    {
        private readonly IMongoDatabase _database;
      
    
       

        public MongoService(IMongoClient client, IOptions<MongoDb_Setting> settings)
        {
            

            var client1 = new MongoClient(settings.Value.ConnectionString);

            _database = client.GetDatabase(settings.Value.DatabaseName);
        }


        public async Task< IMongoCollection<T>> CreateCollection<T>(string? collectionName)
        {
          if(collectionName != null)
            {
                await _database.CreateCollectionAsync(collectionName, new CreateCollectionOptions<T>());

                return _database. GetCollection<T>(collectionName);
                
            }

            return null;

        }

        public async Task< IMongoCollection<T>> GetCollections<T>(string collectionName)
        {
            var _CatagoryCollection = _database.GetCollection<T>(collectionName);

            return _CatagoryCollection;
        }
    }
}
