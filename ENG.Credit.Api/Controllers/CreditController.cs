using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ENG.Credit.Domain;

using Microsoft.AspNetCore.Mvc;

namespace ENG.Credit.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreditController : ControllerBase
    {

        [HttpGet("risk-validation")]
        public IActionResult RiskValidation(decimal salary, int employmentTime, int age, int dependents, decimal amount)
        {
            var service = new RiskValidation();

            var risk = service.Validate(salary, employmentTime, age, dependents, amount);

            return Ok(risk);
        }
    }
}
