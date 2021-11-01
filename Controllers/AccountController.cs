using AccountCalc.Server.Services;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;

namespace AccountCalc.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AccountController : ControllerBase
    {
        private AccountService _service;

        [HttpGet]
        public async Task<IActionResult> GetMonthData(long startTime, long endTime)
        {
            await _service.GetMonthData(startTime,endTime);
            return new JsonResult("OK");
        }

        [HttpPost]
        public async Task<IActionResult> PostAccount([FromBody] AccountModel account)
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
            await _service.Add(account);
            return new JsonResult(account);
        }

        [HttpPost]
        public async Task<IActionResult> PostAccounts([FromBody] AccountModel[] accounts)
        {
            if (!ModelState.IsValid)
            {
                return new JsonResult("Error");
            }
            await _service.AddList(accounts);
            return new JsonResult(value: accounts);
        }
    }
}
