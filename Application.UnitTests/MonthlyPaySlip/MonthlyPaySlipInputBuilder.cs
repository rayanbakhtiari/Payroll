using Domain.PaySlip;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Application.UnitTests.MonthlyPaySlip
{
    public class MonthlyPaySlipInputBuilder
    {
        private string name = "David";
        private string lastName = "Jeffries";
        private decimal annualSalary = 90000m;
        private string superRate = "40%";
        private string payPeriod = "March";
        public List<MonthlyPaySlipInput> BuildAListOf(int length)
        {
            List<MonthlyPaySlipInput> paySlipList = new();
            for (int i = 0; i < length; i++)
            {
                paySlipList.Add(new()
                {
                    Name = name,
                    LastName = lastName,
                    AnnualSalary = annualSalary,
                    SuperRate = superRate,
                    PayPeriod = payPeriod
                });
            }
            return paySlipList;
        }

    }

    public static class MonthlyPaySlipInputBuilderExtensions
    {
        public static List<MonthlyPaySlipInput> WithName(this List<MonthlyPaySlipInput> items, int index, string name)
        {
            if(index > items.Count -1)
                throw new ArgumentOutOfRangeException("index");
            items[index].Name = name;
            return items;
        }
        public static List<MonthlyPaySlipInput> WithLastName(this List<MonthlyPaySlipInput> items, int index, string lastName)
        {
            if(index > items.Count -1)
                throw new ArgumentOutOfRangeException("index");
            items[index].LastName = lastName;
            return items;
        }
        public static List<MonthlyPaySlipInput> WithAnnualSalary(this List<MonthlyPaySlipInput> items, int index, decimal annualSalary)
        {
            if(index > items.Count -1)
                throw new ArgumentOutOfRangeException("index");
            items[index].AnnualSalary = annualSalary;
            return items;
        }
        public static List<MonthlyPaySlipInput> WithSuperRate(this List<MonthlyPaySlipInput> items, int index, string superRate)
        {
            if(index > items.Count -1)
                throw new ArgumentOutOfRangeException("index");
            items[index].SuperRate = superRate;
            return items;
        }
        public static List<MonthlyPaySlipInput> WithPayPeriod(this List<MonthlyPaySlipInput> items, int index, string payPeriod)
        {
            if(index > items.Count -1)
                throw new ArgumentOutOfRangeException("index");
            items[index].PayPeriod = payPeriod;
            return items;
        }
    }
}
