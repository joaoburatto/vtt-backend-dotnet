using System;
using System.IO;

namespace Database
{
    public static class LocalData
    {
	private static string GetRelativePath(string fileName)
	{
            String root = AppContext.BaseDirectory;
            String path = Path.Combine(root, ".database", $"{fileName}.json");
            return path;
        }

        // Get, Load, Save
        public static void SaveJson(string fileName, string data)
	{
            String path = GetRelativePath(fileName);
            // Ensure path exist
            Directory.CreateDirectory(Path.GetDirectoryName(path)!);

            File.AppendAllText(path, data + "\n");

	    Console.WriteLine($"File saved at: {path}");

	}

	public static void ClearJson() // If we choose to delete the "DB"
	{

	}

	public static void LoadJson()
	{
	    //TODO
	}

    }
}
