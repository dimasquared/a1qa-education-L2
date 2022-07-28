using WindowsInput;
using WindowsInput.Native;

namespace Task2Stage2.Utils;

public class UploadFileUtil
{
    public static void UploadFile(string filePath)
    {
        var fullFilePath = Environment.CurrentDirectory + filePath;
        InputSimulator simulator = new InputSimulator();

        simulator.Keyboard.TextEntry(fullFilePath)
            .Sleep(1000)
            .KeyPress(VirtualKeyCode.RETURN);
    }
}