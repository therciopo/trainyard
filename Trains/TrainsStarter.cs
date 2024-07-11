using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trains.Extensions;
using Trains.Models;

namespace Trains;

public class TrainsStarter : ITrainsStarter
{
    private const int MAX_CAR_GROUP_SIZE = 3;
    private const int MAX_LINE_CARS = 10;
    private const int DESTINATION_NOT_FOUND = -1;
    private const int NO_CONTIGUOUS_AVAILABLE_SPOTS = -1;
    private readonly List<string> _outputList = new();

    public string Start(string[] trainLines, char destination)
    {
        for (int currentTrainLine = 0; currentTrainLine < trainLines.Length; currentTrainLine++)
        {
            var startIndex = 0;
            do
            {
                var carGroups = ParseTrainLine(ref trainLines[currentTrainLine], startIndex, destination);

                if (carGroups.Item2 == DESTINATION_NOT_FOUND)
                {
                    startIndex = 0;
                    break;
                }

                MoveCars(carGroups, trainLines, currentTrainLine);
                
                if (IsDestinationCarFirstInTrainLine(trainLines[currentTrainLine], destination))
                {
                    startIndex = carGroups.Item2 + 1;

                    MoveDestinationCarToTrainLine(trainLines, currentTrainLine, startIndex);
                    _outputList.Add($"{destination},{currentTrainLine + 1},0");
                }
                else startIndex = -1;

            } while (startIndex >= 0);
        }

        return string.Join(";", _outputList);
    }

    private static void MoveDestinationCarToTrainLine(string[] trainLines, int currentTrainLine, int startIndex)
    {
        var sb = new StringBuilder(trainLines[currentTrainLine]);
        sb[startIndex - 1] = '0';
        trainLines[currentTrainLine] = sb.ToString();
    }

    private void MoveCars((List<CarGroup>, int) carGroups, string[] trainLines, int currentTrainLine)
    {
        foreach (var carGroup in carGroups.Item1)
        {
            var availableLine = TryFindClosestContiguousSpots(trainLines, currentLine: currentTrainLine, carGroup.Destination.Length);

            if (availableLine == NO_CONTIGUOUS_AVAILABLE_SPOTS)
            {
                var spots = FindAvailableSpots(trainLines, targetPosition: currentTrainLine, carGroup.Destination.Length);
                if (spots.Count == 0) continue;

                for (int i = 0; i < carGroup.Destination.Length; i++)
                {
                    foreach (var s in spots)
                    {
                        if (s.Length > 0)
                        {
                            MoveGroupToDestination(ref trainLines[s.TrainLine], carGroup.Destination.Substring(i, s.Length));
                            RemoveGroupFromSource(ref trainLines[currentTrainLine], carGroup.Destination.Substring(i, s.Length));

                            _outputList.Add($"{carGroup.Destination.Substring(i, s.Length)},{currentTrainLine + 1},{s.TrainLine + 1}");

                            s.Length--;
                            break;
                        }
                        else continue;
                    }
                }
            }
            else
            {
                MoveGroupToDestination(ref trainLines[availableLine], carGroup.Destination);
                RemoveGroupFromSource(ref trainLines[currentTrainLine], carGroup.Destination);

                _outputList.Add($"{carGroup.Destination},{currentTrainLine + 1},{availableLine + 1}");
            }
        }
    }

    public static bool IsDestinationCarFirstInTrainLine(string trainLine, char destination)
    {
        foreach (char car in trainLine)
        {
            if (car == '0')
                continue;
            return car == destination;
        }

        return false;
    }
    public static List<Spot> FindAvailableSpots(string[] trainLines, int targetPosition, int length)
    {
        var spotList = new List<Spot>();

        for (int i = 0; i < trainLines.Length; i++)
        {
            var freeSpots = trainLines[i].Count(x => x == '0');
            if (freeSpots > 0 && i != targetPosition)
            {
                if (spotList.Sum(x => x.Length) <= length)
                {
                    spotList.Add(new Spot { TrainLine = i, Length = freeSpots });
                }
                else break;// enough spots
            }
        }

        return spotList;
    }

    public static int TryFindClosestContiguousSpots(string[] trainLines, int currentLine, int groupLength)
    {
        int closestLine = -1;
        int smallestDifference = int.MaxValue;

        for (int i = 0; i < trainLines.Length; i++)
        {
            if (trainLines[i].Count(x => x == '0') >= groupLength && i != currentLine )
            {
                int difference = Math.Abs(i - currentLine);
                if (difference < smallestDifference)
                {
                    smallestDifference = difference;
                    closestLine = i;
                }
            }
        }

        return closestLine;
    }
    public static (List<CarGroup>, int) ParseTrainLine(ref string trainLine, int startindex, char destination)
    {
        if (trainLine.Length > MAX_LINE_CARS)
        {
            var sb = new StringBuilder(trainLine);
            sb.Remove(0, sb.Length - MAX_LINE_CARS);
            trainLine = sb.ToString();
        }       

        var groups = new List<CarGroup>();
        int selectedIndex = -1;

        var destinationFound = trainLine.IndexOf(destination);
        if (destinationFound == -1) return (groups, selectedIndex);

        var currentGroup = new StringBuilder();

        for (int car = startindex; car < trainLine.Length; car++)
        {
            if (trainLine[car] == '0') continue;

            if (trainLine[car] == destination)
            {
                selectedIndex = startindex = car;
                break;
            }

            currentGroup.Append(trainLine[car]);
            if ((currentGroup.Length == MAX_CAR_GROUP_SIZE || currentGroup.Length < MAX_CAR_GROUP_SIZE && selectedIndex >= 0))
            {
                groups.Add(new CarGroup(car, currentGroup.ToString()));
                currentGroup.Clear();
            }
        }

        if (currentGroup.Length > 0 && selectedIndex >= 0)
        {
            groups.Add(new CarGroup(selectedIndex, currentGroup.ToString()));
        }

        return (groups, selectedIndex);
    }
    public static void MoveGroupToDestination(ref string trainLine, string replacement)
    {
        var firstNonZeroIndex = trainLine.FindFirstNonZeroIndex();

        if (firstNonZeroIndex == -1 || firstNonZeroIndex == trainLine.Length)
            return;

        var lineBuilder = new StringBuilder(replacement + trainLine.Substring(firstNonZeroIndex));
        trainLine = lineBuilder.ToString().PadLeft(10, '0');
    }

    public static void RemoveGroupFromSource(ref string trainLine, string match)
    {
        if (string.IsNullOrEmpty(trainLine) || string.IsNullOrEmpty(match))
            return;

        var sb = new StringBuilder(trainLine);
        int index = sb.ToString().IndexOf(match);

        if (index == -1) return;

        sb.Remove(index, match.Length);
        sb.Insert(index, new string('0', match.Length));

        trainLine = sb.ToString();
    }
}