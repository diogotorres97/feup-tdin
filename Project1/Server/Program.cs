using System;
using System.Runtime.Remoting;

class Program {
  static void Main(string[] args) {
    RemotingConfiguration.Configure("Server.exe.config", false);
    Console.WriteLine("Press Return to terminate.");
    Console.ReadLine();
  }
}