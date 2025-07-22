using System.ComponentModel;
using System.Security.Cryptography;
public class Hasher
{
    public void Run(String Folder_Name)
    {
        if (!Directory.Exists(Folder_Name))
        {
            return;
        }

        String[] files = Directory.GetFiles(Folder_Name);
        foreach (String file in files)
        {
            using (FileStream fs = File.Open(file, FileMode.Open))
            {
                Console.WriteLine(fs);
            }
        }
    }
    public void Hash()
    {
        return;
    }
}