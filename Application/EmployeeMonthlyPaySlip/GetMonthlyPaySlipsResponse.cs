using Domain.PaySlip;

namespace Application.EmployeeMonthlyPaySlip
{
    public class GetMonthlyPaySlipsResponse
    {
        public List<MonthlyPaySlipOutput> Result { get; set; } = new();
    }
}