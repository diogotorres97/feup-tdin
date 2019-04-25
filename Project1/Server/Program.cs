using System;
using System.Runtime.Remoting;

internal static class Program
{
    private static void Main()
    {
        RemotingConfiguration.Configure("Server.exe.config", false);
        Console.WriteLine("Press Return to terminate.");
        Console.ReadLine();
    }
}