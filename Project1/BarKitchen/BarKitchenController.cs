using System.Collections.Generic;

public class BarKitchenController : AbstractController
{
    private ProductType _productType;

    public BarKitchenController(ProductType productType) : base("BarKitchen.exe.config")
    {
        _productType = productType;
    }

    public List<Order> GetListOfOrdersByState(OrderState orderState)
    {
        return Orders.FindAll(order => order.State == orderState && order.Product.Type == _productType);
    }
}