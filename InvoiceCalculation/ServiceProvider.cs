using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceCalculation
{
    public class ServiceProvider : IServiceProvider
    {
        public ServiceProvider(string name, string countryOfResidence, bool vatPayer)
        {
            Name = name;
            CountryOfResidence = countryOfResidence.ToUpper();
            VatPayer = VatPayer;

            ContactDetails = new Contact
            {
                StreetAddress = "123 Main Ave.",
                City = "London",
                StateProvice = "London City",
                Country = this.CountryOfResidence,
                PostalCode = "LN12HA",
                EmailAddress = "info@info.uk",
                PhoneNumber = 0044670123122
            };
        }

        public string Name { get; private set; }
        public string CountryOfResidence { get; private set; }
        public bool VatPayer { get; private set; }
        public Contact ContactDetails { get; set; }
    }
}
