namespace Task6Stage2.Utils;

public sealed class Logger
{
    private static Logger instance;

    private Logger()
    {
    }

    public static Logger GetInstance()
    {
        if (instance == null)
            instance = new Logger();
        return instance;
    }

    private void Log(string level, string message)
    {
        Console.WriteLine($"{DateTime.Now:yyyy-MM-dd hh:mm:ss} {level} - {message}");
    }

    public void Info(string message)
    {
        Log("INFO", message);
    }
    
    public void Debug(string message)
    {
        Log("DEBUG", message);
    }

    public void Warn(string message) {
        Log("WARN", message);
        
    }

    public void Error(string message)
    {
        Log("ERROR", message);
    }
}