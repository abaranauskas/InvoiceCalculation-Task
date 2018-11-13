using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceCalculation
{
    public interface ICustomer
    {
        bool IsEuResident();
        decimal GetVatRate();

        string Name { get;  }
        string CountryOfResidence { get;  }
        bool VatPayer { get;  }
        bool JuridicalPerson { get;  }        
    }
}
