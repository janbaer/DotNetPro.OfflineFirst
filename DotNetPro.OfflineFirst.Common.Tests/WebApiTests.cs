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
            var baseAddress = ConfigurationManager.AppSettings["WebApiBaseAddress"];
            var service = new WebApiService(baseAddress);

            // ACT
            var customers = service.GetCustomersAsync().Result;

            // ASSERT
            Assert.IsNotNull(customers);
            Assert.IsTrue(customers.Any());

        }
        
        [TestMethod]
        public void WhenCallGetEmploayeessAsync_ItShouldReturnAListOfEmployees()
        {
            // ARRANGE
            var baseAddress = ConfigurationManager.AppSettings["WebApiBaseAddress"];
            var service = new WebApiService(baseAddress);

            // ACT
            var employees = service.GetEmployeesAsync().Result;

            // ASSERT
            Assert.IsNotNull(employees);
            Assert.IsTrue(employees.Any());

        }
    }
}
