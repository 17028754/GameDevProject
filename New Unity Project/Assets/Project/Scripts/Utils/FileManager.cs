using System;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;

public static class FileManager
{

	// File path: C:\Users\Vincent\AppData\LocalLow\DefaultCompany\New Unity Project <--- change the user name

    public static bool WriteToFile(string a_FileName, string a_FileContents)
    {
        var fullPath = Path.Combine(Application.persistentDataPath, a_FileName);

        try
        {
            File.WriteAllText(fullPath, a_FileContents);
            return true;
        }
        catch (Exception e)
        {
            // Debug.LogError($"Failed to write to {fullPath} with exception {e}");
            return false;
        }
    }

    public static bool LoadFromFile(string a_FileName, out string result)
    {
        var fullPath = Path.Combine(Application.persistentDataPath, a_FileName);

        try
        {
            result = File.ReadAllText(fullPath);
            return true;
        }
        catch (Exception e)
        {
            // Debug.LogError($"Failed to read from {fullPath} with exception {e}");
            result = "";
            return false;
        }
    }
}