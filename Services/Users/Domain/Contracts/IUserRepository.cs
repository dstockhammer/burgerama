using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace Burgerama.Services.Users.Domain.Contracts
{
    [ContractClass(typeof(UserRepositoryContract))]
    public interface IUserRepository
    {
        User Get(string id);

        IEnumerable<User> GetAll();

        void SaveOrUpdate(User user);
    }
}
