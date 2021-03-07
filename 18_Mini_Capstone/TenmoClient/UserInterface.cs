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


                            GetTransfersByUserId(authService.userId);
                            Console.WriteLine("Please enter the Transfer ID you'd like to see:");
                            int inputTransferId = Convert.ToInt32(Console.ReadLine());
                            GetTransferById(inputTransferId);



                            break;
                        case 3:
                            Console.WriteLine("NOT IMPLEMENTED!"); // TODO: Implement me
                            break;
                        case 4:
                            GetUsers();
                            Console.WriteLine();
                            Console.WriteLine("Please input the userID of the user you would like to send money to: ");
                            int sendToUserId = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("How many TE bucks would you like to send?");
                            decimal moneyAmount = Convert.ToDecimal(Console.ReadLine());

                            AddTransfer(authService.userId, sendToUserId, moneyAmount);

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


        public void GetBalance(int currentUserId)
        {

            Account account = new Account();
            try
            {
                account = accountAPI.GetAccount(currentUserId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }
            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine(account);

        }
        public void GetTransfersByUserId(int userId)
        {
            List<JoinedTransfer> transfers = new List<JoinedTransfer>();

            transfers = transferAPI.GetTransfers();


            Console.WriteLine();
            Console.WriteLine("List of Transfers:");
            foreach (JoinedTransfer transfer in transfers)
            {
                if (transfer.ToId != authService.userId && transfer.FromId == authService.userId)
                {
                    Console.WriteLine($" Transfer ID: {transfer.TransferId} To: {transfer.ToUser}  ${transfer.Amount}");
                }
               else if (transfer.ToId == authService.userId && transfer.FromId != authService.userId)
                {
                    Console.WriteLine($" Transfer ID: {transfer.TransferId} From: {transfer.ToUser}  ${transfer.Amount}");
                }
            }
        }

        public void GetTransferById(int inputTransferId)
        {
            List<JoinedTransfer> allTransfers = transferAPI.GetTransfers();
            JoinedTransfer requested = new JoinedTransfer();
            foreach (JoinedTransfer transfers in allTransfers)
            {
                if (transfers.TransferId == inputTransferId && (transfers.FromId == authService.userId || transfers.ToId == authService.userId))
                {
                    requested = transfers;
                }
                else
                {

                }
            }

            if (requested.TransferId == 0)
            {
                Console.WriteLine("Sorry, that's not a valid transfer ID.");
            }
            else
            {
                Console.WriteLine(requested);
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

            foreach (Users user in users)
            {
                if (user.UserId != authService.userId)
                {
                    Console.WriteLine(user);
                }
                else
                {

                }
            }


        }

        public void AddTransfer(int currentUserId, int sendToUserId, decimal moneyAmount)
        {
            Transfer temp = new Transfer();
            Account fromAccount = accountAPI.GetAccount(currentUserId);
            Account toAccount = accountAPI.GetAccount(sendToUserId);
            temp.TransferTypeId = 1001; //send
            temp.TransferStatusId = 2001; //approved
            temp.AccountFrom = fromAccount.AccountId;
            temp.AccountTo = toAccount.AccountId;
            temp.DollarAmount = moneyAmount;
            bool result = transferAPI.AddTransfer(temp);
            accountAPI.UpdateAccountFromBalance(currentUserId, moneyAmount);
            if (result)
            {
                Console.WriteLine(temp);
            }
            else
            {
                Console.WriteLine("Error: unable to add.");
            }

        }

    }
}
