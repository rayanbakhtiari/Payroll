namespace Domain.PaySlip
{
    public class MonthlyPaySlipInput
    {
        public string Name { get; set; } = String.Empty;
        public string LastName { get; set; } = String.Empty ;
        public decimal AnnualSalary { get; set; }
        public int SuperRate { get; set; }
        public string PayPeriod { get; set; } = String.Empty;

        public string GetFullName()
        {
            return $"{Name} {LastName}";
        }

        public decimal GetGrossIncome()
        {
            return decimal.Round((AnnualSalary / 12),2);
        }
    }
}