using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class BankAccount
{

}

public class Bank
{
    private Dictionary<int, decimal> accounts;

    public Bank()
    {
        accounts = new Dictionary<int, decimal>();
    }

    public void OpenAccount(int accountNumber, decimal initialBalance = 0)
    {
        if (accounts.ContainsKey(accountNumber))
        {
            Console.WriteLine("Account already exists with the given account number.");
        }
        else
        {
            accounts.Add(accountNumber, initialBalance);
            Console.WriteLine($"Account {accountNumber} opened with initial balance: {initialBalance}.");
        }
    }

    public void Deposit(int accountNumber, decimal amount)
    {
        if (accounts.ContainsKey(accountNumber))
        {
            accounts[accountNumber] += amount;
            Console.WriteLine($"Deposited {amount} into account {accountNumber}. Current balance: {accounts[accountNumber]}");
        }
        else
        {
            Console.WriteLine("Account not found.");
        }
    }

    public void Withdraw(int accountNumber, decimal amount)
    {
        if (accounts.ContainsKey(accountNumber))
        {
            if (accounts[accountNumber] >= amount)
            {
                accounts[accountNumber] -= amount;
                Console.WriteLine($"Withdrew {amount} from account {accountNumber}. Current balance: {accounts[accountNumber]}");
            }
            else
            {
                Console.WriteLine("Insufficient funds.");
            }
        }
        else
        {
            Console.WriteLine("Account not found.");
        }
    }

    public void Transfer(int fromAccountNumber, int toAccountNumber, decimal amount)
    {
        if (accounts.ContainsKey(fromAccountNumber) && accounts.ContainsKey(toAccountNumber))
        {
            if (accounts[fromAccountNumber] >= amount)
            {
                accounts[fromAccountNumber] -= amount;
                accounts[toAccountNumber] += amount;
                Console.WriteLine($"Transferred {amount} from account {fromAccountNumber} to account {toAccountNumber}.");
            }
            else
            {
                Console.WriteLine("Insufficient funds.");
            }
        }
        else
        {
            Console.WriteLine("One or both accounts not found.");
        }
    }

    public void PrintAccountBalance(int accountNumber)
    {
        if (accounts.ContainsKey(accountNumber))
        {
            Console.WriteLine($"Account {accountNumber} balance: {accounts[accountNumber]}");
        }
        else
        {
            Console.WriteLine("Account not found.");
        }
    }
}