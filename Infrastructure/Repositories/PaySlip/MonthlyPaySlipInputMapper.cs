using CsvHelper.Configuration;
using Domain.PaySlip;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.PaySlip
{
    internal class MonthlyPaySlipInputMapper: ClassMap<MonthlyPaySlipInput>
    {
        public MonthlyPaySlipInputMapper()
        {
            Map(p => p.Name).Index(0);
            Map(p => p.LastName).Index(1);
            Map(p => p.AnnualSalary).Index(2);
            Map(p => p.SuperRate).Index(3);
            Map(p => p.PayPeriod).Index(4);
        }
    }
}
