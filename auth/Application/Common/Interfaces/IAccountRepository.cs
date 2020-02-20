using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IAccountRepository
    {
        Account GetAccount(string username, string password);

        void InsertAccount(string username, string password, string phone, out Guid userGuid, CancellationToken cancellationToken);
    }
}
