using Application.EmployeeMonthlyPaySlip;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UnitTests.MonthlyPaySlip
{
    public class MonthlyPaySlipInputValidatorTests: ValidatorTestHelper<MonthlyPaySlipInput,MonthlyPaySlipInputValidator>
    {
        MonthlyPaySlipInput monthlyPaySlipInput; 
        public MonthlyPaySlipInputValidatorTests()
        {
            monthlyPaySlipInput = new();
        }
        [Fact]
        public void Employee_Name_Should_Not_Be_NullOrEmpty()
        {
            AssertThrowValidationErrorFor(monthlyPaySlipInput, p => p.Name);
            monthlyPaySlipInput.Name = "David";
            AssertNotThrowValidationErrorFor(monthlyPaySlipInput, p => p.Name);
        }
        [Fact]
        public void Employee_LastName_Should_Not_Be_NullOrEmpty()
        {
            AssertThrowValidationErrorFor(monthlyPaySlipInput, p => p.LastName);
            monthlyPaySlipInput.LastName = "Jeffries";
            AssertNotThrowValidationErrorFor(monthlyPaySlipInput, p => p.LastName);
        }
        [Fact]
        public void Employee_AnnualSalary_Should_Be_Greater_Than_Zero()
        {
            monthlyPaySlipInput.AnnualSalary = 0;
            AssertThrowValidationErrorFor(monthlyPaySlipInput, p => p.AnnualSalary);
            monthlyPaySlipInput.AnnualSalary = 90000;
            AssertNotThrowValidationErrorFor(monthlyPaySlipInput, p => p.AnnualSalary);
        }
        [Fact]
        public void Employee_Super_Rate_Should_Be_Between_0_To_50_Percent()
        {
            monthlyPaySlipInput.SuperRate = -1;
            AssertThrowValidationErrorFor(monthlyPaySlipInput, p => p.SuperRate);

            monthlyPaySlipInput.SuperRate = 51;
            AssertThrowValidationErrorFor(monthlyPaySlipInput, p => p.SuperRate);

            monthlyPaySlipInput.SuperRate = 20;
            AssertNotThrowValidationErrorFor(monthlyPaySlipInput, p => p.SuperRate);
        }
        [Fact]
        public void Pay_Period_Should_Be_A_Month_Name()
        {
            monthlyPaySlipInput.PayPeriod = String.Empty;
            AssertThrowValidationErrorFor(monthlyPaySlipInput, p => p.PayPeriod);
            monthlyPaySlipInput.PayPeriod = "Marcho";
            AssertThrowValidationErrorFor(monthlyPaySlipInput, p => p.PayPeriod);
            monthlyPaySlipInput.PayPeriod = "March";
            AssertNotThrowValidationErrorFor(monthlyPaySlipInput, p => p.PayPeriod);
            monthlyPaySlipInput.PayPeriod = "February";
            AssertNotThrowValidationErrorFor(monthlyPaySlipInput, p => p.PayPeriod);
        }
    }
}
