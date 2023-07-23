using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class BankAccount
{
    public long AccountNumber { get; set; }
    public decimal Balance { get; set; }
    public string OwnerName { get; set; }
}

public class Bank
{
    private List<BankAccount> accounts;
    private string dataFilePath;

    public Bank()
    {
        this.dataFilePath = dataFilePath;
        accounts = LoadAccountsFromJson();
    }

    //Загрузка файла с клиентской базой
    private List<BankAccount> LoadAccountsFromJson()
    {
        if (File.Exists(dataFilePath))
        {
            string jsonData = File.ReadAllText(dataFilePath);
            return JsonConvert.DeserializeObject<List<BankAccount>>(jsonData);
        }
        return new List<BankAccount>();
    }

    //Сохранение последней операции - обновление состояния счета
    private void SaveAccountsToJson()
    {
        string jsonData = JsonConvert.SerializeObject(accounts, Formatting.Indented);
        File.WriteAllText(dataFilePath, jsonData);
    }

    //Создать счет

    public void OpenAccount(int accountNumber, string ownerName)
    {
        if (accounts.Exists(acc => acc.AccountNumber == accountNumber))
        {
            Console.WriteLine("Account already exists with the given account number."); //Ошибка
        }
        else
        {
            BankAccount newAccount = new BankAccount
            {
                AccountNumber = accountNumber,
                OwnerName = ownerName,
                Balance = 0
            };
            accounts.Add(newAccount);
            SaveAccountsToJson();
            Console.WriteLine($"Account {accountNumber} created for {ownerName}.");
        }
    }

    //Пополнить счет

    public void Deposit(long accountNumber, decimal amount)
    {
        BankAccount account = FindAccount(accountNumber);
        if (account != null)
        {
            account.Balance += amount;
            SaveAccountsToJson();
            Console.WriteLine($"Deposited {amount} into account {accountNumber}. Current balance: {account.Balance}");
        }
        else
        {
            Console.WriteLine("Account not found.");
        }
    }

    //Снять со счета

    public void Withdraw(long accountNumber, decimal amount)
    {
        BankAccount account = FindAccount(accountNumber);
        if (account != null)
        {
            if (account.Balance >= amount)
            {
                account.Balance -= amount;
                SaveAccountsToJson();
                Console.WriteLine($"Withdrew {amount} from account {accountNumber}. Current balance: {account.Balance}");
            }
            else
            {
                Console.WriteLine("Insufficient funds."); //Ошибка: Недостаточно средств на счете
            }
        }
        else
        {
            Console.WriteLine("Account not found.");
        }
    }

    //Перевести средства

    public void Transfer(long fromAccountNumber, int toAccountNumber, decimal amount)
    {
        BankAccount fromAccount = FindAccount(fromAccountNumber);
        BankAccount toAccount = FindAccount(toAccountNumber);

        if (fromAccount != null && toAccountNumber != null)
        {
            if (fromAccount.Balance >= amount)
            {
                fromAccount.Balance -= amount;
                toAccount.Balance += amount;
                SaveAccountsToJson();
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

    //Показать состояние счета

    public void PrintAccountBalance(long accountNumber)
    {
        BankAccount account = FindAccount(accountNumber);
        if (account != null)
        {
            Console.WriteLine($"Account Number : {account.AccountNumber}");
            Console.WriteLine($"Owner Name: {account.OwnerName}");
            Console.WriteLine($"Balance: {account.Balance}");
        }
        else
        {
            Console.WriteLine("Account not found.");
        }
    }

    //Удалить счет

    public void DeleteAccount(long accountNumber)
    {
        BankAccount account = FindAccount(accountNumber);
        if (account != null)
        {
            accounts.Remove(account);
            SaveAccountsToJson();
            Console.WriteLine($"Amount {accountNumber} removed.");
        }
        else
        {
            Console.WriteLine("Account not found.");
        }
    }

    //Поиск карты в коллекции accounts по заданному номеру

    private BankAccount FindAccount(long accountNumber)
    {
        return accounts.Find(acc => acc.AccountNumber == accountNumber); //лямбда-выражение
    }
}