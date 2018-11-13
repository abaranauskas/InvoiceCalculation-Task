using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceCalculation
{
    public interface IServiceProvider
    {
        string Name { get;  }
        string CountryOfResidence { get;  }
        bool VatPayer { get;  }
        Contact ContactDetails { get; set; }
    }
}
