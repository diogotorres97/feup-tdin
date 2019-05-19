namespace @interface
{
    public class Sell
    {
        public string uuid { get; set; }
        public int quantity { get; set; }
        public double totalPrice { get; set; }
        public Book book { get; set; }
        public Client client { get; set; }

        public Sell()
        {
        }

        public Sell(string uuid, int quantity, double totalPrice, Book book, Client client)
        {
            this.uuid = uuid;
            this.quantity = quantity;
            this.totalPrice = totalPrice;
            this.book = book;
            this.client = client;
        }
    }
}