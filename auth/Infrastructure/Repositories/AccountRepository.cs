﻿using Application.Common.Interfaces;
using Domain.Entities;
using IdentityServer4.Models;
using Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly IAppDbContext _context;

        public AccountRepository(IAppDbContext context)
        {
            _context = context;
        }

        public Account GetAccount(string username, string password)
        {
            return _context.Accounts.SingleOrDefault(m => m.Username == username && m.EncryptedPassword == password.Sha256());
        }

        public void InsertAccount(string username, string password, string phone, out Guid userGuid, CancellationToken cancellationToken)
        {
            userGuid = Guid.NewGuid();
            _context.Accounts.Add(new Account()
            {
                UserGuid = userGuid,
                Username = username,
                EncryptedPassword = password.Sha256(),
                Phone = phone
            });
            _context.SaveChangesAsync(cancellationToken);
        }
    }
}
