using DotnetCoreVCB.Common.Database;
using Microsoft.EntityFrameworkCore;

namespace DotnetCoreVCB.Models.Repository
{
    public interface IUserRepository
    {
        public List<SimpleUser> GetUsers();
    }

    public class UserRepository:IUserRepository
    {
        DemoDBContextDapper _dbdapper;
        DemoDBContext _dbContext;
        public UserRepository(DemoDBContextDapper dbdapper, DemoDBContext dbContext) { _dbdapper = dbdapper; _dbContext = dbContext; }



        public List<SimpleUser> GetUsers()
        {
            //return _dbdapper.Exec<SimpleUser>("spSelectUser").ToList();
            return _dbContext.SimpleUser.FromSqlRaw("dbo.spSelectUser").ToList();
        }
    }
}
