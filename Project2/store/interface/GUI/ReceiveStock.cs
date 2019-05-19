﻿namespace @interface
{
    class ReceiveStock
    {
        public int id { get; set; }
        public int quantity { get; set; }
        public string processedDate { get; set; }
        public Book book { get; set; }

        public ReceiveStock()
        {
        }

        public ReceiveStock(int id, int quantity, string processedDate, Book book)
        {
            this.id = id;
            this.quantity = quantity;
            this.processedDate = processedDate;
            this.book = book;
        }
    }
}