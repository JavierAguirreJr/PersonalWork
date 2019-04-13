using SGBank.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGBank.Models;
using System.IO;
using System.Configuration;

namespace SGBank.Data
{
    public class FileAccountRepository : IAccountRepository
    {
        string path = @"C:\Repos\dotnet-javier-aguirre\SG.Bank\Accounts.txt";
        
        public List<Account> GetAccounts()
        {
            List<Account> loadAccount = new List<Account>();
            try
            {
                using (StreamReader la = new StreamReader(path))
                {
                    la.ReadLine();
                    string number;

                    while ((number = la.ReadLine()) != null)
                    {
                        Account newAccount = new Account();

                        string[] columns = number.Split(',');

                        newAccount.AccountNumber = columns[0];
                        newAccount.Name = columns[1];
                        newAccount.Balance = decimal.Parse(columns[2]);

                        switch (columns[3].ToUpper())
                        {
                            case "F":
                                newAccount.Type = AccountType.Free;
                                break;
                            case "B":
                                newAccount.Type = AccountType.Basic;
                                break;
                            case "P":
                                newAccount.Type = AccountType.Premium;
                                break;
                            default:
                                throw new Exception("Invalid account type. Account needs to be Free, Basic or Premium...");
                        }
                        loadAccount.Add(newAccount);
                    }
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("Error: There was an error with your file.");
                Console.WriteLine("Error: Details: " + e.Message);
                return null;
            }
            return loadAccount;
        }

        public Account LoadAccount(string AccountNumber)
        {
            var load = GetAccounts();

            var loadIn = load.SingleOrDefault(a => a.AccountNumber == AccountNumber);

            return loadIn;
        }

        public void SaveAccount(Account account)
        {
            var filePath = ConfigurationManager.AppSettings["FileLocation"].ToString();
            var lineContent = string.Empty;
            var contentLine = File.ReadLines(filePath).ToList();

            for (var lineNumber = 1; lineNumber < contentLine.Count(); lineNumber++)
            {
                var lineArray = contentLine[lineNumber].Split(',');
                if (lineArray[0] == account.AccountNumber)
                {
                    lineArray[2] = account.Balance.ToString();
                    contentLine[lineNumber] = lineArray[0] + "," + lineArray[1] + "," + lineArray[2] + "," + lineArray[3];
                    break;
                }
            }

            File.WriteAllLines(filePath, contentLine.ToArray());
        }
    }
}
