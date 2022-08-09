namespace Task5Stage2.Utils;

public static class TextUtil
{
    public static string RandomText()
    {
        return (new Random()).NextDouble().GetHashCode().ToString("x8");
    }
}