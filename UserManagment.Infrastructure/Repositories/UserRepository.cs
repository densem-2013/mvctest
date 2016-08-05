using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using UserManagment.Core.DAL;

namespace UserManagment.Infrastructure.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        //Verify email remote
        bool VerifyEmail(string email);
        IEnumerable<User> GetAll();
    }

    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(DbContext context) : base(context)
        {
            
        }
        public bool VerifyEmail(string email)
        {
            return Get(u => string.Equals(u.Email, email)) == null;
        }

        public IEnumerable<User> GetAll()
        {
            return Context.Set<User>().OrderBy(u=>u.UserName).ToList();
        } 
    }
}
