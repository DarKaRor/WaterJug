namespace WaterJug
{
    internal class Cup
    {
        // This Cup's target Cup. The Cup where the water would be transfered.
        public Cup? target { get; set; } 
        // Max liters.
        int liters { get; set; }
        public int currentLiters { get; set; }
        private bool IsEmpty() => currentLiters == 0;
        private bool IsFull() => currentLiters == liters;
        private void Fill() => currentLiters = liters;

        public Cup(Cup target, int liters, int currentLiters)
        {
            this.target = target;
            this.liters = liters;
            this.currentLiters = currentLiters;
        }

        // Will return false if the cup is already empty. If it isn't, it will empty it and return true.
        public bool Empty()
        {
            if(IsEmpty()) 
                return false;
            currentLiters = 0;
            return true;
        }

        // Will transfer the current amount of liters to the target Cup. If the cup is empty or the target cup is full, it will return false. 
        // Otherwise it will return true.
        public bool Transfer()
        {
            if(IsEmpty() || target.IsFull())
                return false;

            // Saving the target's current amount in an auxiliar variable.
            int previous = target.currentLiters;
            // Adding this Cup's current liters to the target's current liters.
            target.currentLiters += currentLiters;
            // Substracting the transfered amount
            currentLiters -= target.liters - previous;

            if (target.currentLiters > target.liters) 
                target.Fill();

            if (currentLiters < 0)
                currentLiters = 0;

            return true;
        }

        public bool FillIfEmpty()
        {
            // Return false if the target is full to avoid having both cups full.
            if(target.IsFull())
                return false;
            bool wasFull = IsFull();

            if (wasFull is not true)
                Fill();

            return !wasFull; 
        }

        public override string ToString() => currentLiters.ToString();
    }
}
