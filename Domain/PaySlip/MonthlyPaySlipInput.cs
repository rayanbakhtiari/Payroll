namespace Domain.PaySlip
{
    public class MonthlyPaySlipInput
    {
        public string Name { get; set; } = String.Empty;
        public string LastName { get; set; } = String.Empty ;
        public decimal AnnualSalary { get; set; }
        public string SuperRate { get; set; } = String.Empty;
        public string PayPeriod { get; set; } = String.Empty;

        public string GetFullName()
        {
            return $"{Name} {LastName}";
        }

        public decimal GetGrossIncome()
        {
            return decimal.Round((AnnualSalary / 12),2);
        }
        public decimal GetSuperRate()
        {
            decimal super;
            bool isParsable = decimal.TryParse(SuperRate.Replace("%", ""), out super);
            if (!isParsable)
                throw new InvalidDataException("SuperRate is not a valid rate");
            return decimal.Round(super * 0.01m * GetGrossIncome(),2);
        }
    }
}