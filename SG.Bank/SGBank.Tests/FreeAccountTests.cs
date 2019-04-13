﻿using NUnit.Framework;
using SGBank.BLL;
using SGBank.BLL.DepositRules;
using SGBank.BLL.WithdrawRules;
using SGBank.Models;
using SGBank.Models.Interfaces;
using SGBank.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGBank.Tests
{
    [TestFixture]
    public class FreeAccountTests
    {
        [Test]
        public void CanLoadFreeAccountTestData()
        {
            AccountManager manager = AccountManagerFactory.Create();

            AccountLookupResponse response = manager.LookupAccount("12345");

            Assert.IsNotNull(response.Account);
            Assert.IsTrue(response.Success);
            Assert.AreEqual("12345", response.Account.AccountNumber);
        }

        [TestCase("12345", "FreeAccount", 100, AccountType.Free, 250, false)]
        [TestCase("12345", "FreeAccount", 100, AccountType.Free, -100, false)]
        [TestCase("12345", "FreeAccount", 100, AccountType.Basic, 50, false)]
        [TestCase("12345", "FreeAccount", 100, AccountType.Free, 50, true)]

        public void FreeAccountDepositRuleTest
            (string accountNumber, string name, decimal balance, AccountType accountType, decimal amount, bool expectedResult)
        {
            IDeposit deposit = new FreeAccountDepositRule();
            Account account = new Account();

            account.AccountNumber = accountNumber;
            account.Name = name;
            account.Balance = balance;
            account.Type = accountType;

            AccountDepositResponse response = deposit.Deposit(account, amount);

            Assert.AreEqual(expectedResult, response.Success);
            if (response.Success)
            {
                Assert.AreEqual(response.Account.Balance, response.OldBalance += amount);
            }
        }

        [TestCase("12345", "FreeAccount", 100, AccountType.Free, 50, 100, false)]
        [TestCase("12345", "FreeAccount", 100, AccountType.Free, -150, 100, false)]
        [TestCase("12345", "FreeAccount", 100, AccountType.Basic, 50, 100, false)]
        [TestCase("12345", "FreeAccount", 75, AccountType.Free, -90, 100, false)]
        [TestCase("12345", "FreeAccount", 100, AccountType.Free, -50, 50, true)]
        public void FreeAccountWithdrawRuleTest
            (string accountNumber, string name, decimal balance, AccountType accountType, decimal amount, decimal newBalance, bool expectedResult)
        {
            IWithdraw withdraw = new FreeAccountWithdrawRule();
            Account account = new Account();

            account.AccountNumber = accountNumber;
            account.Name = name;
            account.Balance = balance;
            account.Type = accountType;

            AccountWithdrawResponse response = withdraw.IWithdraw(account, amount);

            Assert.AreEqual(expectedResult, response.Success);
            if (response.Success)
            {
                Assert.AreEqual(response.Account.Balance, newBalance);
            }
        }
    }
}