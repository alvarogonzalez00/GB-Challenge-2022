//POCO -> Plain Old C# Object
public class Claim
{
    public Claim()
    {

    }
    public Claim(string claimType, string description, string claimMny, string incidentDt, bool isValid)
    {
        ClaimType = claimType;
        Description = description;
        ClaimMny = claimMny;
        IncidentDt = incidentDt;
        IsValid = isValid;
    }
    public int Id { get; set; }
    public string ClaimType { get; set; }
    public string Description { get; set; }
    public string ClaimMny { get; set;}
    public string IncidentDt { get; set;}
    public bool IsValid {get; set;}


    private DateTime DateOfClaim
    {
        get
        {
            return DateTime.Today;
        }
    }
    
//Format DateOfAccident
  DateTime DateOfAccident
  {
    get
    {
        return DateTime.Parse(IncidentDt);
    }
  }

  //Format Claim Amount to Int from String Input
    public int ClaimAmount
    {
        get
        {
            return Convert.ToInt32(ClaimMny);
        }
    }

    public override string ToString()
    {
        //Google:  C# stringbuilder....
        string str = $"ID: {Id.ToString()}\n"
                     + $"Type: {ClaimType}\n"
                     + $"Description: {Description}\n"
                     + $"Amount: ${ClaimAmount}\n"
                     + $"DateOfAccident: {DateOfAccident}\n"
                     + $"DateOfClaim: {DateOfClaim}\n"
                     + $"IsValid: {IsValid}\n"
                     + "====================================\n";
        return str;
    }
}
