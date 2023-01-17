
public class ClaimRepository
{
    private readonly List<Claim> _clmDb = new List<Claim>();
    private int _count;

    private int _claimAmount;


   public void SetCurrentAmount( string ClaimMny)
    {
        _claimAmount = Convert.ToInt32(ClaimMny);
    }
    public ClaimRepository()
    {
        SeedData();
    }

    public bool AddClmToDb( Claim clm)
    {
        return (clm is null) ? false : AddToDatabase(clm);
    }

    //helper method -> Create
    private bool AddToDatabase(Claim clm)
    {
        AssignId(clm);
        _clmDb.Add(clm);
        return true;
    }

    //helper method -> Create
    private void AssignId(Claim clm)
    {
        _count++;
        clm.Id = _count;
    }

    public List<Claim> GetClaims()
    {
        return _clmDb;
    }

    public Claim GetClaim(int id)
    {
        //LINQ
        return _clmDb.SingleOrDefault(clm => clm.Id == id);
    }

    public bool UpdateClaimData(int clmId, Claim updatedData)
    {
        Claim clmInDb = GetClaim(clmId);

        if (clmInDb != null)
        {
            clmInDb.ClaimType = updatedData.ClaimType;
            clmInDb.Description = updatedData.Description;
            clmInDb.ClaimMny = updatedData.ClaimMny;
            clmInDb.IncidentDt = updatedData.IncidentDt;
            clmInDb.IsValid = updatedData.IsValid;
            return true;
        }
        return false;
    }
   
    

    public bool DeleteDeveloperData(int clmId)
    {
        Claim clmInDb = GetClaim(clmId);
        return _clmDb.Remove(clmInDb);
    }

    //challenge -> Devs without pluralsight
    public List<Claim> ClaimListOrganizedByID()
    {
        return _clmDb.OrderBy(clm => clm.Id).ToList();
    }

    private void SeedData()
    {
        var clmA = new Claim("Car", "Superhero battle", "3200","11/20/2022", true);
        var clmB = new Claim("Home", "Gas Explosion", "110000", "11/12/2000", false);
        var clmC = new Claim("Theft", "Pink Panther", "1400", "11/25/2022", true);

        //add to db
        AddClmToDb(clmA);
        AddClmToDb(clmB);
        AddClmToDb(clmC);
    }
}
