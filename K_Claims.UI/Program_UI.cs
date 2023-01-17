using static System.Console;

public class Program_UI
{
    private ClaimRepository _clmRepo;
    public Program_UI()
    {
        _clmRepo = new ClaimRepository();
    }

    public void Run()
    {
        RunApplication();
    }

    private void RunApplication()
    {

        while (DTUtils.isRunning)
        {
            WriteLine("== Welcome to Komodo Insurance Claims ==\n" +
                  "Choose A Menu Item:\n" +
                  "1. See All Claims\n" +
                  "2. Take care of next claim\n" +
                  "3. Enter a new claim\n" +
                  "=========================\n" +
                  "0. Close Application\n");

            string userInputMenuSelection = ReadLine();
            switch (userInputMenuSelection)
            {
                case "1":
                    SeeAllClaims();
                    break;
                case "2":
                    NextClaimUp();
                    break;
                case "3":
                    InsertClaim();
                    break;
                case "0":
                    DTUtils.isRunning = CloseApplication();
                    break;
                default:
                    WriteLine("Invalid Selection");
                    DTUtils.PressAnyKey();
                    break;
            }
        }
    }

    private bool CloseApplication()
    {
        WriteLine("Thanks, for using Komodo Dev Teams.");
        DTUtils.PressAnyKey();
        return false;
    }

    private void SeeAllClaims()
    {
        Clear();
        ShowEnlistedClms();
        ReadKey();
    }



//Display Claims 
    private void ShowEnlistedClms()
    {
        Clear();
        WriteLine("\n\n==All Claims==\n\n");
        WriteLine("ClaimId".PadLeft(100) +
                  "Type".PadLeft(1) +
                  "Description".PadLeft(1) +
                  "Amount".PadLeft(1) +
                  "DateOfAccident".PadLeft(1) +
                  "DateOfClaim".PadLeft(1) +
                  "IsValid".PadLeft(1));
        List<Claim> clmsInDb = _clmRepo.GetClaims();
        ValidateClaimDatabaseData(clmsInDb);
    }

//Validate if there are claims in the Database
    private void ValidateClaimDatabaseData(List<Claim> clmsInDb)
    {
            if (clmsInDb.Count > 0)
            {
                Clear();
                foreach (var clm in clmsInDb)
                {
                    DisplayClmData(clm);
                }
            }
            else
            {
                WriteLine("There are no Claims in the Database.");
            }
    }

    private void DisplayClmData(Claim clm)
    {
        WriteLine(clm);
    }

    private void InsertClaim ()
    {
        Clear();
        try
        {
            Claim clm = InitialClmCreationSetup();
            if (_clmRepo.AddClmToDb(clm))
            {
                WriteLine($"Successfully Added {clm.Id} to the Database!");
            }
            else
            {
                SomethingWentWrong();
            }
        }
        catch
        {
            SomethingWentWrong();
        }
        ReadKey();
    }

//Create New Claim
    private Claim InitialClmCreationSetup()
    {
        Claim clm = new Claim();
        WriteLine("\n== Add Claim Menu ==\n");
//Switch method to ensure proper claim type is inputted
        WriteLine("Type: ");
        string userInputType = ReadLine();
            switch (userInputType)
            {
                case "Car":
                    clm.ClaimType = "Car";
                    break;
                case "Home":
                    clm.ClaimType = "Home";
                    break;
                case "Theft":
                    clm.ClaimType = "Theft";
                    break;
                default:
                    WriteLine("Invalid Selection");
                    WriteLine("!No Type was Recorded!");
                    DTUtils.PressAnyKey();
                    break;
            }

        WriteLine("Enter a claim description: ");
        clm.Description = ReadLine();

        WriteLine("Amount of Damage: $");
        clm.ClaimMny = ReadLine();
        

//Input Date Of Accident while also validating if it is within 30 days
        bool inputHasBeenValidated = false;
        DateTime userInputedIncidentDate = DateTime.MinValue;
        WriteLine("DateOfAccident: ");
        while (!inputHasBeenValidated)
        {
            userInputedIncidentDate = DateTime.Parse(Console.ReadLine());
            if ((DateTime.Now - userInputedIncidentDate).TotalDays < 30)
            {
                clm.IncidentDt = userInputedIncidentDate.ToString();
                clm.IsValid = true;
                inputHasBeenValidated = true;
            }
            else
            {
                clm.IncidentDt = userInputedIncidentDate.ToString();
                clm.IsValid = false;
                inputHasBeenValidated = true;
            }
        }

        
        return clm;
    }

private void NextClaimUp()
{
    Clear();
    WriteLine("== Claim Queue ==\n" +
            "Here are the details for the next claim to be handled: \n");
    ShowNextClaimUp();
    

}

private void ShowNextClaimUp()
{
    List<Claim> nxtClmUpList = _clmRepo.ClaimListOrganizedByID();
    while (nxtClmUpList.Count > 0)
    {
        var nxtClm = nxtClmUpList.FirstOrDefault();
        DisplayClmData(nxtClm);
    WriteLine("Do you want to handle this claim now(y/n)? ");
    string userDeleteInput = ReadLine();
    if (userDeleteInput == "Y".ToLower())
    {
        if (_clmRepo.DeleteDeveloperData(nxtClm.Id))
        {
            WriteLine($"The Claim with ID: {nxtClm.Id} was SUCCESSFULLY DEALT WITH!\n\n\n\n\n\n");
            break;
            
        }
        
    }
     else
        {
            WriteLine($"The Claim with ID: {nxtClm.Id} was NOT DEALT WITH");
            break;
        }
    Clear();
    }
}


private void SomethingWentWrong()
    {
        WriteLine("Something went wrong.\n" +
                       "Please try again\n");
    }

}
