using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Payroll.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApiController : ControllerBase
    {
        private IMediator? mediator;
        protected IMediator? Mediator
        {
            get
            {
                mediator ??= HttpContext.RequestServices.GetService<IMediator>();
                return mediator;
            }
        }
    }
}
