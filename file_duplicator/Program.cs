using System.ComponentModel;
using System.Net;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Collections;
public class Hasher
{
    private Dictionary<string, int> table = new Dictionary<string, int>();
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
                this.add(Convert.ToBase64String(Hash_Result));
            }
        }

        Console.WriteLine(this.table);
    }

    public void add(string Hash_Result)
    {
        if (!table.ContainsKey(Hash_Result))
        {
            table[Hash_Result] = 1;
        }
        else
        {
            table[Hash_Result] = (int)table[Hash_Result] + 1;
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
        catch (Exception e) //file opening or hashing has failed
        {
            return null;
        }
    }
}