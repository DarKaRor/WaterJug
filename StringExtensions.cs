namespace WaterJug
{
    internal static class StringExtensions
    {

        // This method will add padding to both sides of the string.
        public static string PadBoth(this string str, int length)
        {
            int padToAdd = (length - str.Length);
            int padLeft = padToAdd / 2 + str.Length;
            return str.PadLeft(padLeft).PadRight(length);
        }
    }
}
