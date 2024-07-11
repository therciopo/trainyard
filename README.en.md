# Trains

_Read this in other languages: [French](README.md)._

## Guidelines

You must provide a working C# implementation of ITrainsStarter.cs (which you cannot modify) that solves all acceptance tests (AcceptanceTests.cs).

**The final version of your code must be available/pushed at least 5 hours before you meet with the team.**

After submitting your code, you will join a 90-minute session with development team members in an environment reflecting 
our daily dynamics. We will work together on your code, with new constraints added to the problem. You must, therefore, have a 
well-structured, understandable solution that will allow you and your peers to divide these new tasks easily.

## Problem

Eric is a yardmaster at Canadian National. His daily job is to organize, plan and move railcars
for setting up trains.

A trainyard consists of several "lines" of cars and one train line. Take this model:

![Alt text](images/trainyard_en.png?raw=true)

This trainyard has six lines of cars. Each wagon is represented by a rectangle, and each letter represents a different destination.

The yardmaster's job is to place all the wagons on the train line for a selected destination as
efficiently as possible. To do this, he uses a small yard locomotive, which is used to pull or push trains between the different lines.

Here are the rules he must follow:

- The yard locomotive can pull between 1 and 3 continuous cars.
  - Example: The two contiguous "A" cars on line 5 can be pulled and moved to line 1.
- Each yard line can accommodate up to 10 cars. It may have less (free spaces).
- The yardmaster can only pull the cars from the left side of a line.
- Once cars are put on the train line, they cannot be moved or removed from the line.
- Ideally, the cars of the closest lines are moved. It is more expensive to move cars from line 1 to line 6
  only from line 1 to line 2.
- Ideally, cars are moved in groups (maximum of 3 per group). It is cheaper to move 3 cars at once
  lines from line 1 to line 2 than to make three journeys of 1 car between these two lines.

Eric needs a software application that provides him with a plan consisting of a list of movements to prepare his train line for a particular destination.
Ideally, the software would produce the cheapest plan to assemble the train, that is, the one requiring the fewest and shortest moves.

## Example

Examples of car movements with the above model:
Selected destination: G

- Move 1 car (D) from line #1 to line #2
- Move 1 car (G) from line #1 to train line
- Move 2 cars (C and D) from line #3 to line #1
- Move 1 car (G) from line #3 to train line
- Move 1 car (D) from line #4 to line #3
- Move 1 car (G) from line #4 to train line

After these moves, we would have a train yard in this state:

![Alt text](images/trainyard_g_en.png?raw=true)

At this stage of the example, the job is not yet completed (there are still G cars to put on the train line).
This, however, gives you an idea of what Eric expects for the list of car movements.

## Requirements

- The software application must respond to the ITrainsStarter.cs interface and its Start() method.
- The software application will calculate the best list of movements given a destination to prepare the train and return the series of movements the yardmaster must make to fill the train line.
- All acceptance tests must succeed (AcceptanceTests.cs).

Feel free to improve the test suite with additional relevant tests, or introduce new acceptance criteria that are pertinent to the problem at hand.

## Examples of inputs and outputs of the ITrainsStarter.cs interface

`ITrainsStarter` is an interface between your code and our unit tests. The solution includes a number of basic tests to help validate your understanding of the business logic.
Here is a simple example of inputs and outputs for this class to get you started. To simplify the result, we used only two lines of trains.

- Each character in the input corresponds to a car for a particular destination. Zeros represent empty spaces.
- The char 0 in the output represents the "Train line".
- In the two examples below, destination C has been chosen.

Input (string array):

```
00000ACDGC
00000000DG
```

Output (string):

```
A,1,2;C,1,0;DG,1,2;C,1,0
```

Here is a more complex example.

Input (string array):

```
00000AGCAG
000DCACGDG
```

Output (string):

```
AG,1,2;C,1,0;AGD,2,1;C,2,0;A,2,1;C,2,0
```

If the solution is not possible, an empty string should be returned.

## Evaluation Criteria
The work sample, which includes a review of the code along with the actual meeting to review your solution, will be evaluated with the following criteria in mind:
- Problem Solving
- Technical Competency
- Communication
- Code Cleanliness
- Collaboration
