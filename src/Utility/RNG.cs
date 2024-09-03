namespace VTT.Utility
{
    internal static class RNG
    {
        private static Random Instance { get; } = new Random();
        
        public static int Random(int min, int max)
        {
            return Instance.Next(min, max);
        }
    }
}