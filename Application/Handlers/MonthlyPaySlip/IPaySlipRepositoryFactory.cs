namespace Application.Handlers.MonthlyPaySlip
{
    public interface IPaySlipRepositoryFactory
    {
        IPaySlipRepository CreatePaySlipRepository();
    }
}