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
        private readonly ILogger<SalaryController> _logger;
        private readonly ITaxSlabService _slabService;
        private readonly IComputeService _computeService;

        public SalaryController(ILogger<SalaryController> logger, ITaxSlabService slabService, IComputeService computeService)
        {
            _logger = logger;
            _slabService = slabService;
            _computeService = computeService;
        }

        [HttpPost("compute")]
        public ActionResult<UserSalaryDto[]> Post([FromBody] UserInputDto[] userInputs)
        {
            _logger.LogInformation($"SalaryController: compute() In");
            throw new Exception("testing middleware");
            var slab = _slabService.GetTaxSlabs();
            var salaries = userInputs.Select(input => input.AnnualSalary).ToArray();
            var computedSalary = _computeService.ComputeSalary(slab, salaries);

            int len = userInputs.Length, j = 0;

            var userSalaryDtos = new UserSalaryDto[len];
            
            foreach (UserInputDto input in userInputs)
            {
                ComputedSalary cs = computedSalary[input.AnnualSalary];
                userSalaryDtos[j++] = new UserSalaryDto
                {
                    Name = $"{input.FirstName} {input.LastName}",
                    PayPeriod = $"{input.PayPeriod}",
                    GrossIncome = cs.NetMonthly,
                    IncomeTax = cs.NetTax,
                    NetIncome = cs.NetAnnual,
                    Super = Math.Round(cs.NetMonthly * input.SuperRate / 100, 2)
                };
            }

            _logger.LogInformation($"SalaryController: compute() Exit");
            return Ok(userSalaryDtos);
        }
    }
}
