using InvoiceCalculation;
using System;
using Xunit;

namespace InvoiceCalculationTests
{
    public class CustomerTests
    {
        [Fact]
        public void IsEuResidentWhenountryIsEuMemberFirst()
        {
            //Arranger
            var customer = new Customer("Dell", "United Kingdom", vatPayer:true, juridicalPerson:false);
            var expected = true;

            //Act
            var actual = customer.IsEuResident();

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void IsEuResidentWhenountryIsEuMemberSecond()
        {
            //Arranger
            var customer = new Customer("Dell", "cyprUS", vatPayer: true, juridicalPerson: false);
            var expected = true;

            //Act
            var actual = customer.IsEuResident();

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void IsEuResidentWhenountryIsNotEuMemberFisrt()
        {
            //Arranger
            var customer = new Customer("Dell", "NORWAy", vatPayer: true, juridicalPerson: false);
            var expected = false;

            //Act
            var actual = customer.IsEuResident();

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void IsEuResidentWhenountryIsNotEuMemberSecond()
        {
            //Arranger
            var customer = new Customer("Dell", "Japan", vatPayer: true, juridicalPerson: false);
            var expected = false;

            //Act
            var actual = customer.IsEuResident();

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetVatRateGermany()
        {
            //Arranger
            var customer = new Customer("Dell", "Germany", vatPayer: true, juridicalPerson: false);
            var expected = 19m;

            //Act
            var actual = customer.GetVatRate();

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetVatRateLithuania()
        {
            //Arranger
            var customer = new Customer("Dell", "Lithuania", vatPayer: true, juridicalPerson: false);
            var expected = 21m;

            //Act
            var actual = customer.GetVatRate();

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetVatRateOthers()
        {
            //Arranger
            var customer = new Customer("Dell", "United States", vatPayer: true, juridicalPerson: false);
            var expected = 30.5m;

            //Act
            var actual = customer.GetVatRate();

            //Assert
            Assert.Equal(expected, actual);
        }
    }
}
