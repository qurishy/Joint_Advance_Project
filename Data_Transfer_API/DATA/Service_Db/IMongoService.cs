using MongoDB.Driver;

namespace Data_Transfer_API.DATA.Service_Db
{
    public interface IMongoService
    {
        
       Task< IMongoCollection<T>> GetCollections<T>(string collectionName);

       Task< IMongoCollection<T>> CreateCollection<T>(string collectionName);


    }
}
