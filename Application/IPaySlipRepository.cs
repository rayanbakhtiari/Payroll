using Domain.PaySlip;

namespace Application
{
    public interface IPaySlipRepository
    {
        Task<List<MonthlyPaySlipInput>> GetMonthlyPaySlipList();
    }
}