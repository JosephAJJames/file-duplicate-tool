using System.ComponentModel;
using System.Net;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Collections;
using System.Runtime.CompilerServices;
public class Hasher
{
    private Dictionary<string, int> hash_to_frequeny = new Dictionary<string, int>();

    private Dictionary<string, List<string>> hash_to_name = new Dictionary<string, List<string>>();
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
                this.create_hash_to_file(Convert.ToBase64String(Hash_Result), file);
                this.add(Convert.ToBase64String(Hash_Result));
            }
        }

        List<string> file_sigs = this.get_duplicates();
        foreach (string file_sig in file_sigs)
        {
            List<string> files_to_be_deleted = this.hash_to_name[file_sig];
            delete_files(files_to_be_deleted, Folder_Name);
        }

    }

    public void create_hash_to_file(string hash, string name)
    {
        if (!hash_to_name.ContainsKey(hash))
        {

            List<string> list = new List<string>();
            list.Add(name);
            hash_to_name[hash] = list;
        }
        else
        {

            hash_to_name[hash].Add(name);
        }
    }

    public void delete_files(List<string> file_sigs, string Folder_Name)
    {
        string targetFolder = Path.Combine(Folder_Name, "Duplicates");
        for (int x = 0; x < file_sigs.Count - 1; x++)
        {
            string org_path = (string)file_sigs[x];
            string file_Name = Path.GetFileName(org_path);
            string dest = Path.Combine(targetFolder, file_Name);

            File.Move(org_path, dest);

        }
    }

    public void add(string Hash_Result)
    {
        if (!hash_to_frequeny.ContainsKey(Hash_Result))
        {
            hash_to_frequeny[Hash_Result] = 1;
        }
        else
        {
            hash_to_frequeny[Hash_Result] = (int)hash_to_frequeny[Hash_Result] + 1;
        }
    }

    public List<string> get_duplicates()
    {
        List<string> file_sigs = new List<string>();
        foreach (string key in this.hash_to_frequeny.Keys)
        {
            if (this.hash_to_frequeny[key] > 1)
            {
                file_sigs.Add(key);
            }
        }


        return file_sigs;
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