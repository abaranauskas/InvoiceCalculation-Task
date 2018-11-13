using InvoiceCalculation;
using NSubstitute;
using Xunit;

namespace InvoiceCalculationTests
{
    public class InvoiceTests
    {   

        [Fact]
        public void GetVatTaxServiceProviderIsNotTaxPayerCustomerIsNotTaxPayerSameCountryEuResidence()
        {
            //Arrange
            var serviceProvider = Substitute.For<IServiceProvider>();
            serviceProvider.VatPayer.Returns(false);
            serviceProvider.CountryOfResidence.Returns("Lithuania");

            var customer = Substitute.For<ICustomer>();
            customer.VatPayer.Returns(false);
            customer.IsEuResident().Returns(true);
            customer.CountryOfResidence.Returns("Lithuania");
            customer.GetVatRate().Returns(21);

            var subtotal = 240m;
            var invoice = new Invoice(subtotal, serviceProvider, customer);
            var expected = 50.4m;

            //Act
            var actual = invoice.GetVatTax();

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetVatTaxServiceProviderIsNotTaxPayerCustomerIsNotTaxPayerSameCountryNonEuesidence()
        {
            //Arrange
            var serviceProvider = Substitute.For<IServiceProvider>();
            serviceProvider.VatPayer.Returns(false);
            serviceProvider.CountryOfResidence.Returns("Russia");

            var customer = Substitute.For<ICustomer>();
            customer.VatPayer.Returns(false);
            customer.IsEuResident().Returns(false);
            customer.CountryOfResidence.Returns("Russia");
            customer.GetVatRate().Returns(20);

            var subtotal = 240m;
            var invoice = new Invoice(subtotal, serviceProvider, customer);
            var expected = 48m;

            //Act
            var actual = invoice.GetVatTax();

            //Assert
            Assert.Equal(expected, actual);
        }


        [Fact]
        public void GetVatTaxProviderTaxPayerCustomerIsNotTaxPayerCustomerFromDifferentEuCountries()
        {
            //Arrange
            var serviceProvider = Substitute.For<IServiceProvider>();
            serviceProvider.VatPayer.Returns(true);
            serviceProvider.CountryOfResidence.Returns("Lithuania");

            var customer = Substitute.For<ICustomer>();
            customer.VatPayer.Returns(false);
            customer.IsEuResident().Returns(true);
            customer.CountryOfResidence.Returns("Germany");
            customer.GetVatRate().Returns(19);

            var subtotal = 240m;
            var invoice = new Invoice(subtotal, serviceProvider, customer);            
            var expected = 45.6m;

            //Act
            var actual = invoice.GetVatTax();

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetVatTaxServiceProviderIsNotTaxPayerCustomerIsTaxPayerFromEuDifferentCountries()
        {
            //Arrange
            var serviceProvider = Substitute.For<IServiceProvider>();
            serviceProvider.VatPayer.Returns(false);
            serviceProvider.CountryOfResidence.Returns("Lithuania");

            var customer = Substitute.For<ICustomer>();
            customer.VatPayer.Returns(false);
            customer.IsEuResident().Returns(true);
            customer.CountryOfResidence.Returns("Germany");
            customer.GetVatRate().Returns(19);

            var subtotal = 240m;
            var invoice = new Invoice(subtotal, serviceProvider, customer);
            var expected = 0m;

            //Act
            var actual = invoice.GetVatTax();

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetVatTaxCustomerNonEuResident()
        {
            //Arrange
            var serviceProvider = Substitute.For<IServiceProvider>();
            serviceProvider.VatPayer.Returns(true);
            serviceProvider.CountryOfResidence.Returns("Lithuania");

            var customer = Substitute.For<ICustomer>();
            customer.VatPayer.Returns(true);
            customer.IsEuResident().Returns(false);
            customer.CountryOfResidence.Returns("USA");
            customer.GetVatRate().Returns(6.25m);

            var subtotal = 240m;
            var invoice = new Invoice(subtotal, serviceProvider, customer);
            var expected = 0m;

            //Act
            var actual = invoice.GetVatTax();

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]       
        public void GetVatTaxWhenSubtotalLessZeroOrLess()
        {
            //Arrange
            var serviceProvider = Substitute.For<IServiceProvider>();
            serviceProvider.VatPayer.Returns(true);
            serviceProvider.CountryOfResidence.Returns("Lithuania");

            var customer = Substitute.For<ICustomer>();
            customer.VatPayer.Returns(true);
            customer.IsEuResident().Returns(false);
            customer.CountryOfResidence.Returns("USA");
            customer.GetVatRate().Returns(6.25m);

            var subtotal = 240m;
            var invoice = new Invoice(subtotal, serviceProvider, customer);
            var expectedException = "Subtotal mus be grater than ZERO";

            
            try
            {
                //Act
                var actual = invoice.GetVatTax();
            }
            catch (System.ArgumentException ex)
            {
                //Assert
                Assert.Equal(expectedException, ex.Message);
            }
        }

        [Fact]
        public void GetVatTaxWhenServiceProviderIsNull()
        {
            //Arrange
            ServiceProvider serviceProvider = null;           

            var customer = Substitute.For<ICustomer>();
            customer.VatPayer.Returns(true);
            customer.IsEuResident().Returns(false);
            customer.CountryOfResidence.Returns("USA");
            customer.GetVatRate().Returns(6.25m);

            var subtotal = 240m;
            var invoice = new Invoice(subtotal, serviceProvider, customer);
            var expectedException = "_serviceProvider can not be null";


            try
            {
                //Act
                var actual = invoice.GetVatTax();
            }
            catch (System.ArgumentException ex)
            {
                //Assert
                Assert.Equal(expectedException, ex.Message);
            }
        }

        [Fact]
        public void GetVatTaxWhenCustomerIsNull()
        {
            //Arrange
            var serviceProvider = Substitute.For<IServiceProvider>();
            serviceProvider.VatPayer.Returns(true);
            serviceProvider.CountryOfResidence.Returns("Lithuania");

            Customer customer = null;            

            var subtotal = 240m;
            var invoice = new Invoice(subtotal, serviceProvider, customer);
            var expectedException = "_customer can not be null";


            try
            {
                //Act
                var actual = invoice.GetVatTax();
            }
            catch (System.ArgumentException ex)
            {
                //Assert
                Assert.Equal(expectedException, ex.Message);
            }
        }
    }
}
