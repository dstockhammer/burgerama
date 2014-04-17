using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace Burgerama.Services.Users.Domain.Contracts
{
    [ContractClassFor(typeof(IUserRepository))]
    internal abstract class UserRepositoryContract : IUserRepository
    {
        public User Get(string id)
        {
            Contract.Requires<ArgumentNullException>(id != null);

            return default(User);
        }

        public IEnumerable<User> GetAll()
        {
            Contract.Ensures(Contract.Result<IEnumerable<User>>() != null);
            return default(IEnumerable<User>);
        }

        public void SaveOrUpdate(User venue)
        {
            Contract.Requires<ArgumentNullException>(venue != null);
        }
    }
}
