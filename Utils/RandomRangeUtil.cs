namespace Task2Stage2.Utils;

public class NumberRangeUtil
{
    private readonly Random random = new Random();
    private readonly List<int> lastTakeNumbers = new List<int>();
    private readonly int from;
    private readonly int to;

    public NumberRangeUtil(int from, int to)
    {
        this.to = to;
        this.from = from;
    }

    public int GetNextNotRepeatRandomNumber()
    {
        int randomNumber;
        do
        {
            randomNumber = random.Next(from, to + 1);
        } while (lastTakeNumbers.Contains(randomNumber));

        lastTakeNumbers.Add(randomNumber);
        return randomNumber;
    }
}