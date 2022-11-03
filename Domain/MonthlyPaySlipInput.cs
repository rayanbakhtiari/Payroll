namespace Domain
{
    public class MonthlyPaySlipInput
    {
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public int AnnualSalary { get; set; }
        public int SuperRate { get; set; }
        public string PayPeriod { get; set; }
    }
}