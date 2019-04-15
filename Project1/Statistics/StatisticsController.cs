using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

public class StatisticsController : AbstractController
{
    public uint TotalInvoices;
    public double TotalSumOfDay;
    public ConcurrentDictionary<uint, float> ProductQuantity;
    public ConcurrentDictionary<string, double> AmountByDay;

    public StatisticsController() : base("Statistics.exe.config")
    {
        TotalInvoices = 0;
        TotalSumOfDay = 0;
        ProductQuantity = new ConcurrentDictionary<uint, float>();
        AmountByDay = new ConcurrentDictionary<string, double>();
    }
}