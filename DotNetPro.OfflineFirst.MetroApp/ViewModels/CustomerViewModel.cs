using System;

using DotNetPro.Offlinefirst.Common.Models;
using DotNetPro.OfflineFirst.MetroApp.Common;

namespace DotNetPro.OfflineFirst.MetroApp.ViewModels
{
    public class CustomerViewModel : BindableBase
    {
        #region Private member variables
        private Customer _customer;
        private string _customerId;
        private string _companyName;
        private string _contactTitle;
        private string _address;
        private string _city;
        private string _region;
        private string _postalCode;
        private string _phone;
        private string _fax;
        #endregion

        #region Properties

        public Customer Customer
        {
            get { return _customer; }
            set
            {
                if (value == null) throw new ArgumentNullException();
                _customer = value;
                ReadProperties(_customer);
            }
        }

        public string CustomerId
        {
            get { return _customerId; }
            set { SetProperty(ref _customerId, value); }
        }

        public string CompanyName
        {
            get { return _companyName; }
            set { SetProperty(ref _companyName, value); }
        }

        public string ContactTitle
        {
            get { return _contactTitle; }
            set { SetProperty(ref _contactTitle, value); }
        }

        public string Address
        {
            get { return _address; }
            set { SetProperty(ref _address, value); }
        }

        public string City
        {
            get { return _city; }
            set { SetProperty(ref _city, value); }
        }

        public string Region
        {
            get { return _region; }
            set { SetProperty(ref _region, value); }
        }

        public string PostalCode
        {
            get { return _postalCode; }
            set { SetProperty(ref _postalCode, value); }
        }

        public string Phone
        {
            get { return _phone; }
            set { SetProperty(ref _phone, value); }
        }

        public string Fax
        {
            get { return _fax; }
            set { SetProperty(ref _fax, value); }
        }
        #endregion

        private void ReadProperties(Customer customer)
        {
            this.CustomerId = customer.CustomerId;
            this.CompanyName = customer.CompanyName;
            this.ContactTitle = customer.ContactTitle;
            this.Address = customer.Address;
            this.City = customer.City;
            this.Region = customer.Region;
            this.PostalCode = customer.PostalCode;
            this.Phone = customer.Phone;
            this.Fax = customer.Fax;           
        }
    }
}