using System;
using System.ComponentModel.DataAnnotations;
using NerLaiko.Models;

namespace NerLaiko.ViewModels
{
    public class RorderViewModel
    {
        public int ItemId { get; set; }
        public Order Order { get; set; }
        [DataType(DataType.Date)] public DateTime StartDate { get; set; }
        public int DeliveryInterval { get; set; }
        [Range(1, 1000000)] public int Quantity { get; set; }
    }
}
