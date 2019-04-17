using System.Collections.Concurrent;

public class StatisticsController : AbstractController
{
    public uint TotalInvoices;
    public ConcurrentDictionary<uint, double> ProductQuantity;
    public ConcurrentDictionary<string, double> AmountByDay;

    public StatisticsController() : base("Statistics.exe.config")
    {
        TotalInvoices = 0;
        ProductQuantity = new ConcurrentDictionary<uint, double>();
        AmountByDay = new ConcurrentDictionary<string, double>();
    }
}