using static System.Console;

public static class DTUtils
{
    public static bool isRunning = true;

    public static void PressAnyKey()
    {
        
        WriteLine("Press any Key To Continue.");
        ReadKey();
    }

}
