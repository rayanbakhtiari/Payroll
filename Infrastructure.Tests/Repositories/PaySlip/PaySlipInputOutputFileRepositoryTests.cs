using Domain.PaySlip;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Tests.Repositories.PaySlip
{
    public class PaySlipInputOutputFileRepositoryTests: IClassFixture<SetupFixture>
    {
        private readonly SetupFixture setupFixture = new();

        public PaySlipInputOutputFileRepositoryTests(SetupFixture setupFixture)
        {
            this.setupFixture = setupFixture;
            DeleteOutputDirectoryFiles();
        }
        [Fact]
        public async Task InputAddressFile_IsValid_Should_Return_PaySlipInputList()
        {
            string inputFileAddress =getFullAddress(@"InputData\monthly_pay_slips_input.csv");
            var fileRepositoryFactory = setupFixture.AddPaySlipFileRepositoryFactoryWithFileAddress(inputFileAddress, string.Empty);
            var fileRepository = fileRepositoryFactory.CreatePaySlipRepository();
            var paySlipInputList = await fileRepository.GetMonthlyPaySlipInputList();
            Assert.True(paySlipInputList.Any());

        }
        [Fact]
        public async Task InputAddressFile_IsNotValid_Should_Throw_FileNotFoundException()
        {
            string inputFileAddress =getFullAddress(@"InputData\monthly_pay_slips_input1.csv");
            var fileRepositoryFactory = setupFixture.AddPaySlipFileRepositoryFactoryWithFileAddress(inputFileAddress, string.Empty);
            var fileRepository = fileRepositoryFactory.CreatePaySlipRepository();
            await Assert.ThrowsAsync<FileNotFoundException>(() => fileRepository.GetMonthlyPaySlipInputList());
        }
        [Fact]
        public async Task Should_Create_OutputFile_When_Input_List_Is_Provided()
        {
            string outputFileAddress = getFullAddress(@"OutputData\monthly_pay_slip_out_put_list.csv");
            var fileRepositoryFactory = setupFixture.AddPaySlipFileRepositoryFactoryWithFileAddress(string.Empty, outputFileAddress);
            var fileRepository = fileRepositoryFactory.CreatePaySlipRepository();
            List<MonthlyPaySlipOutput> outputList = new()
            {
                new MonthlyPaySlipOutput() {Name = "David Johnson", GrossIncome = 6666.67m, IncomeTax = 1443.33m, PayPeriod = "01 March - 31 March", Super = 666.67m}
            };
            await fileRepository.InsertMonthlyPaySlipOutputList(outputList);
            Assert.True(File.Exists(outputFileAddress));

        }
        private void DeleteOutputDirectoryFiles()
        {
            string path = @"OutputData";

            DirectoryInfo directory = new DirectoryInfo(path);
            if (!directory.Exists)
                directory.Create();

            foreach (FileInfo file in directory.GetFiles())
            {
                file.Delete();
            }
        }
        private string getFullAddress(string address)
        {
            var basePath = Directory.GetCurrentDirectory();
            return @$"{basePath}\{address}";
        }
    }
}
