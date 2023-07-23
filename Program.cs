
class Program
{
    static long GetUserInputlong(string message)
    {
        long result;
        Console.Write(message);
        while (!long.TryParse(Console.ReadLine(), out result))
        {
            Console.WriteLine("Error: Invalid input. Enter an integer.");
            Console.Write(message);
        }
        return result;
    }

    static decimal GetUserInputDecimal(string message)
    {
        decimal result;
        Console.Write(message);
        while (!decimal.TryParse(Console.ReadLine(), out result))
        {
            Console.WriteLine("Error: Invalid input. Enter a floating point number.");
            Console.Write(message);
        }
        return result;
    }

    static void Main(string[] args)
    {
        string dataFilePath = "bank_accounts.json";
        Bank bank = new Bank(dataFilePath);

        Console.WriteLine("ATM");

        while (true)
        {
            Console.WriteLine("\nOpen commands:");
            Console.WriteLine("1. Create Account");
            Console.WriteLine("2. Deposit");
            Console.WriteLine("3. Withdraw");
            Console.WriteLine("4. West Transfer ");
            Console.WriteLine("5. Info you");
            Console.WriteLine("6. Delete Account(");
            Console.WriteLine("7. Exit");
            Console.Write("Enter command: ");

            string input = Console.ReadLine();
            long choice;
            if (long.TryParse(input, out choice))
            {
                switch (choice)
                {
                    case 1:
                        long accountNumber = GetUserInputlong("Enter account new number : ");
                        Console.WriteLine("Enter Your Name");
                        string ownerName = Console.ReadLine();

                        bank.OpenAccount(accountNumber, ownerName);
                        break;

                    case 2:
                        long depositAccountNumber = GetUserInputlong("Enter account number :");
                        decimal depositAmount = GetUserInputDecimal("Enter the amount to replenish: ");

                        bank.Deposit(depositAccountNumber, depositAmount);
                        break;

                    case 3:
                        long withdrawAccountNumber = GetUserInputlong("Enter account number : ");
                        decimal withdrawAmount = GetUserInputDecimal("Enter the amount to write off: ");

                        bank.Withdraw(withdrawAccountNumber, withdrawAmount);
                        break;

                    case 4:
                        long fromAccountNumber = GetUserInputlong("Enter the sender's account number: ");
                        long toAccountNumber = GetUserInputlong("Enter the beneficiary's account number: ");
                        decimal transferAmount = GetUserInputDecimal("Enter the amount to transfer: ");

                        bank.Transfer(fromAccountNumber, toAccountNumber, transferAmount);
                        break;

                    case 5:
                        long accountInfoNumber = GetUserInputlong("Enter account number : ");

                        bank.PrintAccountBalance(accountInfoNumber);
                        break;

                    case 6:
                        long deleteAccountNumber = GetUserInputlong("Enter number to delete: ");
                        bank.DeleteAccount(deleteAccountNumber);
                        break;

                    case 7:
                        Console.WriteLine("Ending the application.");
                        return;

                    default:
                        Console.WriteLine("Error: Invalid command. Try again....");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Error: Invalid command. Try again....");
            }
        }
    }
}