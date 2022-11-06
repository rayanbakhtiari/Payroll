using Application.Handlers.MonthlyPaySlip;
using Infrastructure.Repositories.PaySlip;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Payroll.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MonthlyPaySlipController : ApiController
    {
        private readonly ILoggerFactory loggerFactory;

        private PaySlipFileRepositoryFactoryWithInputStream PaySlipFileRepositoryFactory
        {
            get
            {
                return HttpContext.RequestServices.GetService<IPaySlipRepositoryFactory>() as PaySlipFileRepositoryFactoryWithInputStream;
            }
        }

        [HttpPost("get-monthly-pay-slip-csv")]
        public async Task<IActionResult> GetMonthlyPaySlipCsv([FromForm] IFormFileCollection file)
        {
            PaySlipFileRepositoryFactory.SetInputStream(file[0].OpenReadStream());
            var result = await Mediator.Send(new GetMonthlyPaySlipsQuery());
            return Ok(PaySlipFileRepositoryFactory.OutputFileAddress);
        }
    }
}
