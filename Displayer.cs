namespace WaterJug
{

    // This class will contain all the data that will be displayed as well as the functionality that will print them properly to the console.
    internal class Displayer
    {
        public List<DisplayColumn> columns = new List<DisplayColumn>();
        public string separator;

        public Displayer(List<DisplayColumn> columns, string separator)
        {
            this.columns = columns;
            this.separator = separator;
        }

        // This is the main function that will be called.
        // It gets the headers and prints them, then loops through the rows and gets the data from each column, then prints them.
        public void Display()
        {
            DisplayRow(columns.Select(x => x.header).ToList(), ConsoleColor.Green);
            for (int i = 0; i < columns[0].content.Length; i++)
            {
                List<string> ToDisplay = new List<string>();
                foreach (DisplayColumn col in columns)
                    ToDisplay.Add(col.content[i]);
                DisplayRow(ToDisplay);
            }
        }
        
        // This function will print to the console a specific set of strings, which come from the DisplayColumn object.
        private void DisplayRow(List<string> strings, ConsoleColor consoleColor = ConsoleColor.Blue)
        {
            string result = "";
            foreach (string str in strings)
                result += str + separator;
            result += "\n" + new string('-', result.Length);
            Console.ForegroundColor = consoleColor;
            Console.WriteLine(result);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }

}
