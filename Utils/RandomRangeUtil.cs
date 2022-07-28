namespace Task2Stage2.Utils;

public class NumberRangeUtil
{
    private static Random random = new Random();

    public static int GetNextNotRepeatRandomNumber(int from, int to, List<int> lastTakeNumbers)
    {
        int randomNumber;
        do
        {
            randomNumber = random.Next(from, to);
        } while (lastTakeNumbers.Contains(randomNumber));

        lastTakeNumbers.Add(randomNumber);
        return randomNumber;
    }
}