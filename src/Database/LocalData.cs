using System.Globalization;

namespace VTT.Database
{
	public static class LocalData
	{
		private static string GetRelativePath(string fileName)
		{
			string root = AppContext.BaseDirectory;
			string path = Path.Combine(root, ".database", $"{fileName}.json");
			
			return path;
		}

		// Get, Load, Save
		public static void SaveJson(string fileName, string data)
		{
			string path = GetRelativePath(fileName);
			
			Directory.CreateDirectory(Path.GetDirectoryName(path)!);
			File.AppendAllText(path, data + "\n");
			
			Console.WriteLine($"File saved at: {path}");
		}


		public static void SaveWithTimestamp<T>(string json)
		{
			DateTime time = DateTime.UtcNow;
			
			SaveJson($"{typeof(T).Name} - {time.Hour}-{time.Minute}-{time.Second}", json);
		}
	}
}
