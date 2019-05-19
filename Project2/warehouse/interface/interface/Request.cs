using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public Request(int id, int quantity, string processedDate, Book book)
        {
            this.id = id;
            this.quantity = quantity;
            this.processedDate = processedDate;
            this.book = book;

        }
    }

}
