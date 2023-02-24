using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.Model.SQL
{
    public class Currency
    {
        public string CurrencyCode { get; set; } = default!;
        public string Name { get; set; } = default!;
        public DateTime ModifiedDate { get; set; } = default!;
    }
}
