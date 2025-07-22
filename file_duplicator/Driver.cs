using System;

public class Driver
{
    public String Folder_Name => $"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}\\Downloads";
    public static void Main(String[] args)
    {
        Driver driver = new Driver();
        Hasher hasher = new Hasher();
        hasher.Run(driver.Folder_Name);
    }
}