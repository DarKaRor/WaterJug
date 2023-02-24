namespace WaterJug
{
    // This class represents the state of the program at a given time.
    // It saves the liters of the cups, the steps taken to reach that state, the previous state before the any change, as well as the action taken to reach the state.
    internal class State
    {
        public int cupXAmount { get; set; }
        public int cupYAmount { get; set; }
        public int? order { get; }
        public State previousState { get; set; }
        public OptionKey previousOption { get; set; }

        public State(int cupXAmount, int cupYAmount, int? order, State previousState, OptionKey previousAction)
        {
            this.cupXAmount = cupXAmount;
            this.cupYAmount = cupYAmount;
            this.order = order;
            this.previousState = previousState;
            this.previousOption = previousAction;
        }

        public override string ToString() =>  $"[{cupXAmount},{cupYAmount}]";
        public string DevPrint() => $"{ToString()} Order: {order} Move: {previousOption} Previous: {previousState}";
    }
}
