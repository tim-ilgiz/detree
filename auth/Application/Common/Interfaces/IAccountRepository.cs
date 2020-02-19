using System;
using System.Collections.Generic;
using System.Text;
using Domain.Entities;

namespace Application.Common.Interfaces
{
    public interface IAccountRepository
    {
        Account GetAccount(string username, string password);

        void InsertAccount(string username, string password, string phone, out Guid userGuid);
    }
}
