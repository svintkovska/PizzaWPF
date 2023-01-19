using DAL.Data;
using DAL.Data.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class UserRepository : GenericRepository<UserEntity>, IUserRepository
    {
        public UserRepository(EFAppContext context) : base(context) { }     
    }
   
}
