using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public class RaportDto
    {
        public string? Year { get; set; }
        public string? Month { get; set; }
        public Decimal TotalPrice { get; set; }
    }
}
