namespace TestApplication.Infrastructure;

public class TestDelegate
{
    public delegate void WriterDelegate(string text);

    public static void Write(string text)
    {
        Console.WriteLine(text);
    }
}