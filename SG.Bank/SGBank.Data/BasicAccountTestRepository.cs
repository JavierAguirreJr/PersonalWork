using SGBank.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGBank.Models;
using System.Configuration;
using System.IO;

namespace SGBank.Data
{
    public class BasicAccountTestRepository : IAccountRepository 
    {
        private static Account _account = new Account()
        {
            Name = "Basic Account",
            Balance = 100M,
            AccountNumber = "33333",
            Type = AccountType.Basic
        };

        public Account LoadAccount(string AccountNumber)
        {
            if (_account.AccountNumber == AccountNumber)
            {
                return _account;
            }
            else
            {
                return null;
            }
        }

        public void SaveAccount(Account account)
        {
            _account = account;
        }
    }
}