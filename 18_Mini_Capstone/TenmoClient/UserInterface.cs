using System;
using System.Collections.Generic;
using TenmoClient.APIClients;
using TenmoClient.Data;


namespace TenmoClient
{
    public class UserInterface
    {
        private readonly ConsoleService consoleService = new ConsoleService();
        private readonly AuthService authService = new AuthService();
        private readonly AccountAPI accountAPI = new AccountAPI();

        private readonly TransferAPI transferAPI = new TransferAPI();

        private readonly UsersAPI userAPI = new UsersAPI();



        private bool shouldExit = false;

        public void Start()
        {
            while (!shouldExit)
            {
                while (!authService.IsLoggedIn)
                {
                    ShowLogInMenu();
                }

                // If we got here, then the user is logged in. Go ahead and show the main menu
                ShowMainMenu();
            }
        }

        private void ShowLogInMenu()
        {
            Console.WriteLine("Welcome to TEnmo!");
            Console.WriteLine("1: Login");
            Console.WriteLine("2: Register");
            Console.Write("Please choose an option: ");

            if (!int.TryParse(Console.ReadLine(), out int loginRegister))
            {
                Console.WriteLine("Invalid input. Please enter only a number.");
            }
            else if (loginRegister == 1)
            {
                HandleUserLogin();
            }
            else if (loginRegister == 2)
            {
                HandleUserRegister();
            }
            else
            {
                Console.WriteLine("Invalid selection.");
            }
        }

        private void ShowMainMenu()
        {
            int menuSelection = -1;
            while (menuSelection != 0)
            {
                Console.WriteLine();
                Console.WriteLine("Welcome to TEnmo! Please make a selection: ");
                Console.WriteLine("1: View your current balance");
                Console.WriteLine("2: View your past transfers");
                Console.WriteLine("3: View your pending requests");
                Console.WriteLine("4: Send TE bucks");
                Console.WriteLine("5: Request TE bucks");
                Console.WriteLine("6: Log in as different user");
                Console.WriteLine("0: Exit");
                Console.WriteLine("---------");
                Console.Write("Please choose an option: ");

                if (!int.TryParse(Console.ReadLine(), out menuSelection))
                {
                    Console.WriteLine("Invalid input. Please enter only a number.");
                }
                else
                {
                    switch (menuSelection)
                    {
                        case 1:
                            GetBalance(authService.userId);
                            break;
                        case 2:


                            GetTransfers();


                            break;
                        case 3:
                            Console.WriteLine("NOT IMPLEMENTED!"); // TODO: Implement me
                            break;
                        case 4:
                            GetUsers();
                            Console.WriteLine();
                            Console.WriteLine("Please input the userID of the user you would like to send money to: ");
                            int userId = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("How many TE bucks would you like to send?");
                            decimal moneyAmount = Convert.ToDecimal(Console.ReadLine());
                            AddTransfer(userId, moneyAmount);

                            break;
                        case 5:
                            Console.WriteLine("NOT IMPLEMENTED!"); // TODO: Implement me
                            break;
                        case 6:
                            Console.WriteLine();
                            UserService.SetLogin(new API_User()); //wipe out previous login info
                            return;
                        default:
                            Console.WriteLine("Goodbye!");
                            shouldExit = true;
                            return;
                    }
                }
            }
        }

        private void HandleUserRegister()
        {
            bool isRegistered = false;

            while (!isRegistered) //will keep looping until user is registered
            {
                LoginUser registerUser = consoleService.PromptForLogin();
                isRegistered = authService.Register(registerUser);
            }

            Console.WriteLine("");
            Console.WriteLine("Registration successful. You can now log in.");
        }

        private void HandleUserLogin()
        {
            while (!UserService.IsLoggedIn) //will keep looping until user is logged in
            {
                LoginUser loginUser = consoleService.PromptForLogin();
                API_User user = authService.Login(loginUser);
                if (user != null)
                {
                    UserService.SetLogin(user);
                }
            }
        }


        public void GetBalance(int userId)
        {

            Account account = new Account();
            try
            {
                account = accountAPI.GetBalance(userId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }
            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine("The balance is: " + account);

        }
        public void GetTransfers()
        {
            List<Transfer> transfers = new List<Transfer>();


            try
            {
                transfers = transferAPI.GetTransfers();
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "Problem getting transfers in interface");
               return;
           }
            Console.WriteLine();
            Console.WriteLine("List of Transfers:");
            foreach(Transfer transfer in transfers)
            {
                Console.WriteLine(transfer);
            }
        }

        public void GetUsers()
        {
            List<Users> users = new List<Users>();

            try
            {
                users = userAPI.GetUsers();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "Problem getting users in interface");
                return;
            }
            Console.WriteLine();
            Console.WriteLine("The available users are: ");

            foreach(Users user in users)
            {
                Console.WriteLine(user);
            }


        }

       public void AddTransfer(int userId, decimal moneyAmount)
        {
            Transfer temp = new Transfer();
            Account account = accountAPI.GetBalance(userId);
            temp.TransferTypeId = 1001; //send
            temp.TransferStatusId = 2001; //approved
            temp.AccountFrom = account.AccountId;
            temp.AccountTo = userId;
            temp.DollarAmount = moneyAmount;
            bool result = transferAPI.AddTransfer(temp);
            if (result)
            {
                Console.WriteLine("Added.");
            }
            else
            {
                Console.WriteLine("Error: unable to add.");
            }
  
        }

    }
}
