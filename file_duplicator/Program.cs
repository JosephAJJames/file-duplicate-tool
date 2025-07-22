using System.ComponentModel;
using System.Net;
using System.Runtime.Intrinsics.Arm;
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
            byte[]? Hash_Result = Hash(file);

            if (Hash_Result != null)
            {

            }
        }
    }
    public byte[]? Hash(String file)
    {
        try
        {
            using (FileStream fs = File.Open(file, FileMode.Open))
            {
                using (SHA256 Sha256 = SHA256.Create())
                {
                    fs.Position = 0;
                    return Sha256.ComputeHash(fs);
                }

            }
        }
        catch (Exception e)
        {
            return null;
        }
    }
}