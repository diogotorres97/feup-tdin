using System;

public class Sell
{
	public string uuid { get; set; }
	public int quantity { get; set; }
	public double totalPrice { get; set; }
	public int clientId { get; set; }
	public int bookId { get; set; }
	
	public Sell()
	{

	}
	
	public Sell(string uuid, int quantity, double totalPrice, int clientId, int bookId )
	{
		this.uuid = uuid;
		this.quantity = quantity;
		this.totalPrice = totalPrice;
		this.clientId = clientId;
		this.bookId = bookId;

	}
}
