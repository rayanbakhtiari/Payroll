using Domain.PaySlip;

namespace Application
{
    public interface IPaySlipRepository
    {
        Task<List<MonthlyPaySlipInput>> GetMonthlyPaySlipInputList();
        Task InsertMonthlyPaySlipOutputList(List<MonthlyPaySlipOutput> result);
    }
}