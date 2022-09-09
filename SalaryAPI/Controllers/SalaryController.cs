using ComputeService;
using Microsoft.AspNetCore.Mvc;
using Models;
using SalaryAPI.Dto;
using SalaryAPI.Dtos;
using TaxSlab;

namespace SalaryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalaryController : ControllerBase
    {
        private readonly ITaxSlabService _slabService;
        private readonly IComputeService _computeService;

        public SalaryController(ITaxSlabService slabService, IComputeService computeService)
        {
            _slabService = slabService;
            _computeService = computeService;
        }

        [HttpPost("compute")]
        public ActionResult<UserSalaryDto[]> Post([FromBody] UserInputDto[] userInputs)
        {
            var slab = _slabService.GetTaxSlabs();
            var salaries = userInputs.Select(input => input.AnnualSalary).ToArray();
            var computedSalary = _computeService.ComputeSalary(slab, salaries);

            int len = userInputs.Length, j = 0;
            var userSalaryDtos = new UserSalaryDto[len];
            for (int i = 0; i < userInputs.Length; i++)
            {
                UserInputDto uid = userInputs[i];
                ComputedSalary cs = computedSalary[uid.AnnualSalary];
                userSalaryDtos[j++] = new UserSalaryDto
                {
                    Name = $"{uid.FirstName} {uid.LastName}",
                    PayPeriod = $"{uid.PayPeriod}",
                    GrossIncome = cs.NetMonthly,
                    IncomeTax = cs.NetTax,
                    NetIncome = cs.NetAnnual,
                    Super = Math.Round(cs.NetMonthly * uid.SuperRate / 100, 2)
                };
            }

            return Ok(userSalaryDtos);
        }
    }
}
