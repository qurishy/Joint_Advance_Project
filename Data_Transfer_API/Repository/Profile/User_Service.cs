using Data_Transfer_API.DATA.Service_Db;
using Data_Transfer_API.Model;
using MongoDB.Driver;

namespace Data_Transfer_API.Repository.Profile
{
    public class User_Service : Repository<User_Info>, IUser_Service
    {
        private readonly IMongoCollection<User_Info>? _collection;



        public User_Service(IMongoService mongoService) : base(mongoService)
        {
            _collection =  mongoService.GetCollections<User_Info>(typeof(User_Info).Name).GetAwaiter().GetResult();

            if (_collection == null)
            {
                mongoService.CreateCollection<User_Info>(typeof(User_Info).Name);
            }
            
        }

        public async Task updateAsync(User_Info entity)
        {
try
            {
                if (_collection == null)
                {
                    throw new Exception("Collection is null");
                }

                var filter = Builders<User_Info>.Filter.Eq("Id", entity.Id);

                await _collection.ReplaceOneAsync(filter, entity);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
