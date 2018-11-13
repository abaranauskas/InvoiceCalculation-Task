using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceCalculation
{
    public class Invoice
    {
        private IServiceProvider _serviceProvide;
        private ICustomer _customer;

        public Invoice(decimal subtotal, IServiceProvider serviceProvide, ICustomer customer)
        {
            Subtotal = subtotal;
            _serviceProvide = serviceProvide;
            _customer = customer;
        }

        public decimal Subtotal { get; set; }        

        public decimal Total
        {
            get { return Subtotal + GetVatTax(); }
        }


        public decimal GetVatTax()
        {
            if (Subtotal <= 0) throw new ArgumentException("Subtotal mus be grater than ZERO");
            if (_serviceProvide==null) throw new ArgumentException("_serviceProvider can not be null");
            if (_customer == null) throw new ArgumentException("_customer can not be null");

            decimal vatTax = 0;

            if (_serviceProvide.VatPayer)
            {
                if (_customer.IsEuResident())
                {
                    if (!_customer.VatPayer)
                    {
                        if (_customer.CountryOfResidence != _serviceProvide.CountryOfResidence)
                        {
                            vatTax = Subtotal * _customer.GetVatRate() / 100;
                        }
                    }                    
                }
            }

            if (_customer.CountryOfResidence == _serviceProvide.CountryOfResidence)
            {
                vatTax = Subtotal * _customer.GetVatRate() / 100;
            }

            return vatTax;
        }
    }
}
