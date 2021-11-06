using AccountCalc.Server.Data;
using AccountCalc.Server.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;

namespace AccountCalc.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AccountController : ControllerBase
    {
        private readonly AccountDbContext _db;
        //private AccountService _service = new AccountService(_db, null);
        public AccountController(AccountDbContext db, ILogger<AccountController> logger)
        {
            _db = db;
            //_logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetMonthData(long startTime, long endTime)
        {
            DateTime start = new DateTime(1970, 1, 1).AddSeconds(startTime).ToLocalTime();
            DateTime end = new DateTime(1970, 1, 1).AddSeconds(endTime).ToLocalTime();
            AccountService _service = new AccountService(_db);
            List<AccountModel> dataList = await _service.GetMonthData(start, end);
            return new JsonResult(dataList);
        }

        [HttpPost]
        public async Task<IActionResult> PostAccount([FromBody] AccountModel account)
        {
            AccountService _service = new AccountService(_db);

            foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(account))
            {
                string name = descriptor.Name;
                object value = descriptor.GetValue(account);
                Debug.WriteLine($"{name}={value}");
            }

            if (!ModelState.IsValid)
            {
                return new JsonResult("Error");
            }
            await _service.Add(account);
            return new JsonResult(account);
        }

        [HttpPost]
        public async Task<IActionResult> PostAccounts([FromBody] AccountModel[] accounts)
        {
            AccountService _service = new AccountService(_db);

            if (!ModelState.IsValid)
            {
                return new JsonResult("Error");
            }
            await _service.AddList(accounts);
            return new JsonResult(value: accounts);
        }
    }
}
