using AsyncDemo;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");

        AsyncTest.TestReaderWriterLock();

        Console.ReadKey();
    }
}