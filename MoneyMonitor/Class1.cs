using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyMonitor
{
    public class Filter
    {
        public DateTime dateb { get; set; }
        public DateTime datee { get; set; }
    }

    public class Finance
    {
        [Key] public int Id { get; set; }
        public DateTime date { get; set; }
        public double Money { get; set; }
        public int category { get; set; }
        public int subcategory { get; set; }
        public string? comment { get; set; }
    }



}
