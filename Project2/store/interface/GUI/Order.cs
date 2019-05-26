namespace @interface
{
    public class Order
    {
        public string uuid { get; set; }
        public int quantity { get; set; }
        public double totalPrice { get; set; }
        public string state { get; set; }
        public string stateDate { get; set; }
        public Book book { get; set; }
        public Client client { get; set; }

        public Order()
        {
        }

        public Order(dynamic data, Book book, Client client)
        {
            uuid = (string) data.uuid;
            quantity = (int) data.quantity;
            totalPrice = (double) data.totalPrice;
            state = (string) data.state;
            stateDate = (string) data.stateDate;
            this.book = book;
            this.client = client;
        }

        public Order(string uuid, int quantity, double totalPrice, string state, string stateDate, Book book,
            Client client)
        {
            this.uuid = uuid;
            this.quantity = quantity;
            this.totalPrice = totalPrice;
            this.state = state;
            this.stateDate = stateDate;
            this.book = book;
            this.client = client;
        }
    }
}