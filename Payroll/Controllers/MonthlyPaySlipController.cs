using Application.Handlers.MonthlyPaySlip;
using Infrastructure.Repositories.PaySlip;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Payroll.Helper;
using System.IO;
using System.Net.Mime;

namespace Payroll.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MonthlyPaySlipController : ApiController
    {
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

            return FileHelper.GetFileResultFrom(PaySlipFileRepositoryFactory.OutputFileAddress);
        }
    }
}
