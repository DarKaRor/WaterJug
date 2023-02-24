namespace WaterJug
{

    // This class contains all the data of a specific column that will be printed into the console.
    // The header, the data, the padding, and the total space that will be needed with the longest word and the extra padding.
    internal class DisplayColumn
    {
        public string[] content;
        public string header;
        public int extraPadding;
        public int totalSpace;

        public DisplayColumn(string[] content, string header, int extraPadding = 2)
        {
            this.content = content;
            this.header = header;
            this.extraPadding = extraPadding;
            GetTotalSpace();
            AddPadding();
        }

        // Assigns to the variable totalSpace the length of the largest string plus the extra padding.
        public void GetTotalSpace()
        {
            totalSpace = Utils.GetLargestLength(new List<string>(content) { header }.ToArray()) + extraPadding;
        }

        // Adds the needed padding to all the data inside the column.
        public void AddPadding()
        {
            content = content.Select(str => str.PadBoth(totalSpace)).ToArray();
            header = header.PadBoth(totalSpace);
        }
    }
}
