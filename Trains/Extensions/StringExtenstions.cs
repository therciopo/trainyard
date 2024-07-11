namespace Trains.Extensions;

public static class StringExtenstions
{
    public static int FindFirstNonZeroIndex(this string trainLine)
    {
        if (string.IsNullOrEmpty(trainLine))
        {
            return -1;
        }

        for (int i = 0; i < trainLine.Length; i++)
        {
            if (trainLine[i] != '0')
            {
                return i;
            }
        }

        return -1;
    }
}