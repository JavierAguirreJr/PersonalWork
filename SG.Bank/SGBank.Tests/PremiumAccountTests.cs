using NUnit.Framework;
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
    public class PremiumAccountTests
    {
        [TestCase("8675309", "Premium Account", 500, AccountType.Free, 200, false)]
        [TestCase("8675309", "Premium Account", 500, AccountType.Premium, -50, false)]
        [TestCase("8675309", "Premium Account", 500, AccountType.Premium, 9001, true)]
        public void PremiumAccountDepositRuleTest(string accountNumber, string name, decimal balance, AccountType accountType, decimal amount, bool expectedResult)
        {
            IDeposit deposit = new NoLimitDepositRule();
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
        [TestCase("8675309", "Premium Account", 500, AccountType.Basic, 100, 500, false)]
        [TestCase("8675309", "Premium Account", 500, AccountType.Premium, 350, 500, false)]
        [TestCase("8675309", "Premium Account", 500, AccountType.Premium, -1200, 500, false)]
        [TestCase("8675309", "Premium Account", 500, AccountType.Premium, -250, 250, true)]
        [TestCase("8675309", "Premium Account", 500, AccountType.Premium, -999, -499, true)]
        public void PremiumAccountWithdrawRuleTest(string accountNumber, string name, decimal balance, AccountType accountType, decimal amount, decimal newBalance, bool expectedResult)
        {
            IWithdraw withdraw = new PremiumAccountWithdrawRule();
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
