using Application.EmployeeMonthlyPaySlip;
using Domain.PaySlip;
using MediatR;
using Moq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UnitTests.MonthlyPaySlip
{
    public class MonthlyPaySlipTests : IClassFixture<SetupFixture>
    {
        private readonly SetupFixture setupFixture;
        private IMediator Mediator => setupFixture.Mediator;

        public MonthlyPaySlipTests(SetupFixture setupFixture)
        {
            this.setupFixture = setupFixture;
        }
        [Fact]
        public async Task Any_PaySlip_Item_Is_Not_Valid_Should_Throw_MonthlyPaySlipInputValidationException()
        {
            SetupPaySlipRepositoryWith(p => p.BuildAListOf(5).WithName(0,String.Empty));
            await Assert.ThrowsAsync<MonthlyPaySlipInputValidationException>(() => Mediator.Send(new GetMonthlyPaySlipsQuery()));

            SetupPaySlipRepositoryWith(p => p.BuildAListOf(5));
            var exception = await Record.ExceptionAsync(() => Mediator.Send(new GetMonthlyPaySlipsQuery()));
            Assert.Null(exception);

        }
        [Fact]
        public async Task Response_EmployeeName_Should_Be_Input_Name_Plus_LastName()
        {
            SetupPaySlipRepositoryWith(p => 
                p.BuildAListOf(2)
                    .WithName(0, "Michael").WithLastName(0, "Smith")
                    .WithName(1, "Amelia").WithLastName(1, "Taylor"));

            var response = await Mediator.Send(new GetMonthlyPaySlipsQuery());

            Assert.Equal("Michael Smith", response.Result[0].Name);
            Assert.Equal("Amelia Taylor", response.Result[1].Name);
        }
        [Fact]
        public async Task Response_PayPeriod_Should_be_Correct_Based_On_Input_PayPeriod()
        {
            SetupPaySlipRepositoryWith(p => p.BuildAListOf(1).WithPayPeriod(0, "March"));
            var response = await Mediator.Send(new GetMonthlyPaySlipsQuery());
            Assert.Equal("01 March - 31 March", response.Result[0].PayPeriod);


            SetupPaySlipRepositoryWith(p => p.BuildAListOf(2).WithPayPeriod(0, "January").WithPayPeriod(1, "February"));
            response = await Mediator.Send(new GetMonthlyPaySlipsQuery());
            Assert.Equal("01 January - 31 January", response.Result[0].PayPeriod);
            Assert.Equal("01 February - 28 February", response.Result[1].PayPeriod);


        }

        [Fact]
        public async Task Response_GrossSalary_Should_Be_Input_AnnualSalary_Devided_By_Twelve()
        {
            SetupPaySlipRepositoryWith(p =>
                p.BuildAListOf(2)
                    .WithAnnualSalary(0, 60050)
                    .WithAnnualSalary(1, 120000));

            var response = await Mediator.Send(new GetMonthlyPaySlipsQuery());
            Assert.Equal(5004.17m, response.Result[0].GrossIncome);
            Assert.Equal(10000.00m, response.Result[1].GrossIncome);

        }

        private void SetupPaySlipRepositoryWith(Func<MonthlyPaySlipInputBuilder,List<MonthlyPaySlipInput>> func)
        {
            MonthlyPaySlipInputBuilder test = new MonthlyPaySlipInputBuilder();
            setupFixture.PaySlipRepositoryMock.Setup(pr => pr.GetMonthlyPaySlipList()).ReturnsAsync(() =>
                func(test)
            );
        }
    }
}
