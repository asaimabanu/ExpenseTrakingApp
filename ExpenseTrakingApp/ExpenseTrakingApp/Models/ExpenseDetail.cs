using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ExpenseTrakingApp.Models
{
    enum ECategory
    {
        Shopping,
        Grocery,
        Auto,
        Food,
        Micellaneous
    }
    internal class ExpenseDetail
    {
        public string Text { get; set; }
        public string Name { get; set; }
        public string Amount { get; set; }
        public DateTime Date { get; set; }
        public string Category { get; set; }

        public string FileName { get; set; }
    }
    
}
