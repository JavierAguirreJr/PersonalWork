using NUnit.Framework;
using SGBank.BLL.DepositRules;
using SGBank.BLL.WithdrawRules;
using SGBank.Data;
using SGBank.Models;
using SGBank.Models.Interfaces;
using SGBank.Models.Responses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGBank.Tests
{
    [TestFixture]
    public class FileRepoTest
    {
        private const string _filePath = @"C:\Repos\dotnet-javier-aguirre\SG.Bank\Accounts.txt";
        private const string _originalFilePath = @"C:\Repos\dotnet-javier-aguirre\SG.Bank\AccountsSeed.txt";

        [SetUp]
        public void Setup()
        {
            if(File.Exists(_filePath))
            {
                File.Delete(_filePath);
            }
            File.Copy(_originalFilePath, _filePath);
        }
       
        [Test]
        public void AbleToReadFromFile()
        {
            FileAccountRepository readFile = new FileAccountRepository();

            List<Account> list = readFile.GetAccounts();

            Assert.AreEqual(3, list.Count());

            Account valid = list[1];

            Assert.AreEqual("22222", valid.AccountNumber);
            Assert.AreEqual("Basic Customer", valid.Name);
            Assert.AreEqual(500, valid.Balance);
            Assert.AreEqual(AccountType.Basic, valid.Type);
        }
    }
}
