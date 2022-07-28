using System.Text;

namespace Task2Stage2.Utils;

public class TextUtil
{
    public static string RandomText(char from, char to, int count)
    {
        var random = new Random();
        StringBuilder strBuilder = new StringBuilder();
        
        for (int i = 0; i <= count; i++)
        {
            char str = (char)random.Next(from, to+1);
            strBuilder.Append(str);
        }

        return strBuilder.ToString();
    }
}