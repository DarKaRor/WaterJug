# Water Jug Puzzle

This program will solve the Water Jug Riddle where we are presented with **2** bottles of water that can hold up to a certain amount of liters, and are asked to measure a specific amount of liters using those two bottles. The bottles have no indicator of how much water they are holding other than using each other to substract water from them. We also assume there's an infinite amount of water available to be used.

There are only **3** actions that can be done on each bottle . (One per step)
Empty the bottle
Fill the bottle
Transfer the water from the bottle to the other bottle.

The solution is obtained once any of the bottles hold the desired amount.

Example:
First bottle can measure **10** liters of water.
Second bottle can measure **2** liters of water.
We need to measure **4** liters of water.

|Bottle 1|Bottle 2| Explanation |
|--|--|--|
|0| 2 |Fill the second bottle|
|2| 0 |Transfer the water of the second bottle to the first bottle|
|2| 2 |Fill the second bottle|
|4| 0 |Transfer the water of the second bottle to the first bottle|

We have reached **4** liters by using just the two bottles, knowing how much they can hold.

The solution must also be the one with the less taken steps. Since the solution can be reached by doing different sets of actions in most scenarios.

# How to use it?
When you start the program you will be asked to input the amount of liters that need to be measured. That is, the goal amount.

There are rules that need to be followed before while inputting data into the console:

 - You can't leave the input empty.
 - You must input an integer.
 - All the characters must be digits.
 - You must input a positive integer.
- The input cannot be zero.

Otherwise the program will ask you for the input once more.

After you input the goal amount, you will be asked for the amount of liters that the first bottle can hold. Before inputting this data you must take on count that **the liters of both bottles shouldn't both be less than the desired amount, since it would be impossible to measure that amount using those two bottles.**

Finally, the program will ask for the amount of liters that the second bottle can hold.

With all set, it will prompt a table containing all the steps that need to be taken in order to measure the desired amount, if possible. If the desired amount cannot be  measured using those two bottles, it will say that there were no results and notify how many cases were found and analyzed.

# Functionality
In this section I explain how the algorithm of the program works.

 - First, it will ask the user for input, making all the validations
    needed in order for the program to work and forcing the user to type
    a valid input.
	
 - After getting the input, it will validate if the solution is known by merely looking at the given inputs. If any of the bottles can measure speficially the desired amount, then by just filling that bottle we have the solution. If both bottles hold the same amount, and none are the desired amount, we can't have a solution, among other cases.
 - The program will then start saving its current state, that is, the amount of liters that both bottles hold at the moment, the latest action that has been done with the bottles, and the steps that have been taken so far.
 - The states will be saved inside an array. 
 - The program will do every action (if possible) on the current state. (Fill, Empty, Transfer) for each bottle.
 - Every result from doing the actions will be saved as new state.
 - The program keeps track of the amount of states evaluated, so every new state will be evaluated the exact same way. Doing all the possible actions and saving its results.
 - If the program finds the desired result in any of the analyzed cases, then the program will stop its execution.
 - Otherwise the program will keep evaluating until no action can be performed, let it be because it's not a possible action (Filling a bottle that's already filled), or because the result of doing that action will bring a repeated state.
 - Finally the program will display the results.
 
 # Classes
 **Cup class:**
 
 
 - Variables
   - target [type **Cup**]: It's a reference to another cup, when the current instance of cup makes a call to the **Transfer** method it will use this variable to transfer it liters to.
   
   - liters [type **int**]: The max amount of liters the cup can fill.
   - currentLiters [type **int**]: The current amount of liters the cup contains.
  - Methods
    - IsEmpty [return type **bool**]: Returns true if the cup if **currentLiters** is 0.
    
    - IsFull [return type **bool**]: Return true if **currentLiters** equals **liters**
    
    - Fill [return type **void**]: Will assign the max amount of liters to **currentLiters**
    
    - Empty [return type **bool**: Will return false if the cup is already empty. If it isn't, it will empty it and return true.
    - Transfer [return type **bool**]: Will transfer the current amount of liters to the target Cup. If the cup is empty or the target cup is full, it will return false. Otherwise it will return true.
    - FillIfEmpty [return type **bool**]: It will return false if **target** is already full, otherwise  it will fill the current cup if it's not already full and return true.

 **State class:**
 
 
 - Variables
   - cupXAmount [type **int**]: The amount in liters of the first cup.
   
   - cupYAmount [type **int**]: The amount in liters of the second cup
   - order [type **int**]: The number of the step of this state. (At which point did it happen)
   - previousState [type **State**]: The **State** object that came before the current instance, an action was done that made it change and turn into the current instance.
   - previousOption [type **OptionKey**] The action that was done in order for the current state to happen.
  - Methods
    - DevPrint [return type **string**]: Returns a string containing all the data of the object.
    
 **Option class:**
 - Variables
   - key [type **OptionKey**]: Enum member representing the name of the action that this **Option** object represents.
   
   - action [type **Func< bool >**]: The function that will be executed in this **Option** object

# Requirements

 - .NET 6.0 version installed in your computer. 
 - Windows OS.
