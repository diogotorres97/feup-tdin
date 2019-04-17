using System.Collections.Concurrent;
using System.Collections.Generic;

public class StatisticsController : AbstractController
{
    public uint TotalInvoices;
    public ConcurrentDictionary<uint, double> ProductQuantity;
    public ConcurrentDictionary<string, double> AmountByDay;

    public StatisticsController() : base("Statistics.exe.config")
    {
        List<Invoice> invoices = RestaurantServer.GetListOfInvoices();
        TotalInvoices = 0;
        ProductQuantity = new ConcurrentDictionary<uint, double>();
        AmountByDay = new ConcurrentDictionary<string, double>();
        LoadStatistics(invoices);
    }

    private void LoadStatistics(List<Invoice> invoices)
    {
        TotalInvoices = (uint) invoices.Count;

        invoices.ForEach(ParseInvoice);
    }

    private void ParseInvoice(Invoice invoice)
    {
        invoice.Orders.ForEach(order =>
        {
            if (ProductQuantity.ContainsKey(order.Product.Id))
                ProductQuantity[order.Product.Id] += order.Quantity;
            else
                ProductQuantity.TryAdd(order.Product.Id, order.Quantity);
        });

        if (AmountByDay.ContainsKey(invoice.Date.ToShortDateString()))
            AmountByDay[invoice.Date.ToShortDateString()] += invoice.TotalInvoice;
        else
            AmountByDay.TryAdd(invoice.Date.ToShortDateString(), invoice.TotalInvoice);
    }

    public void AddStatisticsEvent(OperationDelegate<Invoice> statisticsEvent)
    {
        RestaurantServer.PrintEvent += statisticsEvent;
    }

    public void RemovePrinterEvent(OperationDelegate<Invoice> statisticsEvent)
    {
        RestaurantServer.PrintEvent -= statisticsEvent;
    }
}