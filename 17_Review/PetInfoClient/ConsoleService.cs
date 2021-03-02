using PetInfo.APIServices;
using PetInfoClient.APIServices;
using PetInfoClient.Models;
using System;
using System.Collections.Generic;

namespace WorldClient
{
    public class ConsoleService
    {
        private readonly LoginAPIService loginAPIService = new LoginAPIService();
        private readonly PetAPIService petAPIService = new PetAPIService();



        public void Run()
        {
            PrintHeader();
            PrintMenu();

            bool done = false;

            while (!done)
            {

                string userInput = Console.ReadLine();

                switch (userInput)
                {
                    case "1":
                        AddAPet();
                        break;

                    case "2":
                        DeleteAPet();
                        break;

                    case "3":
                        ListPets();
                        break;

                    case "4":
                        LoginLogout();
                        break;

                    case "5":

                        Register();
                        break;


                    case "q":
                    case "Q":
                        done = true;
                        continue;

                    default:
                        Console.WriteLine("Please enter a valid choice.");
                        break;
                }

                PrintMenu();
            }
        }

        private void PrintHeader()
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine(@":'########::'########:'########:'####:'##::: ##:'########::'#######:: ");
            Console.WriteLine(@": ##.... ##: ##.....::... ##..::. ##:: ###:: ##: ##.....::'##.... ##: ");
            Console.WriteLine(@": ##:::: ##: ##:::::::::: ##::::: ##:: ####: ##: ##::::::: ##:::: ##: ");
            Console.WriteLine(@": ########:: ######:::::: ##::::: ##:: ## ## ##: ######::: ##:::: ##: ");
            Console.WriteLine(@": ##.....::: ##...::::::: ##::::: ##:: ##. ####: ##...:::: ##:::: ##: ");
            Console.WriteLine(@": ##:::::::: ##:::::::::: ##::::: ##:: ##:. ###: ##::::::: ##:::: ##: ");
            Console.WriteLine(@": ##:::::::: ########:::: ##::::'####: ##::. ##: ##:::::::. #######:: ");
            Console.WriteLine(@"..:::::::::........:::::..:::::....::..::::..::..:::::::::.......::: ");
            // from: http://www.network-science.de/ascii/ with banner3-D
        }

        private void PrintMenu()
        {
            string logInOut = loginAPIService.LoggedIn ? "Log out" : "Log in";
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Main-Menu Type in a command");
            Console.WriteLine(" 1 - Add a pet");
            Console.WriteLine(" 2 - Delete a pet");
            Console.WriteLine(" 3 - List pets");
            Console.WriteLine(" 4 - " + logInOut);
            Console.WriteLine(" 5 - Register");
            Console.WriteLine(" Q - Quit");
        }

        private void LoginLogout()
        {
            if (loginAPIService.LoggedIn)
            {
                loginAPIService.Logout();
                Console.WriteLine("You are now logged out");
            }
            else
            {
                Console.Write("Please enter username: ");
                string username = Console.ReadLine();
                Console.Write("Please enter password: ");
                string password = Console.ReadLine();
                var login = loginAPIService.Login(username, password);
                if (login)
                {
                    Console.WriteLine("You are now logged in");
                }
                else
                {
                    Console.WriteLine("Unable to login");
                }
            }
        }

        private void Register()
        {
            LoginUser registerUser = new LoginUser();
            Console.Write("Please enter username: ");
            registerUser.Username = Console.ReadLine();
            Console.Write("Please enter password: ");
            registerUser.Password = Console.ReadLine();

            bool isRegistered = loginAPIService.Register(registerUser);
            if (isRegistered)
            {
                Console.WriteLine("");
                Console.WriteLine("Registration successful. You can now log in.");
            }
        }

        private void DeleteAPet()
        {
            Console.WriteLine("Not yet available.");

            //Console.WriteLine("Please select the pet to delete.");
            //ListPets();
            //Console.Write("Please enter a pet number: ");
            //int petNumber = int.Parse(Console.ReadLine());
            //bool result = petDepot.DeletePet(petNumber);
            //if (result)
            //{
            //    Console.WriteLine("Deleted.");
            //}
            //else
            //{
            //    Console.WriteLine("ERROR: Unable to delete.");
            //}

        }

        private void ListPets()
        {
            List<Pet> pets = new List<Pet>();

            try
            {
                pets = petAPIService.GetPets();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }

            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine("The pets are:");
            foreach (Pet pet in pets)
            {
                Console.WriteLine(pet);
            }
            Console.WriteLine();
        }

        private void AddAPet()
        {
            Pet temp = new Pet();

            Console.Write("Enter name: ");
            temp.Name = Console.ReadLine();

            Console.Write("Enter type (dog, cat, parrot, etc.): ");
            temp.Type = Console.ReadLine();

            Console.Write("Enter breed (Chow, German Shepard, DSH, etc.): ");
            temp.Breed = Console.ReadLine();

            bool result = petAPIService.AddPet(temp);
            if (result)
            {
                Console.WriteLine("Added.");
            }
            else
            {
                Console.WriteLine("ERROR: Unable to add.");
            }
        }
    }
}




