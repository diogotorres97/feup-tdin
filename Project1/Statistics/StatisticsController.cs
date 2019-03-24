using System.Collections.Generic;

public class StatisticsController : AbstractController
{
    public uint TotalInvoices;
    public double TotalSumOfDay;
    public Dictionary<uint, float> ProductQuantity;

    public StatisticsController() : base("Statistics.exe.config")
    {
        TotalInvoices = 0;
        TotalSumOfDay = 0;
        ProductQuantity = new Dictionary<uint, float>();
    }
}