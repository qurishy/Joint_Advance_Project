using Data_Transfer_API.Model;

namespace Data_Transfer_API.Repository.Profile
{
    public interface IUser_Service:IRepository<User_Info>
    {
        Task updateAsync(User_Info entity);
    }
}
