using System.Collections.Generic;
using System.Threading.Tasks;
using Datingapp.API.Helpers;
using Datingapp.API.Models;

namespace Datingapp.API.Data
{
    public interface IDateingRepositry
    {
         void Add<T>(T entity) where T: class;
         void Delete<T>(T entity) where T: class;

         Task<bool> SaveAll();
         Task<PagedList<user>> GetUsers(UserParams userParams);
         Task<user> GetUser(int id);

         Task<Photo> GetPhoto(int id);

         Task<Photo> GetMainPhoto(int userId);
        Task<Like> GetLike(int userId, int recipientId);
        Task<Message> GetMessage(int Id);
        Task<PagedList<Message>> GetMessagesForUser(MessageParams messageParams);
        Task<IEnumerable<Message>> GetMessageThread(int userId, int recipientId);
        

    }
}