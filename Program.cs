using System.Runtime.CompilerServices;

namespace WaterJug
{
    static internal class Program
    {
        // MeasureX will always be the highest value.
        static int measureX = 0;
        static int measureY = 0;
        static int amountToMeasure = 0;
        static Cup cupX;
        static Cup cupY;
        // States represents a list of all the measurements and moves that have been saved by the program.
        static List<State> states = new List<State>();
        // IndexToEvaluate represents the State that should be evaluated at the moment.
        static int indexToEvaluate = 0;
        static State parent;
        static OptionKey option;
        static Option[] options;

        //  This Dictionary will allow the program to get an explanation of an option using its key.
        static Dictionary<OptionKey, string> Explanations = new Dictionary<OptionKey, string>
        {
            { OptionKey.FillX, "Fill the first cup (X)" },
            { OptionKey.FillY, "Fill the second cup (Y)" },
            { OptionKey.TransferX, "Transfer the first cup (X) to the second cup (Y)" },
            { OptionKey.TransferY, "Transfer the second cup (Y) to the first cup (X)" },
            { OptionKey.EmptyX, "Empty the first cup (X)" },
            { OptionKey.EmptyY, "Empty the second cup (Y)" }
        };


        public static void Main(string[] args)
        {
            // This is an array of Option objects, which contain an enum representing which action is being taken and a function that executes said action.
            options = new Option[]
            {
                new Option(OptionKey.FillX,     new Func<bool> (() => cupX.FillIfEmpty())),
                new Option(OptionKey.FillY,     new Func<bool> (() => cupY.FillIfEmpty())),
                new Option(OptionKey.EmptyX,    new Func<bool> (() => cupX.Empty())),
                new Option(OptionKey.EmptyY,    new Func<bool> (() => cupY.Empty())),
                new Option(OptionKey.TransferX, new Func<bool> (() => cupX.Transfer())),
                new Option(OptionKey.TransferY, new Func<bool> (() => cupY.Transfer()))
            };

            GetUserInput();

            // Initialize cups.
            CreateCups();

            if (CheckBeforeStart() is not true)
                EndProgram();
                return;


            // While there exists an state that hasn't been tested.
            while (indexToEvaluate < states.Count)
            {
                // Do every action in the current state.
                // If the action reached the goal, exit the loop.
                if (DoOption(states[indexToEvaluate]))
                    break;

                indexToEvaluate++;
            }

            Console.Clear();
            Console.WriteLine($"Having a cup that measures up to {measureX} liters, and a cup that measures up to {measureY} liters. \nHow do you measure {amountToMeasure} liters?\n");

            if (!ReachedGoal())
                Console.WriteLine($"There isn't a solution to this problem with given parameteres. Analyzed {states.Count} different combinations.");

            else 
                DisplayResults();

            EndProgram();
        }


        private static bool CheckBeforeStart()
        {
            if (measureX == amountToMeasure || measureY == amountToMeasure)
            {
                // Should return result with only one step.
                string Equal = measureX == amountToMeasure ? "X" : "Y";
                return Utils.PrintAndReturn($"The result can be obtained by filling the Cup{Equal} since it is the same amount as the desired amount.");
            }

            if (measureX < amountToMeasure && measureY < amountToMeasure)
                return Utils.PrintAndReturn("There is no result because both cups hold less water than the desired amount.");

            if (measureX == measureY && measureX != amountToMeasure)
                return Utils.PrintAndReturn("There is no result because both cups hold the same amount, and it is not the desired amount.");

            if((float)measureX / measureY == 2)
                return Utils.PrintAndReturn("There is no result because one cup is half the size of the other. And none are the desired amount.");

            return true;
        }

        // This method will take the User's input and save them to the variables needed.
        private static void GetUserInput()
        {
            AskUntilValid(out amountToMeasure, "the goal");
            AskUntilValid(out measureX, "the first cup");
            AskUntilValid(out measureY, "the second cup");
        }

        // This method will ask the user for input until it satisfies the rules.
        private static void AskUntilValid(out int liters, string name)
        {
            liters = 0;
            while (IsValidMeasure(liters) is not true)
            {
                Console.Clear();
                Console.WriteLine($"Please insert the liter amount of {name}. Remember it has to be a positive integer value.");
                string? line = Console.ReadLine();

                if (string.IsNullOrEmpty(line))
                    Utils.PrintAndWait("You can't type an empty value.");

                else if (Utils.StringIsInteger(line) is false)
                    Utils.PrintAndWait("The amount must be an integer value.");

                else if (int.TryParse(line, out liters) is false)
                    Utils.PrintAndWait("The value entered could not be parsed to an integer.");

                else if (IsValidMeasure(liters) is false)
                    Utils.PrintAndWait("The value you entered is not in the correct range. \nRemember it has to be higher than zero." + (amountToMeasure is not 0 ? "\nAnd both cups can't both individually hold less water than the desired goal." : ""));
            }
        }

        // This function will return if an amount in liters is considered valid.
        private static bool IsValidMeasure(int liters)
        {
            if (liters <= 0)
                return false;

            if (measureX is not 0 && measureY is not 0)
                if(measureX < amountToMeasure && measureY < amountToMeasure)
                    return false;

            return true;
        }

        // This function will return a State object containing the program's current variables.
        private static State GetCurrentState() => new State(cupX.currentLiters, cupY.currentLiters, parent?.order + 1 ?? 0, parent, option);

        // This function will check if a state is repeated.
        // If a state contains the same liters in both CupX and CupY, it is considered a repeated state.
        public static bool IsStateRepeated(State state = null, List<State> path = null)
        {
            path = path ?? new List<State>(states);
            state = state ?? GetCurrentState();
            return states.Find(stateAux =>
                               stateAux.cupXAmount == state.cupXAmount &&
                               stateAux.cupYAmount == state.cupYAmount)
                               is not null;
        }

        // This function adds the current state to the states list.
        public static void SaveState() => states.Add(GetCurrentState());

        // This function will set the liters of the cups to the state's liters.
        public static void LoadState(State stateToLoad)
        {
            cupX.currentLiters = stateToLoad.cupXAmount;
            cupY.currentLiters = stateToLoad.cupYAmount;
        }

        // This function will initialize both cups. Adding the user's input liters and referencing each other as targets.
        // Then it will add the default state to the states list.
        public static void CreateCups()
        {
            if (measureX < measureY)
            {
                Utils.SwitchIntegers(ref measureX, ref measureY);
                Console.WriteLine("The values of the cups have been switched in order for the program to process them properly.");
            }
            cupX = new Cup(null, measureX, 0);
            cupY = new Cup(null, measureY, 0);
            cupX.target = cupY;
            cupY.target = cupX;
            SaveState();
        }

        // This function will loop through each possible action to be taken on the current state.
        // Each action is defined inside the Cup class. If the action can be done, and the action does not create an already existing state...
        // The state will be added into the list of states, and will check if the goal has already been reached.
        public static bool DoOption(State currentState)
        {
            // Loop through each option
            for (int i = 0; i < options.Length; i++)
            {
                LoadState(currentState);
                if (options[i].action())
                {
                    if (IsStateRepeated())
                        continue;
                    // Update variables of the program's state.
                    option = options[i].key;
                    parent = currentState;
                    SaveState();
                    if (ReachedGoal())
                        break;
                }
            }
            return ReachedGoal();
        }

        // Will return true if any of the cups have reached the desired amount.
        public static bool ReachedGoal() => cupX.currentLiters == amountToMeasure ||
                                            cupY.currentLiters == amountToMeasure;

        // This method will get the last state (the one that reached the goal)
        // Then it will loop through each parent, or previous State, that came before it.
        // It will reverse so it gets ordered from start to end
        // Then use a Displayer object to print the results
        public static void DisplayResults()
        {
            State currentState = states.Last();
            List<State> pathToResult = new List<State>();
            while (currentState.previousState is not null)
            {
                pathToResult.Add(currentState);
                currentState = currentState.previousState;
            }
            pathToResult.Reverse();

            DisplayColumn CupXColumn = new DisplayColumn(pathToResult.Select(x => x.cupXAmount.ToString()).ToArray(), "CupX");
            DisplayColumn CupYColumn = new DisplayColumn(pathToResult.Select(x => x.cupYAmount.ToString()).ToArray(), "CupY");
            DisplayColumn ExplanationColumn = new DisplayColumn(pathToResult.Select(x => Explanations[x.previousOption]).ToArray(), "Explanation");

            List<DisplayColumn> columns = new List<DisplayColumn> { CupXColumn, CupYColumn, ExplanationColumn };

            Displayer displayer = new Displayer(columns, "|");

            displayer.Display();

            Console.WriteLine($"\nThe goal was reached after {pathToResult.Count()} steps");
            
        }

        private static void EndProgram()
        {
            Thread.Sleep(2000);
            Console.WriteLine("Press any key to finish...");
            Utils.ReadKeyWhenAble();
            Environment.Exit(0);
        }
    }
}