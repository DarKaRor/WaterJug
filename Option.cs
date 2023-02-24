namespace WaterJug
{
    // Enum representing the actions that can be taken on a cup.
    public enum OptionKey
    {
        FillX,
        FillY,
        TransferX,
        TransferY,
        EmptyX,
        EmptyY
    };

    // This class contains a key representing the name of an action, and a function containing the code of the action to be executed.
    internal class Option
    {
        public OptionKey key { get; set;  }
        public Func<bool> action { get; set; }

        public Option(OptionKey key, Func<bool> action)
        {
            this.key = key;
            this.action = action;
        }
    }
}
