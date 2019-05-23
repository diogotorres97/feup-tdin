namespace @interface
{
    class TopBooks
    {
        public string total { get; set; }
        public Book book { get; set; }

        public TopBooks(string total, Book book)
        {
            this.total = total;
            this.book = book;
        }
    }
}