using AccountCalc.Server.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AccountCalc.Server.Services
{
    public class AccountService
    {
        private readonly AccountDbContext _db;
        //private readonly ILogger<AccountService> _logger;
        public AccountService(AccountDbContext db
            //, ILogger<AccountService>? logger
            )
        {
            _db = db;
            //_logger = logger;
        }

        public async Task<int> Add(AccountModel account)
        {
            _db.Account.Add(account);
            return await _db.SaveChangesAsync();
        }

        public async Task<List<AccountModel>> GetMonthData(DateTime startTime, DateTime endTime)
        {
            var accounts = await _db.Account
                .Where(a => a.createTime >= startTime && a.createTime <= endTime)
                .ToListAsync();
            if (accounts == null)
            {
                //return NotFoundResult();
            }
            return accounts;
        }

        public async Task<int> AddList(AccountModel[] accounts)
        {
            _db.Account.AddRange(accounts);
            return await _db.SaveChangesAsync();
        }
    }
}
