using System.Collections.Generic;

namespace @interface
{
    public class Client
    {
        public int id { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string email { get; set; }
        public List<Order> orders { get; set; }
        public List<Sell> sells { get; set; }

        public Client()
        {
        }

        public Client(int id, string name, string address, string email, List<Order> orders, List<Sell> sells)
        {
            this.id = id;
            this.name = name;
            this.address = address;
            this.email = email;
            this.orders = orders;
            this.sells = sells;
        }
    }
}