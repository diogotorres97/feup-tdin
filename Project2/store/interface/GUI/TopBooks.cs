using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace @interface
{
    class TopBooks
    {
        public string _total { get; set; }
        public Book _book { get; set; }

        public TopBooks(string total, Book book)
        {
            _total = total;
            _book = book;
        }
    }
}
