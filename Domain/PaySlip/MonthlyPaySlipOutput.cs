﻿namespace Domain.PaySlip
{
    public class MonthlyPaySlipOutput
    {
        public string PayPeriod { get; set; } = String.Empty;
        public string Name { get; set; } = String.Empty;
        public decimal GrossIncome { get; set; }
    }
}