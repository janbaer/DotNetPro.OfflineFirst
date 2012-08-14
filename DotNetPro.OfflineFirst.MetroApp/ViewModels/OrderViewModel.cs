using System;

using DotNetPro.OfflineFirst.MetroApp.Common;
using DotNetPro.Offlinefirst.Common.Models;

namespace DotNetPro.OfflineFirst.MetroApp.ViewModels
{
    public class OrderViewModel : BindableBase
    {
        #region Private member variables
        private Order _order;
        private int _orderId;
        private string _customerID;
        private int _employeeID;
        private string _orderDate;
        private DateTime? _requiredDate;
        private DateTime? _shippedDate;
        private int _shipVia;
        private decimal _freight;
        private string _shipName;
        private string _shipAddress;
        private string _shipCity;
        private string _shipRegion;
        private string _shipCountry;
        private string _image;
        #endregion

        #region Properties
        public Order Order
        {
            get { return _order; }
            set
            {
                if (value == null) throw new ArgumentNullException();
                _order = value;
                ReadProperties(_order);
            }
        }

        public int OrderID
        {
            get { return _orderId; }
            set { SetProperty(ref _orderId, value); }
        }

        public string CustomerID
        {
            get { return _customerID; }
            set { SetProperty(ref _customerID, value); }
        }

        public int EmployeeID
        {
            get { return _employeeID; }
            set { SetProperty(ref _employeeID, value); }
        }

        public string Image
        {
            get { return _image; }
            set { SetProperty(ref _image, value); }
        }

        public string OrderDate
        {
            get { return _orderDate; }
            set { SetProperty(ref _orderDate, value); }
        }

        public DateTime? RequiredDate
        {
            get { return _requiredDate; }
            set { SetProperty(ref _requiredDate, value); }
        }

        public DateTime? ShippedDate
        {
            get { return _shippedDate; }
            set { SetProperty(ref _shippedDate, value); }
        }

        public int ShipVia
        {
            get { return _shipVia; }
            set { SetProperty(ref _shipVia, value); }
        }

        public decimal Freight
        {
            get { return _freight; }
            set { SetProperty(ref _freight, value); }
        }

        public string ShipName
        {
            get { return _shipName; }
            set { SetProperty(ref _shipName, value); }
        }

        public string ShipAddress
        {
            get { return _shipAddress; }
            set { SetProperty(ref _shipAddress, value); }
        }

        public string ShipCity
        {
            get { return _shipCity; }
            set { SetProperty(ref _shipCity, value); }
        }

        public string ShipRegion
        {
            get { return _shipRegion; }
            set { SetProperty(ref _shipRegion, value); }
        }

        public string ShipCountry
        {
            get { return _shipCountry; }
            set { SetProperty(ref _shipCountry, value); }
        }
        #endregion

        private void ReadProperties(Order order)
        {
            this.OrderID = order.OrderID;
            this.CustomerID = order.CustomerID;
            this.EmployeeID = order.EmployeeID;
            this.OrderDate = order.OrderDate.HasValue ? string.Format("{0:d}", order.OrderDate.Value) : string.Empty;
            this.RequiredDate = order.RequiredDate;
            this.ShippedDate = order.ShippedDate;
            this.ShipVia = order.ShipVia;
            this.Freight = order.Freight;
            this.ShipName = order.ShipName;
            this.ShipAddress = order.ShipAddress;
            this.ShipCity = order.ShipCity;
            this.ShipRegion = order.ShipRegion;
            this.ShipCountry = order.ShipCountry;

            this.Image = "../Assets/DarkGray.png";
        }
       
    }
}