using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.TaxCalculator
{
    internal class TaxCalculator2022 : ITaxCalculator
    {
        private List<KeyValuePair<decimal, decimal>> getTaxRates()
        {
            List<KeyValuePair<decimal, decimal>> taxRates = new();
            taxRates.Add(new(0.105m,0m ));
            taxRates.Add(new(0.175m, 14000m ));
            taxRates.Add(new(0.3m,48000m ));
            taxRates.Add(new(0.33m,70000m ));
            taxRates.Add(new(0.39m,180000m));

            return taxRates;
            
        }
        public decimal GetTaxForAnnualSalary(decimal annualSalary)
        {
           var taxRates = getTaxRates();
            decimal incomeTax = 0m;
            for(int i = taxRates.Count -1; i >= 0; i--)
            {
                var taxRate = taxRates[i];
                if (annualSalary > taxRate.Value)
                {
                    incomeTax += (annualSalary - taxRate.Value) * taxRate.Key;
                    annualSalary = taxRate.Value;
                }
            }
            return decimal.Round(incomeTax / 12,2);
        }
    }
}
