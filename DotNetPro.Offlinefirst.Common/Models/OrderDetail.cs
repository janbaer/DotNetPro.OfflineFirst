using System;
using System.ComponentModel.DataAnnotations;

namespace DotNetPro.OfflineFirst.WebApi.Models
{
    [Table("Order_Details")]
    public class OrderDetail
    {
        [Key, Column(Order = 0)]
        public int OrderID { get; set; }
        [Key, Column(Order = 1)]
        public int ProductID { get; set; }
        public decimal UnitPrice { get; set; }
        public Int16 Quantity { get; set; }
        public float Discount { get; set; }
//        public Order Order { get; set; }
    }
}