using Domain.PaySlip;

namespace Application.Handlers.MonthlyPaySlip
{
    public class GetMonthlyPaySlipsResponse
    {
        public List<MonthlyPaySlipOutput> Result { get; set; } = new();
    }
}