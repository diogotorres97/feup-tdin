using System;

public class Client
{
	public int id { get; set; }
	public string name { get; set; }
	public string address { get; set; }
	public string email { get; set; }
	public int stock ;
	
	public Client()
	{

	}
	
	public Client(int id, string name, string address, string email, int stock )
	{
		this.id = id;
		this.name = name;
		this.address = address;
		this.email = email;
        this.stock = stock;
	}
}
