using AccountCalc.Server.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AccountCalc.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AccountController : ControllerBase
    {
        private readonly AccountDbContext _db;
        private readonly ILogger<AccountController> _logger;
        public AccountController(AccountDbContext db, ILogger<AccountController> logger)
        {
            _db = db;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AccountModel account)
        {
            foreach(PropertyDescriptor descriptor in TypeDescriptor.GetProperties(account))
            {
                string name = descriptor.Name;
                object value = descriptor.GetValue(account);
                Debug.WriteLine($"{name}={value}");
            }

            if (!ModelState.IsValid)
            {
                return new JsonResult("Error");
            }
            _db.account.Add(account);

            await _db.SaveChangesAsync();
            return new JsonResult(account);
        }

        [HttpPost]
        public async Task<IActionResult> CreateList([FromBody] AccountModel[] accounts)
        {
            if (!ModelState.IsValid)
            {
                return new JsonResult("Error");
            }
            _db.account.AddRange(accounts);

            await _db.SaveChangesAsync();
            return new JsonResult(value: accounts);
        }

    }
}
