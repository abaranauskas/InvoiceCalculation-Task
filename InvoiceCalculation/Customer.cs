using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceCalculation
{
    public class Customer : ICustomer
    {
        public Customer(string name, string countryOfResidence,
                        bool vatPayer, bool juridicalPerson)
        {
            Name = name;
            CountryOfResidence = countryOfResidence.ToUpper();
            VatPayer = VatPayer;
            JuridicalPerson = juridicalPerson;
            ContactDetails = new Contact
            {
                StreetAddress = "123 Gedimino Ave.",
                City = "Vilnius",
                StateProvice = "Vilnius City",
                Country = this.CountryOfResidence,
                PostalCode = "12364",
                EmailAddress = "info@abc.com",
                PhoneNumber = 0037067012312
            };
        }

        public string Name { get; private set; }
        public string CountryOfResidence { get; private set; }
        public bool VatPayer { get; private set; }
        public bool JuridicalPerson { get; private set; }
        public Contact ContactDetails { get; set; }        

        public bool IsEuResident()  //darau prielaida kad pasiimu is duomenu bazes
        {
            var euCountries = new[]{ "Austria","Belgium","Bulgaria","Croatia","Cyprus",
                "Czech Republic","Denmark","Estonia","Finland","France","Germany","Greece",
                "Hungary","Ireland","Italy","Latvia","Lithuania","Luxembourg","Malta",
                "Netherlands","Poland","Portugal","Romania","Slovenia","Slovakia","Spain",
                "Sweden","United Kingdom" };

            bool euResident = false;
            foreach (var country in euCountries)
            {
                if (country.ToUpper() == this.CountryOfResidence)
                {
                    euResident = true;
                }
            }

            return euResident;
        }

        public decimal GetVatRate()  //darau prielaida kad pasiimu is duomenu bazes
        {
            decimal vatRate;
            switch (this.CountryOfResidence)
            {
                case "LITHUANIA":
                    vatRate = 21;
                    break;
                case "GERMANY":
                    vatRate = 19;
                    break;
                case "RUSSIA":
                    vatRate = 20;
                    break;
                case "SWEDEN":
                    vatRate = 25;
                    break;
                case "UNITED KINGDOM":
                    vatRate = 20;
                    break;
                default:
                    vatRate = 30.5m;
                    break;
            }

            return vatRate;
        }
    }
}
