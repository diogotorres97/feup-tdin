namespace @interface
{
    public class Book
    {
        public int id { get; set; }
        public string title { get; set; }
        public string author { get; set; }
        public double price { get; set; }
        public int stock { get; set; }

        public Book()
        {
        }

        public Book(dynamic book)
        {
            id = (int) book.id;
            title = (string) book.title;
            author = (string) book.author;
            price = (double) book.price;
            stock = (int) book.stock;
        }

        public Book(int id, string title, string author, double price, int stock)
        {
            this.id = id;
            this.title = title;
            this.author = author;
            this.price = price;
            this.stock = stock;
        }
    }
}