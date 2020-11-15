using System;

namespace PragueParking1._0
{
    class Program
    {
        //Global variable
        static string[] parkingSpaces = new string[100]; // single diminsional array for parking with 100 spaces.

        static void Main(string[] args)
        {
            Startup(); // call method Startup
            bool running = true;
            while (running)
            {
                //Main menu Selection//
                int selection = MainMenu(); // calls MainMenu that returns int.
                switch (selection)
                {
                    case 1:
                        RegisterVehicle(); // calls  RegisterVehicle 
                        break;
                    case 2:
                        MoveVehicle(); //Calls MoveVehicle Method
                        break;
                    case 3:
                        UnregisterVehicle(); //Calls UnregisterVehicle Method
                        break;
                    case 4:
                        SearchVehicle(); //Calls SearchVehicle Method
                        break;
                    case 5:
                        ShowAllParkingSpots(); //Calls  ShowAllParkingSpots Method
                        break;
                    case 0:
                        bool sure = Exit(); // Calls Exit Method
                        if (sure)
                        {
                            running = false;
                            break;
                        }
                        else
                        {
                            break;
                        }
                    default:
                        Console.WriteLine("Please, make a selection between 0 - 5. \n" +
                            "-------------------- \n");
                        break;
                }
            }
        }

        //Startup med empty metod //
        static void Startup()
        {

            for (int i = 0; i < parkingSpaces.Length; i++) //Loop on all parkingspaces...
            {
                parkingSpaces[i] = "EMPTY"; // Here all spaces is filled with "EMPTY" string.
            }

        }

        //Main menu metod//
        static int MainMenu()
        {
            Console.WriteLine("Main Menu: \n" +
                "Press one of the following numbers to select your option. \n" +
                "1 - Register Vehicle \n" +
                "2 - Move Vehicle \n" +
                "3 - Unregister Vehicle \n" +
                "4 - Search Vehicle Registration \n" +
                "5 - Show All Parking Spots \n" +
                "0 - EXIT");
            if (int.TryParse(Console.ReadLine(), out int result)) // checks that if an integer has been set as the menu option.
            {
                return result;
            }
            else // otherwise we will get an error message...
            {
                Console.WriteLine("Invalid input! \n" +
                    "Please, make a selection using integer only. \n");
                return 404; // custom internal error code for items not found.
            }
        }

        static void RegisterVehicle() //Method to Register a Vehicle, in this method I am calling 2 methods, SelectType() and AddVehicle()
        {
            Console.WriteLine("You have selected '1 - Register Vechicle' \n" +
                "-------------------- \n");
            int selection = SelectType(); //retuns int, from menu selections
            if (selection != 404) // if you didn't cause internal error
            {
                switch (selection)
                {
                    case 1:
                        Console.WriteLine("You have selected to add a car. \n" +
                            "-------------------- \n");
                        AddVehicle(selection); //car selection 
                        break;
                    case 2:
                        Console.WriteLine("You have selected to add an MC. \n" +
                            "-------------------- \n");
                        AddVehicle(selection); //mc selection 
                        break;
                    case 3:
                        break;
                    default:
                        return;
                }
            }
            else
            {
                Console.WriteLine("You have made an invalid selection! \n");
            } // if you did cause internal error ;P
        }

        static int SelectType()
        {
            Console.WriteLine("Please select type: \n" +
                "1 - Car \n" +
                "2 - MC \n" +
                "3 - Main Menu");
            var selection = Console.ReadLine();
            if (int.TryParse(selection, out int result)) // make sure user enters an integer
            {
                return result;
            }
            else
            {
                return 404; // else - internal error code... because the method return type is int, so we use it to our advantage
            }
        }

        static void AddVehicle(int vehicleType)// Method AddVehicle, takes int parameter which is vehicleType. in this method i am calling 4 Methods- SetRegistration,CheckDuplicate,AddMC and else AddCar
        {
            string enteredVehicle = SetRegistration(vehicleType);// calls  SetRegistration which returns string - which is vehicleType and regNum.
            
            if (enteredVehicle.Equals("ERROR"))
            {
                Console.WriteLine("You have entered an incorrect registration. \n" +
                    "Please, try again.\n" +
                        "-------------------- \n");
                enteredVehicle = SetRegistration(vehicleType);
            }

            if (!CheckDuplicate(enteredVehicle))//checks if its not in the parking spaces already
            {
                if (enteredVehicle.Contains("MC")) //Checks what type of vehicle is going to be added... if MC...
                {
                    AddMC(enteredVehicle); //Add Mc
                }
                else //else If car...
                {
                    AddCar(enteredVehicle); //Add Car
                }
            }
        }

        static bool CheckDuplicate(string vehicle) //Method to check if there is any duplicate with vehicle string (ex. #Car ABC123), as validation
        {
            bool duplicate = false; //A boolean to know if duplicate has been found
            for (int i = 0; i < parkingSpaces.Length; i++)//Loop all parkingspaces...
            {
                if (vehicle.Equals(parkingSpaces[i])) //If vehicle (ex. #Car ABC123) already exists...
                {

                    Console.WriteLine("The entered vehicle is a duplicate! \n" +
                    "--------------------");
                    duplicate = true;
                    return duplicate; //Returns false as soon as duplicate is registered, to break the loop
                }
            }
            return duplicate; //If the loop finishes without finding a duplicate the bool is returned false
        }

        static string SetRegistration(int vehicleType) //Method to set vehicle registration, with int vehicle type parameter.
        {
            Console.WriteLine("Please enter vehicle registration. \n");
            string vehicleStorage = "";//This string will have more strings added to in the next step, but it will start empty.
            switch (vehicleType)//Switch on vechicle int coming from SelectType() - AddVehicle() - SetRegistration().
            {
                case 1:
                    vehicleStorage += "#Car "; //If user selected "1 - Car" in SelectType() (int vehicleType = 1) the string "#Car" is added to string vehicleStorage ("" = "#Car ")
                    break;
                case 2:
                    vehicleStorage += "#MC "; //and the same as above for mc.
                    break;
                default:
                    break;
            }

            string regNum = Console.ReadLine();//Ask user for registration string
            if (regNum.Length < 11)//Max 10 characters
            {
                Console.WriteLine("Registration correct!\n" +
                        "-------------------- \n");
                return vehicleStorage += regNum; //Add the registration string to string vehicleStorage ("#Car" = "#Car ABC123")
            }
            else
            {
                return "ERROR"; //return string error
            }

        }

        static void AddCar(string car)//Method to Add a Car process with a vehicle string
        {
            bool added = false; 
            int spot = 0; 
            for (int i = 0; i < parkingSpaces.Length; i++)
            {
                if (parkingSpaces[i].Equals("EMPTY"))
                {
                    parkingSpaces[i] = car;
                    added = true;
                    spot = i + 1;
                    break;
                }
            }
            if (added)
            {
                Console.WriteLine(car + " added successfully in spot " + spot + "! \n" +
                "------------------ \n");
            }
            else
            {
                Console.WriteLine("Parking lot is full at the moment! \n" +
                "Please, unregister a vehicle, or try again later :) \n" +
                "------------------ \n");
            }
        }

        static void AddMC(string mc)//Begin add mc process with a vehicle string
        {
            bool added = false;//Boolean to handle success or fail messages at the end
            int spot = 0; //An int to show user what spot vehicle has been parked (at the end)

            for (int i = 0; i < parkingSpaces.Length; i++)//This loop will start to look for SINGLE MC already parked...
            {
                if (parkingSpaces[i].Contains("MC")) //Are there any parking spaces that already has MC parked?
                {
                    if (!parkingSpaces[i].Contains("|")) //If parking space does not contain separator ("|"), we have A SINGLE MC...
                    {
                        parkingSpaces[i] += " | " + mc; //So the new MC (mc) is parked next to the SINGLE MC, which is shown by adding the separator before adding the new MC to the string. (ex. = [#MC ABC123 | #MC DEF456])
                        added = true;//Now the new MC is added, change bool to true
                        spot = i + 1;//Add +1 to index, to get correct parking spot for user
                        break;//break the loop, to continue (only add once)
                    }
                }
            }
            if (!added) //If the MC is still not added (if there was no SINGLE MC to park next to)
            {
                for (int i = 0; i < parkingSpaces.Length; i++) //This loop will start to look for EMPTY parking spots
                {
                    if (parkingSpaces[i].Equals("EMPTY")) //If the parking spot is EMPTY...
                    {
                        parkingSpaces[i] = mc; //Park the MC
                        added = true;//Change bool to true, because it is added now
                        spot = i + 1;//Add +1 to index, to get correct spot
                        break;//Break loop so not to add the mc twice (or more times).
                    }
                }
            }
            if (added) //If the mc is added now, give user success message
            {
                Console.WriteLine(mc + " added successfully in spot " + spot + "! \n" +
                "------------------ \n");
            }
            else //Else something went wrong and user can try again.
            {
                Console.WriteLine("Parking lot is full at the moment! \n" +
             "Please, unregister a vehicle, or try again later :)");
            }
        }

        static void MoveVehicle()//Method to move vehicle from one spot to another, calls 2 other methods.
        {
            Console.WriteLine("You have selected 2 - Move Vehicle. \n" +
                "Please, enter vehicle registration: \n");
            string registration = Console.ReadLine();//Ask user for registration of Vehicle to move
            if (registration.Length < 11) // make sure the entered registration is correct format
            {
                int foundOnIndex = SearchVehicleByRegistration(registration); // fetch parking index of searched vehicle, if it is found by search method
                if (foundOnIndex != 404) // if fetching didn't cause internal error
                {
                    SetNewParkingSpot(foundOnIndex, registration); // call method to set new parking spot, using found index, and vehicle registration as extra validation...
                }
                else
                {
                    Console.WriteLine("An error occured! Please, try again. \n" +
                    "---------------- \n");
                }
            }
            else
            {
                Console.WriteLine("Vehicle registrations are max 10 characters. \n");
            }
        }

        static void UnregisterVehicle()
        {
            Console.WriteLine("You have selected 3 - Unregister Vehicle \n" +
                "Please, enter registration: \n");
            string registration = Console.ReadLine();
            if (registration.Length < 11) // make sure the entered registration is correct format
            {
                int foundOnIndex = SearchVehicleByRegistration(registration); // fetch parking index
                if (foundOnIndex != 404) // if fetching didn't cause internal error
                {
                    Console.WriteLine("Unregister vehicle. Are you sure? Y/N \n"); // ask user for confirmation
                    string response = Console.ReadLine().ToLower(); // convert user entry to lower case (Y = y)
                    if (!int.TryParse(response, out int result)) // make sure an integer wasn't entered
                    {
                        if (!String.IsNullOrEmpty(response) && !String.IsNullOrWhiteSpace(response)) // make sure string isn't null, empty or whitespace.
                        {
                            if (response.Equals("y") || response.Equals("n")) // final check that only "y" or "n" has been entered.
                            {
                                switch (response)
                                {
                                    case "y": // if "y" then make the selected vehicle's spot empty
                                        parkingSpaces[foundOnIndex] = "EMPTY";
                                        Console.WriteLine("Vehicle successfully unregistered! \n");
                                        break;
                                    case "n":
                                        Console.WriteLine("Operation aborted! \n");
                                        break;
                                    default:
                                        break;
                                }
                            }
                            else
                            {
                                Console.WriteLine("Please enter 'Y' or 'N' only! \n");
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Please enter 'Y' or 'N' only! \n");
                    }
                }
            }
            else
            {
                Console.WriteLine("Invalid registration! Please, try again. \n");
            }
        }

        static void SearchVehicle() //SearchVehicle Method and this Method calls SearchVehicleByRegistration method
        {
            Console.WriteLine("You have selected 4 - Search Vehicle. \n" +
                "Please enter vehicle registration: \n");
            string registration = Console.ReadLine();
            int result = SearchVehicleByRegistration(registration); 
            if (result == 404) 
            {
                Console.WriteLine("An error occured! Please, try again. \n" +
                    "---------------- \n");
            }
        }

        static int SearchVehicleByRegistration(string registration) //Method to search vehicles by registration
        {
            bool found = false; //Boolean to know if a match is found
            int foundOnIndex = 0; //int to know what index vehicle is stored in the array
            int spot = 0;  
            if (registration.Length < 11)//Max 10 char
            {
                for (int i = 0; i < parkingSpaces.Length; i++)//This loop starts to look for vehicles that matches entered registration
                {
                    if (parkingSpaces[i].Contains(registration))//If registration exists in parkingSpaces
                    {
                        found = true; //Change bool to true
                        spot = i + 1; //Use index to set parking spot
                        foundOnIndex = i; //set index of the found vehicle, to show user where the vehicle is, and in case the vehicle has to be moved                        
                    }
                }
            }
            else//If registration is more than 10 char
            {
                Console.WriteLine("Please enter correct registration info! \n");
            }

            if (found) //If match was found show success message with parking and vehicle information
            {
                Console.WriteLine("Vehicle found on spot " + spot + ": " + parkingSpaces[foundOnIndex] + "\n" +
                    "-------------------- \n");
                return foundOnIndex;//Return the parking index, in case vehicle has to be moved.
            }
            else //No matches was found, gives fail message to user
            {
                Console.WriteLine("No vehicle found!\n" +
                 "-------------------- \n");
                return 404;//Return error code
            }
        }

        static void SetNewParkingSpot(int moveFrom, string registration) //Method to move a vehicle from one spot to another, which calls 1 more method
        {
            ShowEmptySpots();//Show user all empty spots to choose a new location.
            Console.WriteLine("\nPlease, select a new parking spot to move vehicle: \n");
            var selectedSpot = Console.ReadLine();//Ask user to enter shown empty spot
            if (int.TryParse(selectedSpot, out int result))//If user didn't make a mistake and enter a string, we can parse the input to int.
            {
                int moveToSelectedIndex = result - 1; //Convert spot to array index with -1
                int oldSpot = moveFrom + 1; //Remember old spot for success message to user later
                int newSpot = moveToSelectedIndex + 1; //Same as oldSpot

                if (parkingSpaces[moveToSelectedIndex].Equals("EMPTY") || parkingSpaces[moveToSelectedIndex].Equals("MC")) //If user has selected to move to EMPTY spot, or spot with (maybe) single MC
                {
                    if (parkingSpaces[moveFrom].Contains("MC")) //If user is moving an MC
                    {
                        if (parkingSpaces[moveFrom].Contains("|"))//If the MC to be moved is double parked
                        {
                            string[] bothMCs = parkingSpaces[moveFrom].Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries); //If MC is double parked, it has to be split into two vehicles
                            if (bothMCs.Length < 3) //The split operation should result in a string array with two vehicle entries (= .Length < 3)
                            {
                                string mc1 = bothMCs[0].Trim();//Trim() removes empty space from beginning and end of strings (there are empty spaces left over from the split operation)
                                string mc2 = bothMCs[1].Trim();//Same as above

                                if (mc1.Contains(registration))//If mc1 contains user entered registration, this is the MC that will be moved.
                                {
                                    parkingSpaces[moveToSelectedIndex] = mc1;//Set MC to selectedIndex (= the new spot)
                                    parkingSpaces[moveFrom] = mc2;//Set the other MC to now be single parked in the old spot. (ex. [#MC ABC123 | #MC DEF456] = [#MC DEF456])

                                    Console.WriteLine("Vehicle " + mc1 + " successfully moved from spot " + oldSpot + " to spot " + newSpot + " \n" +
                                        "-------------------- \n"); //Give user info for successful operation.
                                }
                                else //user entered registration is found in mc2, and mc2 will be moved.
                                {
                                    parkingSpaces[moveToSelectedIndex] = mc2; //mc2 goes to new spot
                                    parkingSpaces[moveFrom] = mc1; //mc1 is written in alone in old spot

                                    Console.WriteLine("Vehicle " + mc2 + " successfully moved from spot " + oldSpot + " to spot " + newSpot + " \n" +
                                        "-------------------- \n"); //user success message
                                }
                            }
                            else//Or something went wrong with MC parking
                            {
                                Console.WriteLine("Something went wrong. \n");
                            }
                        }
                        else//If MC is single parked in selected spot (this should not happen)
                        {
                            Console.WriteLine("Double parking is a purely automatic function!\n" +
                                "Please select an EMPTY spot.\n");
                        }
                    }
                    else//If moving car to EMPTY space
                    {
                        parkingSpaces[moveToSelectedIndex] = parkingSpaces[moveFrom]; //New index is set to equal the old index (moveFrom)
                        parkingSpaces[moveFrom] = "EMPTY"; //old parking spot is now empty

                        Console.WriteLine("Vehicle " + parkingSpaces[moveToSelectedIndex] + " successfully moved from spot " + oldSpot + " to spot " + newSpot + " \n" +
                            "-------------------- \n"); //success!
                    }
                }
                else//user did not select a shown empty spot.
                {
                    Console.WriteLine("The selected spot is not empty. \n");
                }
            }
        }

        static void ShowEmptySpots() //this Method shows a user all empty spots to choose a new location.
        {
            int gridRow = 0;
            for (int i = 0; i < parkingSpaces.Length; i++)
            {
                if (parkingSpaces[i].Equals("EMPTY"))
                {
                    int spot = i + 1; 
                    Console.Write("[" + spot + ": " + parkingSpaces[i] + "] ");
                    gridRow++;
                    if (gridRow == 10)
                    {
                        Console.WriteLine("");
                        gridRow = 0;
                    }
                }
            }
        }
        static void ShowAllParkingSpots() //Show All Parking Map
        {
            Console.WriteLine("You have selected 5 - Show All Parking Spots. \n");
            int gridRow = 0;
            for (int i = 0; i < parkingSpaces.Length; i++)
            {
                int spot = i + 1;
                Console.Write("[" + spot + ": " + parkingSpaces[i] + "] ");
                gridRow++;
                if (gridRow == 10)
                {
                    Console.WriteLine("");
                    gridRow = 0;
                }
            }
            Console.WriteLine("Showing all parking spots. \n");

        }

        static bool Exit() //Exit Method
        {
            Console.WriteLine("You have selected 0 - Exit. \nAre you sure you wish to exit? Y/N \n");
            string sure = Console.ReadLine().ToLower();

            if (sure.Equals("y") || sure.Equals("n"))
            {
                if (sure.Equals("y"))
                {
                    Console.WriteLine("Push any key to exit application...");
                    Console.ReadKey();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                Console.WriteLine("Please enter 'Y' or 'N' only! \n");
                return false;
            }
        }

    }
}

