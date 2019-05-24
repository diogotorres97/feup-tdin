namespace @interface
{
    class Request
    {
        public int id { get; set; }
        public int quantity { get; set; }
        public string processedDate { get; set; }
        public Book book { get; set; }

        public Request()
        {
        }

        public Request(dynamic data, Book book)
        {
            id = (int) data.id;
            quantity = (int) data.quantity;
            processedDate = (string) data.processedDate;
            this.book = book;
        }

        public Request(int id, int quantity, string processedDate, Book book)
        {
            this.id = id;
            this.quantity = quantity;
            this.processedDate = processedDate;
            this.book = book;
        }
    }
}