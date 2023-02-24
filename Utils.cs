namespace WaterJug
{
    internal static class Utils
    {
        public static void ReadKeyWhenAble()
        {
            while (Console.KeyAvailable)
                Console.ReadKey(false); // skips previous input chars

            Console.ReadKey(); // reads a char
        }
        
        // This function will return a boolean representing if a string is a digit.
        public static bool StringIsInteger(string? value)
        {
            if(value is null)
                return false;

            value = value.Trim();
            value = value.Replace(" ", string.Empty);

            return value.All(char.IsDigit);
        }

        // This function will print something to the console then return a boolean value.
        public static bool PrintAndReturn(string message, bool toReturn = false)
        {
            Console.WriteLine(message);
            return toReturn;
        }

        // This function will something to the console then wait for a key press.
        public static void PrintAndWait(string message)
        {
            Console.WriteLine(message);
            Console.ReadKey();
        }
        
        // This function retuns the length of the longest string inside an array of strings.
        public static int GetLargestLength(string[] strings) => strings.OrderByDescending(x => x.Length).First().Length;

        // This method will swap a and b values
        public static void SwitchIntegers(ref int a, ref int b)
        {
            a += b;
            b = a - b;
            a -= b;
        }
    }
}
