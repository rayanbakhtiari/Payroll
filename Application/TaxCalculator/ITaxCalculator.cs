namespace Application.TaxCalculator
{
    public interface ITaxCalculator
    {
        decimal GetTaxForAnnualSalary(decimal annualSalary);
    }
}