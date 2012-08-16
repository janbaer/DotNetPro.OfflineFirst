﻿using System;
using System.Configuration;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using DotNetPro.Offlinefirst.Common.Services;

namespace DotNetPro.OfflineFirst.Common.Tests
{
    [TestClass]
    public class WebApiTests
    {
        [TestMethod]
        public void WhenCallGetCustomersAsync_ItShouldReturnAListOfCustomers()
        {
            // ARRANGE
            var service = new WebApiService();

            // ACT
            var customers = service.GetCustomersAsync().Result;

            // ASSERT
            Assert.IsNotNull(customers);
            Assert.IsTrue(customers.Any());

        }
        
        [TestMethod]
        public void WhenCallGetOrdersAsync_ItShouldReturnAListOfOrders()
        {
            // ARRANGE
            var service = new WebApiService();

            // ACT
            var employees = service.GetOrdersForCustomerAsync("ALFKI").Result;

            // ASSERT
            Assert.IsNotNull(employees);
            Assert.IsTrue(employees.Any());

        }
    }
}
