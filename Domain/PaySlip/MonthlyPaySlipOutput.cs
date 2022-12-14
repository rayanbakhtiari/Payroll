namespace Domain.PaySlip
{
    public class MonthlyPaySlipOutput
    {
        public string Name { get; set; } = String.Empty;
        public string PayPeriod { get; set; } = String.Empty;
        public decimal GrossIncome { get; set; }
        public decimal IncomeTax { get; set; }
        public decimal NetIncome { get =>  GrossIncome - IncomeTax; }
        public decimal Super { get; set; }
    }
}