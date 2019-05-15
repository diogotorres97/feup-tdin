using System;

public class Order
{
	public string uuid { get; set; }
	public int quantity { get; set; }
	public double totalPrice { get; set; }
	public string state { get; set; }
	public string stateDate { get; set; }
	public int clientId { get; set; }
	public int bookId { get; set; }
    
	public Order()
	{

	}
	
	public Order(string uuid, int quantity, double totalPrice, string state, string stateDate, int clientId, int bookId )
	{
		this.uuid = uuid;
		this.quantity = quantity;
		this.totalPrice = totalPrice;
		this.state = state;
        this.stateDate = stateDate;
		this.clientId = clientId;
		this.bookId = bookId;

	}
}
